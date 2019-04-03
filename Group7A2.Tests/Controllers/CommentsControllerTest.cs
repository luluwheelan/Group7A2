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
    public class CommentsControllerTest
    {
        //Create MOQ data
        CommentsController controller;
        List<Comment> comments;
        Mock<ICommentRepository> mock;

        [TestInitialize]
        public void TestInitialize()
        {

            // mock Comment data
            comments = new List<Comment>
            {
                new Comment
            {
                    CommentId = 100,
                Content = "I want your book",
                Post = new Post
                {
                    PostId = 100,
                    Subject = "Sell Java book",
                    Content = "Post fake content",
                    CategoryId = 1,

                },
                }
            };


            // add Comment data to the mock object
            mock = new Mock<ICommentRepository>();
            mock.Setup(m => m.Comments).Returns(comments
                .AsQueryable());

            // pass the mock to the controller
            controller = new CommentsController(mock.Object);
        }

        [TestMethod]
        public void CreateLoadsView()
        {
            // act
            ViewResult actual = (ViewResult)controller.Create();

            // assert
            Assert.AreEqual("Create", actual.ViewName);
        }

        // POST: Create
        [TestMethod]
        public void CreateSaveValidData()
        {
            // act 
            RedirectToRouteResult actual = (RedirectToRouteResult)controller.Create(comments[0]);

            // assert
            Assert.AreEqual("Details", actual.RouteValues["action"]);
        }

        [TestMethod]
        public void CreateSaveInvalidLoadsEditView()
        {
            // arrange
            controller.ModelState.AddModelError("key", "error data");

            // act
            ViewResult actual = (ViewResult)controller.Create(comments[0]);

            // assert
            Assert.AreEqual("Create", actual.ViewName);
        }

        [TestMethod]
        public void DeleteWithValidId()
        {
            //Arrange
            int validId = 100;
            // act
            var result = controller.DeleteConfirmed(validId) as RedirectToRouteResult;

            // assert
            Assert.AreEqual("Posts", result.RouteValues["Controller"]);

        }

        [TestMethod]
        public void DeleteWithInvalidId()
        {
            //Arrange
            int inValidId = 1000;
            // act
            var result = controller.DeleteConfirmed(inValidId) as ViewResult;

            // assert
            Assert.AreEqual("Error", result.ViewName);

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
        public void DeleteWithInValidId()
        {
            //Arrange
            int inValidId = 20;
            // act
            ViewResult result = controller.Delete(inValidId) as ViewResult;

            // assert
            Assert.AreEqual("Error", result.ViewName);
        }

        [TestMethod]
        public void EditWithValidId()
        {
            int validId = 100;
            // act           
            ViewResult actual = controller.Edit(validId) as ViewResult;

            // assert           
            Assert.AreEqual("Edit", actual.ViewName);
        }

        [TestMethod]
        public void EditWithInvalidId()
        {
            int invalidId = 3;
            // act           
            ViewResult actual = controller.Edit(invalidId) as ViewResult;

            // assert           
            Assert.AreEqual("Error", actual.ViewName);

        }
        [TestMethod]
        public void EditInvalidModelReturnEditView()
        {

            // arrange
            controller.ModelState.AddModelError("key", "some error here");

            // act
            ViewResult actual = (ViewResult)controller.Edit(comments[0]);

            // assert
            Assert.AreEqual("Edit", actual.ViewName);

        }

        // POST: Edit
        [TestMethod]
        public void EditSaveValidReturnPostDetailView()
        {

            // act 
            RedirectToRouteResult actual = (RedirectToRouteResult)controller.Edit(comments[0]);

            // assert
            Assert.AreEqual("Details", actual.RouteValues["action"]);
        }

        // POST: Edit
        [TestMethod]
        public void EditSaveValidReturnPostDetail()
        {

            // act 
            RedirectToRouteResult actual = (RedirectToRouteResult)controller.Edit(comments[0]);

            // assert
            Assert.AreEqual("Posts", actual.RouteValues["controller"]);
        }


    }
}
