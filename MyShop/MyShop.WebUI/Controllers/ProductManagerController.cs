using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Models;
using MyShop.DataAccess.InMemory;

namespace MyShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        ProductRepository Context;

        public ProductManagerController()
        {
            Context = new ProductRepository();
        }

        // GET: ProductManager
        public ActionResult Index()
        {
            List<Product> products = Context.Collection().ToList<Product>();
            return View(products);
        }

        public ActionResult Create()
        {
            Product product = new Product();
            return View(product);
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (!ModelState.IsValid) return View(product);

            Context.Insert(product);
            Context.Commit();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(string id)
        {
            Product product = Context.Find(id);
            if (product == null) return HttpNotFound();

            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product, string id)
        {
            Product productToEdit = Context.Find(id);
            if (productToEdit == null) return HttpNotFound();

            productToEdit.Category = product.Category;
            productToEdit.Description = product.Description;
            productToEdit.Image = product.Image;
            productToEdit.Name = product.Name;
            productToEdit.Price = product.Price;

            Context.Commit();

            return RedirectToAction("Index");
        }


        public ActionResult Delete(string id)
        {
            Product product = Context.Find(id);
            if (product == null) return HttpNotFound();
            return View(product);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string id)
        {
            Product product = Context.Find(id);
            if (product == null) return HttpNotFound();
            Context.Delete(product.Id);
            Context.Commit();
            return RedirectToAction("Index");
        }


    }
}