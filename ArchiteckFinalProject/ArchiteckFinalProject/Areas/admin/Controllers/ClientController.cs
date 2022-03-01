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
    [Authorize(Roles = "Admin")]
    public class ClientController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ClientController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        //[AllowAnonymous]

        public IActionResult Index()
        {
            return View(_context.Clients.Include(ci => ci.ClientImages).ToList());
        }
        [AllowAnonymous]

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]

        public IActionResult Create(Client client)
        {
            _context.Clients.Add(client);
           _context.SaveChanges();


            foreach (var image in client.ClientImageFile)
            {
                if (image.ContentType == "image/jpeg" || image.ContentType == "image/png")
                {
                    if (image.Length <= 2097152)
                    {

                        //Create Blog
                        string fileName = Guid.NewGuid() + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + image.FileName;
                        string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", fileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            image.CopyTo(stream);
                        }

                        ClientImage clientImage = new ClientImage();
                        clientImage.Image = fileName;
                        clientImage.ClientId = client.Id;

                        _context.ClientImages.Add(clientImage);
                        _context.SaveChanges();




                    }

                    else
                    {
                        ModelState.AddModelError("", "You can upload only less than 2 mb.");
                        return View(client);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "You can upload only .jpeg, .jpg and .png");
                    return View(client);
                }
            }


            return View(client);


        }
        [AllowAnonymous]

        public IActionResult Update(int? id)
        {
            Client client = _context.Clients.Include(ci =>ci.ClientImages).FirstOrDefault(i => i.Id == id);


            return View(client);
        }
        [HttpPost]
        [AllowAnonymous]

        public IActionResult Update(Client client)
        {
            if (ModelState.IsValid)
            {

                if (client.ClientImageFile != null)
                {
                    List<ClientImage> clientImages = _context.ClientImages.Where(ci => ci.ClientId == client.Id).ToList();
                    foreach (var Img in clientImages)
                    {
                        string oldPathName = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", Img.Image);

                        if (!string.IsNullOrEmpty(oldPathName))
                        {
                            if (System.IO.File.Exists(oldPathName))
                            {
                                System.IO.File.Delete(oldPathName);
                            }
                        }
                        _context.ClientImages.Remove(Img);
                    }
                    _context.SaveChanges();

                    foreach (var item in client.ClientImageFile)
                    {

                        if (item.ContentType == "image/png" || item.ContentType == "image/jpeg")
                        {
                            if (item.Length <= 2097152)
                            {
                                string fileName = Guid.NewGuid() + "-" + DateTime.Now.ToString("yyyyMMddHHmmSS") + "-" + item.FileName;
                                string path = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", fileName);

                                using (var stream = new FileStream(path, FileMode.Create))
                                {
                                    item.CopyTo(stream);
                                }

                                ClientImage clientImages1 = new ClientImage();
                                clientImages1.Image = fileName;
                                clientImages1.ClientId = client.Id;

                                _context.ClientImages.Add(clientImages1);
                                _context.SaveChanges();

                            }
                        }
                    }

                   

                }
             
                _context.Clients.Update(client);
                _context.SaveChanges();

                return RedirectToAction("Index");

            }


            return View(client);
        }
        [AllowAnonymous]

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                HttpContext.Session.SetString("NullIdError", "Id can not be null");
                return RedirectToAction("Index");
            }

            Client client = _context.Clients.Find(id);
            if (client == null)
            {
                HttpContext.Session.SetString("NullDataError", "Can not found the data");
                return RedirectToAction("Index");
            }

            //Delete old image
            List<ClientImage> clientImages = _context.ClientImages.Where(ci => ci.ClientId == id).ToList();
            foreach (var item in clientImages)
            {
                if (!string.IsNullOrEmpty(item.Image))
                {
                    string oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", item.Image);
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }
            }

            _context.Clients.Remove(client);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}












