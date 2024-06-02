using System.ComponentModel.DataAnnotations;

namespace Database;

public class Ware
{
    [Key]
    public int WareID { get; set; }

    [Display(Name = "WareGroup ID")]
    public int WareGroupID { get; set; }


    [Display(Name = "WareSubGroup ID")]
    public int WareSubGroupID { get; set; }


    [Display(Name = "Ware name")]
    [Required]
    [MaxLength(95)]
    public string Name { get; set; }

    [Display(Name = "Ware price")]
    [Required]
    public int Price { get; set; }

    [Display(Name = "Ware Image")]
    [Required]
    public string Image { get; set; }

    [Display(Name = "Count")]
    public int Count { get; set; }

    [Display(Name = "Description")]
    [Required]
    [MaxLength(300)]
    public string Description { get; set; }

    [Display(Name = "Date of Insert")]
    [Required]
    public DateTime DateOfInsert { get; set; }

    [Display(Name = "Ware Images")]
    public string Walpapers { get; set; }

    [Display(Name = "View")]
    public int Visit { get; set; }


    // public ICollection<Order> Orders { get; set; }

}
