using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using AstroFight.Controls;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace AstroFight.States
{
    public class MenuState : State
    {
        private List<Component> _components;
        private Texture2D _logo;
        private Song _backgroundSong;
        private Song _playSong;
        private SoundEffect _click;

        public MenuState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            // BGM
            _backgroundSong = content.Load<Song>("Sounds/MaskedWolf");
            _playSong = content.Load<Song>("Sounds/BGM_ToadallyKrossedOut");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(_backgroundSong);

            // sfx
            _click = _content.Load<SoundEffect>("Sounds/perc_click");

            // Logo
            _logo = _content.Load<Texture2D>("Logo/Title3");

            // Buttons
            var buttonTexture_Easy = _content.Load<Texture2D>("Buttons/Easy_Button");
            var buttonTexture_Normal = _content.Load<Texture2D>("Buttons/Normal_Button");
            var buttonTexture_Hard = _content.Load<Texture2D>("Buttons/Hard_Button");
            var buttonTexture_Endless = _content.Load<Texture2D>("Buttons/Endless_Button");
            var buttonTexture_Exit = _content.Load<Texture2D>("Buttons/Exit2");

            var easyGameButton = new Button(buttonTexture_Easy)
            {
                Position = new Vector2(210, 380),
            };
            easyGameButton.Click += EasyGameButton_Click;

            var normalGameButton = new Button(buttonTexture_Normal)
            {
                Position = new Vector2(210, 487),
            };
            normalGameButton.Click += NormalGameButton_Click;

            var hardGameButton = new Button(buttonTexture_Hard)
            {
                Position = new Vector2(210, 594),
            };
            hardGameButton.Click += HardGameButton_Click;

            var endlessGameButton = new Button(buttonTexture_Endless)
            {
                Position = new Vector2(210, 701),
            };
            endlessGameButton.Click += EndlessGameButton_Click;

            var exitGameButton = new Button(buttonTexture_Exit)
            {
                Position = new Vector2(520, 820),
            };
           exitGameButton.Click += ExitGameButton_Click;

            _components = new List<Component>()
            {
                easyGameButton,
                normalGameButton,
                hardGameButton,
                endlessGameButton,
                exitGameButton,
            };
        }
        /*
        protected SoundEffect LoadSound(string soundName)
        {
            return _content.Load<SoundEffect>(soundName);
        }
        */

        public static bool IsRepeating { get; set; }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(_logo, new Vector2(150, 105), Color.White);

            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }

        private void EasyGameButton_Click(object sender, EventArgs e)
        {
            _click.Play();
            _game.ChangeState(new EasyGameState(_game, _graphicsDevice, _content));
            // BGM
            MediaPlayer.Stop();
            //MediaPlayer.Play(_playSong);
            MediaPlayer.IsRepeating = true;
        }

        private void NormalGameButton_Click(object sender, EventArgs e)
        {
            _click.Play();
            _game.ChangeState(new NormalGameState(_game, _graphicsDevice, _content));
            // BGM
            MediaPlayer.Stop();
            //MediaPlayer.Play(_playSong);
            MediaPlayer.IsRepeating = true;
        }

        private void HardGameButton_Click(object sender, EventArgs e)
        {
            _click.Play();
            _game.ChangeState(new HardGameState(_game, _graphicsDevice, _content));
            // BGM
            MediaPlayer.Stop();
            //MediaPlayer.Play(_playSong);
            MediaPlayer.IsRepeating = true;
        }
        private void EndlessGameButton_Click(object sender, EventArgs e)
        {
            _click.Play();
            _game.ChangeState(new EndlessState(_game, _graphicsDevice, _content));
            // BGM
            MediaPlayer.Stop();
            //MediaPlayer.Play(_playSong);
            MediaPlayer.IsRepeating = true;
        }

        private void ExitGameButton_Click(object sender, EventArgs e)
        {
            _click.Play();
            _game.Exit();
        }

        public override void PostUpdate(GameTime gameTime)
        {
            // remove sprites if they're not needed
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in _components)
                component.Update(gameTime);
        }
    }
}