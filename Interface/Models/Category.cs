
using System.ComponentModel.DataAnnotations;


namespace PetShop.Models
{
    public partial class Category
    {

        [Key]
        public int category_id { get; set; }
        public string name { get; set; }

    }
}
