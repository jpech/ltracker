using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ltracker.Models
{
    public class NewAssignmentsViewModel
    {
        [Required]
        [Display(Name = "Fecha de asignación")]
        
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? AssingmentDate { get; set; }
        public SelectList IndividualList { get; set; }
        public SelectList CourseList { get; set; }
        [Required]
        [DisplayName("Persona")]
        public int? IndividualId { get; set; }
        [Required]
        [DisplayName("Curso")]
        public int? CourseId { get; set; }

    }

    public class AssignmentViewModel
    {
        public int? Id { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? AssingmentDate { get; set; }
        public bool? IsCompleted { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? StartDate { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? FinishDate { get; set; }
        public decimal? TotalHours { get; set; }
        public CourseViewModel Course { get; set; }
        public IndividualViewModel Individual { get; set; }
    }

    public class EditAssignmentViewModel
    {
        public int? Id { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? AssingmentDate { get; set; }

        [DisplayName("Completado")]
        public bool IsCompleted { get; set; }
        [DisplayName("Inicio")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? StartDate { get; set; }
        [DisplayName("Fin")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? FinishDate { get; set; }
        [DisplayName("Horas Totales")]
        public decimal? TotalHours { get; set; }

        public CourseViewModel Course { get; set; }
        public IndividualViewModel Individual { get; set; }
        public int CourseId { get; set; }
        public int IndividualId { get; set; }
    }
}