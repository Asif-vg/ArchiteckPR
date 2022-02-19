using ArchiteckFinalProject.Data;
using ArchiteckFinalProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArchiteckFinalProject.Controllers
{
    public class AboutController : Controller
    {
        private readonly AppDbContext _context;

        public AboutController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            VmAbout model = new VmAbout()
            {
                Setting = _context.Settings.FirstOrDefault(),
                Socials = _context.Socials.ToList(),
                About = _context.Abouts.FirstOrDefault(),
                Solution =_context.Solutions.FirstOrDefault(),
                Banner=_context.Banners.FirstOrDefault(b => b.Page == "about"),
                Teams = _context.Teams.Include(pp => pp.PersonPosition).Include(pts => pts.PersonToSocials).ThenInclude(ps => ps.PersonSocial).ToList(),
                Services = _context.Services.Include(sc => sc.ServiceCatagory).ToList()


            };
            return View(model);
        }
    }
}
