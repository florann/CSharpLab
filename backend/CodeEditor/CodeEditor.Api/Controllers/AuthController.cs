using Microsoft.AspNetCore.Mvc;

namespace CodeEditor.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> login()
        {


            throw new NotImplementedException();
        }
    }
}
