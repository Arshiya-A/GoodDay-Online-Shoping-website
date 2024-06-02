using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace MainProject.Views.Shared
{
    public class _SearchResult : PageModel
    {
        private readonly ILogger<_SearchResult> _logger;

        public _SearchResult(ILogger<_SearchResult> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}