using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Vision.Controllers
{
    [ApiController]
    public class ErrorController : ControllerBase
    {
        /// <summary>
        /// Log all the errors.
        /// </summary>
        /// <returns>Nothing</returns>
        [HttpGet("/error")]
        public IActionResult Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var stackTrace = context?.Error?.StackTrace;
            var errorMessage = context?.Error?.Message;

            return Problem();
        }

    }
}
