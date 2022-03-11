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
using Microsoft.Xna.Framework.Media;

namespace AstroFight.States
{
    public class VictoryNormal : State
    {
        private List<Component> _components;
        private SoundEffect _click;
        private Song _bgHard;
        Texture2D popupV;
        public VictoryNormal(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            // sfx
            _click = _content.Load<SoundEffect>("Sounds/perc_click");
            
            // buttons
            var buttonTexture_home = _content.Load<Texture2D>("Buttons/Home_Pink");
            var buttonTexture_restart = _content.Load<Texture2D>("Buttons/Restart");
            var buttonTexture_nextlevel = _content.Load<Texture2D>("Buttons/Next");

            _bgHard = _content.Load<Song>("Sounds/BGM_Blue Star");

            popupV = _content.Load<Texture2D>("Pictures/VictoryTrophy");

            var homeGameButton = new Button(buttonTexture_home)
            {
                Position = new Vector2(100, 550),
            };
            homeGameButton.Click += HomeGameButton_Click;

            var restartGameButton = new Button(buttonTexture_restart)
            {
                Position = new Vector2(249, 550),
            };
            restartGameButton.Click += RestartGameButton_Click;

            var nextlevelButton = new Button(buttonTexture_nextlevel)
            {
                Position = new Vector2(398, 550),
            };
            nextlevelButton.Click += NextlevelGameButton_Click;

            _components = new List<Component>()
            {
                homeGameButton,
                restartGameButton,
                nextlevelButton,
            };

        }
        public static bool IsRepeating { get; set; }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(popupV, new Vector2(100, 100), null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }

        private void NextlevelGameButton_Click(object sender, EventArgs e)
        {
            //Nextlevel
            _click.Play();
            MediaPlayer.Play(_bgHard);
            _game.ChangeState(new HardGameState(_game, _graphicsDevice, _content));
        }

        private void RestartGameButton_Click(object sender, EventArgs e)
        {
            //Restart
            _click.Play();
            _game.ChangeState(new NormalGameState(_game, _graphicsDevice, _content));
        }

        private void HomeGameButton_Click(object sender, EventArgs e)
        {
            //Home
            _click.Play();
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

