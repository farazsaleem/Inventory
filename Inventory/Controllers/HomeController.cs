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
        private readonly ILogger<HomeController> _logger;
      
        private IGenericRepository<tblInvertory> _ItblInvertoryRepository;
        private IGenericRepository<tblCategory> _ItblCategoryRepository;
        public ApplicationDbContext _context;
        public HomeController(ILogger<HomeController> logger, IGenericRepository<tblInvertory> ItblInvertoryRepository,
            IGenericRepository<tblCategory> ItblCategoryRepository, ApplicationDbContext context)
        {
            _logger = logger;
            this._ItblInvertoryRepository = ItblInvertoryRepository;
            _ItblCategoryRepository = ItblCategoryRepository;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<tblInvertory> Processor = await _context.tblInvertories.Include(x => x.tblCategory).ToListAsync();
            return View(Processor);
        }

        public async Task<IActionResult> Detail(int id)
        {
            tblInvertory Processor = await _context.tblInvertories.Include(x => x.tblCategory).Where(x=>x.Id==id).FirstOrDefaultAsync();
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

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            IEnumerable<tblCategory> list = await _ItblCategoryRepository.GetAllAsync();
            var Inventory = await _ItblInvertoryRepository.GetById(id);
            ViewBag.list = list;
            return View(Inventory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(tblInvertory tblInvertory)
        {
            await _ItblInvertoryRepository.Update(tblInvertory);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var Inventory = new tblInvertory()
            {
                Id=id,
            };
         
             await _ItblInvertoryRepository.Remove(Inventory);
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
            await _ItblCategoryRepository.Add(tblCategory);
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
