using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MaawinPages.Models;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Security;
using System.Web.Routing;

namespace MaawinPages.Controllers
{
    public class MaawinTradingPagesController : Controller
    {
        DAL data2DB = new DAL();
        static List<ProductModel> prd;
        // GET: MaawinTradingPages
        public ActionResult HomePage()
        {
            return View();
        }
        [HttpGet]
        public ActionResult SpicesHerbs()
        {
            List<ProductModel> prds = data2DB.GetData(Category.AgriProducts.ToString(), AgriProducts.SpicesHerbs.ToString());
            ViewBag.Data = prds.Count;  
            return View(prds);
        }
        
        public ActionResult ProductPartialView(string category,string type,string src)
        {
            List<ProductModel> prds = data2DB.GetData(Category.AgriProducts.ToString(), AgriProducts.SpicesHerbs.ToString(),src);
            ViewBag.Data = prds.Count;
            return View(prds);
        }

        [HttpGet]
        [Authorize]
        public ActionResult ProductAddPage()
        {
            return View();            
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult LoginView()
        {
            return View();
        }
        
        [HttpPost]
        [AllowAnonymous]
        public ActionResult LoginView(LoginModel m, string returnUrl)
        {
            if(m.username.Equals("admin") && m.password.Equals("admin"))
            {
                FormsAuthentication.SetAuthCookie(m.username,false);
                return Redirect(returnUrl);
            }
            else
            {
                ViewBag.LoginFailed = "Login Failed";
                return View();
            }
        }

        [HttpPost]
        public ActionResult ProductAddPage(ProductModel prdNew,HttpPostedFileBase image)
        {
            string ImageName = System.IO.Path.GetFileName(image.FileName); 
            string physicalPath = Server.MapPath("~/Resources/" + ImageName);
            image.SaveAs(physicalPath);
            prdNew.pImgLocation = "/Resources/" + ImageName;
            int succes = data2DB.Add2DB(prdNew);
            if(succes == 1)
            {
                ViewBag.Message = prdNew.pName + " Successfully uploaded";
            }
            else
            {
                ViewBag.Message = prdNew.pName + " Failed to upload";
            }
            return View();
        }
        [Authorize]
        public ActionResult ProductDeletePage()
        {
            ViewBag.Data = data2DB.GetData();
            return View();
        }

        public ActionResult DeleteProduct(string name)
        {
            int i = data2DB.DeleteData(name);
            if (i == 1)
                return View("ProductDeletePage");
            else
                return Json("Delete Failed");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("HomePage");
        }
        public ActionResult SearchPage(String srctext)
        {
            List<ProductModel> prds = data2DB.GetData(srctext);
            ViewBag.Data = prds.Count;
            return View(prds);
        }
    }
}