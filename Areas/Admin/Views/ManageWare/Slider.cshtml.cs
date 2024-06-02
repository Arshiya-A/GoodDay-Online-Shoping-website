using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace MainProject.Areas.Admin.Views.ManageWare
{
    public class Slider : PageModel
    {
        private readonly ILogger<Slider> _logger;

        public Slider(ILogger<Slider> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}