using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace MainProject.Areas.Admin.Views.Home
{
    public class Search : PageModel
    {
        private readonly ILogger<Search> _logger;

        public Search(ILogger<Search> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}