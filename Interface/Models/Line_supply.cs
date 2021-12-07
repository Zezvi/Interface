
using System.ComponentModel.DataAnnotations;


namespace PetShop.Models
{
    public partial class Line_supply
    {

        [Key]
        public int line_supply_id { get; set; }
        public string firstcount_good { get; set; }
        public decimal? purchase_cost { get; set; }
        public int? good_id { get; set; }
        public int? supply_id { get; set; }

    }
}
