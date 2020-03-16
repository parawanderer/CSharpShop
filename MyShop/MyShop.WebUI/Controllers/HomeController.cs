using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class HomeController : Controller
    {

        IRepository<Product> Context;
        IRepository<ProductCategory> ProductCategories;

        public HomeController(IRepository<Product> productContext, IRepository<ProductCategory> productCategoryContext)
        {
            Context = productContext;
            ProductCategories = productCategoryContext;
        }

        

       /* public ActionResult Index()
        {
            List<Product> products = Context.Collection().ToList();
            return View(products);
        }*/

        public ActionResult Index(string category=null)
        {
            List<Product> products;
            List<ProductCategory> productCategories = ProductCategories.Collection().ToList();
            if (category == null)
            {
                products = Context.Collection().ToList();
            }
            else
            {
                products = Context.Collection().Where(p => p.Category == category).ToList();
            }

            ProductListViewModel model = new ProductListViewModel();
            model.Products = products;
            model.ProductCategories = productCategories;
            return View(model);
        }

        public ActionResult Details(string id)
        {
            Product product = Context.Find(id);
            if (product == null) return HttpNotFound();

            return View(product);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}