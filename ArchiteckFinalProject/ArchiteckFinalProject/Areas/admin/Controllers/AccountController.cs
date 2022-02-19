using ArchiteckFinalProject.Data;
using ArchiteckFinalProject.Models;
using ArchiteckFinalProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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

    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AccountController(AppDbContext context, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager, IWebHostEnvironment webHostEnvironment)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _context = context;
            _roleManager = roleManager;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(VmRegister vmRegister)
        {
            if (ModelState.IsValid)
            {
                //if (vmRegister.ImageFile != null)
                //{
                //    string fileName = Guid.NewGuid() + "-" + DateTime.Now.ToString("yyyyMMddHHmmSS") + "-" + vmRegister.ImageFile.FileName;
                //    string path = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", fileName);
                //    using (var stream = new FileStream(path, FileMode.Create))
                //    {
                //        vmRegister.ImageFile.CopyTo(stream);
                //    }
                    CustomUser newUser = new CustomUser()
                    {
                        FullName = vmRegister.Name +" " + vmRegister.Surname,
                        //Text = vmRegister.Text,
                        CreatedDate = DateTime.Now,
                        Email = vmRegister.Email,
                        UserName = vmRegister.Email,
                        Password=vmRegister.Password
                        //Image = fileName
                    };

                    var result = await _userManager.CreateAsync(newUser, vmRegister.Password);

                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(newUser, false);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        foreach (var item in result.Errors)
                        {
                            ModelState.AddModelError("", item.Description);
                        }
                        return View(vmRegister);
                    }
                //}

            }
            return View(vmRegister);
        }
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]

        public async Task<IActionResult> Login(VmRegister vmRegister)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(vmRegister.Email) || !string.IsNullOrEmpty(vmRegister.Password))
                {
                    var result = await _signInManager.PasswordSignInAsync(vmRegister.Email, vmRegister.Password, false, false);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Username or password is not correct");
                        return View(vmRegister);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Please write the ones mentioned");
                    return View(vmRegister);
                }

            }

            return View(vmRegister);
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return View();
        }
        public IActionResult Profile()
        {
            CustomUser model = _context.CustomUsers.FirstOrDefault();

            return View(model);
        }
        public IActionResult Users()
        {
            VmUser model = new VmUser();
            model.CustomUsers = _context.CustomUsers.ToList();
            model.Roles = _context.Roles.ToList();
            model.UserRoles = _context.UserRoles.ToList();
            return View(model);
        }

        public IActionResult UpdateUser(string id)
        {
            CustomUser user = _context.CustomUsers.Find(id);
            ViewBag.Roles = _context.Roles.ToList();
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser(CustomUser model)
        {
            if (ModelState.IsValid)
            {
                CustomUser user = _context.CustomUsers.Find(model.Id);
                user.FullName = model.FullName;

                IdentityUserRole<string> userRole = _context.UserRoles.FirstOrDefault(u => u.UserId == model.Id);
               
                if (userRole != null)
                {
                    string oldRole = _context.Roles.Find(userRole.RoleId).Name;
                    await _userManager.RemoveFromRoleAsync(user, oldRole);
                }
             

                IdentityRole selectedRole = _context.Roles.Find(model.RoleId);

                await _userManager.AddToRoleAsync(user, selectedRole.Name);
                _context.SaveChanges();
                return RedirectToAction("Users");

            }
            return View(model);
        }


        public IActionResult Roles()
        {
            List<IdentityRole> roles = _context.Roles.ToList();
            return View(roles);
        }


        public IActionResult RoleCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RoleCreate(IdentityRole model)
        {
            await _roleManager.CreateAsync(model);
            return RedirectToAction("Roles");
        }
        public IActionResult RoleUpdate(string id)
        {
            IdentityRole identityRole = _context.Roles.FirstOrDefault(b => b.Id == id);
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RoleUpdate(IdentityRole model)
        {
            await _roleManager.UpdateAsync(model);
            _context.SaveChanges();
            return View();
        }

        public IActionResult RoleDelete(string? id)
        {
            if (id == null)
            {
                HttpContext.Session.SetString("NullIdError", "Id can not be null");
                return RedirectToAction("Roles");
            }

            IdentityRole identityRole = _context.Roles.Find(id);
            if (identityRole == null)
            {
                HttpContext.Session.SetString("NullDataError",
                                              "Can not found the data");
                return RedirectToAction("Roles");
            }
            IdentityRole identityRole1 = null;

            if (id != null)
            {
                identityRole1 = _context.Roles.Find(id);
                _context.Roles.Remove(identityRole1);
                _context.SaveChanges();
            }


            return RedirectToAction("Roles");
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ForgotPassword(CustomUser model)
        {
            try
            {
                var query = _context.CustomUsers.Where(m => m.Email == model.Email).SingleOrDefault();
                if (query != null)
                {
                    query.Email = model.Email;
                    query.PasswordHash = model.Password;
                    _context.SaveChanges();
                    return RedirectToAction("Login");
                }
                else
                {
                    TempData["msg"] = "Account not updated";
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex;
            }
           
            return View(model);
        }
    }
}
