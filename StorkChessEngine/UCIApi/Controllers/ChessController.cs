using Microsoft.AspNetCore.Mvc;
using StorkEngine;

namespace UCIApi.Controllers
{
    [ApiController]
    [Route("api/chess")]
    public class ChessController : Controller
    {
        private readonly ChessEngine _chessEngine;

        public ChessController (ChessEngine chessEngine)
        {
            _chessEngine = chessEngine;
        }

        [HttpPost("start-game")]
        public IActionResult StartGame()
        {
            _chessEngine.NewGame(); 
            return Ok
        }
    }
}
