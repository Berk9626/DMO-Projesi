using Client.Models.View;
using Client.ServiceDMO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Client.Controllers
{
    
    public class LoginController : Controller
    {
        // GET: Login
        Client.ServiceDMO.ServiceDMOClient s = new ServiceDMOClient();
        public ActionResult Login()
        {
            LoginModel model = new LoginModel();
            //Session["supplierId"] = 1;
            //Session["role"] = "TED";

            return View();
        }
        [HttpPost]

        public ActionResult Login(LoginModel m)
        {

            UserDTO userDTO = s.Login(m.UserID, m.Password);
            if (userDTO != null)
            {
                Session["UserId"] = userDTO.UserID;
                Session["SupplierId"] = userDTO.SupplierID;
                Session["Role"] = userDTO.Role;
                Session["Error"] = "Login Başarılı";

            }
            else Session["Error"] = "Login Başarısız";

            return View();
        }
        
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index","Home");
        }
    }
}