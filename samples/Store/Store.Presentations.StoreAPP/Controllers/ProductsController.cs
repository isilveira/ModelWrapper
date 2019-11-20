using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Store.Core.Application.Products.Queries.GetProductsByFilter;
using Store.Presentations.StoreAPP.Controllers.Bases;

namespace Store.Presentations.StoreAPP.Controllers
{
    public class ProductsController : MediatorControllerBase
    {
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(ILogger<ProductsController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index(GetProductsByFilterQuery request)
        {
            return View(await SendRequest(request));
        }
    }
}