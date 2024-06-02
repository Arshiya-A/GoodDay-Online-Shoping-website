using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace MainProject.Views.Home
{
    public class _MostExpensiveWares : PageModel
    {
        private readonly ILogger<_MostExpensiveWares> _logger;

        public _MostExpensiveWares(ILogger<_MostExpensiveWares> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}