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

    public class FaqController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FaqController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        [AllowAnonymous]

        public IActionResult Index()
        {
            return View(_context.Faqs.ToList());
        }
        [AllowAnonymous]

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]

        public IActionResult Create(Faq faq)
        {

            if (ModelState.IsValid)
            {
                if (faq.ImageFile != null)
                {
                    if (faq.ImageFile.ContentType == "image/png" || faq.ImageFile.ContentType == "image/jpeg")
                    {
                        if (faq.ImageFile.Length <= 2097152)
                        {
                            string fileName = Guid.NewGuid() + "-" + DateTime.Now.ToString("yyyyMMddHHmmSS") + "-" + faq.ImageFile.FileName;
                            string pathName = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", fileName);

                            using (var stream = new FileStream(pathName, FileMode.Create))
                            {
                                faq.ImageFile.CopyTo(stream);

                            }
                            faq.Image = fileName;

                            _context.Faqs.Add(faq);
                            _context.SaveChanges();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError("", "You can upload only less than 2mb");
                            return View(faq);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "You can upload only .jpeg , .jpg and .png");
                        return View(faq);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Please all data enter");

                    return View(faq);

                }
            }
            ModelState.AddModelError("", "Please all data enter");

            return View(faq);
        }
        [AllowAnonymous]

        public IActionResult Update(int? id)
        {
            Faq faq = _context.Faqs.Find(id);
            return View(faq);
        }

        
        [HttpPost]
        [AllowAnonymous]

        public IActionResult Update(Faq faq)
        {
            if (ModelState.IsValid)
            {
                if (faq.ImageFile != null)
                {
                    if (faq.ImageFile.ContentType == "image/png" || faq.ImageFile.ContentType == "image/jpeg")
                    {

                        if (faq.ImageFile.Length < 2097152)
                        {
                            if (!string.IsNullOrEmpty(faq.Image))
                            {
                                string pathFileImage = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", faq.Image);
                                if (System.IO.File.Exists(pathFileImage))
                                {
                                    System.IO.File.Delete(pathFileImage);
                                }
                            }

                            string fileName = Guid.NewGuid() + "-" + DateTime.Now.ToString("yyyyMMddHHmmSS") + "-" + faq.ImageFile.FileName;
                            string pathName = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", fileName);

                            using (var stream = new FileStream(pathName, FileMode.Create))
                            {
                                faq.ImageFile.CopyTo(stream);

                            }
                            faq.Image = fileName;
                            _context.Faqs.Update(faq);
                            _context.SaveChanges();
                            return RedirectToAction("Index");

                        }
                    }
                }
                else
                {
                    _context.Faqs.Update(faq);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            return View(faq);
        }
        [AllowAnonymous]

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                HttpContext.Session.SetString("NullIdError", "Id can not be null");
                return RedirectToAction("Index");
            }

            Faq faq = _context.Faqs.Find(id);
            if (faq == null)
            {
                HttpContext.Session.SetString("NullDataError", "Can not found the data");
                return RedirectToAction("Index");
            }

            if (id != null)
            {
                string pathFIle = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", faq.Image);
                if (!string.IsNullOrEmpty(faq.Image))
                {
                    if (System.IO.File.Exists(pathFIle))
                    {
                        System.IO.File.Delete(pathFIle);
                    }
                }
            }


            if (faq != null)
            {
                _context.Faqs.Remove(faq);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(faq);
        }
    }
}
