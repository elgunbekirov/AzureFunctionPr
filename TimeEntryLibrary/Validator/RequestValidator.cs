using App.Common.Abstraction;
using App.Common.DTO;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace App.Common.Validator
{
    internal class RequestValidator : IRequestValidator
    {
        public IList<string> Validate(TimeEntryDTO timeEntryDTO)
        {
            var failedValidations = new List<string>();

            if (timeEntryDTO.Required.Contains(nameof(timeEntryDTO.Properties.StartOn)) && timeEntryDTO.Properties.StartOn == null)
            {
                failedValidations.Add($"{nameof(timeEntryDTO.Properties.StartOn)} is required field");
            }

            if (timeEntryDTO.Required.Contains(nameof(timeEntryDTO.Properties.EndOn)) && timeEntryDTO.Properties.EndOn == null)
            {
                failedValidations.Add($"{nameof(timeEntryDTO.Properties.EndOn)} is required field");
            }

            if (!DateTime.TryParseExact(timeEntryDTO.Properties.StartOn?.Type, timeEntryDTO.Properties.StartOn?.Format, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime startdate))
            {
                failedValidations.Add($"{timeEntryDTO.Properties.StartOn?.Type} could not be converted to date by using {timeEntryDTO.Properties.StartOn?.Format} format ");
            }

            if (!DateTime.TryParseExact(timeEntryDTO.Properties.EndOn?.Type, timeEntryDTO.Properties.EndOn?.Format, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime enddate))
            {
                failedValidations.Add($"{timeEntryDTO.Properties.EndOn?.Type} could not be converted to date by using {timeEntryDTO.Properties.EndOn?.Format} format ");
            }

            if (DateTime.Compare(startdate, enddate) > 0)
            {
                failedValidations.Add($"StartOn-{startdate} can't be greater than EndOn-{enddate}");
            }

            return failedValidations;
        }
    }
}
