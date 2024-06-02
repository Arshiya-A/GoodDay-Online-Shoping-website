using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace MainProject.Views.Order
{
    public class GetOrders : PageModel
    {
        private readonly ILogger<GetOrders> _logger;

        public GetOrders(ILogger<GetOrders> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}