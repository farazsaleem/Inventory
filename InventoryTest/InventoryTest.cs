using BSL.Interface;
using DAL;
using Inventory.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace InventoryTest
{
    public class InventoryTest 
    {
        #region Property  
        public Mock<IInventoryService> mock = new Mock<IInventoryService>();


        #endregion

        [Fact]
        public async void GetAllTest()
        {
            // Arrange  
            var mockService = new Mock<IInventoryService>();
            mockService.Setup(service =>
                service.GetAllAsync()).ReturnsAsync(new List<tblInvertory>());

            var sut = new HomeController(mockService.Object);

            // Act  
            var result =await sut.Index();

            // Assert  
            var viewResult = Assert.IsType<ViewResult>(result);
            var viewModel = Assert.IsType<List<tblInvertory>>(viewResult.Model);
           
        }
      

        [Fact(DisplayName = "DetailTest")]
        public async void DetailTest()
        {
            var controller = new HomeController(mock.Object);
            var result =await controller.Detail(1);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async void InvalidModelTest()
        {

            // Arrange
            var model = new tblInvertory { Processor = "" }; // Invalid model
            var controller = new HomeController(mock.Object);

            // Have to explictly add this
            controller.ModelState.AddModelError("Processor", "Required");

            // Act
            var result = await controller.Create(model);

            // Assert etc
        }


        [Fact]
        public async void Create_ModelStateValid_CreateInventoryCalledOnce()
        {
            tblInvertory inventory = null;
            mock.Setup(r => r.Add(It.IsAny<tblInvertory>()))
                .Callback<tblInvertory>(x => inventory = x);

            var model = new tblInvertory
            {
                Quantity = 1,
                Processor = "Test",
                Brand = "Test",
                CategoryID = 1,
                ComputerType = "ABC",
                FromFactor = "1",
                RamSlots = 1,
                ScreenSize = "12*14",
                UsbPorts = 1,
            };
            var controller = new HomeController(mock.Object);
            await controller.Create(model);

            mock.Verify(x => x.Add(It.IsAny<tblInvertory>()), Times.Once);

            Assert.Equal(inventory.Quantity, model.Quantity);
            Assert.Equal(inventory.Processor, model.Processor);
            Assert.Equal(inventory.Brand, model.Brand);
        }


        [Fact]
        public async void Create_ModelStateValid_CreateCategoryCalledOnce()
        {
            tblCategory category = null;
            mock.Setup(r => r.CreateCategory(It.IsAny<tblCategory>()))
                .Callback<tblCategory>(x => category = x);

            var model = new tblCategory
            {
                CategoryName = "ABC",
               
            };
            var controller = new HomeController(mock.Object);
            await controller.CreateCategory(model);

            mock.Verify(x => x.CreateCategory(It.IsAny<tblCategory>()), Times.Once);

            Assert.Equal(category.CategoryName, model.CategoryName);
        }

        [Fact]
        public async Task  DeleteItemTest()
        {
            var model = new tblInvertory
            {
                Id = 1,

            };
            mock.Setup(repo => repo.Remove(It.IsAny<tblInvertory>())).Returns(Task.CompletedTask);

            var controller = new HomeController(mock.Object);
            await controller.Delete(model.Id);

            // Assert
            mock.Verify(repo => repo.Remove(It.IsAny<tblInvertory>()), Times.Once);
        }
    }
}
