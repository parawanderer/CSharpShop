using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using MyShop.Core.Models;

namespace MyShop.DataAccess.InMemory
{
    public class ProductCategoryRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<ProductCategory> ProductCategories;

        public ProductCategoryRepository()
        {
            ProductCategories = cache["productCategories"] as List<ProductCategory>;
            if (ProductCategories == null) ProductCategories = new List<ProductCategory>();
        }

        public void Commit()
        {
            cache["productCategories"] = ProductCategories;
        }

        public void Insert(ProductCategory p)
        {
            ProductCategories.Add(p);
        }

        public void Update(ProductCategory productCategory)
        {
            ProductCategory productCategoryToUpdate = ProductCategories.Find(p => p.Id == productCategory.Id);
            if (productCategoryToUpdate != null)
            {
                productCategoryToUpdate = productCategory;
            }
            else
            {
                throw new Exception("ProductCategory Not Found!");
            }
        }

        public ProductCategory Find(string id)
        {
            return ProductCategories.Find(p => p.Id == id);
        }

        public IQueryable<ProductCategory> Collection()
        {
            return ProductCategories.AsQueryable();
        }

        public void Delete(string id)
        {
            ProductCategory productCategoryToDelete = ProductCategories.Find(p => p.Id == id);
            if (productCategoryToDelete != null)
            {
                ProductCategories.Remove(productCategoryToDelete);
            }
            else
            {
                throw new Exception("ProductCategory Not Found!");
            }
        }
    }
}

