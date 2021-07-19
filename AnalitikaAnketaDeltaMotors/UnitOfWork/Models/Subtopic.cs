using AnalitikaAnketaDeltaMotors.UnitOfWork.Models;
using System.Collections.Generic;
namespace UnitOfWorkExample.UnitOfWork.Models
{
    public class Subtopic
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TopicId { get; set; }
        public Topic Topic { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}
