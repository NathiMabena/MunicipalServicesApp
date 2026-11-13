using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MunicipalServicesApp
{
    public partial class ServiceRequestStatusForm : Form
    {
        // Our 3 Advanced Data Structure Engines 
        private ServiceRequestAvlTree avlTree = new ServiceRequestAvlTree();
        private ServiceRequestMinHeap minHeap = new ServiceRequestMinHeap();
        private ServiceRequestGraph requestGraph = new ServiceRequestGraph();

        // Static memory dictionaries so timestamps and status updates persist across form opens
        private static Dictionary<int, string> persistedStatuses = new Dictionary<int, string>();
        private static Dictionary<int, DateTime> persistedTimestamps = new Dictionary<int, DateTime>();

        // UI Controls
        private TextBox txtSearchId;
        private DataGridView dgvRequests;
        private Label lblGraphOutput;
        private Label lblActiveMode;

        public ServiceRequestStatusForm()
        {
            InitializeComponent();
            BuildCustomInterface();
            this.Load += ServiceRequestStatusForm_Load;
        }

        private void ServiceRequestStatusForm_Load(object sender, EventArgs e)
        {
            LoadRequestsIntoStructures();
            DisplayRequests(avlTree.GetInOrderList(), "Showing All Requests Organised by AVL Tree (In-Order ID Traversal)");
            RunGraphAndMstAnalysis(1001);
        }

        // =====================================================================
        // 1. DATA INITIALIZATION INTEGRATION 
        // =====================================================================
        private void LoadRequestsIntoStructures()
        {
            avlTree = new ServiceRequestAvlTree();
            minHeap = new ServiceRequestMinHeap();
            requestGraph = new ServiceRequestGraph();

            // Core Municipal Service Requests across Pretoria
            List<ServiceRequest> initialRequests = new List<ServiceRequest>
            {
                new ServiceRequest(1001, "Pretoria Central, Church St", "Water and Utilities",
                    "Main water pipe burst flooding the intersection.", "In Progress", 1, new DateTime(2026, 11, 10)),

                new ServiceRequest(1002, "Pretoria Central, Church St", "Roads",
                    "Road surface subsidence and sinkhole risk caused by water pipe burst.", "Pending", 1, new DateTime(2026, 11, 10)),

                new ServiceRequest(1003, "Hatfield, Burnett St", "Electricity",
                    "Substation feeder trip affecting traffic lights and streetlamps.", "In Progress", 2, new DateTime(2026, 11, 11)),

                new ServiceRequest(1004, "Mamelodi, Tsamaya Ave", "Sanitation",
                    "Blocked storm-water drainage channel after heavy rain.", "Pending", 2, new DateTime(2026, 11, 11)),

                new ServiceRequest(1005, "Sunnyside, Esselen St", "Roads",
                    "Deep pothole damaging passing vehicles near bus stop.", "In Progress", 3, new DateTime(2026, 11, 12)),

                new ServiceRequest(1006, "Arcadia, Pretorius St", "Electricity",
                    "Faulty streetlight transformer blocked by Church St road closure.", "Resolved", 4, new DateTime(2026, 11, 12))
            };

            // Apply any status updates made during this session to the core requests
            foreach (var req in initialRequests)
            {
                if (persistedStatuses.ContainsKey(req.NumericId))
                    req.Status = persistedStatuses[req.NumericId];
            }

            // Connect the core municipal network in the Graph
            foreach (var req in initialRequests)
            {
                requestGraph.AddVertex(req.NumericId);
            }

            requestGraph.AddEdge(1001, 1002, 0.8, "Pipe burst directly undermined road surface");
            requestGraph.AddEdge(1002, 1005, 1.9, "Same Roads maintenance resurfacing team");
            requestGraph.AddEdge(1002, 1006, 1.4, "Road closure blocks access to transformer");
            requestGraph.AddEdge(1003, 1006, 2.3, "Shared electrical grid line to Arcadia");
            requestGraph.AddEdge(1001, 1004, 3.2, "Runoff water flowing into storm-water drain");
            requestGraph.AddEdge(1004, 1005, 2.6, "Overflowing drain eroding Esselen St asphalt");

            // LIVE INTEGRATION WITH PART 1: Pull citizen issues with Dynamic Priority & Smart Graph Links
            if (ReportIssuesForm.Form1.reportedIssues != null)
            {
                int nextId = 1007;
                foreach (var issue in ReportIssuesForm.Form1.reportedIssues)
                {
                    // 1. Calculate dynamic priority from keywords and category
                    int dynamicPriority = DetermineIssuePriority(issue.Category, issue.Description);

                    // 2. Preserve the exact timestamp from when the issue was first logged
                    if (!persistedTimestamps.ContainsKey(nextId))
                    {
                        // Uses issue.SubmittedDate if added to Issue.cs, otherwise locks in the first-seen timestamp
                        var prop = issue.GetType().GetProperty("SubmittedDate");
                        DateTime loggedTime = prop != null ? (DateTime)prop.GetValue(issue) : DateTime.Now;
                        persistedTimestamps[nextId] = loggedTime;
                    }

                    // 3. Preserve status updates if the dispatcher advanced this request's status
                    string currentStatus = persistedStatuses.ContainsKey(nextId)
                        ? persistedStatuses[nextId]
                        : "Pending (Logged by Citizen)";

                    var liveReq = new ServiceRequest(
                        nextId,
                        issue.Location,
                        issue.Category,
                        issue.Description,
                        currentStatus,
                        dynamicPriority,
                        persistedTimestamps[nextId]
                    );

                    initialRequests.Add(liveReq);
                    requestGraph.AddVertex(nextId);

                    // 4. Dynamically link to the matching infrastructure hub & calculate suburb distance
                    LinkCitizenRequestToGraph(liveReq);

                    nextId++;
                }
            }

            // Insert every request into both the AVL Tree and the Min-Heap
            foreach (var req in initialRequests)
            {
                avlTree.Insert(req);
                minHeap.Insert(req);
            }
        }

        // =====================================================================
        // 2. DYNAMIC PRIORITY & SMART GRAPH ROUTING ENGINE
        // =====================================================================
        private int DetermineIssuePriority(string category, string description)
        {
            string desc = (description ?? "").ToLower();
            string cat = (category ?? "").ToLower();

            // Priority 1 - Critical: Major outages, pipe leaks/bursts, safety hazards
            string[] criticalKeywords = {
                "burst", "flood", "leakage", "leak", "broken pipeline", "pipeline",
                "power outage", "outage", "blackout", "no water", "no electricity",
                "live wire", "substation", "sinkhole", "sewage overflow", "danger", "emergency"
            };
            if (criticalKeywords.Any(k => desc.Contains(k)))
                return 1; // 1 - Critical

            // Priority 2 - High: Traffic signals, transformers, blocked drains, or utility categories
            string[] highKeywords = {
                "traffic light", "robot", "transformer", "blocked drain", "drainage", "low pressure", "Main"
            };
            if (highKeywords.Any(k => desc.Contains(k)) || cat.Contains("water") || cat.Contains("electricity"))
                return 2; // 2 - High

            // Priority 3 - Medium: Roads, potholes, sanitation, streetlamps
            string[] mediumKeywords = {
                "pothole", "asphalt", "road", "streetlight", "street lamp", "refuse", "waste", "garbage"
            };
            if (mediumKeywords.Any(k => desc.Contains(k)) || cat.Contains("roads") || cat.Contains("sanitation"))
                return 3; // 3 - Medium

            // Priority 4 - Low: General inquiries, minor maintenance, parks
            return 4; // 4 - Low
        }

        private void LinkCitizenRequestToGraph(ServiceRequest citizenReq)
        {
            string cat = (citizenReq.Category ?? "").ToLower();
            string desc = (citizenReq.Description ?? "").ToLower();
            double distanceKm = EstimateSuburbDistanceKm(citizenReq.Location, citizenReq.NumericId);

            if (cat.Contains("water") || desc.Contains("pipe") || desc.Contains("leak") || desc.Contains("water"))
            {
                // Connect to REQ-1001 (Main Water Pipe Hub)
                requestGraph.AddEdge(1001, citizenReq.NumericId, distanceKm,
                    "Connected to Central Water reticulation pressure line");

                // If severe leakage, also link to REQ-1004 (Storm-water drainage)
                if (citizenReq.Priority == 1)
                {
                    requestGraph.AddEdge(1004, citizenReq.NumericId, Math.Round(distanceKm + 1.1, 1),
                        "Surface water runoff feeding into storm-water drainage");
                }
            }
            else if (cat.Contains("electricity") || desc.Contains("power") || desc.Contains("outage") || desc.Contains("light"))
            {
                // Connect to REQ-1003 (Hatfield Substation Feeder) & REQ-1006 (Arcadia Transformer)
                requestGraph.AddEdge(1003, citizenReq.NumericId, distanceKm,
                    "Fed by Hatfield-Arcadia high-voltage substation line");
                requestGraph.AddEdge(1006, citizenReq.NumericId, Math.Round(distanceKm + 0.7, 1),
                    "Shared municipal electrical maintenance crew");
            }
            else if (cat.Contains("roads") || desc.Contains("pothole") || desc.Contains("traffic"))
            {
                // Connect to REQ-1002 (Church St Road Subsidence) & REQ-1005 (Sunnyside Roads Team)
                requestGraph.AddEdge(1005, citizenReq.NumericId, distanceKm,
                    "Assigned to Ward Roads & Asphalt resurfacing unit");
                requestGraph.AddEdge(1002, citizenReq.NumericId, Math.Round(distanceKm + 0.9, 1),
                    "Traffic diversion impact from Church St closure");
            }
            else
            {
                // Sanitation or general: Connect to REQ-1004 (Sanitation/Drainage Hub)
                requestGraph.AddEdge(1004, citizenReq.NumericId, distanceKm,
                    "Routed to Municipal Sanitation & Waste response depot");
            }
        }

        private double EstimateSuburbDistanceKm(string location, int numericId)
        {
            string loc = (location ?? "").ToLower();

            if (loc.Contains("central") || loc.Contains("church")) return 0.9;
            if (loc.Contains("arcadia") || loc.Contains("pretorius")) return 1.5;
            if (loc.Contains("sunnyside") || loc.Contains("esselen")) return 1.8;
            if (loc.Contains("hatfield") || loc.Contains("burnett")) return 2.4;
            if (loc.Contains("brooklyn") || loc.Contains("menlyn")) return 3.3;
            if (loc.Contains("mamelodi") || loc.Contains("tsamaya")) return 4.2;
            if (loc.Contains("centurion") || loc.Contains("soshanguve")) return 5.1;

            // Deterministic fallback based on ID so different logs don't share the exact same distance
            return Math.Round(1.6 + ((numericId % 5) * 0.4), 1);
        }

        // =====================================================================
        // 3. LIVE STATUS PROGRESSION (PENDING -> IN PROGRESS -> RESOLVED)
        // =====================================================================
        private void btnAdvanceStatus_Click(object sender, EventArgs e)
        {
            if (dgvRequests.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a service request row in the table first.",
                                "No Request Selected",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            var selectedReq = dgvRequests.SelectedRows[0].DataBoundItem as ServiceRequest;
            if (selectedReq == null) return;

            // Cycle status through the municipal workflow
            if (selectedReq.Status.StartsWith("Pending"))
                selectedReq.Status = "In Progress";
            else if (selectedReq.Status == "In Progress")
                selectedReq.Status = "Resolved";
            else
                selectedReq.Status = "Pending";

            // Persist the updated status so it stays saved across form opens
            persistedStatuses[selectedReq.NumericId] = selectedReq.Status;

            dgvRequests.Refresh();
            MessageBox.Show($"Status for {selectedReq.RequestId} updated to '{selectedReq.Status}'.",
                            "Status Updated",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
        }

        // =====================================================================
        // 4. AVL TREE SEARCH & HEAP SORTING ACTIONS
        // =====================================================================
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string rawInput = txtSearchId.Text.Trim().ToUpper();

            if (string.IsNullOrEmpty(rawInput))
            {
                MessageBox.Show("Please enter a Unique Request ID (e.g., REQ-1001 or 1001) before searching.",
                                "Request ID Required",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            string cleanNumber = rawInput.Replace("REQ-", "").Trim();
            if (!int.TryParse(cleanNumber, out int numericId))
            {
                MessageBox.Show("Invalid ID format. Please enter a valid numeric ID such as 1001 or REQ-1001.",
                                "Invalid Format",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            ServiceRequest foundRequest = avlTree.Search(numericId);

            if (foundRequest == null)
            {
                MessageBox.Show($"No service request found matching ID 'REQ-{numericId}'.",
                                "Request Not Found",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                return;
            }

            DisplayRequests(new List<ServiceRequest> { foundRequest },
                            $"AVL Tree O(log n) Search Result for Unique ID: {foundRequest.RequestId}");
            RunGraphAndMstAnalysis(numericId);
        }

        private void btnSortById_Click(object sender, EventArgs e)
        {
            List<ServiceRequest> sortedById = avlTree.GetInOrderList();
            DisplayRequests(sortedById, "Organised by AVL Tree (In-Order Traversal by Unique ID)");
        }

        private void btnSortByPriority_Click(object sender, EventArgs e)
        {
            List<ServiceRequest> sortedByPriority = minHeap.GetRequestsSortedByPriority();
            DisplayRequests(sortedByPriority, "Organised by Min-Heap Priority Queue (Critical Urgency First)");
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSearchId.Text) && dgvRequests.Rows.Count >= 6)
            {
                MessageBox.Show("The search filter is already cleared and showing all requests.",
                                "Already Clear",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                return;
            }

            txtSearchId.Clear();
            DisplayRequests(avlTree.GetInOrderList(), "Showing All Requests — Organised by AVL Tree (In-Order ID Traversal)");
            RunGraphAndMstAnalysis(1001);
        }

        // =====================================================================
        // 5. GRAPH TRAVERSAL (BFS) & MINIMUM SPANNING TREE (MST)
        // =====================================================================
        private void dgvRequests_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvRequests.SelectedRows.Count > 0)
            {
                var selectedReq = dgvRequests.SelectedRows[0].DataBoundItem as ServiceRequest;
                if (selectedReq != null)
                {
                    RunGraphAndMstAnalysis(selectedReq.NumericId);
                }
            }
        }

        private void RunGraphAndMstAnalysis(int startId)
        {
            List<int> bfsCluster = requestGraph.BreadthFirstSearch(startId);
            string bfsText = string.Join("  ➔  ", bfsCluster.Select(id => $"REQ-{id}"));

            List<GraphEdge> mstEdges = requestGraph.CalculateMinimumSpanningTree(startId);
            double totalDistance = mstEdges.Sum(edge => edge.DistanceKm);

            string mstRoute = string.Join("   |   ",
                mstEdges.Select(edge => $"REQ-{edge.SourceId} ↔ REQ-{edge.TargetId} ({edge.DistanceKm:0.0} km)"));

            lblGraphOutput.Text =
                $"Selected Request: REQ-{startId}\n" +
                $"• BFS Graph Traversal (Connected Infrastructure Cluster):  {bfsText}\n" +
                $"• Minimum Spanning Tree (Prim's Optimal Dispatch Route - Total: {totalDistance:0.0} km):\n" +
                $"   {mstRoute}";
        }

        private void DisplayRequests(List<ServiceRequest> requests, string modeDescription)
        {
            lblActiveMode.Text = modeDescription;
            dgvRequests.DataSource = null;
            dgvRequests.DataSource = requests;

            if (dgvRequests.Columns["NumericId"] != null)
                dgvRequests.Columns["NumericId"].Visible = false;
            if (dgvRequests.Columns["Priority"] != null)
                dgvRequests.Columns["Priority"].Visible = false;
        }

        // =====================================================================
        // 6. PROGRAMMATIC UI BUILDER
        // =====================================================================
        private void BuildCustomInterface()
        {
            this.Text = "ServiceRequestStatusForm";
            this.Size = new Size(980, 620);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(153, 180, 209);

            Button btnBack = new Button
            {
                Text = "Back to Main Menu",
                Location = new Point(20, 20),
                Size = new Size(155, 50)
            };
            StyleButton(btnBack);
            btnBack.Click += (s, e) => this.Close();

            Label lblTitle = new Label
            {
                Text = "Service Request Status",
                Font = new Font("Segoe UI", 22F, FontStyle.Bold),
                ForeColor = Color.MidnightBlue,
                BackColor = Color.Transparent,
                AutoSize = true,
                Location = new Point(275, 22)
            };

            Label lblSearchPrompt = new Label
            {
                Text = "Track by Unique ID",
                Font = new Font("Segoe UI Semibold", 12F),
                ForeColor = Color.Black,
                BackColor = Color.Transparent,
                AutoSize = true,
                Location = new Point(22, 105)
            };

            Label lblSearchHint = new Label
            {
                Text = "(e.g., REQ-1001 or 1001)",
                Font = new Font("Segoe UI", 9.5F),
                ForeColor = Color.FromArgb(40, 40, 40),
                BackColor = Color.Transparent,
                AutoSize = true,
                Location = new Point(22, 130)
            };

            txtSearchId = new TextBox
            {
                Font = new Font("Segoe UI", 11F),
                Location = new Point(25, 155),
                Size = new Size(210, 30)
            };

            Button btnSearch = new Button
            {
                Text = "Search ID",
                Location = new Point(25, 198),
                Size = new Size(100, 40)
            };
            StyleButton(btnSearch);
            btnSearch.Click += btnSearch_Click;

            Button btnClear = new Button
            {
                Text = "Clear",
                Location = new Point(135, 198),
                Size = new Size(100, 40)
            };
            StyleButton(btnClear);
            btnClear.Click += btnClear_Click;

            Label lblOrganise = new Label
            {
                Text = "Organise & Manage",
                Font = new Font("Segoe UI Semibold", 12F),
                ForeColor = Color.Black,
                BackColor = Color.Transparent,
                AutoSize = true,
                Location = new Point(22, 255)
            };

            Button btnSortById = new Button
            {
                Text = "Sort by ID (AVL Tree)",
                Location = new Point(25, 285),
                Size = new Size(210, 40)
            };
            StyleButton(btnSortById);
            btnSortById.Click += btnSortById_Click;

            Button btnSortByPriority = new Button
            {
                Text = "Sort by Urgency (Min-Heap)",
                Location = new Point(25, 333),
                Size = new Size(210, 40)
            };
            StyleButton(btnSortByPriority);
            btnSortByPriority.Click += btnSortByPriority_Click;

            Button btnAdvanceStatus = new Button
            {
                Text = "Update Selected Status",
                Location = new Point(25, 381),
                Size = new Size(210, 40)
            };
            StyleButton(btnAdvanceStatus);
            btnAdvanceStatus.Click += btnAdvanceStatus_Click;

            lblActiveMode = new Label
            {
                Text = "Organised by AVL Tree",
                Font = new Font("Segoe UI Semibold", 10F),
                ForeColor = Color.MidnightBlue,
                BackColor = Color.Transparent,
                AutoSize = true,
                Location = new Point(265, 82)
            };

            dgvRequests = new DataGridView
            {
                Location = new Point(265, 108),
                Size = new Size(675, 313),
                BackgroundColor = Color.FromArgb(153, 180, 209),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
            };
            dgvRequests.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvRequests.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvRequests.SelectionChanged += dgvRequests_SelectionChanged;

            Label lblGraphHeading = new Label
            {
                Text = "Graph Dependencies && Minimum Spanning Tree (MST) Dispatch",
                Font = new Font("Segoe UI", 13F, FontStyle.Bold),
                ForeColor = Color.MidnightBlue,
                BackColor = Color.Transparent,
                AutoSize = true,
                Location = new Point(22, 435),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Left
            };

            lblGraphOutput = new Label
            {
                Text = "Select a service request in the table to view connected faults and the optimal MST route.",
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.Black,
                BackColor = Color.Transparent,
                AutoSize = false,
                Location = new Point(25, 463),
                Size = new Size(915, 105),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
            };

            this.Controls.Add(btnBack);
            this.Controls.Add(lblTitle);
            this.Controls.Add(lblSearchPrompt);
            this.Controls.Add(lblSearchHint);
            this.Controls.Add(txtSearchId);
            this.Controls.Add(btnSearch);
            this.Controls.Add(btnClear);
            this.Controls.Add(lblOrganise);
            this.Controls.Add(btnSortById);
            this.Controls.Add(btnSortByPriority);
            this.Controls.Add(btnAdvanceStatus);
            this.Controls.Add(lblActiveMode);
            this.Controls.Add(dgvRequests);
            this.Controls.Add(lblGraphHeading);
            this.Controls.Add(lblGraphOutput);
        }

        private void StyleButton(Button btn)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = Color.SteelBlue;
            btn.ForeColor = Color.White;
            btn.Font = new Font("Segoe UI Semibold", 10F);
            btn.Cursor = Cursors.Hand;

            btn.MouseEnter += (s, e) => btn.BackColor = Color.DodgerBlue;
            btn.MouseLeave += (s, e) => btn.BackColor = Color.SteelBlue;
        }
    }
}