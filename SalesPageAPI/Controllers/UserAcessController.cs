using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SalesPageAPI.Controllers
{
    [ApiController]
    [Route("/api/Users/[Controller]")]
    public class UserAcessController : ControllerBase
    {
        [HttpGet]
        [Authorize(Policy = "IdadeNecessariaMinima")]
        public IActionResult GetAuthorization()
        {
            return Ok("Acesso permitido ! ");
        }

    }
}

