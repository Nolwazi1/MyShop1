using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyShop.Core.Models;

namespace MyShop.DataAccess.InMemory
{
    public class ProductRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<Products> products;

        public ProductRepository() {
            products = cache["products"] as List<Products>;
            if (products == null)
            {
                products = new List<Products>();
            }
        }

        public void Commit() {
            cache["products"] = products;
        }

        public void Insert(Products p) {
            products.Add(p);
        }

        public void Update(Products product) {
            Products productToUdate = products.Find(p => p.Id == product.Id);
            if (productToUdate != null)
            {
                productToUdate = product;
            }
            else
            {
                throw new Exception("Product not found");
            }
        }

        public Products Find(string Id) {
            Products product = products.Find(p => p.Id == Id);

            if (product!= null)
            {
                return product;
            }
            else
            {
                throw new Exception("Product not found");
            }
        }

        public IQueryable<Products> collection() {
            return products.AsQueryable();
        }

        public void Delete(string Id) {
            Products productToDelete = products.Find(p => p.Id == Id);

            if (productToDelete != null)
            {
                products.Remove(productToDelete);
            }
            else {
                throw new Exception("Product not found");
            }
        }
    }
}
