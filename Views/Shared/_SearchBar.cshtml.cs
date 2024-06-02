using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace MainProject.Views.Shared
{
    public class _SearchBar : PageModel
    {
        private readonly ILogger<_SearchBar> _logger;

        public _SearchBar(ILogger<_SearchBar> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}