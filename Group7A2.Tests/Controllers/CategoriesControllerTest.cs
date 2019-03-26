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
                new Category{CategoryId = 300, Name = "Ctest1", Description = "Moq date category one"},
                new Category{CategoryId = 301, Name = "Ctest2", Description = "Moq date category two"},
                new Category{CategoryId = 302, Name = "Ctest3", Description = "Moq date category three"}
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
            //Instance is decleared in TestInitialize()

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
        public void DetailReturnsViewWithValidId()
        {
            //Act

            //Assert

        }

        [TestMethod]
        public void DetailReturnsErrorWithoutId()
        {
            // act


            // assert

        }

        [TestMethod]
        public void DeleteWithoutIdReturnsError()
        {
            // act


            // assert

        }

        [TestMethod]
        public void DeleteWithValidIdConfirmed()
        {
            //Arrange
            int id = 2;
            // act
            ViewResult actual = controller.DeleteConfirmed(id) as ViewResult;

            // assert
            Assert.AreEqual("Index", actual.ViewName);
        }

        [TestMethod]
        public void DeleteWithInValidId()
        {
            //act 
            ViewResult actual = controller.Delete(4) as ViewResult;

            //assert 

        }

        [TestMethod]
        public void EditWithValidId()
        {
            // act           
            ViewResult actual = controller.Edit(1) as ViewResult;

            // assert           
            Assert.AreEqual("Edit", actual.ViewName);
        }

        [TestMethod]
        public void EditWithInvalidId()
        {
            // act           
            ViewResult actual = controller.Edit(4) as ViewResult;

            // assert   

        }
    }
}
