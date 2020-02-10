using Client.ServiceDMO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Client.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        ServiceDMO.ServiceDMOClient s = new ServiceDMO.ServiceDMOClient();
        public ActionResult List()
        {
            int supplierId = Convert.ToInt32(Session["SupplierId"]);
            string role = Session["Role"].ToString();
            ICollection<ProductsDTO> list = s.GetAllProducts(supplierId, role);
            return View(list);
        }


        public ActionResult Delete(int id)
        {
            string msg = s.DeleteProduct(id, Session["Role"].ToString(), Convert.ToInt32(Session["SupplierId"]));//güvenlik
            Session["msg"] = msg;
            return RedirectToAction("List");


        }
        [HttpGet]
        public ActionResult AddProduct()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddProduct(ProductsDTO p)
        {
            p.SupplierID = (int)Session["SupplierId"];
            s.AddProduct(Session["Role"].ToString(), p);

            
            return RedirectToAction("List");

        }
    }
}