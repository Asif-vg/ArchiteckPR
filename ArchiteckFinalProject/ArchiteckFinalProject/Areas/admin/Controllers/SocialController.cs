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
    [Authorize(Roles = "Moderator")]

    public class SocialController : Controller
    {
        private readonly AppDbContext _context;

        public SocialController(AppDbContext context)
        {
            _context = context;
        }
        //[AllowAnonymous]

        public IActionResult Index()
        {
            return View(_context.Socials.ToList());
        }
        [AllowAnonymous]

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]

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
        [AllowAnonymous]

        public IActionResult Update(int? id)
        {
            Social social = _context.Socials.Find(id);


            return View(social);
        }
        [HttpPost]
        [AllowAnonymous]

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
        [AllowAnonymous]

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
