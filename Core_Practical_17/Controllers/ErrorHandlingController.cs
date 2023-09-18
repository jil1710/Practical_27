using Microsoft.AspNetCore.Mvc;

namespace Core_Practical_17.Controllers
{
    [Route("Error/{action}")]
    public class ErrorHandlingController : Controller
    {

        
        public IActionResult PageNotFound()
        {
            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}
