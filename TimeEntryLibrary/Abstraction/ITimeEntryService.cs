using App.Common.DTO;

namespace App.Common.Abstraction
{
    public interface ITimeEntryService
    {
        TimeEntryResponseDTO Save(TimeEntryDTO timeEntryDTO);
    }
}
