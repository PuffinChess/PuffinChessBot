using Microsoft.AspNetCore.Mvc;
using StorkEngine;

namespace UCIApi.Controllers
{
    [ApiController]
    [Route("api/chess")]
    public class ChessController : Controller
    {
        private readonly IChessEngine _chessEngine;

        public ChessController(IChessEngine chessEngine)
        {
            _chessEngine = chessEngine;
        }

        [HttpPost("Command")]
        public IActionResult PostCommandRecieved(string message)
        {

            if (null == message || "" == message)
            {
                return BadRequest();
            }

            string response = ProcessCommand(message);

            if (response != null)
            {
                return Ok(response);
            }
            return BadRequest();


        }

        private string ProcessCommand(string message)
        {
            string commandType = message.Trim();
            string response = "";
            switch (commandType)
            {
                //"id name Stork\nid author Milan\nuciok" needs to be done somewhere but also maybe not because its going over lichess in the end?

                case "uci":
                    response = "uciok";
                    break;
                case "isready":
                    response = "readyok";
                    break;
                case "position":
                    response = _chessEngine.ReturnBestMove(message);
                    break;
                case "go":
                    //Figure out how much time is remaining for the bot
                    break;
                case "stop":
                    //Stop thinking
                    break;
                case "quit":
                    //QuidGame
                    break;
                default:
                    return null;
                    //Log somthing here since clearly somthing went wrong.
                    break;

            }
            return response;
        }
    }
}
