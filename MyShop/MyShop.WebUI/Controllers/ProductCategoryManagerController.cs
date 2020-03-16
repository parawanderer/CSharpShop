using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.DataAccess.InMemory;

namespace MyShop.WebUI.Controllers
{
    public class ProductCategoryManagerController : Controller
    {
        IRepository<ProductCategory> Context;

        public ProductCategoryManagerController(IRepository<ProductCategory> productCategoryContext)
        {
            Context = productCategoryContext;
        }

        // GET: ProductManager
        public ActionResult Index()
        {
            List<ProductCategory> productCategories = Context.Collection().ToList();
            return View(productCategories);
        }

        public ActionResult Create()
        {
            ProductCategory productCategory = new ProductCategory();
            return View(productCategory);
        }

        [HttpPost]
        public ActionResult Create(ProductCategory productCategory)
        {
            if (!ModelState.IsValid) return View(productCategory);

            Context.Insert(productCategory);
            Context.Commit();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(string id)
        {
            ProductCategory productCategory = Context.Find(id);
            if (productCategory == null) return HttpNotFound();

            return View(productCategory);
        }

        [HttpPost]
        public ActionResult Edit(ProductCategory productCategory, string id)
        {
            ProductCategory productCategoryToEdit = Context.Find(id);
            if (productCategoryToEdit == null) return HttpNotFound();

            productCategoryToEdit.Category = productCategory.Category;

            Context.Commit();

            return RedirectToAction("Index");
        }


        public ActionResult Delete(string id)
        {
            ProductCategory productCategory = Context.Find(id);
            if (productCategory == null) return HttpNotFound();
            return View(productCategory);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string id)
        {
            ProductCategory productCategory = Context.Find(id);
            if (productCategory == null) return HttpNotFound();
            Context.Delete(productCategory.Id);
            Context.Commit();
            return RedirectToAction("Index");
        }
    }
}