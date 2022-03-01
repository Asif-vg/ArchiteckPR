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
    //[Authorize]

    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        //public IActionResult ChartArchiteck()
        //{
        //    return Json(Statistic());
        //}

        //public List<Service> Statistic()
        //{
        //    List<Service> services =new List<Service>();
        //    services = _context.Services.Select(x => new Service
        //    {
        //        Name=x.Name,

        //    }).ToList();
        //    return services;
        //}
    }
}



