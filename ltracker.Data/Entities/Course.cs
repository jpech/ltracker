using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ltracker.Data.Entities
{
    public class Course
    {
        public int? Id { get; set; }
        public string Name{ get; set; }
        public decimal? DurationAVG { get; set; }
        public string Description { get; set; }
        public ICollection<Topic> Topics { get; set; }

        //public ICollection<Individual> Individuals { get; set; }
    }
}
