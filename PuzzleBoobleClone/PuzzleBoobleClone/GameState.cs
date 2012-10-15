using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PuzzleBoobleClone
{   
    class GameState : GameElement
    {
        private List<GameElement> Elements;

        public GameState() 
        {
            this.Elements = new List<GameElement>();
        }

        void GameElement.Initialize()
        {
            Elements.ForEach(e => e.Initialize());
        }

        void GameElement.LoadContent(Game1 game)
        {
            Elements.ForEach(e => e.LoadContent(game));
        }

        void GameElement.UnloadContent()
        {
            Elements.ForEach(e => e.UnloadContent());
        }

        void GameElement.Update(Microsoft.Xna.Framework.GameTime gameTime, Game1 game)
        {
            Elements.ForEach(e => e.Update(gameTime, game));
        }

        void GameElement.Draw(Microsoft.Xna.Framework.GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            Elements.ForEach(e => e.Draw(gameTime, spriteBatch));
        }
    }
}
