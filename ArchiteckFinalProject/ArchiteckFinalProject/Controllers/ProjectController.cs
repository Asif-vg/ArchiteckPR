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
    public class ProjectController : Controller
    {
        private readonly AppDbContext _context;

        public ProjectController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(VmSearch search)
        {
            VmProject vmProject = new VmProject()
            {
                Setting = _context.Settings.FirstOrDefault(),
                Socials = _context.Socials.ToList(),
                Projects = _context.Projects.ToList(),
                Project = _context.Projects.Include(pa =>pa.ProjectArchiteck).FirstOrDefault(),
                Banner = _context.Banners.FirstOrDefault(b => b.Page == "project")
            };


            vmProject.Projects = _context.Projects.Include(pa => pa.ProjectArchiteck)

                                   .Where(b => (search.searchData != null ? b.Name.Contains(search.searchData) : true) &&
                                              (search.arcId != null ? b.ProjectArchiteckId == search.arcId : true)
                                               ).ToList();




            vmProject.ProjectArchitecks = _context.ProjectArchitecks.ToList();
            return View(vmProject);
        }

        public IActionResult Detail(int? id)
        {
            Project project = null;

            Setting setting = _context.Settings.FirstOrDefault();
            List<Social> socials = _context.Socials.ToList();
            Banner banner = _context.Banners.FirstOrDefault(b => b.Page == "pdetail");
            List<Project> projects = _context.Projects.ToList();

            if (id != null)
            {
                project = _context.Projects.Find(id);
            }

            VmProject vmProject = new VmProject()
            {
                Socials = socials,
                Setting = setting,
                Project = project,
                Projects=projects,
                Banner = banner,
            };

            vmProject.ProjectArchitecks = _context.ProjectArchitecks.ToList();

            return View(vmProject);
        }
    }
}
