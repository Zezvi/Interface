
using System.ComponentModel.DataAnnotations;


namespace PetShop.Models
{
    public partial class Supplier
    {

        [Key]
        public int supplier_id { get; set; }
        public string name { get; set; }
        public string phonenumber { get; set; }

    }
}
