using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database;

namespace MainProject.ViewModels
{
    public class ShopingCartViewModel
    {
        public int ShopingCartID { get; set; }
        public int CustomerID { get; set; }
        public int WareID { get; set; }
        public string DelivaryPlace { get; set; }
        public int Count { get; set; }
        public int Price { get; set; }
    }
}