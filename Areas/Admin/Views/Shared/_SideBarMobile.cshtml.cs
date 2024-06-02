using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace MainProject.Areas.Admin.Views.Shared
{
    public class _SideBarMobile : PageModel
    {
        private readonly ILogger<_SideBarMobile> _logger;

        public _SideBarMobile(ILogger<_SideBarMobile> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}