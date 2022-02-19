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

    public class SocialController : Controller
    {
        private readonly AppDbContext _context;

        public SocialController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.Socials.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Social social)
        {

            if (ModelState.IsValid)
            {

                _context.Socials.Add(social);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Icon and Link is important data");
            }

            return View(social);
        }

        public IActionResult Update(int? id)
        {
            Social social = _context.Socials.Find(id);


            return View(social);
        }
        [HttpPost]
        public IActionResult Update(Social social)
        {
            if (ModelState.IsValid)
            {

                _context.Socials.Update(social);
                _context.SaveChanges();
                return RedirectToAction("Index");

            }

            return View(social);
        }

        public IActionResult Delete(int? id)
        {

            Social social = _context.Socials.Find(id);


            if (social != null)
            {
                _context.Socials.Remove(social);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(social);
        }
    }
}
