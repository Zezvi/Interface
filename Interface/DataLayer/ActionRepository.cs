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
        public List<Models.Action> ShowALL()
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
        public bool Edit(Models.Action model)
        {
            try
            {
                Models.Action temp = ctx.Action.FirstOrDefault(n => n.action_id == model.action_id);
                temp = model;
                ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }


        }
        public bool Remove(Models.Action model)
        {
            try
            {
                Models.Action temp = ctx.Action.FirstOrDefault(n => n.action_id == model.action_id);
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
