using System;

namespace App.Common.Entity
{
    public class TimeEntry
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int Duration { get; set; }
    }
}
