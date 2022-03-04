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
        //public IActionResult Message()
        //{
        //    return View();
        //}

        
        public async Task<IActionResult> Message(string nm, string sbj, string eml, string txt)
        {
            VmResponse response = new();

            Message message1 = new();

            message1.Email = eml;
            message1.Name = nm;
            message1.Subject = sbj;
            message1.Text = txt;
            message1.CreatedDate = DateTime.Now;

             await _context.Messages.AddAsync(message1);
             await _context.SaveChangesAsync();

            response.Success = true;
            return Json(response);

        }
    }
}
