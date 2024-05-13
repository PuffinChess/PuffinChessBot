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

        [HttpPost("Command")]
        public IActionResult CommandRecieved()
        {
            using (StreamReader reader = new StreamReader(Request.Body))
            {
                string response = ProcessCommand(reader.ReadToEnd()); 
                return Ok(response);
            }

        }

        private string ProcessCommand(string message)
        {
            string commandType = message.Trim();

            switch (commandType)
            {
                //"id name Stork\nid author Milan\nuciok" needs to be done somewhere but also maybe not because its going over lichess in the end?

                case "uci":
                    //Respond "uciok"
                    break;
                case "isready":
                    //Respond "readyok"
                    break;
                case "position":
                    //Figure out what board state is being provided
                    //Figure out which moves have been played
                    //Calculate next move
                    //Respond to the frontend
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
                    //Log somthing here since clearly somthing went wrong.
                    break;

            }
            return "Not implementd";
        }
    }
}
