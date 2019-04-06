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
    public class PostsController : Controller
    {

        IPostRepository db;

        public PostsController()
        {
            this.db = new EFDataPosts();
        }
        //If the contrustor pass in parameter, will use mock date, mean it is for testing.
        public PostsController(IPostRepository mockDb)
        {
            this.db = mockDb;
        }

        // GET: PostsEFDataPosts
        //public ActionResult Index()
        //{
        //    var posts = db.Posts.Include(p => p.Category);
        //    return View(posts.ToList());
        //}

        // GET: Posts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                return View("Error");
            }
            PostCommentViewModel postComment = new PostCommentViewModel();
            postComment.post = db.Posts.SingleOrDefault(c => c.PostId == id);
            //postComment.post.Comments = db.Comments.OrderByDescending(p => p.PostTime.Date).ThenBy(p => p.PostTime.TimeOfDay).ToList();
            
            //Post post = db.Posts.SingleOrDefault(c => c.PostId == id);
            if (postComment.post == null)
            {
                //return HttpNotFound();
                return View("Error");
            }
            postComment.post.Comments = postComment.post.Comments.OrderByDescending(p => p.PostTime.Date).ThenBy(p => p.PostTime.TimeOfDay).ToList();
            return View("Details",postComment);
        }


        // GET: Posts/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name");
            return View("Create");
        }

        // POST: Posts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "PostId,Subject,Content,CategoryId")] Post post)
        {
            if (ModelState.IsValid)
            {
                post.Author = User.Identity.Name;
                db.Save(post);
                //db.SaveChanges();
                //return RedirectToAction("Create");
                return RedirectToAction("PostList", "Categories", new { id = post.CategoryId });
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", post.CategoryId);
            return View("Create", post);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateComment([Bind(Include = "CommentId,Content,PostId")] Comment newComment)
        {
            PostCommentViewModel pc = new PostCommentViewModel();
            int id = newComment.PostId;
            if (ModelState.IsValid)
            {

                newComment.Author = User.Identity.Name;
                //db.Comments.Add(comment);
                //db.SaveChanges();
                
                db.Save(newComment);
                pc.post = db.Posts.SingleOrDefault(c => c.PostId == id);
                pc.post.Comments = pc.post.Comments.OrderByDescending(p => p.PostTime.Date).ThenBy(p => p.PostTime.TimeOfDay).ToList();
                ModelState.Clear();
                return PartialView("Details", pc);
            }

            pc.post = db.Posts.SingleOrDefault(c => c.PostId == id);
            return PartialView("Details", pc);
        }

        // GET: Posts/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                return View("Error");
            }
            Post post = db.Posts.SingleOrDefault(c => c.PostId == id);
            if (post == null)
            {
                //return HttpNotFound();
                return View("Error");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", post.CategoryId);
            return View("Edit", post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "PostId,Subject,Content,CategoryId")] Post post)
        {
            if (ModelState.IsValid)
            {
                post.Author = User.Identity.Name;
                //db.Entry(post).State = EntityState.Modified;
                //db.SaveChanges();
                db.Save(post);
                return RedirectToAction("Details", new { id = post.PostId});
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", post.CategoryId);
            return View("Edit", post);
        }

        // GET: Posts/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                return View("Error");
            }
            Post post = db.Posts.SingleOrDefault(c => c.PostId == id);
            if (post == null)
            {
                //return HttpNotFound();
                return View("Error");
            }
            return View("Delete", post); 
            
        }

        // POST: Posts/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            Post post = db.Posts.SingleOrDefault(c => c.PostId == id);
            
            if (post == null)
            {
                return View("Error");
            }
            int categoryId = post.CategoryId;
            db.Delete(post);
            return RedirectToAction("PostList", "Categories", new { id = categoryId });
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
