﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArchiteckFinalProject.Controllers
{
    public class ErrorPageController : Controller
    {
        public IActionResult Error1(int code)
        {
            //ViewBag.ErrorPage = "Admin";
            //ViewBag.ErrorPagewe = "View";

            return View();
        }
    }
}
