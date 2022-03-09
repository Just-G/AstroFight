using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Project.Content.Sprite
{
    class BallTest : Sprite
    {
        private int[,] grid;
        private int[,] grid_copy;

        Random ran = new Random();

        public int _speed;
        public float _direction;
        public bool bout = false;
        public bool border = false;

        public int xPos;
        public int yPos;
        public int state = 0;

        public int type;
        public int color;
        //ublic bool rbout = false;
        public BallTest(Texture2D texture)
            : base(texture)
        {

        }

        public override void Update()
        {
            xPos = (int)_position.X / 50;
            yPos = (int)_position.Y / 50;
            
            switch (state)
            {
                case 0:
                    if (bout == false)
                    {
                        _position.X += (float)(_speed * Math.Cos(_direction));
                        _position.Y += (float)(_speed * Math.Sin(_direction));
                    }
                    else
                    {
                        _position.X += -(float)(_speed * Math.Cos(_direction));
                        _position.Y += (float)(_speed * Math.Sin(_direction));
                    } 
                    break;
                 case 1:
                    
                    break;
            }
            
        }

        public override void Draw(SpriteBatch spritebatch)
        {
            if (state == 0)
            {
                spritebatch.Draw(_texture, _position, null, Color.White, _rotation, _origin, 1f, SpriteEffects.None, 0f);
            }          
        }

        public int[,] nuclear(int[,] _grid)
        {
            grid = _grid;
            //boom leftup
            //System.Diagnostics.Debug.WriteLine("lol");
            for (int i = 0; i <= 2; i++)
            {
                for(int j = 0; j <= 2; j++)
                {
                    if (grid[yPos-i, xPos-j] == -1)
                    {
                        break;
                    }
                     grid[yPos-i, xPos-j] = 9;
                }
                if (grid[yPos-i, xPos] == -1)
                {
                    break;
                }
            }
            //boom rightup
            for (int i = 0; i <= 2; i++)
            {
                for (int j = 0; j <= 2; j++)
                {
                    if (grid[yPos-i, xPos+j] == -1)
                    {
                        break;
                    }
                    grid[yPos-i, xPos+j] = 9;
                }
                if (grid[yPos-i, xPos] == -1)
                {
                    break;
                }
            }
            //boom leftdown
            for (int i = 0; i <= 2; i++)
            {
                for (int j = 0; j <= 2; j++)
                {
                    if (grid[yPos+i, xPos-j] == -1)
                    {
                        break;
                    }
                    grid[yPos+i, xPos-j] = 9;
                }
                if (grid[yPos+i, xPos] == -1)
                {
                    break;
                }
            }
            //boom rightdown
            for (int i = 0; i <= 2; i++)
            {
                for (int j = 0; j <= 2; j++)
                {
                    if (grid[yPos+i, xPos+j] == -1)
                    {
                        break;
                    }
                    grid[yPos+i, xPos+j] = 9;
                }
                if (grid[yPos+i, xPos] == -1)
                {
                    break;
                }
            }
            grid_copy = grid;
            //celling up
            if (grid[1,1] == -1)
            {
                for (int i = 1; i <= 15; i++)
                {
                    for (int j = 1; j <= 10; j++)
                    {
                        grid[i, j] = grid_copy[i + 1, j];
                    }
                }
            }

            return grid;
        }

        public int[,] colorBucket(int[,] _grid)
        {
            grid = _grid;
            color = ran.Next(1, 6);
            //paint leftup
            for (int i = 0; i <= 2; i++)
            {
                for (int j = 0; j <= 2; j++)
                {
                    if (grid[yPos - i, xPos - j] == -1)
                    {
                        break;
                    }
                    if (grid[yPos - i, xPos - j] != 0 && grid[yPos - i, xPos - j] != -1)
                    {
                        grid[yPos - i, xPos - j] = color;
                    } 
                }
                if (grid[yPos - i, xPos] == -1)
                {
                    break;
                }
            }
            //paint rightup
            for (int i = 0; i <= 2; i++)
            {
                for (int j = 0; j <= 2; j++)
                {
                    if (grid[yPos - i, xPos + j] == -1)
                    {
                        break;
                    }
                    if (grid[yPos - i, xPos + j] != 0 && grid[yPos - i, xPos + j] != -1)
                    {
                        grid[yPos - i, xPos + j] = color;
                    }
                    
                }
                if (grid[yPos - i, xPos] == -1)
                {
                    break;
                }
            }
            //paint leftdown
            for (int i = 0; i <= 2; i++)
            {
                for (int j = 0; j <= 2; j++)
                {
                    if (grid[yPos + i, xPos - j] == -1)
                    {
                        break;
                    }
                    if (grid[yPos + i, xPos - j] != -1 && grid[yPos + i, xPos - j] != 0)
                    {
                        grid[yPos + i, xPos - j] = color;
                    }
                }
                if (grid[yPos + i, xPos] == -1)
                {
                    break;
                }
            }
            //paint rightdown
            for (int i = 0; i <= 2; i++)
            {
                for (int j = 0; j <= 2; j++)
                {
                    if (grid[yPos + i, xPos + j] == -1)
                    {
                        break;
                    }
                    if (grid[yPos + i, xPos + j] != 0 && grid[yPos + i, xPos + j] != -1)
                    {
                        grid[yPos + i, xPos + j] = color;
                    }
                }
                if (grid[yPos + i, xPos] == -1)
                {
                    break;
                }
            }
            return grid;
        }

        public int[,] bombLine(int[,] _grid)
        {
            grid = _grid;
            //bomb
            for (int j = 1; j <= 10; j++)
            {
                grid[yPos, j] = 9;
            }
            return grid;
        }
    }
}
