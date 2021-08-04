using System.Collections.Generic;
using UnitOfWorkExample.UnitOfWork.Models;
namespace AnalitikaAnketaDeltaMotors.UnitOfWork.Models
{
    public class Tag
    {
        public Tag()
        {
            this.ImportDatas = new HashSet<ImportData>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
        public virtual ICollection<ImportData> ImportDatas { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}