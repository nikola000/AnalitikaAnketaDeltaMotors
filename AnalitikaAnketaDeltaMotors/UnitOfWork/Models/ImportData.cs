using AnalitikaAnketaDeltaMotors.UnitOfWork.Models;
using System;
using System.Collections.Generic;
namespace UnitOfWorkExample.UnitOfWork.Models
{
    public class ImportData
    {
        public ImportData()
        {
            this.Tags = new HashSet<Tag>();
            this.Entries = new HashSet<Entry>();
        }
        public int Id { get; set; }
        public DateTime ImportDate { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Entry> Entries { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }        
    }
}