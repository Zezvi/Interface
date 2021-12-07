using System;
using System.ComponentModel.DataAnnotations;


namespace PetShop.Models
{
    public partial class Action
    {
        [Key]
        public int action_id { get; set; }
        public string name { get; set; }
        public DateTime? data_start { get; set; }
        public DateTime? data_end { get; set; }
        public int? discount { get; set; }
    }
}
