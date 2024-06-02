using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace MainProject.Areas.Admin.Views.Home
{
    public class FilterSection : PageModel
    {
        private readonly ILogger<FilterSection> _logger;

        public FilterSection(ILogger<FilterSection> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}