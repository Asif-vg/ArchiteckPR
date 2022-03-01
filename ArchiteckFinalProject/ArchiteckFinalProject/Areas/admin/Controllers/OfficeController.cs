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

    public class OfficeController : Controller
    {

        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public OfficeController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        [AllowAnonymous]

        public IActionResult Index()
        {
            return View(_context.Offices.ToList());
        }
        [AllowAnonymous]

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]

        public IActionResult Create(Office office)
        {

            if (ModelState.IsValid)
            {
                if (office.ImageFile != null)
                {
                    if (office.ImageFile.ContentType == "image/png" || office.ImageFile.ContentType == "image/jpeg")
                    {
                        if (office.ImageFile.Length <= 2097152)
                        {
                            string fileName = Guid.NewGuid() + "-" + DateTime.Now.ToString("yyyyMMddHHmmSS") + "-" + office.ImageFile.FileName;
                            string pathName = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", fileName);

                            using (var stream = new FileStream(pathName, FileMode.Create))
                            {
                                office.ImageFile.CopyTo(stream);

                            }
                            office.Image = fileName;

                            _context.Offices.Add(office);
                            _context.SaveChanges();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError("", "You can upload only less than 2mb");
                            return View(office);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "You can upload only .jpeg , .jpg and .png");
                        return View(office);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Please all data enter");

                    return View(office);

                }
            }
            ModelState.AddModelError("", "Please all data enter");

            return View(office);
        }

        [AllowAnonymous]

        public IActionResult Update(int? id)
        {
            Office office = _context.Offices.Find(id);
            return View(office);
        }

        [HttpPost]
        [AllowAnonymous]

        public IActionResult Update(Office office)
        {
            if (ModelState.IsValid)
            {
                if (office.ImageFile != null)
                {
                    if (office.ImageFile.ContentType == "image/png" || office.ImageFile.ContentType == "image/jpeg")
                    {

                        if (office.ImageFile.Length < 2097152)
                        {
                            if (!string.IsNullOrEmpty(office.Image))
                            {
                                string pathFileImage = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", office.Image);
                                if (System.IO.File.Exists(pathFileImage))
                                {
                                    System.IO.File.Delete(pathFileImage);
                                }
                            }

                            string fileName = Guid.NewGuid() + "-" + DateTime.Now.ToString("yyyyMMddHHmmSS") + "-" + office.ImageFile.FileName;
                            string pathName = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", fileName);

                            using (var stream = new FileStream(pathName, FileMode.Create))
                            {
                                office.ImageFile.CopyTo(stream);

                            }
                            office.Image = fileName;
                            _context.Offices.Update(office);
                            _context.SaveChanges();
                            return RedirectToAction("Index");

                        }
                    }
                }
                else
                {
                    _context.Offices.Update(office);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(office);
        }
        [AllowAnonymous]

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                HttpContext.Session.SetString("NullIdError", "Id can not be null");
                return RedirectToAction("Index");
            }

            Office office = _context.Offices.Find(id);
            if (office == null)
            {
                HttpContext.Session.SetString("NullDataError", "Can not found the data");
                return RedirectToAction("Index");
            }

            if (id!=null)
            {
                string pathFIle = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", office.Image);
                if (!string.IsNullOrEmpty(office.Image))
                {
                    if (System.IO.File.Exists(pathFIle))
                    {
                        System.IO.File.Delete(pathFIle);
                    }
                }
            }


            if (office != null)
            {
                _context.Offices.Remove(office);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(office);
        }
    }
}
