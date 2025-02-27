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

    public class CompanyIndicatorController : Controller
    {

        private readonly AppDbContext _context;

        public CompanyIndicatorController(AppDbContext context)
        {
            _context = context;
        }
       
        public IActionResult Index()
        {
            return View(_context.CompanyIndicators.ToList());
        }

      

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
       

        public IActionResult Create(CompanyIndicator companyIndicator)
        {
            if (ModelState.IsValid)
            {
                _context.CompanyIndicators.Add(companyIndicator);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Please all data enter");
            return View(companyIndicator);
        }
    
        public IActionResult Update(int? id)
        {
            CompanyIndicator companyIndicator = null;

            if (id != null)
            {
                companyIndicator = _context.CompanyIndicators.Find(id);
                return View(companyIndicator);

            }
            else
            {
                ModelState.AddModelError("", "Id is null ,that's why not found Indicator");
                return RedirectToAction("Index");
            }

        }
        [HttpPost]
        

        public IActionResult Update(CompanyIndicator companyIndicator)
        {
            if (ModelState.IsValid)
            {
                _context.CompanyIndicators.Update(companyIndicator);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(companyIndicator);
        }
       

        public IActionResult Delete(int? id)
        {
            CompanyIndicator companyIndicator = null;

            if (id != null)
            {
                companyIndicator = _context.CompanyIndicators.Find(id);
                _context.CompanyIndicators.Remove(companyIndicator);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
