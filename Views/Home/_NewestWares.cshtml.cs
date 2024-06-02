using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace MainProject.Views.Shared
{
    public class _NewestWares : PageModel
    {
        private readonly ILogger<_NewestWares> _logger;

        public _NewestWares(ILogger<_NewestWares> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}