using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace MainProject.Views.Home
{
    public class _GoToSubGroup : PageModel
    {
        private readonly ILogger<_GoToSubGroup> _logger;

        public _GoToSubGroup(ILogger<_GoToSubGroup> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}