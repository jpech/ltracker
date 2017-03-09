using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ltracker.Models
{
    public class NewIndividualViewModel
    {
        public int? Id { get; set; }

        [DisplayName("Nombre")]
        [Required]
        [MaxLength(200)]
        [MinLength(1)]
        public string Name { get; set; }

        [DisplayName("Correo electrónico")]
        [Required]
        [MaxLength(300)]
        [EmailAddress(ErrorMessage = "La entrada no tiene formato de correo electrónico")]
        [Remote("CheckEmail", "Invidivual", HttpMethod = "GET", ErrorMessage = "El email está ocupado")]
        public string Email { get; set; }
    }

    public class DetailsIndividualViewModel
    {
        public int? Id { get; set; }

        [DisplayName("Nombre")]
        public string Name { get; set; }
        [DisplayName("Correo electrónico")]
        public string Email { get; set; }
        public ICollection<CoursesViewModel> Courses { get; set; }
    }

    public class CoursesViewModel
    {
        public int? Id { get; set; }

        public string Name { get; set; }
    }

    public class IndividualViewModel
    {
        public int? Id { get; set; }

        [Display(Name ="Nombre")]
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [Display(Name ="Correo electrónico")]
        [Required]
        [MaxLength(200)]
        [EmailAddress(ErrorMessage ="La entrada no tiene el formato correcto")]
        [Remote("CheckEmail", "Invidivual", HttpMethod = "GET", ErrorMessage = "El email está ocupado")]
        public string Email { get; set; }

        public ICollection<CoursesViewModel> Courses { get; set; }
    }

    public class EditIndividualViewModel
    {
        public int? Id { get; set; }

        [DisplayName("Nombre")]
        [Required]
        [MaxLength(200)]
        [MinLength(1)]
        public string Name { get; set; }

        [DisplayName("Correo electrónico")]
        [Required]
        [MaxLength(300)]
        [EmailAddress(ErrorMessage = "La entrada no tiene formato de correo electrónico")]
        public string Email { get; set; }
        public string EmailAnterior { get; set; }

    }
}