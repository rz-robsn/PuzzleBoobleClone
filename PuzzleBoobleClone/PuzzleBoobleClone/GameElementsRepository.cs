using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using PuzzleBoobleClone.GameElements;

namespace PuzzleBoobleClone
{   
    public class GameElementsRepository : GameElement
    {
        private List<GameElement> Elements;

        public Texture2D SpriteSheet;

        public GameElementsRepository()
        {
            this.Elements = new List<GameElement>();
            this.Elements.Add(new BagAndLauncherMachine());
        }

        public void LoadContent(Game1 game)
        {
            SpriteSheet = game.Content.Load<Texture2D>("Images/sprites");
        }
        
        public void Update(GameTime gameTime, Game1 game)
        {
            Elements.ForEach(e => e.Update(gameTime, game));
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Game1 game)
        {
            Elements.ForEach(e => e.Draw(gameTime, spriteBatch, game));
        }
    }
}
