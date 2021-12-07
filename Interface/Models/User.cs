
using System.ComponentModel.DataAnnotations;


namespace PetShop.Models
{
    public partial class User
    {
        [Key]
        public int user_id { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public string fio { get; set; }
        public int? role { get; set; }

    }
}
