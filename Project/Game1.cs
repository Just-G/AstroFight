using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using AstroFight.Content.Sprite;
using System;
using System.Collections.Generic;
using AstroFight.Content;
using AstroFight.States;

namespace AstroFight
{
    public class Game1 : Game
    {
        const int TILESIZE = 50;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Player _player;
        private BallTest _balltest;
        private Celling _celling;

        private State _currentState;
        private State _nextState;

        Random ran = new Random();
        public Vector2 _distance;
        public int stage = 1;
        int testtest = 1;
        int turn_count;
        public float mouse;
        Random ball_ran = new Random();

        bool swapY = true;

        public Vector2 _origin;

        Texture2D _rect, ship_yello, line, ship_blue, ship_green, ship_purple, ship_red, boom, bg, railbase, nuke, rainbow, bombline, dish;
        int count = 0;
        int count_combo = 0;
        List<Point> pre_point = new List<Point>();

        public int[,] grid_copy;
        public int[,] _grid;
        public int[,] ball_next;


        int counter = 1;
        int limit = 10;
        float countDuration = 0.5f; //every  2s.
        float currentTime = 0f;
        int type;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = 600;  // set this value to the desired width of your window
            _graphics.PreferredBackBufferHeight = 900;   // set this value to the desired height of your window
            _graphics.ApplyChanges();
            switch (testtest)
            {
                case 1:
                    _grid = new int[18, 12]
                {
                {-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1 },
                {-1,1,1,2,2,2,2,2,2,2,2,-1 },
                {-1,2,2,0,0,0,2,1,2,2,1,-1 },
                {-1,1,1,1,1,1,1,1,1,1,1,-1 },
                {-1,0,0,0,0,1,1,1,1,1,1,-1 },
                {-1,0,0,0,0,0,0,0,0,0,0,-1 },
                {-1,0,0,0,0,0,0,0,0,0,0,-1 },
                {-1,0,0,0,0,0,0,0,0,0,0,-1 },
                {-1,0,0,0,0,0,0,0,0,0,0,-1 },
                {-1,0,0,0,0,0,0,0,0,0,0,-1 },
                {-1,0,0,0,0,0,0,0,0,0,0,-1 },
                {-1,0,0,0,0,0,0,0,0,0,0,-1 },
                {-1,0,0,0,0,0,0,0,0,0,0,-1 },
                {-1,0,0,0,0,0,0,0,0,0,0,-1 },
                {-1,0,0,0,0,0,0,0,0,0,0,-1 },
                {-1,0,0,0,0,0,0,0,0,0,0,-1 },
                {-1,0,0,0,0,0,0,0,0,0,0,-1 },
                {-1,0,0,0,0,0,0,0,0,0,0,-1 }
                };
                    break;
                case 2:
                    _grid = new int[18, 12]
                {
                {-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1 },
                {-1,1,1,2,2,2,2,2,2,2,2,-1 },
                {-1,2,2,0,0,0,2,1,2,2,1,-1 },
                {-1,1,1,1,1,1,1,1,1,1,1,-1 },
                {-1,0,0,0,0,1,1,1,1,1,1,-1 },
                {-1,4,4,2,3,4,5,1,2,2,0,-1 },
                {-1,0,0,0,0,0,0,0,0,0,0,-1 },
                {-1,0,0,0,0,0,0,0,0,0,0,-1 },
                {-1,0,0,0,0,0,0,0,0,0,0,-1 },
                {-1,0,0,0,0,0,0,0,0,0,0,-1 },
                {-1,0,0,0,0,0,0,0,0,0,0,-1 },
                {-1,0,0,0,0,0,0,0,0,0,0,-1 },
                {-1,0,0,0,0,0,0,0,0,0,0,-1 },
                {-1,0,0,0,0,0,0,0,0,0,0,-1 },
                {-1,0,0,0,0,0,0,0,0,0,0,-1 },
                {-1,0,0,0,0,0,0,0,0,0,0,-1 },
                {-1,0,0,0,0,0,0,0,0,0,0,-1 },
                {-1,0,0,0,0,0,0,0,0,0,0,-1 }
                };
                    break;
            }
            type = ran.Next(1, 6);
            grid_copy = new int[18, 12];
            grid_copy = _grid;
            turn_count = 0;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            var texture = Content.Load<Texture2D>("Sprites/railgun");
            var mother = Content.Load<Texture2D>("Sprites/MotherShipWithBabara");
            line = this.Content.Load<Texture2D>("Sprites/Line");
            railbase = this.Content.Load<Texture2D>("Sprites/BaseG");
            dish = this.Content.Load<Texture2D>("Sprites/dish");
            _player = new Player(texture)
            {
                _position = new Vector2(295, 850)
            };
            _celling = new Celling(mother)
            {
                _position = new Vector2(50, -350)
            };
            _rect = new Texture2D(_graphics.GraphicsDevice, 50, 50);
            ship_yello = this.Content.Load<Texture2D>("Sprites/SpaceShipYello");
            ship_blue = this.Content.Load<Texture2D>("Sprites/SpaceShipBlue");
            ship_green = this.Content.Load<Texture2D>("Sprites/SpaceShipGreen");
            ship_purple = this.Content.Load<Texture2D>("Sprites/SpaceShipPurple");
            ship_red = this.Content.Load<Texture2D>("Sprites/SpaceShipRed");
            nuke = this.Content.Load<Texture2D>("Sprites/NukeMininlaVersion");
            rainbow = this.Content.Load<Texture2D>("Sprites/rainbow");
            bombline = this.Content.Load<Texture2D>("Sprites/item1");
            boom = this.Content.Load<Texture2D>("Sprites/explode");
            bg = this.Content.Load<Texture2D>("Backgrounds/BackGround");
            /*Color[] data = new Color[TILESIZE * TILESIZE];
            for (int i = 0; i < data.Length; ++i) data[i] = Color.White;
            _rect.SetData(data);*/
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            // TODO: Add your update logic here
            //_player.Update();
            currentTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            //effect boom
            if (currentTime >= countDuration)
            {
                counter++;
                currentTime -= countDuration; // "use up" the time
                for (int i = 0; i < 18; i++)
                {
                    for (int j = 0; j < 12; j++)
                    {
                        if (_grid[i, j] == 9)
                        {
                            _grid[i, j] = 0;
                        }
                    }
                }
            }
            switch (stage)
            {
                //player aim phase
                case 1:
                    _player.Update();
                    pre_point.Clear();
                    if (count > 1)
                    {
                        count_combo += count;
                    }
                    count = 0;
                    //celling dropping
                    if (turn_count == 5)
                    {
                        turn_count = 0;
                        cellingDrop();
                        _celling.Update();
                    }
                    //check lose
                    for (int i = 1; i < 11; i++)
                    {
                        if (_grid[15, i] != 0 && _grid[15, i] != 9 && _grid[15, i] != 6)
                        {
                            stage = 3;
                        }
                    }
                    //random color
                    switch (type)
                    {
                        case 1:
                            var texture_ship_yello = Content.Load<Texture2D>("Sprites/SpaceShipYello");
                            _balltest = new BallTest(texture_ship_yello)
                            {
                                _position = new Vector2(300, 850),
                                _speed = 0,
                                type = 1
                            };
                            break;
                        case 2:
                            var texture_ship_blue = Content.Load<Texture2D>("Sprites/SpaceShipBlue");
                            _balltest = new BallTest(texture_ship_blue)
                            {
                                _position = new Vector2(300, 850),
                                _speed = 0,
                                type = 2
                            };
                            break;
                        case 3:
                            var texture_ship_green = Content.Load<Texture2D>("Sprites/SpaceShipGreen");
                            _balltest = new BallTest(texture_ship_green)
                            {
                                _position = new Vector2(300, 850),
                                _speed = 0,
                                type = 3
                            };
                            break;
                        case 4:
                            var texture_ship_pueple = Content.Load<Texture2D>("Sprites/SpaceShipPurple");
                            _balltest = new BallTest(texture_ship_pueple)
                            {
                                _position = new Vector2(300, 850),
                                _speed = 0,
                                type = 4
                            };
                            break;
                        case 5:
                            var texture_ship_red = Content.Load<Texture2D>("Sprites/SpaceShipRed");
                            _balltest = new BallTest(texture_ship_red)
                            {
                                _position = new Vector2(300, 850),
                                _speed = 0,
                                type = 5
                            };
                            break;
                    }
                    //show special
                    if (count_combo >= 10)
                    {
                        _grid[17, 3] = 6;
                    }
                    else if (count_combo >= 5 && count_combo < 10)
                    {
                        _grid[17, 3] = 7;
                    }
                    else if (count_combo >= 2 && count_combo < 5)
                    {
                        _grid[17, 3] = 8;
                    }
                    else if (count_combo < 5)
                    {
                        _grid[17, 3] = 0;
                    }
                    //if click go to ball moving phase
                    if (_player._mouseState.LeftButton == ButtonState.Pressed &&
                        _player._previousMouseState.LeftButton == ButtonState.Released)
                    {
                        _balltest._direction = _player._rotation;
                        _balltest._speed = 10;
                        turn_count += 1;
                        stage = 2;
                        type = ran.Next(1, 6);
                    }
                    //if right click shoot
                    if (_player._mouseState.RightButton == ButtonState.Pressed &&
                        _player._previousMouseState.RightButton == ButtonState.Released)
                    {
                        if (count_combo >= 10)
                        {
                            count_combo = 0;
                            var texture_nuke = Content.Load<Texture2D>("Sprites/NukeMininlaVersion");
                            _balltest = new BallTest(texture_nuke)
                            {
                                _position = new Vector2(300, 850),
                                _speed = 10,
                                type = 6
                            };
                            _balltest._direction = _player._rotation;
                            stage = 2;
                            turn_count += 1;
                            type = ran.Next(1, 6);
                        }
                        else if (count_combo >= 5 && count_combo < 10)
                        {
                            count_combo = 0;
                            var texture_colorfull = Content.Load<Texture2D>("Sprites/rainbow");
                            _balltest = new BallTest(texture_colorfull)
                            {
                                _position = new Vector2(300, 850),
                                _speed = 10,
                                type = 7
                            };
                            _balltest._direction = _player._rotation;
                            stage = 2;
                            turn_count += 1;
                            type = ran.Next(1, 6);
                        }
                        else if (count_combo >= 2 && count_combo < 5)
                        {
                            count_combo = 0;
                            var texture_line = Content.Load<Texture2D>("Sprites/item1");
                            _balltest = new BallTest(texture_line)
                            {
                                _position = new Vector2(300, 850),
                                _speed = 10,
                                type = 8
                            };
                            _balltest._direction = _player._rotation;
                            stage = 2;
                            turn_count += 1;
                            type = ran.Next(1, 6);
                        }
                    }
                    break;
                //ball moving phase
                case 2:
                    _player.Update();
                    _balltest.Update();
                    // find collition
                    //System.Diagnostics.Debug.WriteLine(grid_checkball[2,1]);

                    //ball basic
                    //check bounce
                    if (_balltest._position.X <= 60 || _balltest._position.X >= 540)
                    {
                        _balltest.bout = !_balltest.bout;
                    }
                    //if player aim at <75 degree or below && >105 degree
                    if (((_player.mouse < 75 && _player.mouse > 10) && _balltest.bout == true) || ((_player.mouse < 170 && _player.mouse > 105) && _balltest.bout == false))
                    {
                        //System.Diagnostics.Debug.WriteLine("45");
                        //left up
                        if (_grid[_balltest.yPos - 1, _balltest.xPos - 1] != 0 && _grid[_balltest.yPos, _balltest.xPos] == 0 && _grid[_balltest.yPos - 1, _balltest.xPos - 1] != -1)
                        {
                            _grid[_balltest.yPos, _balltest.xPos] = _balltest.type;
                            _balltest.state = 1;
                            //stage = 1;
                            //System.Diagnostics.Debug.WriteLine("left up");
                            bubble_pop(_balltest.yPos, _balltest.xPos, _balltest.yPos, _balltest.xPos);
                            if (_balltest.type == 6)
                            {
                                _grid = _balltest.nuclear(_grid);
                                turn_count -= 1;
                            }
                            else if (_balltest.type == 7)
                            {
                                _grid = _balltest.colorBucket(_grid);
                            }
                            else if (_balltest.type == 8)
                            {
                                _grid = _balltest.bombLine(_grid);
                            }
                            else if (count >= 2)
                            {
                                foreach (Point p in pre_point)
                                {
                                    _grid[p.X, p.Y] = 9;
                                }
                            }
                            stage = 1;
                        }
                    }
                    else if (((_player.mouse < 75 && _player.mouse > 10) && _balltest.bout == false) || ((_player.mouse < 170 && _player.mouse > 105) && _balltest.bout == true))
                    {
                        //right up
                        if (_grid[_balltest.yPos - 1, _balltest.xPos + 1] != 0 && _grid[_balltest.yPos, _balltest.xPos] == 0 && _grid[_balltest.yPos - 1, _balltest.xPos + 1] != -1)
                        {
                            _grid[_balltest.yPos, _balltest.xPos] = _balltest.type;
                            _balltest.state = 1;
                            //stage = 1;
                            //System.Diagnostics.Debug.WriteLine("right up");
                            bubble_pop(_balltest.yPos, _balltest.xPos, _balltest.yPos, _balltest.xPos);
                            if (_balltest.type == 6)
                            {
                                _grid = _balltest.nuclear(_grid);
                                turn_count -= 1;
                            }
                            else if (_balltest.type == 7)
                            {
                                _grid = _balltest.colorBucket(_grid);
                            }
                            else if (_balltest.type == 8)
                            {
                                _grid = _balltest.bombLine(_grid);
                            }
                            else if (count >= 2)
                            {
                                foreach (Point p in pre_point)
                                {
                                    _grid[p.X, p.Y] = 9;
                                }
                            }
                            stage = 1;
                        }
                    }
                    //left
                    if (_grid[_balltest.yPos, _balltest.xPos - 1] != 0 && _grid[_balltest.yPos, _balltest.xPos] == 0 && _grid[_balltest.yPos, _balltest.xPos - 1] != -1)
                    {
                        _grid[_balltest.yPos, _balltest.xPos] = _balltest.type;
                        _balltest.state = 1;
                        //stage = 1;

                        //System.Diagnostics.Debug.WriteLine("left");
                        bubble_pop(_balltest.yPos, _balltest.xPos, _balltest.yPos, _balltest.xPos);
                        if (_balltest.type == 6)
                        {
                            _grid = _balltest.nuclear(_grid);
                            turn_count -= 1;
                        }
                        else if (_balltest.type == 7)
                        {
                            _grid = _balltest.colorBucket(_grid);
                        }
                        else if (_balltest.type == 8)
                        {
                            _grid = _balltest.bombLine(_grid);
                        }
                        else if (count >= 2)
                        {
                            foreach (Point p in pre_point)
                            {
                                _grid[p.X, p.Y] = 9;
                            }
                        }
                        stage = 1;
                    }
                    //right
                    else if (_grid[_balltest.yPos, _balltest.xPos + 1] != 0 && _grid[_balltest.yPos, _balltest.xPos] == 0 && _grid[_balltest.yPos, _balltest.xPos + 1] != -1)
                    {
                        _grid[_balltest.yPos, _balltest.xPos] = _balltest.type;
                        _balltest.state = 1;
                        //stage = 1;
                        //System.Diagnostics.Debug.WriteLine("right");
                        bubble_pop(_balltest.yPos, _balltest.xPos, _balltest.yPos, _balltest.xPos);

                        if (_balltest.type == 6)
                        {
                            _grid = _balltest.nuclear(_grid);
                            turn_count -= 1;
                        }
                        else if (_balltest.type == 7)
                        {
                            _grid = _balltest.colorBucket(_grid);
                        }
                        else if (_balltest.type == 8)
                        {
                            _grid = _balltest.bombLine(_grid);
                        }
                        else if (count >= 2)
                        {
                            foreach (Point p in pre_point)
                            {
                                _grid[p.X, p.Y] = 9;
                            }
                        }
                        stage = 1;
                    }
                    //up
                    else if (_grid[_balltest.yPos - 1, _balltest.xPos] != 0 && _grid[_balltest.yPos, _balltest.xPos] == 0)
                    {
                        _grid[_balltest.yPos, _balltest.xPos] = _balltest.type;
                        _balltest.state = 1;
                        //stage = 1;
                        //System.Diagnostics.Debug.WriteLine("up");
                        bubble_pop(_balltest.yPos, _balltest.xPos, _balltest.yPos, _balltest.xPos);
                        if (_balltest.type == 6)
                        {
                            _grid = _balltest.nuclear(_grid);
                            turn_count -= 1;
                        }
                        else if (_balltest.type == 7)
                        {
                            _grid = _balltest.colorBucket(_grid);
                        }
                        else if (_balltest.type == 8)
                        {
                            _grid = _balltest.bombLine(_grid);
                        }
                        else if (count >= 2)
                        {
                            foreach (Point p in pre_point)
                            {
                                _grid[p.X, p.Y] = 9;
                            }
                        }
                        stage = 1;
                    }
                    break;
                //System.Diagnostics.Debug.WriteLine(_balltest._position.X);
                case 3:

                    break;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(bg, new Vector2(0, 0), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            _spriteBatch.Draw(line, new Vector2(0, 750), null, Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            _spriteBatch.Draw(railbase, new Vector2(305, 870), null, Color.White, 0f, origin(railbase), 1f, SpriteEffects.None, 0f);
            _spriteBatch.Draw(dish, new Vector2(150, 850), null, Color.White, 0f, origin(dish), 1f, SpriteEffects.None, 0f);
            _player.Draw(_spriteBatch);
            _celling.Draw(_spriteBatch);
            //draw ball
            for (int i = 0; i < 18; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    for (int j = 0; j < 12; j++)
                    {
                        int posY = i;
                        switch (swapY)
                        {
                            case true:
                                if (posY % 2 != 0) { posY += 13; }
                                else { posY -= 13; }
                                break;
                            case false:
                                if (posY % 2 != 0) { posY -= 13; }
                                else { posY += 13; }
                                break;
                        }

                        if (_grid[i, j] == -1)
                        {
                            _spriteBatch.Draw(_rect, new Vector2(TILESIZE * j, TILESIZE * i), null, Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                        }
                        else if (_grid[i, j] == 1)
                        {
                            _spriteBatch.Draw(ship_yello, new Vector2((TILESIZE * j) + posY, (TILESIZE * i) + 25), null, Color.White, 0f, origin(ship_yello), 1f, SpriteEffects.None, 0f);
                        }
                        else if (_grid[i, j] == 2)
                        {
                            _spriteBatch.Draw(ship_blue, new Vector2((TILESIZE * j) + posY, (TILESIZE * i) + 25), null, Color.BlanchedAlmond, 0f, origin(ship_blue), 1f, SpriteEffects.None, 0f);
                        }
                        else if (_grid[i, j] == 3)
                        {
                            _spriteBatch.Draw(ship_green, new Vector2((TILESIZE * j) + posY, (TILESIZE * i) + 25), null, Color.BlanchedAlmond, 0f, origin(ship_green), 1f, SpriteEffects.None, 0f);
                        }
                        else if (_grid[i, j] == 4)
                        {
                            _spriteBatch.Draw(ship_purple, new Vector2((TILESIZE * j) + posY, (TILESIZE * i) + 25), null, Color.BlanchedAlmond, 0f, origin(ship_purple), 1f, SpriteEffects.None, 0f);
                        }
                        else if (_grid[i, j] == 5)
                        {
                            _spriteBatch.Draw(ship_red, new Vector2((TILESIZE * j) + posY, (TILESIZE * i) + 25), null, Color.BlanchedAlmond, 0f, origin(ship_red), 1f, SpriteEffects.None, 0f);
                        }
                        else if (_grid[i, j] == 6)
                        {
                            _spriteBatch.Draw(nuke, new Vector2(TILESIZE * j, TILESIZE * i), null, Color.BlanchedAlmond, 0f, origin(nuke), 1f, SpriteEffects.None, 0f);
                        }
                        else if (_grid[i, j] == 7)
                        {
                            _spriteBatch.Draw(rainbow, new Vector2(TILESIZE * j - 0, TILESIZE * i), null, Color.BlanchedAlmond, 0f, origin(rainbow), 1f, SpriteEffects.None, 0f);
                        }
                        else if (_grid[i, j] == 8)
                        {
                            _spriteBatch.Draw(bombline, new Vector2(TILESIZE * j, TILESIZE * i), null, Color.BlanchedAlmond, 0f, origin(bombline), 1f, SpriteEffects.None, 0f);
                        }
                        else if (_grid[i, j] == 9)
                        {
                            _spriteBatch.Draw(boom, new Vector2((TILESIZE * j) + 25, (TILESIZE * i) + 25), null, Color.BlanchedAlmond, 0f, origin(boom), 1f, SpriteEffects.None, 0f);
                        }
                    }
                }
                switch (stage)
                {
                    //bord static
                    case 1:
                        _balltest.Draw(_spriteBatch);

                        break;
                    //ball moving
                    case 2:
                        _balltest.Draw(_spriteBatch);
                        break;
                    case 5:

                        break;

                }

                _spriteBatch.End();

                base.Draw(gameTime);
            }

            public float toDeg(float rad)
            {
                return rad * (180 / (float)Math.PI);
            }

            //check nearbuble
            public void bubble_pop(int y, int x, int prey, int prex)
            {
                pre_point.Add(new Point(y, x));
                Point[] pre_point_array = pre_point.ToArray();

                int count1 = count;
                bool inArray = true;

                //left up
                if (_grid[y - 1, x - 1] == _grid[y, x])
                {
                    for (int i = 0; i < count1 + 1; i++)
                    {
                        Point p = new Point(y - 1, x - 1);
                        if (p != pre_point_array[i])
                        {
                            inArray = false;
                        }
                        else
                        {

                            inArray = true;
                            break;
                        }
                    }
                    if (!inArray)
                    {
                        count++;
                        bubble_pop(y - 1, x - 1, prey - 1, prex - 1);
                    }

                }
                //up
                if (_grid[y - 1, x] == _grid[y, x])
                {
                    for (int i = 0; i < count1 + 1; i++)
                    {
                        Point p = new Point(y - 1, x);
                        if (p != pre_point_array[i])
                        {
                            inArray = false;
                        }
                        else
                        {

                            inArray = true;
                            break;
                        }
                    }
                    if (!inArray)
                    {
                        count++;
                        bubble_pop(y - 1, x, prey - 1, prex);
                    }
                }
                //right up
                if (_grid[y - 1, x + 1] == _grid[y, x])
                {
                    for (int i = 0; i < count1 + 1; i++)
                    {
                        Point p = new Point(y - 1, x + 1);
                        if (p != pre_point_array[i])
                        {
                            inArray = false;
                        }
                        else
                        {

                            inArray = true;
                            break;
                        }
                    }
                    if (!inArray)
                    {
                        count++;
                        bubble_pop(y - 1, x + 1, prey - 1, prex + 1);
                    }
                }
                //left
                if (_grid[y, x - 1] == _grid[y, x])
                {
                    for (int i = 0; i < count1 + 1; i++)
                    {
                        Point p = new Point(y, x - 1);
                        if (p != pre_point_array[i])
                        {
                            inArray = false;
                        }
                        else
                        {
                            inArray = true;
                            break;
                        }
                    }
                    if (!inArray)
                    {
                        count++;
                        bubble_pop(y, x - 1, prey, prex - 1);
                    }
                }
                //righr
                if (_grid[y, x + 1] == _grid[y, x])
                {
                    for (int i = 0; i < count1 + 1; i++)
                    {
                        Point p = new Point(y, x + 1);
                        if (p != pre_point_array[i])
                        {
                            inArray = false;
                        }
                        else
                        {

                            inArray = true;
                            break;
                        }
                    }
                    if (!inArray)
                    {
                        count++;
                        bubble_pop(y, x + 1, prey, prex + 1);
                    }
                }
                //left down
                if (_grid[y + 1, x - 1] == _grid[y, x])
                {
                    for (int i = 0; i < count1 + 1; i++)
                    {
                        Point p = new Point(y + 1, x - 1);
                        if (p != pre_point_array[i])
                        {
                            inArray = false;
                        }
                        else
                        {

                            inArray = true;
                            break;
                        }
                    }
                    if (!inArray)
                    {
                        count++;
                        bubble_pop(y + 1, x - 1, prey + 1, prex - 1);
                    }
                }
                //down
                if (_grid[y + 1, x] == _grid[y, x])
                {
                    for (int i = 0; i < count1 + 1; i++)
                    {
                        Point p = new Point(y + 1, x);
                        if (p != pre_point_array[i])
                        {
                            inArray = false;
                        }
                        else
                        {

                            inArray = true;
                            break;
                        }
                    }
                    if (!inArray)
                    {
                        count++;
                        bubble_pop(y + 1, x, prey + 1, prex);
                    }
                }
                //right down
                if (_grid[y + 1, x + 1] == _grid[y, x])
                {
                    for (int i = 0; i < count1 + 1; i++)
                    {
                        Point p = new Point(y + 1, x + 1);
                        if (p != pre_point_array[i])
                        {
                            inArray = false;
                        }
                        else
                        {

                            inArray = true;
                            break;
                        }
                    }
                    if (!inArray)
                    {
                        count++;
                        bubble_pop(y + 1, x + 1, prey + 1, prex + 1);
                    }
                }
                return;
            }
            //drop celling
            public void cellingDrop()
            {
                //i = y,j = x 
                for (int i = 15; i >= 1; i--)
                {
                    for (int j = 10; j >= 1; j--)
                    {
                        if (i - 1 <= 0)
                        {
                            _grid[i, j] = -1;
                        }
                        else
                            _grid[i, j] = grid_copy[i - 1, j];
                    }
                }
                grid_copy = _grid;

                swapY = !swapY;
            }
            //find origin
            public Vector2 origin(Texture2D ori)
            {
                return _origin = new Vector2(ori.Width / 2, ori.Height / 2);
            }

            public void ChangeState(State state)
            {
                _nextState = state;
            }
        }
    }
}
