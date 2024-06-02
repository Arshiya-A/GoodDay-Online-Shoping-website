using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace MainProject.Views.Home
{
    public class _MostViewedWares : PageModel
    {
        private readonly ILogger<_MostViewedWares> _logger;

        public _MostViewedWares(ILogger<_MostViewedWares> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}