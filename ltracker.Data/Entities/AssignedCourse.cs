using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ltracker.Data.Entities
{
    public class AssignedCourse
    {
        public int? Id { get; set; }
        public DateTime? AssingmentDate { get; set; }
        public bool? IsCompleted { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public decimal? TotalHours { get; set; }
        public Individual Individual { get; set; }
        public int? IndividualId { get; set; }
        public Course Course { get; set; }
        public int? CourseId { get; set; }
    }
}