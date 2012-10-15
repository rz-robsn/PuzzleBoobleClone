using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PuzzleBoobleClone.GameElements
{
    public class BagAndLauncherMachine : GameElement
    {
        /// <summary>
        /// Rectangle of the bag Image on the SpriteSheet
        /// </summary>
        private static Rectangle BAG_RECTANGLE = new Rectangle(344, 834, 57, 25);

        private static Vector2 BAG_POSITION = new Vector2(168, 385);

        public BagAndLauncherMachine() { }

        public void Update(GameTime gameTime, Game1 game) {}

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Game1 game) 
        {
            Texture2D spriteSheet = game.GameElements.SpriteSheet;
            //spriteBatch.Draw(spriteSheet, new Vector2(0, 0), BAG_RECTANGLE, Color.White);
            spriteBatch.Draw(spriteSheet, BAG_POSITION, BAG_RECTANGLE, Color.White, 0f, new Vector2(0, 0), 2.0f, SpriteEffects.None, 0);
        }
    }
}
