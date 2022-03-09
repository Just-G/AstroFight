using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AstroFight.Content.Sprite
{
    class Player : Sprite
    {
        public MouseState _mouseState, _previousMouseState;
        public Vector2 _distance;
        public float mouse;
        public Player(Texture2D texture)
            : base(texture)
        {
            _origin = new Vector2(53, 50);
        }

        public override void Update()
        {
            _mouseState = Mouse.GetState();
            _distance.X = _mouseState.X - _position.X;
            _distance.Y = _mouseState.Y - _position.Y;

            _rotation = (float)Math.Atan2(_distance.Y, _distance.X);
            mouse = -(_rotation * (180 / (float)Math.PI));

            if (mouse > 150 || mouse < -90)
            {
                _rotation = (float)-2.9;
            }
            else if (mouse < 30 && mouse > -90)
            {
                _rotation = (float)-0.3;
            }


            //command write data
            //System.Diagnostics.Debug.WriteLine(_position);
        }

    }
}
