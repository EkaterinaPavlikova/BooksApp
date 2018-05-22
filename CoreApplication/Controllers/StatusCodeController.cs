using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CoreApplication.Controllers
{
    public class StatusCodeController : Controller
    {
        private Int32[] _availableCodes = new Int32[] { 404, 500 };

        [Route("/StatusCode/{statusCode}")]
        public IActionResult Index(Int32 statusCode)
        {

            if (!_availableCodes.Contains(statusCode))
                statusCode = 500;
            return View($"Error-{statusCode}");           
        }
    }
}