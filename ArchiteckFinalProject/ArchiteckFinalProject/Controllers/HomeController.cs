using ArchiteckFinalProject.Data;
using ArchiteckFinalProject.Models;
using ArchiteckFinalProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ArchiteckFinalProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {

            VmHome vmHome = new VmHome() {
                Setting = _context.Settings.FirstOrDefault(),
                Socials = _context.Socials.ToList(),
                About=_context.Abouts.FirstOrDefault(),
                Services=_context.Services.Include(sc => sc.ServiceCatagory).Include(sc => sc.ServiceComments).ToList(),
                Projects = _context.Projects.Include(pa => pa.ProjectArchiteck).ToList(),
                Processes = _context.Processes.ToList(),
                Solution = _context.Solutions.FirstOrDefault(),
                Teams=_context.Teams.Include(pp => pp.PersonPosition).Include(pts => pts.PersonToSocials).ThenInclude(ps => ps.PersonSocial).ToList(),
                Clients=_context.Clients.Include(ci => ci.ClientImages).ToList(),
                HomeBanners=_context.HomeBanners.ToList(),
                ServiceComments=_context.ServiceComments.ToList(),
                Indicators=_context.Indicators.ToList()

            };

            return View(vmHome);
        }

        public IActionResult Subscribe(string eml)
        {
            VmSubscribeResponse response = new VmSubscribeResponse();

            //if (string.IsNullOrEmpty(eml))
            //{
            //    response.Status = false;
            //    response.Message = "Subscription failed! You must enter your email";
            //    return Json(response);
            //}

            bool isExist = _context.Subscribes.Any(s => s.Email == eml);

            if (isExist)
            {
                response.Status = false;
                response.Message = "Your have already subscribed!";
                return Json(response);
            }

            Subscribe subscribe = new Subscribe();
            subscribe.CreatedDate = DateTime.Now;
            subscribe.Email = eml;
            _context.Subscribes.Add(subscribe);
            _context.SaveChanges();

            response.Status = true;
            //response.Message = "You subscribe successfully!";
            return Json(response);
        }

    }
}
