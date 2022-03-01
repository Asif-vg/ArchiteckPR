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
//using MailKit.Net.Smtp;

namespace ArchiteckFinalProject.Areas.admin.Controllers
{
    [Area("admin")]
    //[Authorize]

    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        //private readonly UserManager<CustomUser> _UserManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        //UserManager<CustomUser> UserManager,
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
            ViewBag.Roles = _context.Roles.ToList();

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
       
        //public IActionResult RoleUpdate(string id)
        //{
        //    IdentityRole identityRole = _context.Roles.FirstOrDefault(b => b.Id == id);
        //    return View();
        //}
        //[HttpPost]
        //public async Task<IActionResult> RoleUpdate(IdentityRole model)
        //{
        //    await _roleManager.UpdateAsync(model);
        //    _context.SaveChanges();
        //    return View();
        //}

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

        //public IActionResult ForgotPassword()
        //{
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> ForgotPassword(string email)
        //{
        //    if (string.IsNullOrWhiteSpace(email))
        //        return RedirectToAction("Login", "Error");

        //    CustomUser customUser = await _UserManager.FindByEmailAsync(email);

        //    if (customUser == null)
        //        return RedirectToAction("Login", "Error");

        //    var message = new MimeMessage();
        //    message.From.Add(new MailboxAddress("Architeck", "quluyevasifcode@gmail.com"));
        //    message.To.Add(new MailboxAddress(customUser.UserName, customUser.Email));
        //    message.Subject = "Reset Password";

        //    string emailbody = string.Empty;

        //    using (StreamReader stream = new StreamReader(Path.Combine(_webHostEnvironment.WebRootPath, "Template", "ForgetPassword.html")))
        //    {
        //        emailbody = stream.ReadToEnd();
        //    }

        //    string forgetpasswordtoken = await _userManager.GeneratePasswordResetTokenAsync(customUser);

        //    string url = Url.Action("changepassword", "account", new { Id = customUser.Id, token = forgetpasswordtoken }, Request.Scheme);

        //    emailbody = emailbody.Replace("{{UserName}}", $"{customUser.UserName}").Replace("{{url}}", $"{url}");

        //    message.Body = new TextPart(TextFormat.Html) { Text = emailbody };

        //    using var smtp = new SmtpClient();
        //    smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
        //    smtp.Authenticate("quluyevasifcode@gmail.com", "Codeasif123.");
        //    smtp.Send(message);
        //    smtp.Disconnect(true);

        //    return View();
        //}


        public IActionResult ForgotPassword()
        {

            VmForgotPassword model = new();
            //model.Setting = _context.Setting.FirstOrDefault();
            //model.SiteSocial = _context.SiteSocials.ToList();




            return View(model);
        }

        [HttpPost]
        public IActionResult ForgotPassword(VmForgotPassword model)
        {


            if (ModelState.IsValid)
            {
                if (_context.CustomUsers.Any(u => u.Email == model.Email))
                {


                    var user = _context.CustomUsers.FirstOrDefault(u => u.Email == model.Email);

                    if (user != null)
                    {

                        string resetCode = Guid.NewGuid().ToString();
                        var verifyUrl = "Account/ChangePassword/" + resetCode;

                        Uri baseUri = new Uri("https://localhost:44329/");

                        var link = baseUri + HttpContext.Request.Scheme.Replace(HttpContext.Request.Scheme, verifyUrl);


                        user.ResetPasswordCode = resetCode;

                        _context.SaveChanges();

                        var subject = "Password Reset Request";
                        var body = "Hi " + user.FullName + ", <br/> You recently requested to reset your password for your account. Click the link below to reset it. " +
                         " <br/><br/><a href='" + link + "'>" + link + "</a> <br/><br/>" +
                         "If you did not request a password reset, please ignore this email or reply to let us know.<br/><br/> Thank you";

                        SendEmail(user.Email, body, subject);

                        ViewBag.Message = "Reset password link has been sent to your email id.";

                    }

                    return RedirectToAction("Login");
                }
                else
                {
                    ModelState.AddModelError("", "Such an Email doesnt exist!");
                    return View(model);
                }
            }

            return View(model);
        }

        private void SendEmail(string emailAddress, string body, string subject)
        {

            MailMessage RecoveryMessage = new MailMessage("quluyevasifcode@gmail.com", emailAddress);
            RecoveryMessage.Subject = subject;
            RecoveryMessage.Body = body;

            RecoveryMessage.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            NetworkCredential NetworkCred = new NetworkCredential("quluyevasifcode@gmail.com", "Codeasif123.");
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = NetworkCred;
            smtp.Port = 587;
            smtp.Send(RecoveryMessage);
        }

        //public async Task<IActionResult> ChangePassword(string Id, string token)
        //{
        //    if (string.IsNullOrWhiteSpace(Id) || string.IsNullOrWhiteSpace(token))
        //    {
        //        return NotFound();
        //    }

        //    CustomUser customUser = await _UserManager.FindByIdAsync(Id);

        //    if (customUser == null)
        //    {
        //        return NotFound();
        //    }

        //    VmChangePassword resetPasswordVM = new VmChangePassword
        //    {
        //        Id = Id,
        //        Token = token
        //    };

        //    return View(resetPasswordVM);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> ChangePassword(VmChangePassword vmChangePassword)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View();
        //    }

        //    if (string.IsNullOrWhiteSpace(vmChangePassword.Id) || string.IsNullOrWhiteSpace(vmChangePassword.Token))
        //    {
        //        return NotFound();
        //    }

        //    CustomUser customUser = await _userManager.FindByIdAsync(vmChangePassword.Id);

        //    if (customUser == null)
        //    {
        //        return NotFound();
        //    }

        //    IdentityResult identityResult = await _userManager.ResetPasswordAsync(customUser, vmChangePassword.Token, vmChangePassword.Password);

        //    if (!identityResult.Succeeded)
        //    {
        //        foreach (IdentityError error in identityResult.Errors)
        //        {
        //            ModelState.AddModelError("", error.Description);
        //        }
        //        return View(vmChangePassword);
        //    }

        //    return RedirectToAction("Login");
        //}
        public IActionResult ChangePassword(string id)
        {

            if (string.IsNullOrWhiteSpace(id))
            {
                return RedirectToAction("ForgotPassword");
            }

            var user = _context.CustomUsers.Where(a => a.ResetPasswordCode == id).FirstOrDefault();
            if (user != null)
            {
                VmChangePassword model = new VmChangePassword();
                model.ResetCode = id;
                return View(model);
            }
            else if (user == null)
            {
                return NotFound();

            }
            else
            {
                TempData["ResetError"] = "Something went wrong!";
                return RedirectToAction("ForgotPassword");
            }


        }

        [HttpPost]
        public async Task<ActionResult> ChangePassword(VmChangePassword model)
        {
            if (ModelState.IsValid)
            {
                CustomUser user = _context.CustomUsers.Where(a => a.ResetPasswordCode == model.ResetCode).FirstOrDefault();
                if (user != null)
                {
                    if (model.NewPassword.Length >= 7)
                    {
                        await _userManager.RemovePasswordAsync(user);
                        await _userManager.AddPasswordAsync(user, model.NewPassword);
                        user.ResetPasswordCode = "";
                        await _context.SaveChangesAsync();
                        TempData["ResetSuccess"] = "true";
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        TempData["pswsyntax"] = "Password length should be at least 7 characters";
                        return View(model);
                    }
                }
                else
                {
                    //VmForgotPassword model2 = new();
                    TempData["ResetError2"] = "Try it again!";
                    return RedirectToAction("ForgotPassword");
                }
            }
            else
            {
                return View(model);
            }
        }

    }
}
//    }
//}
