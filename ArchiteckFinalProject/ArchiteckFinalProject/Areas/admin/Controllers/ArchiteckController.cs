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
    [Authorize(Roles = "Admin")]

    public class ArchiteckController : Controller
    {
        private readonly AppDbContext _context;

        public ArchiteckController(AppDbContext context)
        {
            _context = context;
        }
       

        public IActionResult Index()
        {
            return View(_context.ProjectArchitecks.ToList());
        }
      
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        

        public IActionResult Create(ProjectArchiteck architeck)
        {
            if (ModelState.IsValid)
            {
                if (architeck.Name != null)
                {
                    _context.ProjectArchitecks.Add(architeck);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Please all data enter");
                    return View(architeck);
                }

            }
            return View(architeck);
        }
      

        public IActionResult Update(int? id)
        {
            ProjectArchiteck architeck = null;

            if (id != null)
            {
                architeck = _context.ProjectArchitecks.Find(id);
                return View(architeck);

            }
            else
            {
                ModelState.AddModelError("", "Id is null ,that's why not found Category");
                return RedirectToAction("Index");
            }

        }
        [HttpPost]
        
        public IActionResult Update(ProjectArchiteck architeck)
        {
            if (ModelState.IsValid)
            {
                _context.ProjectArchitecks.Update(architeck);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(architeck);
        }
       
        public IActionResult Delete(int? id)
        {
            ProjectArchiteck architeck = null;

            try
            {
                if (id != null)
                {
                    architeck = _context.ProjectArchitecks.Find(id);
                    _context.ProjectArchitecks.Remove(architeck);
                    _context.SaveChanges();
                }
               
                return Json(new
                {
                    code = 204,
                    message = "Item has been deleted successfully!"
                });
            }
            catch (Exception)
            {
                return Json(new
                {
                    code = 500,
                    message = "Something went wrong!"
                });
            }
           

            //return RedirectToAction("Index");
        }
    }
}
