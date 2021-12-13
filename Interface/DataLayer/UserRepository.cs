using PetShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.DataLayer
{
    class UserRepository
    {
        PetShopContext context;
        public UserRepository()
        {
            context = new PetShopContext();
        }

        public void Add(User model)
        {

            try
            {
                context.User.Add(model);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
        }
        public List<User> Get()
        {

            List<User> models = new List<User>();
            using (PetShopContext ctx = new PetShopContext())
            {
                try
                {
                    models = ctx.User.ToList<User>();

                }
                catch
                {
                    models = null;
                }
            }

            return models;
        }
        public User GetOne(int? Id)
        {
            User ct = new User();
            try
            {
                ct = context.User.FirstOrDefault(n => n.user_id == Id);

            }
            catch
            {
                ct = null;
            }
            return ct;
        }
        public bool Edit(User ct)
        {
            try
            {
                User temp = context.User.FirstOrDefault(n => n.user_id == ct.user_id);
                temp.fio = ct.fio;
                temp.login = ct.login;
                temp.password = ct.password;
                temp.role = ct.role;
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
                User temp = context.User.FirstOrDefault(n => n.user_id == id);
                context.User.Remove(temp);
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
