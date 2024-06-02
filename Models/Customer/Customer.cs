using System.ComponentModel.DataAnnotations;

namespace Database;


public class Customer
{
    [Key]
    public int CustomerID { get; set; }

    [Display(Name = "Name")]
    [Required]
    [MaxLength(20)]
    public string Name { get; set; }

    [Display(Name = "Family")]
    [Required]
    [MaxLength(50)]
    public string Family { get; set; }

    [Display(Name = "PhoneNumber")]
    [Required]
    [MaxLength(13)]
    public string PhoneNumber { get; set; }

    [Display(Name = "Email")]
    [MaxLength(30)]
    public string Email { get; set; }

    [Display(Name = "Date")]
    public DateTime Date { get; set; }
    [Display(Name = "DeathTime")]

    public string UserId { get; set; }
    public DateTime DeathTime { get; set; }

    // public virtual ICollection<Order> Orders { get; set; }
    // public virtual ICollection<ShopingCart> ShopingCart { get; set; }
}
