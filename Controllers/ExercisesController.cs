using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using FitnessNotebook.DAO;
using FitnessNotebook.Models;
using System.Data.SqlClient;
using Microsoft.AspNet.Identity;
using System.Web.Helpers;
using System;
using System.Collections.Generic;

namespace FitnessNotebook.Controllers
{
    public class ExercisesController : Controller
    {
        private FitNotebookDB_Context db = new FitNotebookDB_Context();

        // GET: Exercises
        public ActionResult Index()
        {
            return View(db.Exercises.SqlQuery("SELECT * FROM Exercises WHERE Email = @user", new SqlParameter("@user", User.Identity.Name)).ToList());
        }

        // GET: Exercises/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exercises exercises = db.Exercises.Find(id);
            if (exercises == null)
            {
                return HttpNotFound();
            }
            
            return View(exercises);

        }

        // GET: Exercises/Create
        public ActionResult Create()
        {
            Exercises newExercise = new Exercises();
            newExercise.Email = User.Identity.GetUserName();


            //Populate the drop down menu for the four exercise types.
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "Endurance", Value = "0" });
            items.Add(new SelectListItem { Text = "Strength", Value = "1" });
            items.Add(new SelectListItem { Text = "Balance", Value = "2" });
            items.Add(new SelectListItem { Text = "Flexibility", Value = "3" });

            ViewBag.ExerciseTypes = items;

            return View(newExercise);
        }

        // POST: Exercises/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ExerciseID,Email,ExerciseName,Repetitions,Sets,WeightType,Weight,Date,Duration,DistanceDone")] Exercises exercises)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Exercises.Add(exercises);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    return RedirectToAction("Error");
                }
                
                
            }
          
            return View(exercises);
        }

        // GET: Exercises/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exercises exercises = db.Exercises.Find(id);
            if (exercises == null)
            {
                return HttpNotFound();
            }

            return View(exercises);
        }

        // POST: Exercises/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ExerciseID,Email,ExerciseName,Repetitions,Sets,WeightType,Weight,Date,Duration,DistanceDone")] Exercises exercises)
        {
            if (ModelState.IsValid)
            {
                db.Entry(exercises).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(exercises);
        }

        // GET: Exercises/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exercises exercises = db.Exercises.Find(id);
            if (exercises == null)
            {
                return HttpNotFound();
            }
            return View(exercises);
        }

        // POST: Exercises/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Exercises exercises = db.Exercises.Find(id);
            db.Exercises.Remove(exercises);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Chart Creation Methods
        
        public ActionResult ProgressChart(string exerciseName)
        {
            if (exerciseName == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                var report = db.Exercises.AsEnumerable().Select(s => new
                {
                    Email = s.Email,
                    Weight = s.Weight,
                    ExeriseName = s.ExerciseName,
                }).Where(s => s.Email == User.Identity.Name && s.ExeriseName == exerciseName);

                var chart = new Chart(width: 800, height: 350, theme: ChartTheme.Green).AddTitle("Progress Over Time\n\nValues Shown In Order Of Entry");

                foreach (var i in report)
                {
                    chart.AddSeries(
                       chartType: "column",
                       axisLabel: exerciseName,
                       yValues: new[] { i.Weight },
                       yFields: i.Weight.ToString())
                .GetBytes("png");

                }
                chart.Write("png");
                
                
            }

            return null;
        }

        private void Select(object weight)
        {
            throw new NotImplementedException();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
