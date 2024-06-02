using System.ComponentModel.DataAnnotations;

namespace Database;

public class Order
{
    public int OrderID { get; set; }

    [Display(Name = "Customer")]
    [Required]
    public int CustomerID { get; set; }

    [Display(Name = "Ware")]
    [Required]
    public int WareID { get; set; }

    [Required]
    [Display(Name = "Ware Count")]
    public int Count { get; set; }


    [Display(Name = "DelivaryPlace")]
    [MaxLength(300)]
    [Required]
    public string DelivaryPlace { get; set; }

    [Display(Name = "Order Date")]
    [MaxLength(300)]
    [Required]
    public DateTime OrderDate { get; set; }


    // public virtual Ware Ware { get; set; }
    // public virtual ICollection<ShopingCart> ShopingCart { get; set; }

}
