using System;
using System.Collections.Generic;

namespace App.Common.DTO
{
    public class TimeEntryResponseDTO
    {
        public List<SavedDay> SavedDays { get; set; }
        public List<FailedDay> FailedDays { get; set; }

        public TimeEntryResponseDTO()
        {
            SavedDays = new List<SavedDay>();
            FailedDays = new List<FailedDay>();
        }
    }

    public class FailedDay
    {
        public DateTime Day { get; set; }
        public string Reason { get; set; }
    }

    public class SavedDay
    {
        public DateTime Day { get; set; }
        public Guid Id { get; set; }
    }
}
