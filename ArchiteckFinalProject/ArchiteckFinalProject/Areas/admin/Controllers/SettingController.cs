using ArchiteckFinalProject.Data;
using ArchiteckFinalProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ArchiteckFinalProject.Areas.admin.Controllers
{
    [Area("admin")]
    //[Authorize]

    public class SettingController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SettingController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            int i = 0;
            foreach (var item in _context.Settings.ToList())
            {
                i++;
            }
            ViewBag.Count = i;

            Setting setting = _context.Settings.FirstOrDefault();

            return View(setting);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Setting setting)
        {
            if (ModelState.IsValid)
            {
                if (setting.LogoFile != null)
                {
                    if (setting.LogoFile.ContentType == "image/png" || setting.LogoFile.ContentType == "image/jpeg")
                    {
                        if (setting.LogoFile.Length < 2097152)
                        {
                            string filename = Guid.NewGuid() + "-" + DateTime.Now.ToString("yyyyMMddHHmmSS") + "-" + setting.LogoFile.FileName;
                            string path = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", filename);
                            using (var stream = new FileStream(path, FileMode.Create))
                            {
                                setting.LogoFile.CopyTo(stream);
                            }
                            setting.Logo = filename;

                            _context.Settings.Add(setting);
                            _context.SaveChanges();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Image must be then less 2mb");
                            return RedirectToAction("Index");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Image must be png or jpeg format");
                        return RedirectToAction("Index");
                    }
                }

                           }
            return View(setting);
        }
        public IActionResult Update(int? id)
        {
            Setting setting = _context.Settings.FirstOrDefault(i => i.Id == id);
            return View(setting);
        }
        [HttpPost]
        public IActionResult Update(Setting setting)
        {
            if (ModelState.IsValid)
            {
                if (setting.LogoFile != null && setting.VideoFile  != null)
                {
                    if (setting.VideoFile.ContentType == "image/png" || setting.LogoFile.ContentType == "image/jpeg" 
                        && setting.VideoFile.ContentType == "video/mp4")
                    {

                        if (setting.LogoFile.Length < 2097152)
                        {
                            if (setting.VideoFile.Length < 20000000)
                            {

                                if (!string.IsNullOrEmpty(setting.Logo))
                                {
                                    string pathFileImage = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", setting.Logo);
                                    if (System.IO.File.Exists(pathFileImage))
                                    {
                                        System.IO.File.Delete(pathFileImage);
                                    }
                                }

                                string fileName = Guid.NewGuid() + "-" + DateTime.Now.ToString("yyyyMMddHHmmSS") + "-" + setting.LogoFile.FileName;
                                string pathName = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", fileName);

                                using (var stream = new FileStream(pathName, FileMode.Create))
                                {
                                    setting.LogoFile.CopyTo(stream);

                                }

                                if (!string.IsNullOrEmpty(setting.Video))
                                {
                                    string pathFileVideo = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", setting.Video);
                                    if (System.IO.File.Exists(pathFileVideo))
                                    {
                                        System.IO.File.Delete(pathFileVideo);
                                    }
                                }

                                string videoFileName = Guid.NewGuid() + "-" + DateTime.Now.ToString("yyyyMMddHHmmSS") + "-" + setting.LogoFile.FileName;
                                string pathvideoName = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", videoFileName);

                                using (var stream = new FileStream(pathvideoName, FileMode.Create))
                                {
                                    setting.VideoFile.CopyTo(stream);

                                }

                                setting.Logo = fileName;
                                setting.Video = videoFileName;
                                _context.Settings.Update(setting);
                                _context.SaveChanges();
                                return RedirectToAction("Index");
                            }
                        }
                    }
                }
                else
                {
                   
                    
                    _context.Settings.Update(setting);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(setting);
        }
      
        public IActionResult Delete(int? id)
        {
            Setting setting = null;

            if (id != null)
            {
                setting = _context.Settings.Find(id);
            }
            if (setting != null)
            {
                if (!string.IsNullOrEmpty(setting.Logo))
                {
                    string oldPathFile = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", setting.Logo);

                    if (System.IO.File.Exists(oldPathFile))
                    {
                        System.IO.File.Delete(oldPathFile);
                    }
                    _context.Settings.Remove(setting);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            if (setting != null)
            {
                if (!string.IsNullOrEmpty(setting.Video))
                {
                    string oldPathFile = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", setting.Video);

                    if (System.IO.File.Exists(oldPathFile))
                    {
                        System.IO.File.Delete(oldPathFile);
                    }
                    _context.Settings.Remove(setting);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            ModelState.AddModelError("", "setting is not found");
            return RedirectToAction("Index");
        }
    }
}
