using System;
using System.ComponentModel.DataAnnotations;


namespace PetShop.Models
{
    public partial class Supply
    {

        [Key]
        public int supply_id { get; set; }
        public string numnote { get; set; }
        public DateTime? data { get; set; }
        public int? supplier_id { get; set; }
        public int? user_id { get; set; }

    }
}
