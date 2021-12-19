using PetShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PetShop.DataLayer
{
    class ActionRepository
    {
        PetShopContext ctx;
        public ActionRepository()
        {
            ctx = new PetShopContext();
        }

        public void Add(Models.Action model)
        {

            try
            {
                ctx.Action.Add(model);
                ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
        }
        public List<Models.Action> Get()
        {

            List<Models.Action> models = new List<Models.Action>();
            using (PetShopContext temp = new PetShopContext())
            {
                try
                {
                    models = temp.Action.ToList<Models.Action>();

                }
                catch
                {
                    models = null;
                }
            }

            return models;
        }
        public Models.Action GetOne(int? Id)
        {
            Models.Action model = new Models.Action();
            try
            {
                model = ctx.Action.FirstOrDefault(n => n.action_id == Id);

            }
            catch
            {
                model = null;
            }
            return model;
        }
        public Models.Action GetOnebyDate(DateTime dateAction)
        {
            Models.Action model = new Models.Action();
            try
            {
                model = ctx.Action.FirstOrDefault(n => n.data_start <= dateAction && n.data_end >= dateAction);
            }
            catch
            {
                model = null;
            }
            return model;
        }
        public bool Edit(Models.Action model)
        {
            try
            {
                Models.Action temp = ctx.Action.FirstOrDefault(n => n.action_id == model.action_id);
                temp = model;
                temp.action_id = model.action_id;
                temp.data_end = model.data_end;
                temp.data_start = model.data_start;
                temp.discount = model.discount;
                temp.name = model.name;
                ctx.SaveChanges();
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
                Models.Action temp = ctx.Action.FirstOrDefault(n => n.action_id == id);
                ctx.Action.Remove(temp);
                ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }


        }
    }
}
