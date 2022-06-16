using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWorkExample.UnitOfWork.Models;

namespace AnalitikaAnketaDeltaMotors.Classes
{
    public class SeriesContainer
    {
        public int Total() {
            return Low + Medium + High;
        }
        public Topic Topic { get; set; }
        public Subtopic Subtopic { get; set; }
        public int Low { get; set; }
        public int Medium { get; set; }
        public int High { get; set; }
    }
}
