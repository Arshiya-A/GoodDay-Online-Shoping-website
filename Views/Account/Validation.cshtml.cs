using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace MainProject.Views.Account
{
    public class Validation : PageModel
    {
        private readonly ILogger<Validation> _logger;

        public Validation(ILogger<Validation> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}