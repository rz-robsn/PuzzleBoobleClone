using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PuzzleBoobleClone.GameElements
{
    public class Bobbles : GameElement
    {
        private static Rectangle LEFT_BOOBLE_RECTANGLE = new Rectangle(1, 17, 18, 20);
        private Vector2 LeftBooblePosition = new Vector2(265, 395);

        private static Rectangle RIGHT_BOOBLE_RECTANGLE = new Rectangle(0, 122, 18, 20);
        private Vector2 RightBooblePosition = new Vector2(350, 396);

        
        public void Update(GameTime gameTime, Game1 game) {}

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Game1 game)
        {
            Texture2D spriteSheet = game.GameElements.SpriteSheet;

            spriteBatch.Draw(spriteSheet, LeftBooblePosition, LEFT_BOOBLE_RECTANGLE, Color.White, 0f, new Vector2(0, 0), 2.0f, SpriteEffects.None, 0.7f);
            spriteBatch.Draw(spriteSheet, RightBooblePosition, RIGHT_BOOBLE_RECTANGLE, Color.White, 0f, new Vector2(0, 0), 2.0f, SpriteEffects.None, 0.7f);
        }
    }
}
