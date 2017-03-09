using ltracker.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ltracker.Models
{
    public class CourseViewModel
    {
        public int? Id { get; set; }

        [Required]
        [MaxLength(200)]
        [Display(Name ="Nombre")]
        public string Name { get; set; }

        [Display(Name ="Horas promedio")]
        public decimal? DurationAVG { get; set; }

        [MaxLength(500)]
        [Display(Name = "Descripción")]
        public string Description { get; set; }

        public ICollection<TopicViewModel> AvailableTopics { get; set; }
        public int[] SelectedTopics { get; set; }
    }

    public class DetailsCourseViewModel
    {
        public int? Id { get; set; }

        public string Name { get; set; }
        public decimal? DurationAVG { get; set; }
        public string Description { get; set; }
        public ICollection<TopicViewModel> Topics { get; set; }
    }

    public class TopicViewModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
    }
}