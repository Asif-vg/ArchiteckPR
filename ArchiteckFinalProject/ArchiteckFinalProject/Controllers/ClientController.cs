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
    public class ClientController : Controller
    {
        private readonly AppDbContext _context;

        public ClientController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            VmClient client = new VmClient()
            {
                
                Setting = _context.Settings.FirstOrDefault(),
                Socials = _context.Socials.ToList(),
                Banner = _context.Banners.FirstOrDefault(b => b.Page == "client"),
                Clients=_context.Clients.ToList()
                
            };
            return View(client);
        }
    }
}
