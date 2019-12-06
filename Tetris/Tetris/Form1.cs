using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris
{
    public partial class Form1 : Form
    {
        private List<Block> Blocks = new List<Block>();

        public Form1()
        {
            InitializeComponent();

            new Settings();
            Timer.Interval = 1000 / Settings.Speed;
            Timer.Tick += Update;
            Timer.Start();

            StartGame();
        }

        private void StartGame()
        {
            new Settings();
            Blocks.Clear();
            NewBlock();
        }

        private void NewBlock()
        {
            Random r = new Random();
            Block b = new Block();
            Blocks.Add(b);
            b.X = r.Next(0, canvas.Size.Width / Settings.Width);
            //b.Y = r.Next(0, canvas.Size.Height / Settings.Height);
            b.Y = -1;
        }

        private void Update(object sender, EventArgs e)
        {
            if(Settings.GameOver)
            {
                if(Input.KeyPressed(Keys.Enter))
                {
                    StartGame();
                }
            }

            else
            {
                if (Input.KeyPressed(Keys.Right))
                {
                    Settings.Dir = Direction.Right;
                }
                else if (Input.KeyPressed(Keys.Left))
                {
                    Settings.Dir = Direction.Left;
                } 
                else
                {
                    Settings.Dir = Direction.Null;
                }

                MoveBlock();
            }
            canvas.Invalidate();
        }

        private void MoveBlock()
        {
            Blocks.Last().Y++;
            

            switch (Settings.Dir)
            {
                case Direction.Right:
                    Blocks.Last().X++;
                    break;
                case Direction.Left:
                    Blocks.Last().X--;
                    break;
                case Direction.Null:
                    Blocks.Last().X += 0;
                    break;
            }
            CheckStop();
        }

        private void CheckStop()
        {
            if (Blocks.Last().Y >= canvas.Size.Height / Settings.Height - 1)
            {
                Blocks.Last().Stop = true;
                NewBlock();
            }
            
            if (Blocks.Count > 1)
            {
                for (int i = 0; i < Blocks.Count; i++)
                {
                    //Checks for collision with another block
                    if (Blocks.Last().Y == Blocks[i].Y - 1 && Blocks.Last().X == Blocks[i].X)
                    {
                        Blocks.Last().Stop = true;
                        NewBlock();
                        break;
                    }
                }
            }
            
            /*
            if (Blocks.Last().Stop && Blocks.Last().Y == 0)
            {
                //Die
            }
            else
            {
                NewBlock();
            }*/
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Input.ChangeState(e.KeyCode, true);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            Input.ChangeState(e.KeyCode, false);
        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics canvas = e.Graphics;
            if (!Settings.GameOver)
            {
                Brush brush = Brushes.Aqua;
                for (int i = 0; i < Blocks.Count; i++)
                {
                    canvas.FillRectangle(
                        brush,
                        new Rectangle(Blocks[i].X * Settings.Width,
                        Blocks[i].Y * Settings.Height,
                        Settings.Width, Settings.Height));
                        
                       
                }
            }
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }
    }
}
