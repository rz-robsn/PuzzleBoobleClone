using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace PuzzleBoobleClone.GameElements
{
    public class AimingArrow : GameElement
    {
        private static Rectangle SPRITE_RECTANGLE = new Rectangle(0, 514, 23, 56);
        private Vector2 Position = new Vector2(295,322);

        public AimingArrow() { }

        public void Update(GameTime gameTime, Game1 game){}

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Game1 game)
        {
            Texture2D spriteSheet = game.GameElements.SpriteSheet;

            spriteBatch.Draw(spriteSheet, Position, SPRITE_RECTANGLE, Color.White, 0f, new Vector2(0, 0), 2.0f, SpriteEffects.None, 0.8f);
        }
    }
}
