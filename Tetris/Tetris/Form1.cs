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
        private List<Sblock> Sblocks = new List<Sblock>();

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
            Sblock s = new Sblock();
            Sblocks.Add(s);

            Block a = new Block();
            Block b = new Block();
            //Block c = new Block();

            int Type = r.Next(0, 2);

            if(Type == 0)
            {
                a.X = 8;
                a.Y = -1;
                s.Blocks.Add(a);
                Blocks.Add(a);
            } else if (Type == 1)
            {
                a.X = 8;
                b.X = 7;
                a.Y = -1;
                b.Y = -1;
                s.Blocks.Add(a);
                s.Blocks.Add(b);
                Blocks.Add(a);
                Blocks.Add(b);
            }
            

            /*Blocks.Add(b);
            b.X = r.Next(0, canvas.Size.Width / Settings.Width);
            //b.Y = r.Next(0, canvas.Size.Height / Settings.Height);
            b.Y = -1;*/
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
            int limit;
            
            if (dir == Direction.Right)
            {
                limit = Sblocks.Last().GetGreatestX();
                if(limit == canvas.Size.Width / Settings.Width - 1)
                {
                    return false;
                }   
                foreach (Block b in Blocks)
                {
                    if (Sblocks.Last().GetAllX().Contains(b.X - 1) && Sblocks.Last().GetAllY().Contains(b.Y) && b.Stop) {
                        return false;
                    }
                }
            }
            else if (dir == Direction.Left)
            {
                limit = Sblocks.Last().GetSmallestX();
                if (limit == 0)
                {
                    return false;
                }
                foreach (Block b in Blocks)
                {
                    if (Sblocks.Last().GetAllX().Contains(b.X + 1) && Sblocks.Last().GetAllY().Contains(b.Y) && b.Stop)
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

            foreach (Block b in Sblocks.Last().Blocks)
            {   
                if(!b.Stop)
                {
                    b.Y++;
                }
            }

            //Blocks.Last().Y++;
            switch (Settings.Dir)
            {
                case Direction.Right:
                    if (NoCollision(Direction.Right)) 
                    {
                        //Blocks.Last().X++;
                        foreach (Block b in Sblocks.Last().Blocks)
                        {
                            b.X++;
                        }
                    }
                    break;

                case Direction.Left:
                    if(NoCollision(Direction.Left))
                    {
                        //Blocks.Last().X--;
                        foreach (Block b in Sblocks.Last().Blocks)
                        {
                            b.X--;
                        }
                    }
                    break;
                case Direction.Null:
                    //Blocks.Last().X += 0;
                    break;
            }
            CheckStop();
        }

        private void CheckStop()
        {
            //If block reaches the bottom
           /* if (Blocks.Last().Y >= canvas.Size.Height / Settings.Height - 1)
            {
                Blocks.Last().Stop = true;
                //NewBlock();
            }*/

            foreach(Block b in Sblocks.Last().Blocks)
            {
                if(b.Y >= 16)
                {
                    Sblocks.Last().StopAll();
                    NewBlock();
                    break;
                }
            }
            
            for(int i = 0; i < Blocks.Count; i++)
            {
                foreach(Block b in Sblocks.Last().Blocks)
                {
                    foreach (Block b2 in Blocks)
                    {
                        if (b.Y == b2.Y - 1 && b.X == b2.X && b2.Stop &&!b.Stop)
                        {
                            Sblocks.Last().StopAll();
                            NewBlock();
                            break;
                        }
                    }
                }
            }

            //NewBlock();
            /*
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
            }*/
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
            /*
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
            }*/

            Graphics canvas = e.Graphics;
            if(!Settings.GameOver)
            {
                Brush brush = Brushes.Aqua;
                for(int i = 0; i < Sblocks.Count; i++)
                {
                    for(int j = 0; j < Sblocks[i].Blocks.Count; j++ )
                    {
                        canvas.FillRectangle(
                        brush,
                        new Rectangle(Sblocks[i].Blocks[j].X * Settings.Width,
                        Sblocks[i].Blocks[j].Y * Settings.Height,
                        Settings.Width, Settings.Height));
                    }
                }
            }
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }
    }
}
