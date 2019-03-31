using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Group7A2.Models;

namespace Group7A2.Controllers
{
    [RequireHttps] 
    public class CategoriesController : Controller
    {
        //private Group7A2Context db = new Group7A2Context();
        //Create two constructors
        //dafault constructors with no params, and will get Entity Framework
        ICategoryRepository db;

        public CategoriesController()
        {
            this.db = new EFDataCategories();
        }
        //If the contrustor pass in parameter, will use mock date, mean it is for testing.
        public CategoriesController(ICategoryRepository mockDb)
        {
            this.db = mockDb;
        }

        // GET: Categories
        [Route("")]
        [Route("index")]
        [Route("home")]
        [Route("category")]
        [Route("categories/Index")]
        public ActionResult Index()
        {
            List<Category> categories = db.Categories.ToList();
            foreach (Category category in categories)
            {
                //only show the latest two posts title
                category.Posts = category.Posts.OrderByDescending(p => p.PostTime).Take(2).ToList();
            }
            return View("Index", categories);
        }

        //GET: Categories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                return View("Error");
            }
            //Category category = db.Categories.Find(id);
            Category category = db.Categories.SingleOrDefault(c => c.CategoryId == id);
            if (category == null)
            {
                //return HttpNotFound();
                return View("Error");
            }
            return View(category);
        }

        // GET: Categories/Details/5
        //[Route("category/{id}")]
        public ActionResult PostList(int? id)
        {
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                return View("Error");
            }
            //Category category = db.Categories.Find(id);
            Category category = db.Categories.SingleOrDefault(c => c.CategoryId == id);
            
            if (category == null)
            {
                //return HttpNotFound("Error");
                return View("Error");
            }
            category.Posts = category.Posts.OrderByDescending(p => p.PostTime).ToList();
            return View("PostList",category);
        }

        // GET: Categories/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View("Create");
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "CategoryId,Name,Description")] Category category)
        {
            if (ModelState.IsValid)
            {
                //db.Categories.Add(category);
                //db.SaveChanges();
                db.Save(category);
                return RedirectToAction("Index");
            }

            return View("Create", category);
        }

        // GET: Categories/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                return View("Error");
            }
            //Category category = db.Categories.Find(id);
            Category category = db.Categories.SingleOrDefault(c => c.CategoryId == id);
            if (category == null)
            {
                //return HttpNotFound();
                return View("Error");
            }
            return View("Edit", category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "CategoryId,Name,Description")] Category category)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(category).State = EntityState.Modified;
                //db.SaveChanges();
                db.Save(category);
                return RedirectToAction("Index");
            }
            return View("Edit", category);
        }

        // GET: Categories/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                return View("Error");
            }
            //Category category = db.Categories.Find(id);
            Category category = db.Categories.SingleOrDefault(c => c.CategoryId == id);
            if (category == null)
            {
                //return HttpNotFound();
                return View("Error");
            }
            return View("Delete", category);
        }

        // POST: Categories/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return View("Error");
            }
            //Category category = db.Categories.Find(id);
            Category category = db.Categories.SingleOrDefault(c => c.CategoryId == id);
            if (category == null)
            {
                return View("Error");
            }
            //db.Categories.Remove(category);
            //db.SaveChanges();
            db.Delete(category);
            return RedirectToAction("Index");
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
