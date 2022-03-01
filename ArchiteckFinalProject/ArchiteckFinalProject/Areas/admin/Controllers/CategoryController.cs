using ArchiteckFinalProject.Data;
using ArchiteckFinalProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArchiteckFinalProject.Areas.admin.Controllers
{
    [Area("admin")]
    [Authorize]

    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }
        [AllowAnonymous]

        public IActionResult Index()
        {
            return View(_context.ServiceCatagories.ToList());
        }
        [AllowAnonymous]

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]

        public IActionResult Create(ServiceCatagory model)
        {
            if (ModelState.IsValid)
            {
                if (model.Name != null)
                {
                    _context.ServiceCatagories.Add(model);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Please all data enter");
                    return View(model);
                }

            }
            return View(model);
        }
        [AllowAnonymous]

        public IActionResult Update(int? id)
        {
            ServiceCatagory catagory = null;

            if (id != null)
            {
                catagory = _context.ServiceCatagories.Find(id);
                return View(catagory);

            }
            else
            {
                ModelState.AddModelError("", "Id is null ,that's why not found Category");
                return RedirectToAction("Index");
            }

        }
        [HttpPost]
        [AllowAnonymous]

        public IActionResult Update(ServiceCatagory catagory)
        {
            if (ModelState.IsValid)
            {
                _context.ServiceCatagories.Update(catagory);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(catagory);
        }
        [AllowAnonymous]

        public IActionResult Delete(int? id)
        {
            ServiceCatagory catagory = null;

            if (id != null)
            {
                catagory = _context.ServiceCatagories.Find(id);
                _context.ServiceCatagories.Remove(catagory);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
