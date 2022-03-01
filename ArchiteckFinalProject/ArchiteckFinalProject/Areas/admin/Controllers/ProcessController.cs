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
    [Authorize]

    public class ProcessController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProcessController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        [AllowAnonymous]

        public IActionResult Index()
        {
            return View(_context.Processes.ToList());
        }
        [AllowAnonymous]


        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]

        public IActionResult Create(Process process)
        {

            if (ModelState.IsValid)
            {
                if (process.ImageFile != null)
                {
                    if (process.ImageFile.ContentType == "image/png" || process.ImageFile.ContentType == "image/jpeg")
                    {
                        if (process.ImageFile.Length <= 2097152)
                        {
                            string fileName = Guid.NewGuid() + "-" + DateTime.Now.ToString("yyyyMMddHHmmSS") + "-" + process.ImageFile.FileName;
                            string pathName = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", fileName);

                            using (var stream = new FileStream(pathName, FileMode.Create))
                            {
                                process.ImageFile.CopyTo(stream);

                            }
                            process.Image = fileName;

                            _context.Processes.Add(process);
                            _context.SaveChanges();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError("", "You can upload only less than 2mb");
                            return View(process);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "You can upload only .jpeg , .jpg and .png");
                        return View(process);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Please all data enter");

                    return View(process);

                }
            }
            ModelState.AddModelError("", "Please all data enter");
            return View(process);
        }



        [AllowAnonymous]

        public IActionResult Update(int? id)
        {
            Process process = _context.Processes.Find(id);
            return View(process);
        }

        [HttpPost]
        [AllowAnonymous]

        public IActionResult Update(Process process)
        {
            if (ModelState.IsValid)
            {
                if (process.ImageFile != null)
                {
                    if (process.ImageFile.ContentType == "image/png" || process.ImageFile.ContentType == "image/jpeg")
                    {

                        if (process.ImageFile.Length < 2097152)
                        {
                            if (!string.IsNullOrEmpty(process.Image))
                            {
                                string pathFileImage = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", process.Image);
                                if (System.IO.File.Exists(pathFileImage))
                                {
                                    System.IO.File.Delete(pathFileImage);
                                }
                            }

                            string fileName = Guid.NewGuid() + "-" + DateTime.Now.ToString("yyyyMMddHHmmSS") + "-" + process.ImageFile.FileName;
                            string pathName = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", fileName);

                            using (var stream = new FileStream(pathName, FileMode.Create))
                            {
                                process.ImageFile.CopyTo(stream);

                            }
                            process.Image = fileName;
                            _context.Processes.Update(process);
                            _context.SaveChanges();
                            return RedirectToAction("Index");

                        }
                    }
                }
                else
                {
                    _context.Processes.Update(process);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            return View(process);
        }
        [AllowAnonymous]

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                HttpContext.Session.SetString("NullIdError", "Id can not be null");
                return RedirectToAction("Index");
            }

            Process process = _context.Processes.Find(id);
            if (process == null)
            {
                HttpContext.Session.SetString("NullDataError", "Can not found the data");
                return RedirectToAction("Index");
            }

            

            _context.Processes.Remove(process);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
