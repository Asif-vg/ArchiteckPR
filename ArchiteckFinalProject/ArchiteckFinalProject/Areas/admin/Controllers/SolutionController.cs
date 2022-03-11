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

    public class SolutionController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SolutionController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
       

        public IActionResult Index()
        {
            return View(_context.Solutions.ToList());
        }
      

        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Create(Solution solution)
        {

            if (ModelState.IsValid)
            {
                if (solution.ImageFile != null)
                {
                    if (solution.ImageFile.ContentType == "image/png" || solution.ImageFile.ContentType == "image/jpeg")
                    {
                        if (solution.ImageFile.Length <= 2097152)
                        {
                            string fileName = Guid.NewGuid() + "-" + DateTime.Now.ToString("yyyyMMddHHmmSS") + "-" + solution.ImageFile.FileName;
                            string pathName = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", fileName);

                            using (var stream = new FileStream(pathName, FileMode.Create))
                            {
                                solution.ImageFile.CopyTo(stream);

                            }
                            solution.Image = fileName;

                            _context.Solutions.Add(solution);
                            _context.SaveChanges();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError("", "You can upload only less than 2mb");
                            return View(solution);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "You can upload only .jpeg , .jpg and .png");
                        return View(solution);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Please all data enter");

                    return View(solution);

                }
            }
            ModelState.AddModelError("", "Please all data enter");
            return View(solution);
        }
        

        public IActionResult Update(int? id)
        {
            Solution solution = _context.Solutions.Find(id);
            return View(solution);
        }
      

        [HttpPost]
        public IActionResult Update(Solution solution)
        {
            if (ModelState.IsValid)
            {
                if (solution.ImageFile != null)
                {
                    if (solution.ImageFile.ContentType == "image/png" || solution.ImageFile.ContentType == "image/jpeg")
                    {

                        if (solution.ImageFile.Length < 2097152)
                        {
                            if (!string.IsNullOrEmpty(solution.Image))
                            {
                                string pathFileImage = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", solution.Image);
                                if (System.IO.File.Exists(pathFileImage))
                                {
                                    System.IO.File.Delete(pathFileImage);
                                }
                            }

                            string fileName = Guid.NewGuid() + "-" + DateTime.Now.ToString("yyyyMMddHHmmSS") + "-" + solution.ImageFile.FileName;
                            string pathName = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", fileName);

                            using (var stream = new FileStream(pathName, FileMode.Create))
                            {
                                solution.ImageFile.CopyTo(stream);

                            }
                            solution.Image = fileName;
                            _context.Solutions.Update(solution);
                            _context.SaveChanges();
                            return RedirectToAction("Index");

                        }
                    }
                }
                else
                {
                    _context.Solutions.Update(solution);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(solution);
        }
        

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                HttpContext.Session.SetString("NullIdError", "Id can not be null");
                return RedirectToAction("Index");
            }

            Solution solution = _context.Solutions.Find(id);
            if (solution == null)
            {
                HttpContext.Session.SetString("NullDataError", "Can not found the data");
                return RedirectToAction("Index");
            }

            if (id != null)
            {
                string pathFIle = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", solution.Image);
                if (!string.IsNullOrEmpty(solution.Image))
                {
                    if (System.IO.File.Exists(pathFIle))
                    {
                        System.IO.File.Delete(pathFIle);
                    }
                }
            }


            if (solution != null)
            {
                _context.Solutions.Remove(solution);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(solution);
        }
    }
}
