using BSL.Interface;
using DAL;
using Inventory.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTesting
{
    public class Class1
    {

        TestController _controller;
        //ApplicationDbContext _context;
        //IGenericRepository<tblInvertory> _Inventoryservice;
        //IGenericRepository<tblCategory> _Categoryservice;
        //ILogger<TestController> _logger;
        public Class1()
        {
 
            _controller = new TestController();
        }


        [Fact]
        public void Get_order_detail_success()
        {
            
            var okResult = _controller.Index();
            // Assert
           // Assert.IsType<OkObjectResult>(okResult.ExecuteResultAsync);
        }
    }
}
