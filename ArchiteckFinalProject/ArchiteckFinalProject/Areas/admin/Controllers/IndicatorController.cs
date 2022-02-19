using ArchiteckFinalProject.Data;
using ArchiteckFinalProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArchiteckFinalProject.Areas.admin.Controllers
{
    [Area("admin")]
    [Authorize]

    public class IndicatorController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public IndicatorController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            return View(_context.Indicators.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Indicator indicator)
        {
            if (ModelState.IsValid)
            {
                    _context.Indicators.Add(indicator);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
            }

             ModelState.AddModelError("", "Please all data enter");
                return View(indicator);
        }

        public IActionResult Update(int? id)
        {
            Indicator indicator = null;

            if (id != null)
            {
                indicator = _context.Indicators.Find(id);
                return View(indicator);

            }
            else
            {
                ModelState.AddModelError("", "Id is null ,that's why not found Indicator");
                return RedirectToAction("Index");
            }

        }
        [HttpPost]
        public IActionResult Update(Indicator indicator)
        {
            if (ModelState.IsValid)
            {
                _context.Indicators.Update(indicator);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(indicator);
        }
        public IActionResult Delete(int? id)
        {
            Indicator indicator = null;

            if (id != null)
            {
                indicator = _context.Indicators.Find(id);
                _context.Indicators.Remove(indicator);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
