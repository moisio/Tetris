using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class Sblock
    {
        public int X { get; set; }
        public int Y { get; set; }
        public List<Block> Blocks { get; set; }

        public Sblock()
        {
            X = 0;
            Y = 0;
            Blocks = new List<Block>();
        }

        public void StopAll()
        {
            foreach(Block b in Blocks)
            {
                b.Stop = true;
            }
        }

        public int GetGreatestX()
        {
            int x = 0;
            foreach(Block b in Blocks)
            {
                if (b.X > x)
                {
                    x = b.X;
                }
            }
            return x;
        }

        public int GetSmallestX()
        {
            int x = 16;
            foreach (Block b in Blocks)
            {
                if (b.X < x)
                {
                    x = b.X;
                }
            }
            return x;
        }

        public List<int> GetAllX()
        {
            List<int> values = new List<int>();
            foreach(Block b in Blocks)
            {
                values.Add(b.X);
            }
            return values;
        }

        public List<int> GetAllY()
        {
            List<int> values = new List<int>();
            foreach(Block b in Blocks)
            {
                values.Add(b.Y);
            }
            return values;
        }
    }
}
