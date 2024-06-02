using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace MainProject.Areas.Admin.Views.Shared
{
    public class _SideBar : PageModel
    {
        private readonly ILogger<_SideBar> _logger;

        public _SideBar(ILogger<_SideBar> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}