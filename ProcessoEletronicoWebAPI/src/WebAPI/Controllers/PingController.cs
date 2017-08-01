using Microsoft.AspNetCore.Mvc;

namespace ProcessoEletronicoService.WebAPI.Controllers
{
    //[Route("api/ping")]
    public class PingController : Controller
    {
        /// <summary>
        /// Health check.
        /// </summary>
        /// <returns>Pong.</returns>
        /// <response code="200">Pong!</response>
        [HttpGet]
        [ProducesResponseType(typeof(string), 200)]
        public IActionResult Get()
        {
            return Ok("Pong!");
        }
    }
}
