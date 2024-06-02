using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database;

namespace MainProject.Admin.ViewModels
{
    public class GeneralGroup
    {
        public IEnumerable<WareGroup> WareGroups { get; set; }
        public IEnumerable<WareSubgroup> WareSubgroups { get; set; }
    }
}