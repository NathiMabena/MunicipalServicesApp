using System;

namespace MunicipalServicesApp
{
    public class LocalEvent
    {
        public string Title { get; set; }
        public string Category { get; set; }
        public DateTime EventDate { get; set; }
        public string Description { get; set; }

        // Constructor
        public LocalEvent(string title, string category, DateTime eventDate, string description)
        {
            Title = title;
            Category = category;
            EventDate = eventDate;
            Description = description;
        }
    }
}