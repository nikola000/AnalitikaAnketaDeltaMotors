using AnalitikaAnketaDeltaMotors.UnitOfWork.Models;
using System;
using System.Collections.Generic;
namespace UnitOfWorkExample.UnitOfWork.Models
{
    public class ImportData
    {
        public int Id { get; set; }
        public DateTime ImportDate { get; set; }
        public string Description { get; set; }
        public List<Entry> Entries { get; set; }
        public List<Tag> Tags { get; set; }        
    }
}