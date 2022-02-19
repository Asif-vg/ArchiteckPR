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
    //[Authorize]

    public class ServiceController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ServiceController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Service> model =_context.Services.OrderByDescending(o => o.CreatedDate)

                                                  .Include(sc => sc.ServiceCatagory)
                                                  .ToList();
            return View(model);
        }

        public IActionResult Create()
        {
            ViewBag.Category = _context.ServiceCatagories.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Service model)
        {
            if (ModelState.IsValid)
            {
                if (model.ServiceImageFile.ContentType == "image/jpeg" || model.ServiceImageFile.ContentType == "image/png")
                {
                    if (model.ServiceImageFile.Length <= 2097152)
                    {
                        string fileName = Guid.NewGuid() + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + model.ServiceImageFile.FileName;
                        string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            model.ServiceImageFile.CopyTo(stream);
                        }

                        model.ServiceImage = fileName;
                        //Title = model.Title;
                        model.CreatedDate = DateTime.Now;

                        _context.Services.Add(model);
                        _context.SaveChanges();

                        return RedirectToAction("Index");

                    }
                    else
                    {
                        ModelState.AddModelError("", "You can upload only less than 2 mb");
                        ViewBag.Category = _context.ServiceCatagories.ToList();

                        return View(model);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "You can upload only .jpeg, .jpg and .png");
                    ViewBag.Category = _context.ServiceCatagories.ToList();

                    return View(model);
                }

            }


            return View(model);
        }


        public IActionResult Update(int? id)
        {
            Service service = _context.Services.Find(id);
            ViewBag.Category = _context.ServiceCatagories.ToList();
            return View(service);

        }

        [HttpPost]
        public IActionResult Update(Service service)
        {
            if (ModelState.IsValid)
            {
                if (service.ServiceImageFile != null)
                {
                    if (service.ServiceImageFile.ContentType == "image/jpeg" || service.ServiceImageFile.ContentType == "image/png")
                    {
                        if (service.ServiceImageFile.Length <= 2097152)
                        {
                            //Delete old image
                            if (!string.IsNullOrEmpty(service.ServiceImage))
                            {
                                string oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", service.ServiceImage);
                                if (System.IO.File.Exists(oldImagePath))
                                {
                                    System.IO.File.Delete(oldImagePath);
                                }
                            }

                            //Create new image
                            string fileName = Guid.NewGuid() + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + service.ServiceImageFile.FileName;
                            string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", fileName);
                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                service.ServiceImageFile.CopyTo(stream);
                            }

                            service.ServiceImage = fileName;
                        }
                        else
                        {
                            ModelState.AddModelError("", "You can upload only less than 2 mb.");
                            ViewBag.Category = _context.ServiceCatagories.ToList();
                            return View(service);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "You can upload only .jpeg, .jpg and .png");
                        ViewBag.Category = _context.ServiceCatagories.ToList();
                        return View(service);
                    }
                }


                _context.Services.Update(service);
                _context.SaveChanges();

               
                return RedirectToAction("Index");

            }

            ViewBag.Category = _context.ServiceCatagories.ToList();
            return View(service);
        }


        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                HttpContext.Session.SetString("NullIdError", "Id can not be null");
                return RedirectToAction("Index");
            }

            Service service = _context.Services.Find(id);
            if (service == null)
            {
                HttpContext.Session.SetString("NullDataError",
                                              "Can not found the data");
                return RedirectToAction("Index");
            }


            if (id != null)
            {
                string pathFIle = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", service.ServiceImage);
                if (!string.IsNullOrEmpty(service.ServiceImage))
                {
                    if (System.IO.File.Exists(pathFIle))
                    {
                        System.IO.File.Delete(pathFIle);
                    }
                }
            }


            if (service != null)
            {
                _context.Services.Remove(service);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(service);
        }




    }
}
