using ltracker.Data.Entities;
using ltracker.Data.Repositories;
using ltracker.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Expressions;

namespace ltracker.Models
{
    public class CourseController : BaseController
    {
        // GET: Course
        public ActionResult Index()
        {
            var repository = new CourseRepository(context);
            var courses = repository.Query(null, "Name");
            var model = MapperHelpers.Map<IEnumerable<CourseViewModel>>(courses);

            return View(model);
        }

        // GET: Course/Details/5
        public ActionResult Details(int id)
        {
            var repository = new CourseRepository(context);
            var includes = new Expression<Func<Course, object>>[] { x => x.Topics };
            var courses = repository.QueryIncluding(x => x.Id == id, includes).SingleOrDefault();
            var models = MapperHelpers.Map<DetailsCourseViewModel>(courses);
            return View(models);
        }

        // GET: Course/Create
        public ActionResult Create()
        {
            var model = new CourseViewModel();
            var topicRepository = new TopicRepository(context);
            var topics = topicRepository.Query(null, "Name DESC");
            model.AvailableTopics = MapperHelpers.Map<ICollection<TopicViewModel>>(topics);
            return View(model);
        }

        // POST: Course/Create
        [HttpPost]
        public ActionResult Create(CourseViewModel model)
        {
            try
            {
                var repository = new CourseRepository(context);
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    var courses = MapperHelpers.Map<Course>(model);
                    repository.InsertCourseWithTopic(courses, model.SelectedTopics);
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    var topicRepository = new TopicRepository(context);
                    var topics = topicRepository.Query(null, "Name DESC");
                    model.AvailableTopics = MapperHelpers.Map<ICollection<TopicViewModel>>(topics);
                    return View(model);
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Course/Edit/5
        public ActionResult Edit(int id)
        {
            var repository = new CourseRepository(context);
            var repoTopics = new TopicRepository(context);

            var includes = new Expression<Func<Course, object>>[] { x => x.Topics };
            var courses = repository.QueryIncluding(x => x.Id == id, includes).SingleOrDefault();
            var models = MapperHelpers.Map<CourseViewModel>(courses);
            var topics = repoTopics.Query(null, "Name");
            models.AvailableTopics = MapperHelpers.Map<ICollection<TopicViewModel>>(topics);
            models.SelectedTopics = courses.Topics.Select(x => x.Id.Value).ToArray();
            return View(models);
        }

        // POST: Course/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, CourseViewModel model)
        {
            var topicRepository = new TopicRepository(context);
            try
            {
                // TODO: Add update logic here
                var repository = new CourseRepository(context);
                if (ModelState.IsValid)
                {
                    var course = MapperHelpers.Map<Course>(model);
                    repository.UpdateCourseWithTopic(course, model.SelectedTopics);
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
                var topics = topicRepository.Query(null, "Name");
                model.AvailableTopics = MapperHelpers.Map<ICollection<TopicViewModel>>(topics);
                return View(model);

            }
            catch(Exception ex)
            {
                var topics = topicRepository.Query(null, "Name");
                model.AvailableTopics = MapperHelpers.Map<ICollection<TopicViewModel>>(topics);
                return View(model);
            }
        }

        // GET: Course/Delete/5
        public ActionResult Delete(int id)
        {
            var repository = new CourseRepository(context);

            var cursos = repository.Find(id);
            var model = MapperHelpers.Map<CourseViewModel>(cursos);

            return View(model);
        }

        // POST: Course/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, CourseViewModel model)
        {
            try
            {
                if (id != model.Id)
                {
                    return new HttpNotFoundResult();
                }
                // TODO: Add delete logic here
                var repository = new CourseRepository(context);
                var cursos = repository.Find(model.Id);
                repository.Delete(cursos);
                context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(model);
            }
        }
    }
}
