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
                Content = "I want your book",
                Post = new Post
                {
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
            int id = 1;
            // act
            ViewResult actual = controller.Delete(id) as ViewResult;

            // assert
            Assert.AreEqual("Delete", actual.ViewName);

        }

        [TestMethod]
        public void DeleteWithInValidId()
        {
            //Arrange
            int id = 20;
            // act
            ViewResult result = controller.Delete(id) as ViewResult;

            // assert
            Assert.AreEqual("Error", result.ViewName);
        }


        [TestMethod]
        public void DeleteConfirmedInvalidIdReturnError()
        {
            //Arrange
            int id = 20;
            // act
            ViewResult actual = controller.DeleteConfirmed(id) as ViewResult;

            // assert
            Assert.AreEqual("Error", actual.ViewName);
        }



    }
}
