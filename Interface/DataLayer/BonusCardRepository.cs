﻿using PetShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PetShop.DataLayer
{
    class BonusCardRepository
    {
        PetShopContext context;
        public BonusCardRepository()
        {
            context = new PetShopContext();
        }

        public void Add(BonusCard bonus)
        {

            try
            {
                context.BonusCard.Add(bonus);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
        }
        public List<BonusCard> Get()
        {

            List<BonusCard> models = new List<BonusCard>();
            using (PetShopContext ctx = new PetShopContext())
            {
                try
                {
                    models = ctx.BonusCard.ToList();

                }
                catch (Exception ex)
                {
                    string str = ex.Message;
                    models = null;
                }
            }

            return models;
        }
        public BonusCard GetOne(int? Id)
        {
            BonusCard bc = new BonusCard();
            try
            {
                bc = context.BonusCard.FirstOrDefault(n => n.bonus_card_id == Id);

            }
            catch
            {
                bc = null;
            }
            return bc;
        }
        public bool Edit(BonusCard bc)
        {
            try
            {
                BonusCard temp = context.BonusCard.FirstOrDefault(n => n.bonus_card_id == bc.bonus_card_id);
                temp.bonus_card_id = bc.bonus_card_id;
                temp.card_number = bc.card_number;
                temp.bonus = bc.bonus;
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool Remove(int bc)
        {
            try
            {
                BonusCard temp = context.BonusCard.FirstOrDefault(n => n.bonus_card_id == bc);
                context.BonusCard.Remove(temp);
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
