using System;

namespace MunicipalServicesApp
{
    public class Event
    {
        public string Title { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public DateTime EventDate { get; set; }

        public Event(string title, string category, string description, DateTime eventDate)
        {
            Title = title;
            Category = category;
            Description = description;
            EventDate = eventDate;
        }
    }
}