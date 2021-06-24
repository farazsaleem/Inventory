using BSL.Interface;
using DAL;
using Inventory.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory.Controllers
{
    public class HomeController : Controller
    {
       
        private readonly IInventoryService _inventoryService;
        public HomeController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }


        public async Task<IActionResult> Index()
        {
            List<tblInvertory> result = await _inventoryService.GetAllAsync();
            return View(result);
        }

        public async Task<IActionResult> Detail(int id)
        {
            tblInvertory result = await _inventoryService.Detail(id);
            return View(result);
        }
        public async Task<IActionResult> Create()
        {
            List<tblCategory> list = await _inventoryService.GetAllCategoryAsync();
            ViewBag.list = list;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(tblInvertory tblInvertory)
        {
            if(!ModelState.IsValid)
                return View("Create", tblInvertory);
            await _inventoryService.Add(tblInvertory);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            List<tblCategory> list = await _inventoryService.GetAllCategoryAsync();
            var Inventory = await _inventoryService.Detail(id);
            ViewBag.list = list;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(tblInvertory tblInvertory)
        {
            await _inventoryService.Update(tblInvertory);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var Inventory = new tblInvertory()
            {
                Id=id,
            };
         
             await _inventoryService.Remove(Inventory);
            return RedirectToAction("Index");
        }


        public IActionResult CreateCategory()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCategory(tblCategory tblCategory)
        {
            await _inventoryService.CreateCategory(tblCategory);
            return RedirectToAction("Index");
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
