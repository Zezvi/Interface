using PetShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PetShop.DataLayer
{
    class CategoryRepository
    {
        PetShopContext context;
        public CategoryRepository()
        {
            context = new PetShopContext();
        }

        public void Add(Category model)
        {

            try
            {
                context.Category.Add(model);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
        }
        public List<Category> Get()
        {

            List<Category> models = new List<Category>();
            using (PetShopContext ctx = new PetShopContext())
            {
                try
                {
                    models = ctx.Category.ToList<Category>();

                }
                catch
                {
                    models = null;
                }
            }

            return models;
        }
        public Category GetOne(int? Id)
        {
            Category ct = new Category();
            try
            {
                ct = context.Category.FirstOrDefault(n => n.category_id == Id);

            }
            catch
            {
                ct = null;
            }
            return ct;
        }
        public bool Edit(Category ct)
        {
            try
            {
                Category temp = context.Category.FirstOrDefault(n => n.category_id == ct.category_id);
                temp = ct;
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool Remove(Category ct)
        {
            try
            {
                Category temp = context.Category.FirstOrDefault(n => n.category_id == ct.category_id);
                context.Category.Remove(temp);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
