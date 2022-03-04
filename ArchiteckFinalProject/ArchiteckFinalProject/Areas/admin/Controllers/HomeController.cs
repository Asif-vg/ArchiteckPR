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
    [Authorize(Roles = "Admin")]

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
        public IActionResult ChartArchiteck()
        {
            return Json(Statistic());
        }

        public List<Indicator> Statistic()
        {
            var list = new List<Indicator>();
            list.Add(new Indicator { Title = "asif", Count = 1200 });
            //List<Indicator> indicator = new List<Indicator>();
            //indicator = _context.Indicators.Select(x => new Indicator
            //{
            //    Count = x.Count,
            //    Title = x.Title,

            //}).ToList();
            //return indicator;
            return list;
        }
    }
}



