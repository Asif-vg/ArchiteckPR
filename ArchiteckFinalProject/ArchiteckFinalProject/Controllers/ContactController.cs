using ArchiteckFinalProject.Data;
using ArchiteckFinalProject.Models;
using ArchiteckFinalProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArchiteckFinalProject.Controllers
{
    public class ContactController : Controller
    {
        private readonly AppDbContext _context;

        public ContactController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            Setting setting = _context.Settings.FirstOrDefault();
            List<Social> socials = _context.Socials.ToList();
            Banner banner = _context.Banners.FirstOrDefault(b => b.Page == "contact");
            List<Office> office = _context.Offices.ToList();
            VmContact vmContact = new VmContact()
            {
                Setting = setting,
                Socials = socials,
                Banner = banner,
                Offices =office
            };

            return View(vmContact);
        }
        public IActionResult Message()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Message(VmContact vmContact)
        {
            if (ModelState.IsValid)
            {

                vmContact.Contact.CreatedDate = DateTime.Now;
                _context.Messages.Add(vmContact.Contact);
                _context.SaveChanges();
                return RedirectToAction("Index");

            }
            Setting setting = _context.Settings.FirstOrDefault();
            List<Social> socials = _context.Socials.ToList();
            Banner banner = _context.Banners.FirstOrDefault(b => b.Page == "contact");
            List<Office> office = _context.Offices.ToList();
            VmContact vmContact1 = new VmContact()
            {
                Setting = setting,
                Socials = socials,
                Contact = vmContact.Contact,
                Banner = banner,
                Offices = office
            };


            return View("Index", vmContact1);
        }
    }
}
