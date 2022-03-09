using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Project.Content
{

    class BallType
    {
        Random ran = new Random();

        public int RandomBall()
        {
            int type = ran.Next(1, 7);
            return type;
        }
    }
}
