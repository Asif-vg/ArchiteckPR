using ArchiteckFinalProject.Data;
using ArchiteckFinalProject.Models;
using ArchiteckFinalProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArchiteckFinalProject.Controllers
{
    public class ServiceController : Controller
    {
        private readonly AppDbContext _context;

        public ServiceController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            VmService model = new VmService()
            {
                Setting = _context.Settings.FirstOrDefault(),
                Socials = _context.Socials.ToList(),
                Services = _context.Services.ToList(),
                Service = _context.Services.FirstOrDefault(),
                Banner = _context.Banners.FirstOrDefault(b => b.Page == "service")
            };
            return View(model);
        }

        public IActionResult Detail(int? id)
        {
            Service service = null;
           
            Setting setting = _context.Settings.FirstOrDefault();
            List<Social> socials = _context.Socials.ToList();
            Banner banner = _context.Banners.FirstOrDefault(b => b.Page == "sdetail");
            List<ServiceComment> serviceComments = _context.ServiceComments.OrderByDescending(bc => bc.CreatedDate).Where(i => i.ServiceId == id).ToList();
            List<ServiceComment> replyComments = _context.ServiceComments.Where(bc => bc.ParentId != null).ToList();

            if (id != null)
            {
                service = _context.Services.Find(id);
            }

            VmService vmservice = new VmService()
            {
                Socials = socials,
                Setting = setting,
                Service = service,
                Services =_context.Services.Include(sc => sc.ServiceCatagory).ToList(), 
                ServiceComments = serviceComments,
                Banner = banner,
                ReplyComments = replyComments
            };
            return View(vmservice);
        }

        [HttpPost]
        public IActionResult Comment(VmService vmService)
        {
            Setting setting = _context.Settings.FirstOrDefault();
            List<Social> socials = _context.Socials.ToList();
            Banner banner = _context.Banners.FirstOrDefault(b => b.Page == "sdetail");
            List<Service> popularService = _context.Services.OrderByDescending(o => o.CreatedDate).Take(3).ToList();
            Service service = _context.Services.Find(vmService.ServiceComment.ServiceId);
            List<ServiceComment> serviceComments = _context.ServiceComments.OrderByDescending(bc => bc.CreatedDate).Where(i => i.ServiceId == vmService.ServiceComment.ServiceId).ToList();
            List<ServiceComment> replyComments = _context.ServiceComments.Where(bc => bc.ParentId != null).ToList();

            if (ModelState.IsValid)
            {
                vmService.ServiceComment.CreatedDate = DateTime.Now;
                _context.ServiceComments.Add(vmService.ServiceComment);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            VmService vmService1 = new VmService()
            {
                Socials = socials,
                Setting = setting,
                ServiceComment = vmService.ServiceComment,
                Service = service,
                Banner = banner,
                //PopularPosts = popularBlogs,
                ServiceComments = serviceComments,
                //CustomUser = custom,
                ReplyComments = replyComments
            };

            return View("detail", vmService1);
        }
    }
}
