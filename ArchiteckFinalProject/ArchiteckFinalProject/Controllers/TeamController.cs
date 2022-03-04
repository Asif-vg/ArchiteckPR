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
    public class TeamController : Controller
    {
        private readonly AppDbContext _context;

        public TeamController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            VmTeam vmTeam = new VmTeam() {
                Setting = _context.Settings.FirstOrDefault(),
                Socials = _context.Socials.ToList(),
                Indicators=_context.Indicators.ToList(),
                Services=_context.Services.Include(sc => sc.ServiceComments).Include(sca=> sca.ServiceCatagory).ToList(),
                Teams = _context.Teams.Include(pp => pp.PersonPosition)
                .Include(pts=> pts.PersonToSocials).ThenInclude(ps=>ps.PersonSocial).ToList(),
                Banner = _context.Banners.FirstOrDefault(b => b.Page == "team"),
            };
            return View(vmTeam);
        }
    }
}
