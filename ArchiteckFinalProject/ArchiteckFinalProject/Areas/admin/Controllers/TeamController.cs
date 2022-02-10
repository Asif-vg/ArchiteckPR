using ArchiteckFinalProject.Data;
using ArchiteckFinalProject.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ArchiteckFinalProject.Areas.admin.Controllers
{
    [Area("admin")]
    public class TeamController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public TeamController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            return View(_context.Teams
                                      .Include(p => p.PersonPosition)
                                      .Include(pts=> pts.PersonToSocials)
                                      .ThenInclude(ps => ps.PersonSocial).ToList());
        }

        public IActionResult Create()
        {
            ViewBag.Position = _context.PersonPositions.ToList();
            ViewBag.PersonSocial = _context.PersonSocials.ToList();

            return View();
        }
        [HttpPost]
        public IActionResult Create(Team team)
        {

            if (ModelState.IsValid)
            {
                if (team.ImageFile != null)
                {
                    if (team.ImageFile.ContentType == "image/png" || team.ImageFile.ContentType == "image/jpeg")
                    {
                        if (team.ImageFile.Length <= 2097152)
                        {
                            string fileName = Guid.NewGuid() + "-" + DateTime.Now.ToString("yyyyMMddHHmmSS") + "-" + team.ImageFile.FileName;
                            string pathName = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", fileName);

                            using (var stream = new FileStream(pathName, FileMode.Create))
                            {
                                team.ImageFile.CopyTo(stream);

                            }
                            team.Image = fileName;

                            _context.Teams.Add(team);
                            _context.SaveChanges();

                            //Create Social to Person
                            if (team.PersonToSocialsId != null && team.PersonToSocialsId.Count > 0)
                            {
                                foreach (var item in team.PersonToSocialsId)
                                {
                                    PersonToSocial personToSocial = new PersonToSocial();
                                    personToSocial.PersonSocialId = item;
                                    personToSocial.TeamId = team.Id;
                                    _context.PersonToSocials.Add(personToSocial);
                                    _context.SaveChanges();
                                }
                            }

                            return RedirectToAction("Index");

                        }
                        else
                        {
                            ModelState.AddModelError("", "You can upload only less than 2mb");
                            ViewBag.Position = _context.PersonPositions.ToList();
                            ViewBag.PersonSocial = _context.PersonSocials.ToList();

                            return View(team);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "You can upload only .jpeg , .jpg and .png");
                        ViewBag.Position = _context.PersonPositions.ToList();
                        ViewBag.PersonSocial = _context.PersonSocials.ToList();

                        return View(team);
                    }
                }
                else
                {
                    ViewBag.Position = _context.PersonPositions.ToList();
                    ViewBag.PersonSocial = _context.PersonSocials.ToList();

                    return View(team);

                }
            }
            ModelState.AddModelError("", "Please all data enter");
            ViewBag.Position = _context.PersonPositions.ToList();
            ViewBag.PersonSocial = _context.PersonSocials.ToList();

            return View(team);
        }

        public IActionResult Update(int? id)
        {
            Team team = _context.Teams.Include(pts => pts.PersonToSocials).ThenInclude(t => t.PersonSocial).FirstOrDefault(b => b.Id == id);
            team.PersonToSocialsId = _context.PersonToSocials.Where(t => t.TeamId == id).Select(a => a.PersonSocialId).ToList();
            ViewBag.Position = _context.PersonPositions.ToList();
            ViewBag.PersonSocial = _context.PersonSocials.ToList();
            return View(team);
        }

        [HttpPost]
        public IActionResult Update(Team team)
        {
            if (ModelState.IsValid)
            {
                if (team.ImageFile != null)
                {
                    if (team.ImageFile.ContentType == "image/jpeg" || team.ImageFile.ContentType == "image/png")
                    {
                        if (team.ImageFile.Length <= 2097152)
                        {
                            //Delete old image
                            if (!string.IsNullOrEmpty(team.Image))
                            {
                                string oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", team.Image);
                                if (System.IO.File.Exists(oldImagePath))
                                {
                                    System.IO.File.Delete(oldImagePath);
                                }
                            }

                            //Create new image
                            string fileName = Guid.NewGuid() + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + team.ImageFile.FileName;
                            string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", fileName);
                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                team.ImageFile.CopyTo(stream);
                            }

                            team.Image= fileName;
                        }
                        else
                        {
                            ModelState.AddModelError("", "You can upload only less than 2 mb.");
                            ViewBag.Position = _context.PersonPositions.ToList();
                            ViewBag.PersonSocial = _context.PersonSocials.ToList();
                            return View(team);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "You can upload only .jpeg, .jpg and .png");
                        ViewBag.Position = _context.PersonPositions.ToList();
                        ViewBag.PersonSocial = _context.PersonSocials.ToList();
                        return View(team);
                    }
                }


                _context.Teams.Update(team);
                _context.SaveChanges();

                //Delete old data
                List<PersonToSocial> personToSocials = _context.PersonToSocials.Where(t => t.TeamId == team.Id).ToList();
                foreach (var item in personToSocials)
                {
                    _context.PersonToSocials.Remove(item);
                }
                _context.SaveChanges();

                //Create new Tag to blog
                if (team.PersonToSocialsId != null && team.PersonToSocialsId.Count > 0)
                {
                    foreach (var item in team.PersonToSocialsId)
                    {
                        PersonToSocial personToSocial = new PersonToSocial();
                        personToSocial.PersonSocialId = item;
                        personToSocial.TeamId = team.Id;
                        _context.PersonToSocials.Add(personToSocial);
                    }

                    _context.SaveChanges();
                }
                return RedirectToAction("Index");

            }

            ViewBag.Position = _context.PersonPositions.ToList();
            ViewBag.PersonSocial = _context.PersonSocials.ToList();
            return View(team);
        }


        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                HttpContext.Session.SetString("NullIdError", "Id can not be null");
                return RedirectToAction("Index");
            }

            Team team = _context.Teams.Find(id);
            if (team == null)
            {
                HttpContext.Session.SetString("NullDataError", "Can not found the data");
                return RedirectToAction("Index");
            }

            if (id != null)
            {
                string pathFIle = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", team.Image);
                if (!string.IsNullOrEmpty(team.Image))
                {
                    if (System.IO.File.Exists(pathFIle))
                    {
                        System.IO.File.Delete(pathFIle);
                    }
                }
            }


            if (team != null)
            {
                _context.Teams.Remove(team);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(team);
        }
    }
}
