using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace MainProject.Areas.Admin.Views.Order
{
    public class GetCustomerInfo : PageModel
    {
        private readonly ILogger<GetCustomerInfo> _logger;

        public GetCustomerInfo(ILogger<GetCustomerInfo> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}