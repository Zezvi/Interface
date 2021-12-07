using System;
using System.ComponentModel.DataAnnotations;


namespace PetShop.Models
{
    public partial class BonusCard
    {
        [Key]
        public int bonus_card_id { get; set; }
        public int? bonus { get; set; }
        public string card_number { get; set; }

    }
}
