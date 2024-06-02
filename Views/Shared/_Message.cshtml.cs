using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace MainProject.Views.Shared
{
    public class _Message : PageModel
    {
        private readonly ILogger<_Message> _logger;

        public _Message(ILogger<_Message> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}