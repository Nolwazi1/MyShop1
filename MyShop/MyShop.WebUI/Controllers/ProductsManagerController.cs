using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Models;
using MyShop.DataAccess.InMemory;

namespace MyShop.WebUI.Controllers
{
    public class ProductsManagerController : Controller
    {
        ProductRepository context;

        public ProductsManagerController()
        {
            context = new ProductRepository();
        }

        // GET: ProductsManager
        public ActionResult Index()
        {
            List<Products> products = context.collection().ToList();
            return View(products);
        }

        public ActionResult Create()
        {
            Products product = new Products();
            return View(product);
        }

        [HttpPost]
        public ActionResult Create(Products product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            else
            {
                context.Insert(product);
                context.Commit();
                return RedirectToAction("Index");
            }            
        }

        public ActionResult Edit(string Id) {
            Products product = context.Find(Id);
            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(product);
            }
        }

        [HttpPost]
        public ActionResult Edit(Products product, string Id) {
            Products productToEdit = context.Find(Id);
            if (productToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(product);
                }
                else
                {
                    productToEdit.Gategory = product.Gategory;
                    productToEdit.Description = product.Description;
                    productToEdit.Image = product.Image;
                    productToEdit.Name = product.Name;
                    productToEdit.Price = product.Price;

                    context.Commit();

                    return RedirectToAction("Index");
                }
            }
        }

        public ActionResult Delete(string Id)
        {
            Products productToDelete = context.Find(Id);
            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productToDelete);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            Products productToDelete = context.Find(Id);

            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(Id);
                context.Commit();
                return RedirectToAction("Index");
            }
        }
    }
}