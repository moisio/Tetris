using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public enum Direction
    {
        Null,
        Left,
        Right
    };

    class Settings
    {
        public static int Width { get; set; }
        public static int Height { get; set; }
        public static int Speed { get; set; }
        public static int Score { get; set; }
        public static bool GameOver { get; set; }
        public static Direction Dir { get; set; }

        //Constructor
        public Settings()
        {
            Width = 25;
            Height = 25;
            Speed = 5;
            Score = 0;
            GameOver = false;
        }
    }
}
