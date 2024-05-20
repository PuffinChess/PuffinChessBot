using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StorkEngineApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UCIController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "Get";
        }
    }
}
