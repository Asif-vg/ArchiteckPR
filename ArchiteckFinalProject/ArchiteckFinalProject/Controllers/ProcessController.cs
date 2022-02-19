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
    public class ProcessController : Controller
    {
        private readonly AppDbContext _context;

        public ProcessController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            VmProcess vmProcess = new VmProcess()
            {
                Setting=_context.Settings.FirstOrDefault(),
                Socials = _context.Socials.ToList(),
                Banner = _context.Banners.FirstOrDefault(b => b.Page == "process"),
                Processes=_context.Processes.ToList(),
                Projects=_context.Projects.Include(pa => pa.ProjectArchiteck).ToList(),
                Services=_context.Services.Include(sc => sc.ServiceCatagory).ToList()
            };
            return View(vmProcess);
        }
    }
}
