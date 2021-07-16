using UnitOfWorkExample.UnitOfWork.Models;
using AnalitikaAnketaDeltaMotors.Classes;

namespace AnalitikaAnketaDeltaMotors.UnitOfWork.Models
{
    public class EntryScore
    {
        public int Id { get; set; }
        public int SubtopicId { get; set; }
        public Subtopic Subtopic { get; set; }
        public Entry Entry { get; set; }
        public int EntryId { get; set; }
        public Utils.Score Score { get; set; }       
    }
}
