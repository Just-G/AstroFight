﻿using System;
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
        private Song _backgroundSong;
        private Song _playSong;
        private Texture2D _logo;

        public MenuState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            // BGM
            _backgroundSong = content.Load<Song>("Sounds/MaskedWolf");
            _playSong = content.Load<Song>("Sounds/BGM_ToadallyKrossedOut");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(_backgroundSong);

            // Logo
            _logo = _content.Load<Texture2D>("Logo/Title3");

            // Buttons
            var buttonTexture_easy = _content.Load<Texture2D>("Buttons/Easy_Button3");
            var buttonTexture_normal = _content.Load<Texture2D>("Buttons/Normal_Button3");
            var buttonTexture_hard = _content.Load<Texture2D>("Buttons/Hard_Button3");

            var easyGameButton = new Button(buttonTexture_easy)
            {
                Position = new Vector2(210, 428),
            };
            easyGameButton.Click += EasyGameButton_Click;

            var normalGameButton = new Button(buttonTexture_normal)
            {
                Position = new Vector2(210, 535),
            };
            normalGameButton.Click += NormalGameButton_Click;

            var hardGameButton = new Button(buttonTexture_hard)
            {
                Position = new Vector2(210, 642),
            };
            hardGameButton.Click += HardGameButton_Click;

            _components = new List<Component>()
            {
                easyGameButton,
                normalGameButton,
                hardGameButton,
            };
        }
        public static bool IsRepeating { get; set; }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(_logo, new Vector2(150, 139), Color.White);

            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }

        private void EasyGameButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new EasyGameState(_game, _graphicsDevice, _content));
            // Sounds
            MediaPlayer.Stop();
            MediaPlayer.Play(_playSong);
            MediaPlayer.IsRepeating = true;
        }

        private void NormalGameButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new NormalGameState(_game, _graphicsDevice, _content));
            // Sounds
            MediaPlayer.Stop();
            MediaPlayer.Play(_playSong);
            MediaPlayer.IsRepeating = true;
        }

        private void HardGameButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new HardGameState(_game, _graphicsDevice, _content));
            // Sounds
            MediaPlayer.Stop();
            MediaPlayer.Play(_playSong);
            MediaPlayer.IsRepeating = true;
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

        /*
        private void QuitGameButton_Click(object sender, EventArgs e)
        {
            _game.Exit();
        }
        */
    }
}