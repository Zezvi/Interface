using PetShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PetShop.DataLayer
{
    class CheckLayer
    {

        PetShopContext context;
        public CheckLayer()
        {
            context = new PetShopContext();
        }

        public bool Add(Check model)
        {

            try
            {
                context.Check.Add(model);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                MessageBox.Show(ex.StackTrace);
                return false;
            }
        }
        public List<Check> Get()
        {

            List<Check> models = new List<Check>();
            using (PetShopContext ctx = new PetShopContext())
            {
                try
                {
                    models = ctx.Check.ToList<Check>();

                }
                catch
                {
                    models = null;
                }
            }

            return models;
        }
        public Check GetOne(int? Id)
        {
            Check ct = new Check();
            try
            {
                ct = context.Check.FirstOrDefault(n => n.check_id == Id);

            }
            catch
            {
                ct = null;
            }
            return ct;
        }
        public bool Edit(Check ct)
        {
            try
            {
                Check temp = context.Check.FirstOrDefault(n => n.check_id == ct.check_id);
                temp = ct;
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool Remove(Check ct)
        {
            try
            {
                Check temp = context.Check.FirstOrDefault(n => n.check_id == ct.check_id);
                context.Check.Remove(temp);
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
