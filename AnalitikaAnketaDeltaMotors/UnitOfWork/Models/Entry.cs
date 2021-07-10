using AnalitikaAnketaDeltaMotors.UnitOfWork.Models;
using System;

namespace UnitOfWorkExample.UnitOfWork.Models
{
    public class Entry
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Ocena { get; set; }
        public string Odgovor { get; set; }
        public string PredlogPoboljsanja { get; set; }
        public string Kontakt { get; set; }
        public int ImportDataId{get; set; }
        public ImportData ImportData { get; set; }
    }
}