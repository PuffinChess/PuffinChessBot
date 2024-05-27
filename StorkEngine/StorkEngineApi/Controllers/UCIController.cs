using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StorkEngine;

namespace StorkEngineApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UCIController : ControllerBase
    {
        private readonly IEngine _chessEngine;

        public UCIController(IEngine engine)
        {
            _chessEngine = engine;
        }

        [EnableCors]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("MessageGotten");
        }

        [EnableCors]
        [HttpPost]
        public IActionResult Post([FromBody] string message)
        {
            Console.WriteLine(message);
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
            string commandType = message.Split()[0];
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
                    response = _chessEngine.BestMove(message);
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
            return response;
        }
    }

}
