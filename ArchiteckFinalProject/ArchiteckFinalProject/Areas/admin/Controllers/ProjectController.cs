using ArchiteckFinalProject.Data;
using ArchiteckFinalProject.Models;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]

    public class ProjectController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProjectController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        [AllowAnonymous]

        public IActionResult Index()
        {
            return View(_context.Projects.OrderByDescending(o => o.CreatedDate).Include(pa => pa.ProjectArchiteck).ToList());
        }
        [AllowAnonymous]

        public IActionResult Create()
        {
            ViewBag.Architeck = _context.ProjectArchitecks.ToList();
            return View();
        }
        [HttpPost]
        [AllowAnonymous]

        public IActionResult Create(Project project)
        {

            if (ModelState.IsValid)
            {
                if (project.ImageFile != null)
                {
                    if (project.ImageFile.ContentType == "image/png" || project.ImageFile.ContentType == "image/jpeg")
                    {
                        if (project.ImageFile.Length <= 2097152)
                        {
                            string fileName = Guid.NewGuid() + "-" + DateTime.Now.ToString("yyyyMMddHHmmSS") + "-" + project.ImageFile.FileName;
                            string pathName = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", fileName);

                            using (var stream = new FileStream(pathName, FileMode.Create))
                            {
                                project.ImageFile.CopyTo(stream);

                            }
                            project.Image = fileName;
                            project.CreatedDate = DateTime.Now;


                            _context.Projects.Add(project);
                            _context.SaveChanges();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError("", "You can upload only less than 2mb");
                            ViewBag.Architeck = _context.ProjectArchitecks.ToList();

                            return View(project);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "You can upload only .jpeg , .jpg and .png");
                        ViewBag.Architeck = _context.ProjectArchitecks.ToList();

                        return View(project);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Please all data enter");
                    ViewBag.Architeck = _context.ProjectArchitecks.ToList();


                    return View(project);

                }
            }
            ModelState.AddModelError("", "Please all data enter");

            return View(project);
        }
        [AllowAnonymous]

        public IActionResult Update(int? id)
        {
            Project project = _context.Projects.Find(id);
            ViewBag.Architeck = _context.ProjectArchitecks.ToList();

            return View(project);

        }

        [HttpPost]
        [AllowAnonymous]

        public IActionResult Update(Project project)
        {
            if (ModelState.IsValid)
            {
                if (project.ImageFile != null)
                {
                    if (project.ImageFile.ContentType == "image/jpeg" || project.ImageFile.ContentType == "image/png")
                    {
                        if (project.ImageFile.Length <= 2097152)
                        {
                            //Delete old image
                            if (!string.IsNullOrEmpty(project.Image))
                            {
                                string oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", project.Image);
                                if (System.IO.File.Exists(oldImagePath))
                                {
                                    System.IO.File.Delete(oldImagePath);
                                }
                            }

                            //Create new image
                            string fileName = Guid.NewGuid() + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + project.ImageFile.FileName;
                            string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", fileName);
                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                project.ImageFile.CopyTo(stream);
                            }

                            project.Image = fileName;
                        }
                        else
                        {
                            ModelState.AddModelError("", "You can upload only less than 2 mb.");
                            ViewBag.Architeck = _context.ProjectArchitecks.ToList();

                            return View(project);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "You can upload only .jpeg, .jpg and .png");
                        ViewBag.Architeck = _context.ProjectArchitecks.ToList();

                        return View(project);
                    }
                }
                _context.Entry(project).State = EntityState.Modified;
                _context.Entry(project).Property("CreatedDate").IsModified = false;

                //_context.Projects.Update(project);
                _context.SaveChanges();


                return RedirectToAction("Index");

            }

            ViewBag.Architeck = _context.ProjectArchitecks.ToList();

            return View(project);
        }

        [AllowAnonymous]

        public IActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    HttpContext.Session.SetString("NullIdError", "Id can not be null");
                    return RedirectToAction("Index");
                }

                Project project = _context.Projects.Find(id);
                if (project == null)
                {
                    HttpContext.Session.SetString("NullDataError", "Can not found the data");
                    return RedirectToAction("Index");
                }

                if (id != null)
                {
                    string pathFIle = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", project.Image);
                    if (!string.IsNullOrEmpty(project.Image))
                    {
                        if (System.IO.File.Exists(pathFIle))
                        {
                            System.IO.File.Delete(pathFIle);
                        }
                    }
                }


                if (project != null)
                {
                    _context.Projects.Remove(project);
                    _context.SaveChanges();
                    //return RedirectToAction("Index");
                }
                return Json(new
                {
                    code = 204,
                    message = "Item has been deleted successfully!"
                });
            }


            catch (Exception)
            {
                return Json(new
                {
                    code = 500,
                    message = "Something went wrong!"
                });
            }
            //return View(project);
        }
    }
}
