using Microsoft.AspNetCore.Mvc;

namespace AFIRegistration.Api.Utils
{
    public class BaseController : ControllerBase
    {
        protected new IActionResult Ok()
        {
            return base.Ok(Envelope.Ok());
        }
        protected IActionResult Ok<T>(T result)
        {
            return base.Ok(Envelope.Ok<T>(result));
        }
        protected IActionResult CreatedAtRoute<T>(string name, object routevalue, T value)
        {
            return base.CreatedAtRoute(name, routevalue, Envelope.Ok(value));
        }
        protected IActionResult Error(string errorMessage)
        {

            return BadRequest(Envelope.Error(errorMessage));
        }
    }
}
