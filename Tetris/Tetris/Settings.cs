using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class Settings
    {
        public static int Width { get; set; }
        public static int Height { get; set; }
        public static int Speed { get; set; }
        public static int Score { get; set; }
        public static bool GameOver { get; set; }

        //Constructor
        public Settings()
        {
            Width = 24;
            Height = 24;
            Speed = 5;
            Score = 0;
            GameOver = false;
        }
    }
}
