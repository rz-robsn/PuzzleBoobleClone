using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PuzzleBoobleClone.GameElements
{
    public class Bounds : GameElement
    {
        /// <summary>
        /// The Bounds Of the Ball Field.
        /// </summary>
        public Rectangle Rectangle = new Rectangle(190, 45, 259, 322);

        public Bounds() 
        {
        }

        public void Update(GameTime gameTime, Game1 game)
        {
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Game1 game)
        {
            Texture2D spriteSheet = game.GameElements.SpriteSheet;
        }
    }
}
