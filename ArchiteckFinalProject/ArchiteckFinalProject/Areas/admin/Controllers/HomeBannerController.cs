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

    public class HomeBannerController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeBannerController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        [AllowAnonymous]

        public IActionResult Index()
        {
            return View(_context.HomeBanners.ToList());
        }
        [AllowAnonymous]

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]

        public IActionResult Create(HomeBanner homeBanner)
        {

            if (ModelState.IsValid)
            {
                if (homeBanner.BgImageFile != null)
                {
                    if (homeBanner.BgImageFile.ContentType == "image/png" || homeBanner.BgImageFile.ContentType == "image/jpeg")
                    {
                        if (homeBanner.BgImageFile.Length <= 2097152)
                        {
                            string fileName = Guid.NewGuid() + "-" + DateTime.Now.ToString("yyyyMMddHHmmSS") + "-" + homeBanner.BgImageFile.FileName;
                            string pathName = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", fileName);

                            using (var stream = new FileStream(pathName, FileMode.Create))
                            {
                                homeBanner.BgImageFile.CopyTo(stream);

                            }
                            homeBanner.BgImage = fileName;

                            _context.HomeBanners.Add(homeBanner);
                            _context.SaveChanges();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError("", "You can upload only less than 2mb");
                            return View(homeBanner);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "You can upload only .jpeg , .jpg and .png");
                        return View(homeBanner);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Please all data enter");

                    return View(homeBanner);

                }
            }
            ModelState.AddModelError("", "Please all data enter");

            return View(homeBanner);
        }
        [AllowAnonymous]

        public IActionResult Update(int? id)
        {
            HomeBanner homeBanner = _context.HomeBanners.Find(id);
            return View(homeBanner);
        }

        [HttpPost]
        [AllowAnonymous]

        public IActionResult Update(HomeBanner homeBanner)
        {
            if (ModelState.IsValid)
            {
                if (homeBanner.BgImageFile != null)
                {
                    if (homeBanner.BgImageFile.ContentType == "image/png" || homeBanner.BgImageFile.ContentType == "image/jpeg")
                    {

                        if (homeBanner.BgImageFile.Length < 2097152)
                        {
                            if (!string.IsNullOrEmpty(homeBanner.BgImage))
                            {
                                string pathFileImage = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", homeBanner.BgImage);
                                if (System.IO.File.Exists(pathFileImage))
                                {
                                    System.IO.File.Delete(pathFileImage);
                                }
                            }

                            string fileName = Guid.NewGuid() + "-" + DateTime.Now.ToString("yyyyMMddHHmmSS") + "-" + homeBanner.BgImageFile.FileName;
                            string pathName = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", fileName);

                            using (var stream = new FileStream(pathName, FileMode.Create))
                            {
                                homeBanner.BgImageFile.CopyTo(stream);

                            }
                            homeBanner.BgImage = fileName;
                            _context.HomeBanners.Update(homeBanner);
                            _context.SaveChanges();
                            return RedirectToAction("Index");

                        }
                    }
                }
                else
                {
                    _context.HomeBanners.Update(homeBanner);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(homeBanner);
        }
        [AllowAnonymous]

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                HttpContext.Session.SetString("NullIdError", "Id can not be null");
                return RedirectToAction("Index");
            }

            HomeBanner homeBanner = _context.HomeBanners.Find(id);
            if (homeBanner == null)
            {
                HttpContext.Session.SetString("NullDataError", "Can not found the data");
                return RedirectToAction("Index");
            }

            if (id != null)
            {
                string pathFIle = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", homeBanner.BgImage);
                if (!string.IsNullOrEmpty(homeBanner.BgImage))
                {
                    if (System.IO.File.Exists(pathFIle))
                    {
                        System.IO.File.Delete(pathFIle);
                    }
                }
            }


            if (homeBanner != null)
            {
                _context.HomeBanners.Remove(homeBanner);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(homeBanner);
        }
    }
}
