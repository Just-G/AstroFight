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

namespace AstroFight.States
{
    public class VictoryHard : State
    {
        private List<Component> _components;
        Texture2D popupV;
        public VictoryHard(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            var buttonTexture_home = _content.Load<Texture2D>("Buttons/Home_Pink");
            var buttonTexture_restart = _content.Load<Texture2D>("Buttons/Restart");
            var buttonTexture_nextlevel = _content.Load<Texture2D>("Buttons/Next");

            popupV = _content.Load<Texture2D>("Pictures/VictoryTrophy");

            var homeGameButton = new Button(buttonTexture_home)
            {
                Position = new Vector2(150, 550),
            };
            homeGameButton.Click += HomeGameButton_Click;

            var restartGameButton = new Button(buttonTexture_restart)
            {
                Position = new Vector2(350, 550),
            };
            restartGameButton.Click += RestartGameButton_Click;


            _components = new List<Component>()
            {
                homeGameButton,
                restartGameButton,
            };

        }
        public static bool IsRepeating { get; set; }

        private void NextlevelButton_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(popupV, new Vector2(100, 100), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }


        private void RestartGameButton_Click(object sender, EventArgs e)
        {
            //Restart
            _game.ChangeState(new HardGameState(_game, _graphicsDevice, _content));
        }

        private void HomeGameButton_Click(object sender, EventArgs e)
        {
            //Home
            _game.ChangeState(new MenuState(_game, _graphicsDevice, _content));
        }

        //=====================================================================================
        public override void PostUpdate(GameTime gameTime)
        {
            //throw new NotImplementedException();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in _components)
                component.Update(gameTime);
        }
    }
}

