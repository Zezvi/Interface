using System;
using System.ComponentModel.DataAnnotations;


namespace PetShop.Models
{
    public partial class Check
    {

        [Key]
        public int check_id { get; set; }
        public DateTime? date_sale { get; set; }
        public decimal? total_price { get; set; }
        public int? bonus_card_id { get; set; }
        public int? user_id { get; set; }

    }
}
