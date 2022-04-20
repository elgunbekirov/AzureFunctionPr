using App.Common.Entity;
using System;

namespace App.Common.Abstraction
{
    public interface ITimeEntryRepository
    {
        Guid Save(TimeEntry obj);
    }
}
