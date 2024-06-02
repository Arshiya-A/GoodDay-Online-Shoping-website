using System.ComponentModel.DataAnnotations;

namespace Database;

public class WareGroup
{

    [Key]
    public int WareGroupID { get; set; }

    [Display(Name = "Ware group name")]
    [Required]
    [MaxLength(30)]
    public string WareGroupName { get; set; }

  

    // public virtual ICollection<WareSubgroup> WareSubgroups { get; set; }
}
