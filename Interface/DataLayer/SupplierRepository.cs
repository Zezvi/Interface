using PetShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.DataLayer
{
    class SupplierRepository
    {
        PetShopContext context;
        public SupplierRepository()
        {
            context = new PetShopContext();
        }

        public void Add(Supplier model)
        {

            try
            {
                context.Supplier.Add(model);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
        }
        public List<Supplier> Get()
        {

            List<Supplier> models = new List<Supplier>();
            using (PetShopContext ctx = new PetShopContext())
            {
                try
                {
                    models = ctx.Supplier.ToList<Supplier>();

                }
                catch
                {
                    models = null;
                }
            }

            return models;
        }
        public Supplier GetOne(int Id)
        {
            Supplier ct = new Supplier();
            try
            {
                ct = context.Supplier.FirstOrDefault(n => n.supplier_id == Id);

            }
            catch
            {
                ct = null;
            }
            return ct;
        }
        public bool Edit(Supplier ct)
        {
            try
            {
                Supplier temp = context.Supplier.FirstOrDefault(n => n.supplier_id == ct.supplier_id);
                temp.phonenumber = ct.phonenumber;
                temp.name = ct.name;
                temp.supplier_id = ct.supplier_id;
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool Remove(int ct)
        {
            try
            {
                Supplier temp = context.Supplier.FirstOrDefault(n => n.supplier_id == ct);
                context.Supplier.Remove(temp);
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
