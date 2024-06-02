using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MainProject.ViewModels
{
    public class WareViewModel
    {
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

        [Display(Name = "Ware count")]
        [Required]
        public int Count { get; set; }

        [Display(Name = "Ware Image")]
        [Required]
        public IFormFile Image { get; set; }


        [Display(Name = "Description")]
        [Required]
        [MaxLength(299)]
        public string Description { get; set; }

        [Display(Name = "Date of Insert")]
        [Required]
        public DateTime DateOfInsert { get; set; }

        [Display(Name = "Ware Images")]
        public List<IFormFile> Walpapers { get; set; }

        [Display(Name = "View")]
        public int Visit { get; set; }
    }
}