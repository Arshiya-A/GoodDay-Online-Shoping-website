using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Database;


namespace Database
{
    public class ShopingCart
    {
        [Key]
        public int ShopingCartID { get; set; }
        public int CustomerID { get; set; }
        public int WareID { get; set; }
        public int Count { get; set; }
        public int Price { get; set; }

        // public virtual Database.Customer Customer { get; set; }
    }
}