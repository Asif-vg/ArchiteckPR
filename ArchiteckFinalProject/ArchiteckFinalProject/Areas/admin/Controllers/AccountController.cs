using ArchiteckFinalProject.Data;
using ArchiteckFinalProject.Models;
using ArchiteckFinalProject.ViewModels;
using MailKit.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AccountController(AppDbContext context, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            //_UserManager = UserManager;
            _context = context;
            _roleManager = roleManager;
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
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
                    FullName = vmRegister.Name + " " + vmRegister.Surname,
                    CreatedDate = DateTime.Now,
                    Email = vmRegister.Email,
                    UserName = vmRegister.Email,
                    Password = vmRegister.Password
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
        [Authorize(Roles = "Admin")]

        public IActionResult Profile()
        {
            CustomUser model = _context.CustomUsers.FirstOrDefault();

            return View(model);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Users()
        {
            VmUser model = new VmUser();
            model.CustomUsers = _context.CustomUsers.ToList();
            model.Roles = _context.Roles.ToList();
            model.UserRoles = _context.UserRoles.ToList();
            return View(model);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateUser(string id)
        {
            CustomUser user = _context.CustomUsers.Find(id);
            ViewBag.Roles = _context.Roles.ToList();
            return View(user);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]

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
            ViewBag.Roles = _context.Roles.ToList();

            return View(model);
        }

        [Authorize(Roles = "Admin")]

        public IActionResult Roles()
        {
            List<IdentityRole> roles = _context.Roles.ToList();
            return View(roles);
        }

        [Authorize(Roles = "Admin")]

        public IActionResult RoleCreate()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> RoleCreate(IdentityRole model)
        {
            await _roleManager.CreateAsync(model);
            return RedirectToAction("Roles");
        }


        [Authorize(Roles = "Admin")]

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
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return RedirectToAction("Error1", "ErrorPage");

            CustomUser customUser = _context.CustomUsers.FirstOrDefault(c => c.Email == email);

            if (customUser == null)
            {
                return RedirectToAction("Error1", "ErrorPage");
            }


            string emailbody = string.Empty;

            using (StreamReader stream = new StreamReader(Path.Combine(_webHostEnvironment.WebRootPath, "Forgot", "ForgetPassword.html")))
            {
                emailbody = stream.ReadToEnd();
            }

            string forgetpasswordtoken = await _userManager.GeneratePasswordResetTokenAsync(customUser);
            //string forgetpasswordtoken = Guid.NewGuid() + ""+DateTime.Now.ToString("dd-MMM-yyyy");


            string url = Url.Action("changepassword", "account", new { Id = customUser.Id, token = forgetpasswordtoken }, Request.Scheme);

            emailbody = emailbody.Replace("{{UserName}}", $"{customUser.UserName}").Replace("{{url}}", $"{url}");

            using (MailMessage message = new MailMessage())
            {
                message.From = new MailAddress("quluyevasifcode@gmail.com");
                message.To.Add(customUser.Email);
                message.Subject = "Reset Password";
                message.Body = emailbody;
                message.IsBodyHtml = true;

                using (SmtpClient smptp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smptp.Credentials = new NetworkCredential("quluyevasifcode@gmail.com", "Codeasif123.");
                    smptp.EnableSsl = true;
                    smptp.Send(message);
                }
                return RedirectToAction("Login");

            }



        }


        public async Task<IActionResult> ChangePassword(string Id, string token)
        {
            if (string.IsNullOrWhiteSpace(Id) || string.IsNullOrWhiteSpace(token))
            {
                return NotFound();
            }

            CustomUser customUser = _context.CustomUsers.FirstOrDefault(c => c.Id == Id);

            if (customUser == null)
            {
                return NotFound();
            }

            VmChangePassword resetPasswordVM = new VmChangePassword
            {
                Id = Id,
                Token = token
            };

            return View(resetPasswordVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(VmChangePassword vmChangePassword)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (string.IsNullOrWhiteSpace(vmChangePassword.Id) || string.IsNullOrWhiteSpace(vmChangePassword.Token))
            {
                return NotFound();
            }

            CustomUser customUser = _context.CustomUsers.FirstOrDefault(c => c.Id == vmChangePassword.Id);

            if (customUser == null)
            {
                return NotFound();
            }

            IdentityResult identityResult = await _userManager.ResetPasswordAsync(customUser, vmChangePassword.Token, vmChangePassword.Password);

            if (!identityResult.Succeeded)
            {
                foreach (IdentityError error in identityResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(vmChangePassword);
            }

            return RedirectToAction("Login");
        }





    }
}

