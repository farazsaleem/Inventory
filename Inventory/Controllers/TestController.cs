using BSL.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory.Controllers
{
    public class TestController : Controller
    {
        private readonly IInventoryService _inventoryService;
        public TestController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }





        
    }
}
