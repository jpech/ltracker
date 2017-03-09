using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using ltracker.Data.Entities;
using ltracker.Models;

namespace ltracker.Helpers
{
    public class MapperHelpers
    {
        internal static IMapper mapper;

        static MapperHelpers()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Individual, IndividualViewModel>().ReverseMap();
                cfg.CreateMap<Individual, NewIndividualViewModel>().ReverseMap();
                cfg.CreateMap<Individual, EditIndividualViewModel>().ReverseMap();

                cfg.CreateMap<Course, CourseViewModel>().ReverseMap();
                cfg.CreateMap<Topic, TopicViewModel>().ReverseMap();

                cfg.CreateMap<Course, DetailsCourseViewModel>();

                cfg.CreateMap< NewAssignmentsViewModel, AssignedCourse >();

                cfg.CreateMap<AssignedCourse, AssignmentViewModel>();

                cfg.CreateMap< EditAssignmentViewModel, AssignedCourse >().ReverseMap();

                cfg.CreateMap<Individual, DetailsIndividualViewModel>().ReverseMap();
                cfg.CreateMap< CoursesViewModel, Course >().ReverseMap();
                

                cfg.CreateMap<Individual, NewAssignmentsViewModel>().ReverseMap();
                cfg.CreateMap<Course, NewAssignmentsViewModel>().ReverseMap();

            });
            mapper = config.CreateMapper();
        }

        public static T Map<T>(object source)
        {
            return mapper.Map<T>(source);
        }
    }
}