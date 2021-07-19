using System;
using System.Collections.Generic;
using UnitOfWorkExample.UnitOfWork.Models;

namespace UnitOfWorkExample.Services
{
    public interface IEntryService
    {
        List<Entry> GetEntriesAsync();
        List<Entry> GetEntriesAsync(DateTime startDate,DateTime endDate);
        Entry GetEntryById(int id);
    }
}
