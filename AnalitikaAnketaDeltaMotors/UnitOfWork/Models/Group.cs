using AnalitikaAnketaDeltaMotors.UnitOfWork.Models;
using System.Collections.Generic;
namespace UnitOfWorkExample.UnitOfWork.Models
{
    public class Group
    {
        public Group()
        {
            this.Tags = new HashSet<Tag>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}