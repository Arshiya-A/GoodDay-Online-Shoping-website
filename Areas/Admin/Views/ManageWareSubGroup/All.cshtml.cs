using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace MainProject.Areas.Admin.Views.ManageWareSubGroup
{
    public class All : PageModel
    {
        private readonly ILogger<All> _logger;

        public All(ILogger<All> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}