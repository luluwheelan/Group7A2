using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Group7A2.Controllers;
using Group7A2.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Group7A2.Tests.Controllers
{
    

    [TestClass]
    public class PostsControllerTest
    {
        //Create MOQ data
        PostsController controller;
        List<Post> posts;
        Mock<IPostRepository> mock;

        [TestInitialize]
        public void TestInitialize()
        {
            // arrange
            mock = new Mock<IPostRepository>();

            // mock Post data
            posts = new List<Post>
            {
                new Post {
                    PostId = 1,
                    Subject = "Mock post 1",
                    Content = "Every monday to friday.",
                    Author = "Lulu",
                    CategoryId = 1,
                    Comments = new List<Comment>{
                        new Comment{CommentId = 1, Content = "Comment 1"
                        },
                        new Comment{CommentId = 2, Content = "Comment 2" }
                    } },
                new Post { PostId = 2,
                    Subject = "Mock post 2",
                    //Author = "Admin",
                    Content = "Every monday to friday.",
                    CategoryId = 1
                }
            };
            Post p2 = new Post
            {
                Subject = "Orillia to Barrie",
                Content = "Every monday to friday.",
                Author = "Lulu",
                Category = new Category { CategoryId = 1, Description = "Cateogry fake description", Name = "fake category"},

            };
            posts.Add(p2);

            // add Post data to the mock object
            mock.Setup(m => m.Posts).Returns(posts.AsQueryable());

            // pass the mock to the controller
            controller = new PostsController(mock.Object);
        }


        [TestMethod]
        public void DetailsReturnValidPost()
        {
            // act
            var actual = (PostCommentViewModel)((ViewResult)controller.Details(1)).Model;

            // assert
            Assert.IsInstanceOfType(actual, typeof(PostCommentViewModel));
        }

        [TestMethod]
        public void DetailsReturnValidPostView()
        {
            // act
            var actual = controller.Details(1) as ViewResult;

            // assert
            Assert.AreEqual("Details", actual.ViewName);
        }

        [TestMethod]
        public void DetailsWithInvalidIdReturnError()
        {
            // act
            ViewResult actual = controller.Details(4) as ViewResult;

            // assert
            Assert.AreEqual("Error", actual.ViewName);
        }

        [TestMethod]
        public void DetailsWithInvalidIdReturnNull()
        {
            //Arrange
            int id = 40; 
            // act
            var actual = controller.Details(id) as ViewResult;

            // assert
            Assert.IsNull(actual.Model);
        }

        [TestMethod]
        public void DetailsWithNoIdReturnError()
        {
            // act
            ViewResult actual = controller.Details(null) as ViewResult;

            // assert
            Assert.AreEqual("Error", actual.ViewName);
        }

        // GET: Create
        [TestMethod]
        public void CreateLoadsCategories()
        {
            // act
            ViewResult actual = (ViewResult)controller.Create();

            // assert
            Assert.IsNotNull(actual.ViewBag.CategoryId);
        }

        [TestMethod]
        public void CreateLoadsView()
        {
            // act
            ViewResult actual = controller.Create() as ViewResult;

            // assert
            Assert.AreEqual("Create", actual.ViewName);
        }

        // POST: Create
        //[TestMethod]
        //public void CreateSaveValidDataReturnToCategoryController()
        //{
        //    // act - turn the ActionResult to a RedirectToRouteResult
        //    var actual = controller.Create(posts[2]) as RedirectToRouteResult;

        //    // assert
        //    Assert.AreEqual("Categories", actual.RouteValues["Controller"]);
        //}

        [TestMethod]
        public void CreateSaveInvalidModelReturnError()
        {
            // arrange
            controller.ModelState.AddModelError("key", "some error here");

            // act
            var actual = controller.Create(posts[0]) as ViewResult;

            // assert
            Assert.IsNotNull(actual.ViewBag.CategoryId);
        }

        [TestMethod]
        public void CreateSaveInvalidLoadsEditView()
        {
            // arrange
            controller.ModelState.AddModelError("key", "some error here");

            // act
            ViewResult actual = (ViewResult)controller.Create(posts[0]);

            // assert
            Assert.AreEqual("Create", actual.ViewName);
        }

        // GET: Delete
        [TestMethod]
        public void DeleteWithNoIdReturnError()
        {
            // Act           
            ViewResult actual = controller.Delete(null) as ViewResult;

            // Assert           
            Assert.AreEqual("Error", actual.ViewName);
        }

        [TestMethod]
        public void DeleteValidId()
        {
            //act             
            ViewResult actual = controller.Delete(1) as ViewResult;

            //assert             
            Assert.AreEqual("Delete", actual.ViewName);
        }

        [TestMethod]
        public void DeleteValidIdReturnModel()
        {
            //act             
            ViewResult actual = controller.Delete(1) as ViewResult;

            //assert             
            Assert.IsNotNull(actual.Model);
        }

        [TestMethod]
        public void DeleteInValidId()
        {
            //act 
            var actual = controller.Delete(400) as ViewResult;

            //assert 
            Assert.IsNull(actual.Model);
        }


        [TestMethod]
        public void DeleteConfirmedInvalidId()
        {
            // act
            ViewResult actual = controller.DeleteConfirmed(4) as ViewResult;

            // assert
            Assert.AreEqual("Error", actual.ViewName);
        }

        [TestMethod]
        public void DeleteConfirmedWithValidIdReturnCategoryView()
        {
            //Arrange
            int id = 1;
            // act
           // ViewResult actual = controller.DeleteConfirmed(1) as ViewResult;
            var result = controller.DeleteConfirmed(id) as RedirectToRouteResult;

            // assert
            Assert.AreEqual("Categories", result.RouteValues["Controller"]);
        }

        [TestMethod]
        public void DeleteConfirmedWithValidIdReturnPostListView()
        {
            //Arrange
            int id = 1;
            // act
            // ViewResult actual = controller.DeleteConfirmed(1) as ViewResult;
            var result = controller.DeleteConfirmed(id) as RedirectToRouteResult;

            // assert
            Assert.AreEqual("PostList", result.RouteValues["Action"]);
        }

        [TestMethod]
        public void DeleteConfirmedValidId()
        {
            // act
            var actual = controller.DeleteConfirmed(1) as RedirectToRouteResult;
            // assert
            Assert.AreEqual("Categories", actual.RouteValues["Controller"]);
        }

        // GET: Edit
        [TestMethod]
        public void EditInvalidNoId()
        {
            // Arrange
            int? id = null;

            // Act          
            ViewResult actual = controller.Edit(id) as ViewResult;

            //Assert           
            Assert.AreEqual("Error", actual.ViewName);
        }

        [TestMethod]
        public void EditValidId()
        {
            // act           
            ViewResult actual = controller.Edit(1) as ViewResult;

            // assert           
            Assert.AreEqual("Edit", actual.ViewName);
        }

        [TestMethod]
        public void EditInvalidId()
        {
            // act           
            ViewResult actual = controller.Edit(4) as ViewResult;

            // assert   
            Assert.AreEqual("Error", actual.ViewName);
        }

        [TestMethod]
        public void EditValidCategory()
        {
            // act
            ViewResult actual = controller.Edit(1) as ViewResult;

            // assert
            Assert.IsNotNull(actual.ViewBag.CategoryId);
        }


        // POST: Edit
        //[TestMethod]
        //public void EditSaveValid()
        //{
        //    // act - casting the ActionResult to a RedirectToRouteResult
        //    RedirectToRouteResult actual = (RedirectToRouteResult)controller.Edit(posts[0]);

        //    // assert
        //    Assert.AreEqual("Details", actual.RouteValues["action"]);
        //}

        [TestMethod]
        public void EditSaveInvalidModel()
        {
            // arrange
            controller.ModelState.AddModelError("key", "some error here");

            // act
            ViewResult actual = (ViewResult)controller.Edit(posts[0]);

            // assert
            Assert.IsNotNull(actual.ViewBag.CategoryId);
        }


        [TestMethod]
        public void EditSaveInvalidLoadsEditView()
        {
            // arrange
            controller.ModelState.AddModelError("key", "some error here");

            // act
            ViewResult actual = (ViewResult)controller.Edit(posts[0]);

            // assert
            Assert.AreEqual("Edit", actual.ViewName);
        }

    }
}
