﻿using BSL.Interface;
using DAL;
using Inventory.Models;
using Microsoft.AspNetCore.Mvc;
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
        private readonly ILogger<HomeController> _logger;
      
        private IGenericRepository<tblInvertory> _ItblInvertoryRepository;
        private IGenericRepository<tblCategory> _ItblCategoryRepository;
        public HomeController(ILogger<HomeController> logger, IGenericRepository<tblInvertory> ItblInvertoryRepository,
            IGenericRepository<tblCategory> ItblCategoryRepository)
        {
            _logger = logger;
            this._ItblInvertoryRepository = ItblInvertoryRepository;
            _ItblCategoryRepository = ItblCategoryRepository;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<tblInvertory> Processor = await _ItblInvertoryRepository.GetAllAsync();
            return View(Processor);
        }
        public async Task<IActionResult> Create()
        {
            IEnumerable<tblCategory> list = await _ItblCategoryRepository.GetAllAsync();
            ViewBag.list = list;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(tblInvertory tblInvertory)
        {
           await _ItblInvertoryRepository.Add(tblInvertory);
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