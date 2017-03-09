using ltracker.Data.Entities;
using ltracker.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ltracker.Models;
using ltracker.Helpers;
using System.Collections;
using System.Linq.Expressions;

namespace ltracker.Controllers
{
    public class InvidivualController : BaseController
    {
        // GET: Invidivual
        public ActionResult Index()
        {
            var repository = new IndividualRepository(context);
            var individuals = repository.GetAll();
            var model = MapperHelpers.Map<IEnumerable<IndividualViewModel>>(individuals);
            return View(model);
        }

        // GET: Invidivual/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Invidivual/Create
        [HttpPost]
        public ActionResult Create(NewIndividualViewModel model)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    var repo = new IndividualRepository(context);

                    var individualQry = new Individual { Email = model.Email };
                    var emailExiste = repo.QueryByExample(individualQry).Count > 0;
                    if (!emailExiste)
                    {
                        var individual = MapperHelpers.Map<Individual>(model);
                        repo.Insert(individual);
                        
                        context.SaveChanges();
                    }
                    else
                    {
                        ModelState.AddModelError("Email", "El Email está ocupado");
                        return View(model);
                    }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Invidivual/Edit/5
        public ActionResult Edit(int id)
        {
            var repo = new IndividualRepository(context);
            var model = repo.Find(id);
            var individual = MapperHelpers.Map<EditIndividualViewModel>(model);
            return View(individual);
        }

        // POST: Invidivual/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, EditIndividualViewModel model)
        {
            try
            {
                var repo = new IndividualRepository(context);

                if (model.Email != model.EmailAnterior)
                {
                    var existeEmail = repo.Query(x => x.Email == model.Email && x.Id != model.Id).Count() > 0;
                    if (existeEmail)
                    {
                        ModelState.AddModelError("Email", "El email está ocupado");
                        return View();
                    }
                }
                else
                {
                    ModelState.Remove("Email");
                }
                
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    var individual = MapperHelpers.Map<Individual>(model);
                    repo.Update(individual);
                    context.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Invidivual/Delete/5
        public ActionResult Delete(int id)
        {
            var repo = new IndividualRepository(context);

            var individual = repo.Find(id);
            var model = MapperHelpers.Map<IndividualViewModel>(individual);
            return View(model);
        }

        // POST: Invidivual/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, IndividualViewModel model)
        {
            try
            {
                var repo = new IndividualRepository(context);
                //if (ModelState.IsValid)
                //{
                    var individual = MapperHelpers.Map<Individual>(model);
                    repo.Delete(individual);
                    context.SaveChanges();
                //}
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Details(int id)
        {
            try
            {
                var repo = new IndividualRepository(context);
                var repoAssignment = new AssignedRepository(context);
                var repoCursos = new CourseRepository(context);

                var individual = repo.Find(id);
                var indi = MapperHelpers.Map<IndividualViewModel>(individual);
                var assginments = repoAssignment.Query(x => x.IndividualId == individual.Id).OrderByDescending(x => x.AssingmentDate).ToList();

                foreach (var item in assginments)
                {
                    var course = repoCursos.Query(x => x.Id == item.CourseId).ToList();
                    var courseModel = MapperHelpers.Map<ICollection<CoursesViewModel>>(course).SingleOrDefault();
                    indi.Courses.Add(courseModel);
                }

                return View(indi);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }

        public JsonResult CheckEmail(string email)
        {
            var repo = new IndividualRepository(context);

            var emailExiste = repo.Query(e => e.Email == email ).Count == 0;

            return Json(emailExiste, JsonRequestBehavior.AllowGet);
        }
    }
}
