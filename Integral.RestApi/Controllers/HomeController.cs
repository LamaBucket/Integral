using Integral.RestApi.Properties;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Integral.RestApi.Controllers
{
    [Controller]
    public class HomeController : Controller
    {
        [HttpGet("Home")]
        public IActionResult GetHomePage()
        {
            return File(Encoding.UTF8.GetBytes(Resources.Homepage), "text/html");
        }

        [HttpGet("Download")]
        public IActionResult DownloadApp()
        {
            return File(Resources.Integral_Client, "application/zip", "Integral-Client-v1.0.zip");
        }

        [HttpGet("Help")]
        public IActionResult GetHelp()
        {
            return File(Resources.Guide, "application/pdf", "Guide.pdf");
        }
    }
}
