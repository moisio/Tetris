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
                MoveBlock();
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

        //Checks if there is a full row of blocks and deletes those blocks.
        private void FullRow()
        {
            List<Block> row = new List<Block>();

            for(int i = 16; i >=0; i--)
            {
                foreach (Block b in Blocks)
                {
                    if (b.Y == i)
                    {
                        if (b.Stop)
                        {
                            row.Add(b);
                        } 
                    }
                }

                if (row.Count == 17)
                {
                    foreach (Block b in row)
                    {
                        b.Y = 20;
                        b.X = 20;
                    }

                    foreach (Block b in Blocks)
                    {
                        if(b.Y < i && b.Stop)
                        {
                            b.Y += 1;
                        }
                    }
                }

                row.Clear();
            }
            
            

            

        }

        //First checks if trying to move out of bounds, then checks if trying to move on to another block
        private bool NoCollision(Direction dir)
        {
            if (dir == Direction.Right)
            {
                if (Blocks.Last().X == canvas.Size.Width / Settings.Width - 1)
                {
                    return false;
                }
                for(int i = 0; i < Blocks.Count; i++)
                {
                    if (Blocks.Last().Y == Blocks[i].Y && Blocks.Last().X == Blocks[i].X - 1)
                    {
                        return false;
                    }
                } 
            }
            else if (dir == Direction.Left)
            {
                if (Blocks.Last().X == 0)
                {
                    return false;
                }
                for (int i = 0; i < Blocks.Count; i++)
                {
                    if (Blocks.Last().Y == Blocks[i].Y && Blocks.Last().X == Blocks[i].X + 1)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private void MoveBlock()
        {
            FullRow();
            Blocks.Last().Y++;
            switch (Settings.Dir)
            {
                case Direction.Right:
                    if (NoCollision(Direction.Right)) 
                    {
                        Blocks.Last().X++;
                    }
                    break;

                case Direction.Left:
                    if(NoCollision(Direction.Left))
                    {
                        Blocks.Last().X--;
                    }
                    break;
                case Direction.Null:
                    Blocks.Last().X += 0;
                    break;
            }
            CheckStop();
        }

        private void CheckStop()
        {
            //If block reaches the bottom
            if (Blocks.Last().Y >= canvas.Size.Height / Settings.Height - 1)
            {
                Blocks.Last().Stop = true;
                //NewBlock();
            }
            
            if (Blocks.Count > 1)
            {
                for (int i = 0; i < Blocks.Count; i++)
                {
                    //Checks for collision with another block
                    if (Blocks.Last().Y == Blocks[i].Y - 1 && Blocks.Last().X == Blocks[i].X)
                    {
                        Blocks.Last().Stop = true;
                        //NewBlock();
                        break;
                    }
                }
            }
   
            if (Blocks.Last().Stop && Blocks.Last().Y == 0)
            {
                Die();
            }
            else if (Blocks.Last().Stop)
            {  
                NewBlock();
            }
        }

        private void Die()
        {
            Settings.GameOver = true;
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
