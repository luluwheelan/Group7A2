using Group7A2.Controllers;
using Group7A2.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Web.Mvc;
using System.Linq;
using System.Collections.Generic;
using Moq;

namespace Group7A2.Tests.Controllers
{
    
    [TestClass]
    public class CategoriesControllerTest
    {
        //Create MOQ data
        CategoriesController controller;
        List<Category> categories;
        Mock<ICategoryRepository> mock;

        [TestInitialize]
        public void TestInitialize()
        {
            categories = new List<Category>
            {
                new Category{CategoryId = 300, Name = "Ctest1", Description = "Moq date category one", Posts = new List<Post>{
                    new Post
            {
                Subject = "Test Post1",
                Content = "Test Post1 content",
                CategoryId = 300,

            },
                    new Post
            {
                Subject = "Test Post2",
                Content = "Test Post2 content",
                CategoryId = 300,

            }

        } },
                new Category{CategoryId = 301, Name = "Ctest2", Description = "Moq date category two"},
                new Category{CategoryId = 302, Name = "Ctest3", Description = "Moq date category three"}
            };
            Post p1 = new Post
            {
                Subject = "Test Post1",
                Content = "Test Post1 content",
                CategoryId = 300,

            };
            Post p2 = new Post
            {
                Subject = "Test Post2",
                Content = "Test Post2 content",
                CategoryId = 300,

            };
            //Set up mock object
            mock = new Mock<ICategoryRepository>();
            mock.Setup(c => c.Categories).Returns(categories.AsQueryable());
            //Initialize the controller and pass the mock object 
            controller = new CategoriesController(mock.Object);
        }

        [TestMethod]
        public void IndexReturnsView()
        {

            //Arrange
            //Instance decleared in TestInitialize()

            //Act
            ViewResult result = controller.Index() as ViewResult; 

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void IndexViewNameIsIndex()
        {
            //Arrange
            //Instance is decleared in TestInitialize()

            //Act
            ViewResult result = controller.Index() as ViewResult;
            string viewName = (result as ViewResult).ViewName;

            //Assert
            Assert.AreEqual("Index", viewName);
        }


        [TestMethod]
        public void IndexLoadsCategories()
        {
            // act
            // call the index method
            // access the data model returned to the view
            // cast the data as a list of type Category
            var results = (List<Category>)((ViewResult)controller.Index()).Model;

            // assert
            CollectionAssert.AreEqual(categories.OrderBy(c => c.Name).ToList(), results);
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
            RedirectToRouteResult actual = (RedirectToRouteResult)controller.Create(categories[0]);

            // assert
            Assert.AreEqual("Index", actual.RouteValues["action"]);
        }

        
        [TestMethod]
        public void CreateSaveInvalidLoadsEditView()
        {
            // arrange
            controller.ModelState.AddModelError("key", "error data");

            // act
            ViewResult actual = (ViewResult)controller.Create(categories[0]);

            // assert
            Assert.AreEqual("Create", actual.ViewName);
        }

        [TestMethod]
        public void PostListReturnsWithValidId()
        {
            //Act
            var result = (Category)((ViewResult)controller.PostList(300)).Model;
            //Assert
            Assert.AreEqual("Ctest1", result.Name);
        }

        [TestMethod]
        public void PostListReturnsErrorWithInvalidId()
        {
            //Act
            ViewResult result = controller.PostList(500) as ViewResult;
            //Assert
            Assert.AreEqual("Error", result.ViewName);
        }

        [TestMethod]
        public void PostListReturnsViewWithValidId()
        {
            //Act
            ViewResult result = controller.PostList(300) as ViewResult;
            //Assert
            Assert.AreEqual("PostList", result.ViewName);
        }

        [TestMethod]
        public void EditWithValidId()
        {
            int id = 300;
            // act           
            ViewResult actual = controller.Edit(id) as ViewResult;

            // assert           
            Assert.AreEqual("Edit", actual.ViewName);
        }

        [TestMethod]
        public void EditWithInvalidId()
        {
            int id = 3;
            // act           
            ViewResult actual = controller.Edit(id) as ViewResult;

            // assert           
            Assert.AreEqual("Error", actual.ViewName);

        }
        [TestMethod]
        public void EditInvalidModelReturnEditView()
        {

            // arrange
            controller.ModelState.AddModelError("key", "some error here");

            // act
            ViewResult actual = (ViewResult)controller.Edit(categories[0]);

            // assert
            Assert.AreEqual("Edit", actual.ViewName);

        }

        // POST: Edit
        [TestMethod]
        public void EditSaveValidReturnIndexView()
        {
            // act 
            RedirectToRouteResult actual = (RedirectToRouteResult)controller.Edit(categories[0]);

            // assert
            Assert.AreEqual("Index", actual.RouteValues["action"]);
        }

        [TestMethod]
        public void DetailReturnsErrorWithInvalidId()
        {
            //Act
            ViewResult result = controller.Details(500) as ViewResult;
            //Assert
            Assert.AreEqual("Error", result.ViewName);

        }

        [TestMethod]
        public void DetailReturnsValidViewWithValidId()
        {
            //Act
            var result = (Category)((ViewResult)controller.Details(300)).Model;
            //Assert
            Assert.AreEqual(categories.ToList()[0], result);

        }


        [TestMethod]
        public void DeleteWithValidId()
        {
            //Arrange
            int id = 300;
            // act
            ViewResult actual = controller.Delete(id) as ViewResult;

            // assert
            Assert.AreEqual("Delete", actual.ViewName);

        }

        [TestMethod]
        public void DeleteWithInValidId()
        {
            //Arrange
            int id = 2;
            // act
            ViewResult result = controller.Delete(id) as ViewResult;

            // assert
            Assert.AreEqual("Error", result.ViewName);
        }

        [TestMethod]
        public void DeleteConfirmedNoIdReturnError()
        {
            // act
            ViewResult actual = controller.DeleteConfirmed(null) as ViewResult;

            // assert
            Assert.AreEqual("Error", actual.ViewName);
        }

        [TestMethod]
        public void DeleteConfirmedInvalidIdReturnError()
        {
            // act
            ViewResult actual = controller.DeleteConfirmed(1) as ViewResult;

            // assert
            Assert.AreEqual("Error", actual.ViewName);
        }

        [TestMethod]
        public void DeleteConfirmedValidId()
        {
            // act
            //ViewResult actual = controller.DeleteConfirmed(300) as ViewResult;
            RedirectToRouteResult actual = (RedirectToRouteResult)controller.DeleteConfirmed(300);

            // assert
            Assert.AreEqual("Index", actual.RouteValues["action"]);
        }
    }
}
