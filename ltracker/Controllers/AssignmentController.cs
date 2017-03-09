using ltracker.Data.Entities;
using ltracker.Data.Repositories;
using ltracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using AutoMapper;
using ltracker.Helpers;
using System.Linq.Expressions;

namespace ltracker.Controllers
{
    public class AssignmentController : BaseController
    {
        // GET: Assignment
        public ActionResult Index(string searchTerm = null, string tipo = null)
        {
            var repository = new AssignedRepository(context);
            var includes = new Expression<Func<AssignedCourse, object>>[] { x => x.Course, x => x.Individual };
            if (tipo == "Persona")
            {
                var courses = repository.QueryIncluding(null, includes, "AssingmentDate").Where(a => searchTerm == null || a.Individual.Name.StartsWith(searchTerm));
                var model = MapperHelpers.Map<ICollection<AssignmentViewModel>>(courses);
                return View(model);
            }
            else
            {
                var courses = repository.QueryIncluding(null, includes, "AssingmentDate").Where(a => searchTerm == null || a.Course.Name.StartsWith(searchTerm));
                var model = MapperHelpers.Map<ICollection<AssignmentViewModel>>(courses);
                return View(model);
            }
        }

        public ActionResult Create()
        {
            var model = new NewAssignmentsViewModel();
            model.CourseList = PopulateCourses(model.CourseId);
            model.IndividualList = PopulateIndividuals(model.IndividualId);
            model.AssingmentDate = DateTime.Now;

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(NewAssignmentsViewModel model)
        {
            try
            {
                var repository = new AssignedRepository(context);

                if (ModelState.IsValid)
                {
                    var assignedCourse = MapperHelpers.Map<AssignedCourse>(model);

                    var individual = MapperHelpers.Map<Individual>(model);

                    assignedCourse.IsCompleted = false;
                    repository.Insert(assignedCourse);
                    context.SaveChanges();

                    return RedirectToAction("Index");
                }
                model.CourseList = PopulateCourses(model.CourseId);
                model.IndividualList = PopulateIndividuals(model.IndividualId);
                return View(model);
            }
            catch (Exception ex)
            {
                model.CourseList = PopulateCourses(model.CourseId);
                model.IndividualList = PopulateIndividuals(model.IndividualId);
                return View(model);
            }
        }

        public ActionResult Edit(int id)
        {
            var repository = new AssignedRepository(context);
            var includes = new Expression<Func<AssignedCourse, object>>[] { x => x.Course, x => x.Individual };
            var criteria = new AssignedCourse { Id = id };
            var courses = repository.QueryByExampleIncludig(criteria, includes).SingleOrDefault();
            var model = MapperHelpers.Map<EditAssignmentViewModel>(courses);

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(int id, EditAssignmentViewModel model)
        {
            try
            {
                var repository = new AssignedRepository(context);
                if (ModelState.IsValid)
                {
                    var update = MapperHelpers.Map<AssignedCourse>(model);
                    repository.Update(update);
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return HttpNotFound();
                }
                
            }
            catch (Exception ex)
            {
                ViewBag.MessageError = ex.Message;
                return View(model);
            }
        }

        public SelectList PopulateIndividuals(object selectedItem = null)
        {
            var repository = new IndividualRepository(context);
            var individuals = repository.Query(null, "Name").ToList();
            individuals.Insert(0, new Individual { Id = null, Name = "Seleccione"});

            return new SelectList(individuals, "Id", "Name", selectedItem);
        }

        public SelectList PopulateCourses(object selectedItem = null)
        {
            var repository = new CourseRepository(context);
            var courses = repository.Query(null, "Name").ToList();
            courses.Insert(0, new Course { Id = null, Name = "Seleccione" });

            return new SelectList(courses, "Id", "Name", selectedItem);
        }

    }
}