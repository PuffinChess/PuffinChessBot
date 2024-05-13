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
        public IActionResult uci()
        {
            _chessEngine.NewGame();
            return Ok("id name Stork\nid author Milan\nuciok");
        }

        [HttpPost("isready")]
        public IActionResult IsReady()
        {
            return Ok("readyok");
        }

        [HttpPost("position")]
        public IActionResult Move()
        {
            string bestMove = "e2e4";
            return Ok($"bestmove {bestMove}");
        }

        [HttpPost("newgame")]
        public IActionResult NewGame()
        {
            return Ok("readyok");
        }
    }
}
