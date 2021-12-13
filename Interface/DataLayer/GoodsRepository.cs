using PetShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PetShop.DataLayer
{
    class GoodsRepository
    {
        PetShopContext context;
        public GoodsRepository()
        {
            context = new PetShopContext();
        }

        public void Add(Good good)
        {

            try
            {
                context.Good.Add(good);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
        }
        public List<Good> Get()
        {

            List<Good> goods = new List<Good>();
            using (PetShopContext ctx = new PetShopContext())
            {
                try
                {
                    goods = ctx.Good.ToList<Good>();

                }
                catch
                {
                    goods = null;
                }
            }

            return goods;
        }
        public Good GetOne(int Id)
        {
            Good gd = new Good();
            try
            {
                gd = context.Good.FirstOrDefault(n => n.good_id == Id);

            }
            catch
            {
                gd = null;
            }
            return gd;
        }
        public bool Edit(Good gd)
        {
            try
            {
                Good good = context.Good.FirstOrDefault(n => n.good_id == gd.good_id);
                good = gd;
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }


        }
        public bool Remove(int id)
        {
            try
            {
                Good temp = context.Good.FirstOrDefault(n => n.good_id == id);
                context.Good.Remove(temp);
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
