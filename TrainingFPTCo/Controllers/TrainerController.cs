﻿using Microsoft.AspNetCore.Mvc;

namespace TrainingFPTCo.Controllers
{
    public class TrainerController : Controller
    {
        public IActionResult Index()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("SessionUserId")))
            {
                return RedirectToAction(nameof(LoginController.Index), "Login");
            }
            return View();
        }
    }
}
