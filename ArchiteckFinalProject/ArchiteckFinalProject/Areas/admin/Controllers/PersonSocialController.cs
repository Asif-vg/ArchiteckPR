﻿using ArchiteckFinalProject.Data;
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

    public class PersonSocialController : Controller
    {
        private readonly AppDbContext _context;

        public PersonSocialController(AppDbContext context)
        {
            _context = context;
        }
       

        public IActionResult Index()
        {
            return View(_context.PersonSocials.ToList());
        }

        

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
       

        public IActionResult Create(PersonSocial personSocial)
        {
            if (ModelState.IsValid)
            {

                if (personSocial !=null)
                {
                    _context.PersonSocials.Add(personSocial);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Please all data enter");
                    return View(personSocial);
                }
            }
            ModelState.AddModelError("", "Please all data enter");
            return View(personSocial);
        }
        

        public IActionResult Update(int? id)
        {
            PersonSocial personSocial = null;

            if (id != null)
            {
                personSocial = _context.PersonSocials.Find(id);
                return View(personSocial);

            }
            else
            {
                ModelState.AddModelError("", "Id can not be null");
                return RedirectToAction("Index");
            }

        }
        [HttpPost]
      

        public IActionResult Update(PersonSocial personSocial)
        {
            if (ModelState.IsValid)
            {
                _context.PersonSocials.Update(personSocial) ;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(personSocial);
        }
       

        public IActionResult Delete(int? id)
        {

            PersonSocial personSocial = _context.PersonSocials.Find(id);
            if (personSocial != null)
            {
                _context.PersonSocials.Remove(personSocial);
                _context.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(personSocial);
        }
    }
}
