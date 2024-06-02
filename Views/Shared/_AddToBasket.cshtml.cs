using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace MainProject.Views.Shared
{
    public class _AddToBasket : PageModel
    {
        private readonly ILogger<_AddToBasket> _logger;

        public _AddToBasket(ILogger<_AddToBasket> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}