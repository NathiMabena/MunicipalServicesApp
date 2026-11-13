using System;

namespace MunicipalServicesApp
{
    public class ServiceRequest
    {
        public string RequestId { get; set; }      // Unique Identifier (e.g., "REQ-1001")
        public int NumericId { get; set; }         // Used by the AVL Tree for fast O(log n) binary comparisons
        public string Location { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }         // "Pending", "In Progress", "Resolved"
        public int Priority { get; set; }          // 1 = Critical, 2 = High, 3 = Medium, 4 = Low (Used by Min-Heap)
        public DateTime SubmittedDate { get; set; }

        // Read-only helper property so the DataGridView shows a clean label for Priority
        public string PriorityLevel
        {
            get
            {
                switch (Priority)
                {
                    case 1: return "1 - Critical";
                    case 2: return "2 - High";
                    case 3: return "3 - Medium";
                    default: return "4 - Low";
                }
            }
        }

        public ServiceRequest(int numericId, string location, string category, string description, string status, int priority, DateTime submittedDate)
        {
            NumericId = numericId;
            RequestId = $"REQ-{numericId}";
            Location = location;
            Category = category;
            Description = description;
            Status = status;
            Priority = priority;
            SubmittedDate = submittedDate;
        }
    }
}