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

        [HttpPost("uci")]
        public IActionResult Uci()
        {
            _chessEngine.NewGame();
            return Ok("id name Stork\nid author Milan\nuciok");
            //Need to also sebnd a "uciok" command back.
        }

        [HttpPost("isready")]
        public IActionResult IsReady()
        {
            _chessEngine.NewGame();
            return Ok("readyok");
        }
    }
}
