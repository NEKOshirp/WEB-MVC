using Microsoft.AspNetCore.Mvc;
using TrainingFPTCo.Models;
using TrainingFPTCo.Models.Queries;

namespace TrainingFPTCo.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            LoginViewModel model = new LoginViewModel();
            return View(model);
        } 

        [HttpPost]
        public IActionResult Index(LoginViewModel model)
        {
            model = new LoginQuery().CheckUserLogin(model.UserName, model.Password);
            if (string.IsNullOrEmpty(model.Id) || string.IsNullOrEmpty(model.Email))
            {
                // nguoi dung dang nhap linh tinh
                // co mot thong bao loi ra view
                ViewData["MessageLogin"] = "Account invalid";
                return View(model);
            }

            // luu thong tin nguoi dung vao session
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("SessionUserId")))
            {
                HttpContext.Session.SetString("SessionUserId", model.Id);
                HttpContext.Session.SetString("SessionUsername", model.UserName);
                HttpContext.Session.SetString("SessionRolesId", model.RolesId);
                HttpContext.Session.SetString("SessionEmail", model.Email);
            }

            if (model.RolesId == "1")
            {
                // RolesId la 1, chuyen huong den trang Dashboard
                return RedirectToAction(nameof(DashboardController.Index), "Dashboard");
            }
            else if (model.RolesId == "2")
            {
                // RolesId la 2, chuyen huong den trang TrainingStaff
                return RedirectToAction(nameof(TrainingStaffController.Index), "TrainingStaff");
            }
            else if (model.RolesId == "3")
            {
                return RedirectToAction(nameof(TrainerController.Index), "Trainer");
            }
            else if (model.RolesId == "4")
            {
                return RedirectToAction(nameof(TraineeController.Index), "Trainee");
            }
            else
            {
                // RolesId khac 1 va 2, xu ly theo logic khac (neu co)
                // ...

                // Truong hop khac, tra ve mot View hoac Redirect toi trang khac
                return View(model); // Hoac return RedirectToAction(...) tuy thuoc vao logic cua ban
            }
        }

        [HttpPost]
        public IActionResult Logout()
        {
            // xoa session da tao ra o login
            // quay ve trang dang nhap
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SessionUserId")))
            {
                // xoa session
                HttpContext.Session.Remove("SessionUserId");
                HttpContext.Session.Remove("SessionUsername");
                HttpContext.Session.Remove("SessionRolesId");
                HttpContext.Session.Remove("SessionEmail");
            }
            // quay ve trang dang nhap
            return RedirectToAction(nameof(LoginController.Index), "Login");
        }
    }
}
