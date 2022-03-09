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

        public MenuState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            var buttonTexture_easy = _content.Load<Texture2D>("Controls/Easy_Button2");
            var buttonTexture_normal = _content.Load<Texture2D>("Controls/Normal_Button2");
            var buttonTexture_hard = _content.Load<Texture2D>("Controls/Hard_Button2");

            // BGM
            _backgroundSong = content.Load<Song>("Sounds/MaskedWolf");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(_backgroundSong);

            var easyGameButton = new Button(buttonTexture_easy)
            {
                Position = new Vector2(210, 420),
            };
            easyGameButton.Click += EasyGameButton_Click;

            var normalGameButton = new Button(buttonTexture_normal)
            {
                Position = new Vector2(210, 520),
            };
            normalGameButton.Click += NormalGameButton_Click;

            var hardGameButton = new Button(buttonTexture_hard)
            {
                Position = new Vector2(210, 620),
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

            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }

        private void EasyGameButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new EasyState(_game, _graphicsDevice, _content));
        }

        private void NormalGameButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new NormalState(_game, _graphicsDevice, _content));
        }

        private void HardGameButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new HardState(_game, _graphicsDevice, _content));
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