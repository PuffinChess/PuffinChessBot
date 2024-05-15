using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorkEngine
{
    public class ChessEngine : IChessEngine
    {

        public string ReturnBestMove(string message)
        {
            Console.WriteLine(message);
            string bestMove = Console.ReadLine();
            return $"bestmove {bestMove}";
        }
    }
}
