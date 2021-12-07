
using System.ComponentModel.DataAnnotations;


namespace PetShop.Models
{
    public partial class Line_check
    {
        [Key]
        public int line_check_id { get; set; }
        public int? count_good { get; set; }
        public decimal? price_client { get; set; }
        public int? check_id { get; set; }
        public int? good_id { get; set; }

    }
}
