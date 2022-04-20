using App.Common.DTO;
using System.Collections.Generic;

namespace App.Common.Abstraction
{
    public interface IRequestValidator
    {
        IList<string> Validate(TimeEntryDTO timeEntryDTO);
    }
}
