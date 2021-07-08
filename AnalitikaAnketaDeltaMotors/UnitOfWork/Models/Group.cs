using AnalitikaAnketaDeltaMotors.UnitOfWork.Models;
using System.Collections.Generic;
namespace UnitOfWorkExample.UnitOfWork.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Tag> Tags { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}