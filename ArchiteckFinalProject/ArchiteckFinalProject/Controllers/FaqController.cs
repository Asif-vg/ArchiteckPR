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
    public class FaqController : Controller
    {
        private readonly AppDbContext _context;

        public FaqController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Faq> faqs = _context.Faqs.ToList();
            VmFaq vmFaq = new VmFaq() {
                Setting = _context.Settings.FirstOrDefault(),
                Socials = _context.Socials.ToList(),
                Banner  = _context.Banners.FirstOrDefault(b => b.Page == "faq"),
                Faq=_context.Faqs.FirstOrDefault(),
                Faqs=faqs

            };
            return View(vmFaq);
        }
    }
}
