using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using AstroFight.Controls;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace AstroFight.States
{
    public class EndlessState : State
    {
        const int TILESIZE = 50;
        private List<Component> _components;
        private SoundEffect _click, _shoot, _pop, _explosion, _alert;
        private SoundEffect _win, _lose;

        private Player _player;
        private Ball _balltest;
        private Celling _celling;

        Random ran = new Random();
        public Vector2 _distance;
        public int stage = 1;
        int turn_count;
        public float mouse;
        public int testtest = 1;
        int initial = 1;
        int count_initial = 0;

        public Vector2 _origin;

        Texture2D _rect, ship_yello, line, ship_blue, ship_green, ship_purple, ship_red, boom, bg, railbase, nuke, rainbow, bombline, dish, popupV, popupL;
        int count = 0;
        int count_combo = 0;
        List<Point> pre_point = new List<Point>();

        public int[,] grid_copy;
        public int[,] _grid;
        public int[,] ball_next;

        float countDuration = 0.5f;
        float currentTime = 0f;
        int type;
        bool check_win;
        bool check_effect = true;

        public EndlessState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            //line = _content.Load<Texture2D>("Line");
            railbase = _content.Load<Texture2D>("Pictures/BaseG");
            dish = _content.Load<Texture2D>("Pictures/dish");

            ship_yello = _content.Load<Texture2D>("Pictures/SpaceShipYello");
            ship_blue = _content.Load<Texture2D>("Pictures/SpaceShipBlue");
            ship_green = _content.Load<Texture2D>("Pictures/SpaceShipGreen");
            ship_purple = _content.Load<Texture2D>("Pictures/SpaceShipPurple");
            ship_red = _content.Load<Texture2D>("Pictures/SpaceShipRed");
            nuke = _content.Load<Texture2D>("Pictures/NukeMininlaVersion");
            rainbow = _content.Load<Texture2D>("Pictures/rainbow");
            bombline = _content.Load<Texture2D>("Pictures/item1");
            boom = _content.Load<Texture2D>("Pictures/explode");
            bg = _content.Load<Texture2D>("Backgrounds/BackGround");
            popupV = _content.Load<Texture2D>("Pictures/VictoryTrophy");
            popupL = _content.Load<Texture2D>("Pictures/GameOver2");

            // sfx
            _click = _content.Load<SoundEffect>("Sounds/perc_click");
            
            _shoot = _content.Load<SoundEffect>("Sounds/Shootsound");
            _pop = _content.Load<SoundEffect>("Sounds/Pop");
            _explosion = _content.Load<SoundEffect>("Sounds/Explosion");
            _alert = _content.Load<SoundEffect>("Sounds/siren2");
            _lose = _content.Load<SoundEffect>("Sounds/Lose");
            

            // Buttons
            var buttonTexture_Home = _content.Load<Texture2D>("Buttons/Home_Pink2");
            var homeButton = new Button(buttonTexture_Home)
            {
                Position = new Vector2(470, 830),
            };
            homeButton.Click += HomeButton_Click;
            _components = new List<Component>()

            { homeButton };
        }
        private void HomeButton_Click(object sender, EventArgs e)
        {
            _click.Play();
            _game.ChangeState(new MenuState(_game, _graphicsDevice, _content));
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(bg, new Vector2(0, 0), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            //spriteBatch.Draw(line, new Vector2(0, 750), null, Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(railbase, new Vector2(305, 870), null, Color.White, 0f, origin(railbase), 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(dish, new Vector2(150, 850), null, Color.White, 0f, origin(dish), 1f, SpriteEffects.None, 0f);
            _player.Draw(spriteBatch);
            //_celling.Draw(spriteBatch);
            foreach (var component in _components)
            {
                component.Draw(gameTime, spriteBatch);
            }
            //draw ball
            for (int i = 0; i < 18; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    if (_grid[i, j] == -1)
                    {
                        //spriteBatch.Draw(_rect, new Vector2(TILESIZE * j, TILESIZE * i), null, Color.Black, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                    }
                    else if (_grid[i, j] == 1)
                    {
                        spriteBatch.Draw(ship_yello, new Vector2((TILESIZE * j) + 25, (TILESIZE * i) + 25), null, Color.White, 0f, origin(ship_yello), 1f, SpriteEffects.None, 0f);
                    }
                    else if (_grid[i, j] == 2)
                    {
                        spriteBatch.Draw(ship_blue, new Vector2((TILESIZE * j) + 25, (TILESIZE * i) + 25), null, Color.White, 0f, origin(ship_blue), 1f, SpriteEffects.None, 0f);
                    }
                    else if (_grid[i, j] == 3)
                    {
                        spriteBatch.Draw(ship_green, new Vector2((TILESIZE * j) + 25, (TILESIZE * i) + 25), null, Color.White, 0f, origin(ship_green), 1f, SpriteEffects.None, 0f);
                    }
                    else if (_grid[i, j] == 4)
                    {
                        spriteBatch.Draw(ship_purple, new Vector2((TILESIZE * j) + 25, (TILESIZE * i) + 25), null, Color.White, 0f, origin(ship_purple), 1f, SpriteEffects.None, 0f);
                    }
                    else if (_grid[i, j] == 5)
                    {
                        spriteBatch.Draw(ship_red, new Vector2((TILESIZE * j) + 25, (TILESIZE * i) + 25), null, Color.White, 0f, origin(ship_red), 1f, SpriteEffects.None, 0f);
                    }
                    else if (_grid[i, j] == 6)
                    {
                        spriteBatch.Draw(nuke, new Vector2(TILESIZE * j, TILESIZE * i), null, Color.White, 0f, origin(nuke), 1f, SpriteEffects.None, 0f);
                    }
                    else if (_grid[i, j] == 7)
                    {
                        spriteBatch.Draw(rainbow, new Vector2(TILESIZE * j - 0, TILESIZE * i), null, Color.White, 0f, origin(rainbow), 1f, SpriteEffects.None, 0f);
                    }
                    else if (_grid[i, j] == 8)
                    {
                        spriteBatch.Draw(bombline, new Vector2(TILESIZE * j, TILESIZE * i), null, Color.White, 0f, origin(bombline), 1f, SpriteEffects.None, 0f);
                    }
                    else if (_grid[i, j] == 9)
                    {
                        spriteBatch.Draw(boom, new Vector2((TILESIZE * j) + 25, (TILESIZE * i) + 25), null, Color.White, 0f, origin(boom), 1f, SpriteEffects.None, 0f);
                    }
                }
            }
            switch (stage)
            {
                //bord static
                case 1:
                    _balltest.Draw(spriteBatch);
                    break;
                //ball moving
                case 2:
                    _balltest.Draw(spriteBatch);
                    break;
                case 3:

                    break;
                case 4:

                    break;
            }
            spriteBatch.End();
        }

        public override void PostUpdate(GameTime gameTime)
        {

        }

        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here
            //_player.Update();

            foreach (var component in _components)
                component.Update(gameTime);

            currentTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (count_initial < 1)
            {
                var texture = _content.Load<Texture2D>("Pictures/railgun");
                //var mother = _content.Load<Texture2D>("Pictures/MotherShipWithBabara");
                turn_count = 0;
                count_combo = 0;
                _player = new Player(texture)
                {
                    _position = new Vector2(295, 850)
                };
                /*_celling = new Celling(mother)
                {
                    _position = new Vector2(50, -350)
                };*/
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
                type = ran.Next(1, 6);
                grid_copy = new int[18, 12];
                grid_copy = _grid;
                turn_count = 0;
                count_initial += 1;
            }
            if (currentTime >= countDuration)
            {
                currentTime -= countDuration;
                //boom effect
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
                    check_win = true;
                    if (count > 1)
                    {
                        count_combo += count;
                    }
                    count = 0;
                    if (turn_count == 4 && check_effect == true)
                    {
                        _alert.Play();
                        check_effect = false;
                    }
                    //chain explosion
                    for (int i = 1; i < 15; i++)
                    {
                        for (int j = 1; j < 11; j++)
                        {

                            if ((_grid[i - 1, j - 1] == 0 || _grid[i - 1, j - 1] == -1) && _grid[i - 1, j] == 0 && (_grid[i - 1, j + 1] == 0 || _grid[i - 1, j + 1] == -1) && _grid[i, j] != 0)
                            {
                                _grid[i, j] = 9;
                            }
                        }
                    }
                    //celling dropping
                    if (turn_count == 5)
                    {
                        check_effect = true;
                        turn_count = 0;
                        cellingDrop();
                        //_celling.Update();
                    }
                    //check lose
                    for (int i = 1; i < 11; i++)
                    {
                        if (_grid[15, i] != 0 && _grid[15, i] != 9 && _grid[15, i] != 6)
                        {
                            _lose.Play();
                            _game.ChangeState(new GameOverEndless(_game, _graphicsDevice, _content));
                        }
                    }
                    //check win
                    for (int i = 1; i <= 14; i++)
                    {
                        for (int j = 1; j <= 10; j++)
                        {
                            if (_grid[i, j] != 0 && _grid[i, j] != -1)
                            {
                                check_win = false;
                            }
                        }
                    }
                    /*if (check_win == true)
                    {
                        //stage = 4;
                        _game.ChangeState(new VictoryHard(_game, _graphicsDevice, _content));
                    }*/
                    //random color
                    switch (type)
                    {
                        case 1:
                            //var texture_ship_yello = Content.Load<Texture2D>("SpaceShipYello");
                            _balltest = new Ball(ship_yello)
                            {
                                _position = new Vector2(300, 850),
                                _speed = 0,
                                type = 1
                            };
                            break;
                        case 2:
                            //var texture_ship_blue = Content.Load<Texture2D>("SpaceShipBlue");
                            _balltest = new Ball(ship_blue)
                            {
                                _position = new Vector2(300, 850),
                                _speed = 0,
                                type = 2
                            };
                            break;
                        case 3:
                            //var texture_ship_green = Content.Load<Texture2D>("SpaceShipGreen");
                            _balltest = new Ball(ship_green)
                            {
                                _position = new Vector2(300, 850),
                                _speed = 0,
                                type = 3
                            };
                            break;
                        case 4:
                            //var texture_ship_pueple = Content.Load<Texture2D>("SpaceShipPurple");
                            _balltest = new Ball(ship_purple)
                            {
                                _position = new Vector2(300, 850),
                                _speed = 0,
                                type = 4
                            };
                            break;
                        case 5:
                            //var texture_ship_red = Content.Load<Texture2D>("SpaceShipRed");
                            _balltest = new Ball(ship_red)
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
                        _shoot.Play();
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
                            var texture_nuke = _content.Load<Texture2D>("Pictures/NukeMininlaVersion");
                            _balltest = new Ball(texture_nuke)
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
                            var texture_colorfull = _content.Load<Texture2D>("Pictures/rainbow");
                            _balltest = new Ball(texture_colorfull)
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
                            var texture_line = _content.Load<Texture2D>("Pictures/item1");
                            _balltest = new Ball(texture_line)
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
                                if (_grid[1, 1] == -1)
                                {
                                    _celling._position.Y -= 50;
                                }
                                _grid = _balltest.nuclear(_grid);
                                _explosion.Play();
                                turn_count -= 1;
                            }
                            else if (_balltest.type == 7)
                            {
                                _grid = _balltest.colorBucket(_grid, 3);
                            }
                            else if (_balltest.type == 8)
                            {
                                _grid = _balltest.bombLine(_grid);
                            }
                            else if (count >= 2)
                            {
                                _pop.Play();
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
                                if (_grid[1, 1] == -1)
                                {
                                    _celling._position.Y -= 50;
                                }
                                _grid = _balltest.nuclear(_grid);
                                _explosion.Play();
                                turn_count -= 1;
                            }
                            else if (_balltest.type == 7)
                            {
                                _grid = _balltest.colorBucket(_grid, 3);
                            }
                            else if (_balltest.type == 8)
                            {
                                _grid = _balltest.bombLine(_grid);
                            }
                            else if (count >= 2)
                            {
                                _pop.Play();
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
                            if (_grid[1, 1] == -1)
                            {
                                _celling._position.Y -= 50;
                            }
                            _grid = _balltest.nuclear(_grid);
                            _explosion.Play();
                            turn_count -= 1;
                        }
                        else if (_balltest.type == 7)
                        {
                            _grid = _balltest.colorBucket(_grid, 3);
                        }
                        else if (_balltest.type == 8)
                        {
                            _grid = _balltest.bombLine(_grid);
                        }
                        else if (count >= 2)
                        {
                            _pop.Play();
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
                            if (_grid[1, 1] == -1)
                            {
                                _celling._position.Y -= 50;
                            }
                            _grid = _balltest.nuclear(_grid);
                            _explosion.Play();
                            turn_count -= 1;
                        }
                        else if (_balltest.type == 7)
                        {
                            _grid = _balltest.colorBucket(_grid, 3);
                        }
                        else if (_balltest.type == 8)
                        {
                            _grid = _balltest.bombLine(_grid);
                        }
                        else if (count >= 2)
                        {
                            _pop.Play();
                            foreach (Point p in pre_point)
                            {
                                _grid[p.X, p.Y] = 9;
                            }
                        }
                        stage = 1;
                    }
                    //up
                    if (_grid[_balltest.yPos - 1, _balltest.xPos] != 0 && _grid[_balltest.yPos, _balltest.xPos] == 0)
                    {
                        _grid[_balltest.yPos, _balltest.xPos] = _balltest.type;
                        _balltest.state = 1;
                        //stage = 1;
                        //System.Diagnostics.Debug.WriteLine("up");
                        bubble_pop(_balltest.yPos, _balltest.xPos, _balltest.yPos, _balltest.xPos);
                        if (_balltest.type == 6)
                        {
                            if (_grid[1, 1] == -1)
                            {
                                _celling._position.Y -= 50;
                            }
                            _grid = _balltest.nuclear(_grid);
                            _explosion.Play();
                            turn_count -= 1;
                        }
                        else if (_balltest.type == 7)
                        {
                            _grid = _balltest.colorBucket(_grid, 3);
                        }
                        else if (_balltest.type == 8)
                        {
                            _grid = _balltest.bombLine(_grid);
                        }
                        else if (count >= 2)
                        {
                            _pop.Play();
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
                //win
                case 4:
                    count_initial = 0;

                    stage = 1;
                    break;
            }

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
                        _grid[i, j] = ran.Next(1, 6); ;
                    }
                    else
                        _grid[i, j] = grid_copy[i - 1, j];
                }
            }
            grid_copy = _grid;
        }
        //find origin
        public Vector2 origin(Texture2D ori)
        {
            return _origin = new Vector2(ori.Width / 2, ori.Height / 2);
        }
    }
}
