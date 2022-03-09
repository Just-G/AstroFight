using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Project.Content.Sprite
{
    class Celling : Sprite
    {
        public Celling(Texture2D texture)
            : base(texture)
        {
            _origin = new Vector2(0, 0);
        }

        public override void Update()
        {
            _position.Y += 50;
        }
    }
}
