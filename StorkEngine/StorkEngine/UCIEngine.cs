using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorkEngine
{
    public class UCIEngine : IEngine
    {
        public string BestMove(string position)
        {
            Console.WriteLine(position);
            string bestMove = Console.ReadLine()!;
            return $"bestmove {bestMove}";
        }
    }
}
