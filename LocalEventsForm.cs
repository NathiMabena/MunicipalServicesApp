using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;

namespace MunicipalServicesApp
{
    public partial class LocalEventsForm : Form
    {
        //SortedDictionary Automatically sorts events by Date
        private SortedDictionary<DateTime, List<LocalEvent>> eventsDictionary;

        //HashSet Ensures we only have unique categories for our search filter
        private HashSet<string> uniqueCategories;

        //Queue Tracks user search history for the recommendation engine
        private Queue<string> searchHistory;

        public LocalEventsForm()
        {
            InitializeComponent();

            // Initialize the structures
            eventsDictionary = new SortedDictionary<DateTime, List<LocalEvent>>();
            uniqueCategories = new HashSet<string>();
            searchHistory = new Queue<string>();

            PopulateMockData();
            DisplayEvents(eventsDictionary.Values.SelectMany(e => e));
        }

        private void PopulateMockData()
        {
            // Adding the event you started, plus a few more to test our search filters
            AddEvent(new LocalEvent("Community Street Football Tournament", "Sports", new DateTime(2026, 11, 15), "Local street football knockout tournament."));
            AddEvent(new LocalEvent("Youth Coding & Cloud Bootcamp", "Education", new DateTime(2026, 11, 20), "Introduction to app development and cloud hosting infrastructure."));
            AddEvent(new LocalEvent("Basic Auto Maintenance Workshop", "Community", new DateTime(2026, 11, 22), "Learn basic vehicle repairs, fuse replacements, and troubleshooting."));
            AddEvent(new LocalEvent("Live Piano Recital", "Arts & Culture", new DateTime(2026, 11, 25), "An evening of contemporary piano covers and duets at the community hall."));
        }
        private void DisplayEvents(IEnumerable<LocalEvent> eventsToDisplay)
        {
            dgvEvents.DataSource = null; // Clear previous data
            dgvEvents.DataSource = eventsToDisplay.ToList(); // Bind the new list
        }

        private void AddEvent(LocalEvent newEvent)
        {
            //Check if the date already exists in the dictionary
            if (!eventsDictionary.ContainsKey(newEvent.EventDate.Date))
            {
                // If not, create a new list for that specific date
                eventsDictionary[newEvent.EventDate.Date] = new List<LocalEvent>();
            }

            // Add the event to the list for that date
            eventsDictionary[newEvent.EventDate.Date].Add(newEvent);

            //Add the category to the HashSet 
            // The HashSet automatically rejects duplicates, giving us a clean list for our UI dropdown
            uniqueCategories.Add(newEvent.Category);
        }

        private void LocalEventsForm_Load(object sender, EventArgs e)
        {
            // Populate the Search Dropdown using our HashSet (Proving the Sets requirement)
            foreach (string category in uniqueCategories)
            {
                cmbCategory.Items.Add(category);
            }

            // Set a default message for the recommendation engine
            lblRecommendation.Text = "Search for events to see recommendations!";

            StyleButton(btnSearch);
            StyleButton(btnClear);
            StyleButton(btnBack);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

            // Check if the user forgot to select a category
            if (cmbCategory.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a category before searching.",
                                "Category Required",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return; // This stops the rest of the search code from running
            }


            //Capture user input
            string selectedCategory = cmbCategory.SelectedItem?.ToString();
            DateTime selectedDate = dtpDate.Value.Date;

            // Flatten our SortedDictionary to easily search through it
            var allEvents = eventsDictionary.Values.SelectMany(ev => ev).ToList();
            var searchResults = allEvents;

            // Filter by Category if one is selected
            if (!string.IsNullOrEmpty(selectedCategory))
            {
                searchResults = searchResults.Where(ev => ev.Category == selectedCategory).ToList();

                // Push the searched category into our Queue to track user behavior
                searchHistory.Enqueue(selectedCategory);

                // Keep the queue size manageable (only remember the last 5 searches)
                if (searchHistory.Count > 5)
                {
                    searchHistory.Dequeue();
                }
            }

            // Filter by Date (Show events occurring on or after the selected date)
            searchResults = searchResults.Where(ev => ev.EventDate.Date >= selectedDate).ToList();

            // Update the DataGridView
            DisplayEvents(searchResults);

            // Trigger the smart recommendation algorithm
            UpdateRecommendations();
        }

        private void UpdateRecommendations()
        {
            // If the user hasn't searched for anything yet, we can't recommend anything
            if (searchHistory.Count == 0) return;


            //Analyze search patterns: Find the most frequently searched category in the Queue
            string topCategory = searchHistory
                .GroupBy(category => category)
                .OrderByDescending(group => group.Count())
                .First().Key;

            //Query the SortedDictionary to find the next upcoming event in that specific category
            var allEvents = eventsDictionary.Values.SelectMany(ev => ev).ToList();

            var recommendedEvent = allEvents
                .Where(ev => ev.Category == topCategory && ev.EventDate >= DateTime.Now)
                .OrderBy(ev => ev.EventDate) // Sort to get the soonest one
                .FirstOrDefault();

            //Present recommendations in a user-friendly manner
            if (recommendedEvent != null)
            {
                lblRecommendation.Text = $"Because you search for '{topCategory}', you might like:\n" +
                                         $"➤ {recommendedEvent.Title} on {recommendedEvent.EventDate.ToShortDateString()}";
            }
            else
            {
                lblRecommendation.Text = $"We see you like '{topCategory}', but there are no upcoming events right now.";
            }
        }





        private void btnClear_Click(object sender, EventArgs e)
        {
            cmbCategory.SelectedIndex = -1; // Reset dropdown
            dtpDate.Value = DateTime.Now;   // Reset date

            // Show all events again
            DisplayEvents(eventsDictionary.Values.SelectMany(ev => ev).ToList());

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
    
            // This safely closes the Local Events form, which automatically triggers the Main Menu to reappear
            this.Close();
        }

        private void StyleButton(Button btn)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 1;
            btn.FlatAppearance.BorderColor = System.Drawing.Color.SteelBlue;
            btn.BackColor = System.Drawing.Color.SteelBlue;
            btn.ForeColor = System.Drawing.Color.White;
            btn.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
            btn.Cursor = Cursors.Hand;
        }
    }
}