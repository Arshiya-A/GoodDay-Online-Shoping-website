using System.ComponentModel.DataAnnotations;

namespace Database;

public class WareSubgroup
{
    [Key]
    public int WareSubGroupID { get; set; }

    [Display(Name = "Ware sub group name")]
    [Required]
    [MaxLength(20)]
    public string WareSubGroupName { get; set; }

    [Display(Name = "Ware group name")]
    public int WareGroupID { get; set; }

    // public virtual WareGroup WareGroup { get; set; }
    // public virtual ICollection<Ware> Wares { get; set; }
}
