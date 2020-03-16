using MyShop.Core.Contracts;
using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.WebUI.Tests.Mocks
{
    public class MockContext<T> : IRepository<T> where T : BaseEntity
    {
        List<T> Items;
        string ClassName;

        public MockContext()
        {
            Items = new List<T>();
        }

        public void Commit()
        {
            return;
        }

        public void Insert(T item)
        {
            Items.Add(item);
        }

        public void Update(T item)
        {
            T itemToUpdate = Items.Find(i => i.Id == item.Id);
            if (itemToUpdate != null)
            {
                itemToUpdate = item;
            }
            else
            {
                throw new Exception(ClassName + " Not Found!");
            }
        }

        public T Find(string id)
        {
            return Items.Find(i => i.Id == id);
        }

        public IQueryable<T> Collection()
        {
            return Items.AsQueryable();
        }

        public void Delete(string id)
        {
            T itemToDelete = Items.Find(i => i.Id == id);
            if (itemToDelete != null)
            {
                Items.Remove(itemToDelete);
            }
            else
            {
                throw new Exception(ClassName + " Not Found!");
            }
        }
    }
}
