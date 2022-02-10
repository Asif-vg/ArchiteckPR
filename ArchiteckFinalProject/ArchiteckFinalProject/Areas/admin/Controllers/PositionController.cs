using ArchiteckFinalProject.Data;
using ArchiteckFinalProject.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArchiteckFinalProject.Areas.admin.Controllers
{
    [Area("admin")]
    public class PositionController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PositionController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public IWebHostEnvironment WebHostEnvironment => _webHostEnvironment;

        public IActionResult Index()
        {
            return View(_context.PersonPositions.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(PersonPosition position)
        {
            if (ModelState.IsValid)
            {
                if (position.Name!=null)
                {
                    _context.PersonPositions.Add(position);
                    _context.SaveChanges();
                    return View(position);
                }
                else
                {
                    ModelState.AddModelError("", "Please all data enter");
                    return View(position);
                }

            }
            return View(position);
        }

        public IActionResult Update(int? id)
        {
            PersonPosition position = null;

            if (id != null)
            {
                position = _context.PersonPositions.Find(id);
                return View(position);

            }
            else
            {
                ModelState.AddModelError("", "Id is null ,that's why not found Position");
                return RedirectToAction("Index");
            }

        }
        [HttpPost]
        public IActionResult Update(PersonPosition position)
        {
            if (ModelState.IsValid)
            {
                _context.PersonPositions.Update(position);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(position);
        }
        public IActionResult Delete(int? id)
        {
            PersonPosition position = null;

            if (id != null)
            {
                position = _context.PersonPositions.Find(id);
                _context.PersonPositions.Remove(position);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
