using System.Collections.Generic;
using System.Linq;
using UnitOfWorkExample.UnitOfWork;
using UnitOfWorkExample.Services;
using UnitOfWorkExample.UnitOfWork.Models;
using System;

namespace UnitOfWorkExample.Services
{
    
    public class EntryService : IEntryService
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public EntryService(IUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }
        public List<Entry> GetEntriesAsync()
        {
            using (var unitOfWork = _unitOfWorkFactory.Create())
            {
                var entries = unitOfWork.Repository().All<Entry>();
                return entries.ToList();
            }
        }

        public List<Entry> GetEntriesAsync(DateTime startDate, DateTime endDate)
        {
            using (var unitOfWork = _unitOfWorkFactory.Create())
            {
                var entries = unitOfWork.Repository().Find<Entry>(x => x.CreatedAt > startDate && x.CreatedAt < endDate);
                    return entries.ToList();
            }
        }

        public Entry GetEntryById(int id)
        {
            using (var unitOfWork = _unitOfWorkFactory.Create())
            {
                var entry = unitOfWork.Repository().Find<Entry>(x => x.Id == id).FirstOrDefault();
                return entry;
            }
        }
    }
}
