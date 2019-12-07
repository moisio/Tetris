using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class Block
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool Stop { get; set; }

        public Block()
        {
            X = 0;
            Y = 0;
            Stop = false;
        }

    }
}
