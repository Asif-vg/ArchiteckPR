using ArchiteckFinalProject.Data;
using ArchiteckFinalProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ArchiteckFinalProject.Areas.admin.Controllers
{
    [Area("admin")]
    [Authorize(Roles = "Admin")]

    public class AboutController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AboutController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        //[AllowAnonymous]

        public IActionResult Index()
        {
            return View(_context.Abouts.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]

        public IActionResult Create(About about)
        {

            if (ModelState.IsValid)
            {
                if (about.ImageFile != null)
                {
                    if (about.ImageFile.ContentType == "image/png" || about.ImageFile.ContentType == "image/jpeg")
                    {
                        if (about.ImageFile.Length <= 2097152)
                        {
                            string fileName = Guid.NewGuid() + "-" + DateTime.Now.ToString("yyyyMMddHHmmSS") + "-" + about.ImageFile.FileName;
                            string pathName = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", fileName);

                            using (var stream = new FileStream(pathName, FileMode.Create))
                            {
                                about.ImageFile.CopyTo(stream);

                            }
                            about.Image = fileName;

                            _context.Abouts.Add(about);
                            _context.SaveChanges();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError("", "You can upload only less than 2mb");
                            return View(about);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "You can upload only .jpeg , .jpg and .png");
                        return View(about);
                    }
                }


               
                else
                {
                    ModelState.AddModelError("", "Please all data enter");

                    return View(about);

                }
            }
            ModelState.AddModelError("", "Please all data enter");
            return View(about);
        }

        public IActionResult Update(int? id)
        {
            About about = _context.Abouts.Find(id);
            return View(about);
        }

        [HttpPost]

        public IActionResult Update(About about)
        {
            if (ModelState.IsValid)
            {
                if (about.ImageFile != null)
                {
                    if (about.ImageFile.ContentType == "image/png" || about.ImageFile.ContentType == "image/jpeg")
                    {

                        if (about.ImageFile.Length < 2097152)
                        {
                            if (!string.IsNullOrEmpty(about.Image))
                            {
                                string pathFileImage = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", about.Image);
                                if (System.IO.File.Exists(pathFileImage))
                                {
                                    System.IO.File.Delete(pathFileImage);
                                }
                            }

                            string fileName = Guid.NewGuid() + "-" + DateTime.Now.ToString("yyyyMMddHHmmSS") + "-" + about.ImageFile.FileName;
                            string pathName = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", fileName);

                            using (var stream = new FileStream(pathName, FileMode.Create))
                            {
                                about.ImageFile.CopyTo(stream);

                            }
                            about.Image = fileName;
                            _context.Abouts.Update(about);
                            _context.SaveChanges();
                            return RedirectToAction("Index");

                        }
                    }
                }

                else
                {
                    _context.Abouts.Update(about);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(about);
        }


        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                HttpContext.Session.SetString("NullIdError", "Id can not be null");
                return RedirectToAction("Index");
            }

            About about = _context.Abouts.Find(id);
            if (about == null)
            {
                HttpContext.Session.SetString("NullDataError", "Can not found the data");
                return RedirectToAction("Index");
            }

            if (id != null)
            {
                string pathFIle = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", about.Image);
                if (!string.IsNullOrEmpty(about.Image))
                {
                    if (System.IO.File.Exists(pathFIle))
                    {
                        System.IO.File.Delete(pathFIle);
                    }
                }
            }


            if (about != null)
            {
                _context.Abouts.Remove(about);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(about);

        }
    }
}
