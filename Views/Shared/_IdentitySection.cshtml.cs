using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace MainProject.Views.Shared
{
    public class _IdentitySection : PageModel
    {
        private readonly ILogger<_IdentitySection> _logger;

        public _IdentitySection(ILogger<_IdentitySection> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}