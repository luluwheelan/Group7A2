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
    public class CommentsController : Controller
    {
        ICommentRepository db;

        public CommentsController()
        {
            this.db = new EFDataComments();
        }
        //If the contrustor pass in parameter, will use mock date, mean it is for testing.
        public CommentsController(ICommentRepository mockDb)
        {
            this.db = mockDb;
        }

        // GET: Comments
        //public ActionResult Index()
        //{
        //    var comments = db.Comments.Include(c => c.Post);
        //    return View(comments.ToList());
        //}

        // GET: Comments/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    //Comment comment = db.Comments.Find(id);
        //    Comment comment = db.Comments.SingleOrDefault(c => c.CommentId == id);
        //    if (comment == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(comment);
        //}

        // GET: Comments/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.PostId = new SelectList(db.Posts, "PostId", "Subject");
            return View("Create");
        }

        // POST: Comments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CommentId,Content,PostId,Author,PostTime")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                //db.Comments.Add(comment);
                //db.SaveChanges();
                int postId = comment.PostId;
                db.Save(comment);
                return RedirectToAction("Details", "Posts",new { id = postId});
            }

            ViewBag.PostId = new SelectList(db.Posts, "PostId", "Subject", comment.PostId);
            return View("Create", comment);
        }

        // GET: Comments/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                return View("Error");
            }
            //Comment comment = db.Comments.Find(id);
            Comment comment = db.Comments.SingleOrDefault(c => c.CommentId == id);
            if (comment == null)
            {
                //return HttpNotFound();
                return View("Error");
            }
            ViewBag.PostId = new SelectList(db.Posts, "PostId", "Subject", comment.PostId);
            return View("Edit", comment);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CommentId,Content,PostId,Author,PostTime")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(comment).State = EntityState.Modified;
                //db.SaveChanges();
                db.Save(comment);
                int postId = comment.PostId;
                return RedirectToAction("Details", "Posts", new { id = postId });
            }
            ViewBag.PostId = new SelectList(db.Posts, "PostId", "Subject", comment.PostId);
            
            return View(comment);
            
        }

        // GET: Comments/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                return View("Error");
            }
            //Comment comment = db.Comments.Find(id);
            Comment comment = db.Comments.SingleOrDefault(c => c.CommentId == id);
            if (comment == null)
            {
                //return HttpNotFound();
                return View("Error");
            }
            return View(comment);
        }

        // POST: Comments/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Comment comment = db.Comments.Find(id);
            Comment comment = db.Comments.SingleOrDefault(c => c.CommentId == id);
            //db.Comments.Remove(comment);
            //db.SaveChanges();
            db.Delete(comment);
            int postId = comment.PostId;
            return RedirectToAction("Details", "Posts", new { id = postId });
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
