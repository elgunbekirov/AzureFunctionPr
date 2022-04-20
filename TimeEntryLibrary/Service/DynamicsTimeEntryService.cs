using App.Common.Abstraction;
using App.Common.DTO;
using App.Common.Entity;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace App.Common.Service
{
    public class DynamicsTimeEntryService : ITimeEntryService
    {
        private readonly ITimeEntryRepository _timeEntryRepository;

        public DynamicsTimeEntryService(ITimeEntryRepository timeEntryRepository)
        {
            _timeEntryRepository = timeEntryRepository;
        }
        public TimeEntryResponseDTO Save(TimeEntryDTO timeEntryDTO)
        {
            var result = new TimeEntryResponseDTO();

            var startDate = DateTime.ParseExact(timeEntryDTO.Properties.StartOn.Type, timeEntryDTO.Properties.StartOn.Format, CultureInfo.InvariantCulture, DateTimeStyles.None);
            var endDate = DateTime.ParseExact(timeEntryDTO.Properties.EndOn.Type, timeEntryDTO.Properties.EndOn.Format, CultureInfo.InvariantCulture, DateTimeStyles.None);

            foreach (var day in EachCalendarDay(startDate, endDate))
            {
                var timeEntry = new TimeEntry
                {
                    Start = day,
                    End = day,
                    Duration = 1
                };

                try
                {
                    var id = _timeEntryRepository.Save(timeEntry);

                    result.SavedDays.Add(new SavedDay { Day = day, Id = id });
                }
                catch (Exception ex)
                {
                    result.FailedDays.Add(new FailedDay { Reason = ex.Message, Day = day });
                }
            }

            return result;
        }

        private IEnumerable<DateTime> EachCalendarDay(DateTime startDate, DateTime endDate)
        {
            for (var date = startDate.Date; date.Date <= endDate.Date; date = date.AddDays(1))
            {
                yield return date;
            }
        }
    }
}
