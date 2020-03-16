using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using MyShop.DataAccess.InMemory;

namespace MyShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        IRepository<Product> Context;
        IRepository<ProductCategory> ProductCategories;

        public ProductManagerController(IRepository<Product> productContext, IRepository<ProductCategory> productCategoryContext)
        {
            Context = productContext;
            ProductCategories = productCategoryContext;
        }

        // GET: ProductManager
        public ActionResult Index()
        {
            List<Product> products = Context.Collection().ToList();
            return View(products);
        }

        public ActionResult Create()
        {

            ProductManagerViewModel viewModel = new ProductManagerViewModel();
            viewModel.Product = new Product();
            viewModel.ProductCategories = ProductCategories.Collection();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(Product product, HttpPostedFileBase file)
        {
            if (!ModelState.IsValid) return View(product);

            if (file != null)
            {
                product.Image = product.Id + Path.GetExtension(file.FileName);
                file.SaveAs(Server.MapPath("//Content//ProductImages//") + product.Image);
            }

            Context.Insert(product);
            Context.Commit();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(string id, HttpPostedFileBase file)
        {
            Product product = Context.Find(id);
            if (product == null) return HttpNotFound();

            if (file != null)
            {
                product.Image = product.Id + Path.GetExtension(file.FileName);
                file.SaveAs(Server.MapPath("//Content//ProductImages//") + product.Image);
            }

            ProductManagerViewModel viewModel = new ProductManagerViewModel();
            viewModel.Product = product;
            viewModel.ProductCategories = ProductCategories.Collection();

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(Product product, string id, HttpPostedFileBase file)
        {
            Product productToEdit = Context.Find(id);
            if (productToEdit == null) return HttpNotFound();

            if (file != null)
            {
                productToEdit.Image = product.Id + Path.GetExtension(file.FileName);
                file.SaveAs(Server.MapPath("//Content//ProductImages//") + productToEdit.Image);
            }

            productToEdit.Category = product.Category;
            productToEdit.Description = product.Description;
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