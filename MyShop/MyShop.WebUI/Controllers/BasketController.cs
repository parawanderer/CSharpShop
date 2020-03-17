﻿using MyShop.Core.Contracts;
using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class BasketController : Controller
    {
        IBasketService BasketService;
        IOrderService OrderService;

        public BasketController(IBasketService basketService, IOrderService orderService)
        {
            this.BasketService = basketService;
            this.OrderService = orderService;
        }

        // GET: Basket
        public ActionResult Index()
        {
            var model = BasketService.GetBasketItems(this.HttpContext);
            return View(model);
        }

        public ActionResult AddToBasket(string Id)
        {
            BasketService.AddToBasket(this.HttpContext, Id);
            return RedirectToAction("Index");

        }

        public ActionResult RemoveFromBasket(string Id)
        {
            BasketService.RemoveFromBasket(this.HttpContext, Id);
            return RedirectToAction("Index");
        }

        public PartialViewResult BasketSummary()
        {
            var basketSummary = BasketService.GetBasketSummary(this.HttpContext);
            return PartialView(basketSummary);
        }

        public ActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Checkout(Order order)
        {
            var basketItems = BasketService.GetBasketItems(this.HttpContext);
            order.OrderStatus = "Order Created";

            // process payment....

            order.OrderStatus = "Payment Processed";
            OrderService.CreateOrder(order, basketItems);
            BasketService.ClearBasket(this.HttpContext);

            return RedirectToAction("Thankyou", new { OrderId = order.Id });
        }

        public ActionResult Thankyou(string orderId) 
        {
            ViewBag.OrderId = orderId;
            return View();
        }


    }

}