using System;
using System.ComponentModel.DataAnnotations;


namespace PetShop.Models
{
    public partial class Good
    {

        [Key]
        public int good_id { get; set; }
        public string name { get; set; }
        public decimal? price { get; set; }
        public int? count_stock { get; set; }
        public string description { get; set; }
        public DateTime? shelf_life { get; set; }
        public int? category_id { get; set; }
        public int? line_supply_id { get; set; }

    }
}
