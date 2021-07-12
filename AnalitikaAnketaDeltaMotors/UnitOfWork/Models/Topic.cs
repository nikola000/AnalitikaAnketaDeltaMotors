using AnalitikaAnketaDeltaMotors.UnitOfWork.Models;
using System.Collections.Generic;
namespace UnitOfWorkExample.UnitOfWork.Models
{
    public class Topic
    {
        public Topic()
        {
            this.Subtopics = new HashSet<Subtopic>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Subtopic> Subtopics { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}
