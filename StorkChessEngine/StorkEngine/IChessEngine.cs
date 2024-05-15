using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorkEngine
{
    public interface IChessEngine
    {
        public string ReturnBestMove(string message);
    }
}
