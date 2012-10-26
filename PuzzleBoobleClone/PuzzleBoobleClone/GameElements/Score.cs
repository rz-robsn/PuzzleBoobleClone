using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PuzzleBoobleClone.GameElements
{
    public class Score : GameElement
    {
        public static int POINTS_PER_BALLS_POPPED = 10;
        public static int POINTS_PER_BALLS_HANGED = 20;

        private static Rectangle SRC_RECTANGLE = new Rectangle(336, 1599, 13, 11);
        private static Vector2 POSITION = new Vector2(50, 15);

        public int Value;

        public void Update(GameTime gameTime, Game1 game)
        {

        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Game1 game)
        {
            spriteBatch.Draw(
               texture: game.GameElements.SpriteSheet,
               position: POSITION,
               sourceRectangle: SRC_RECTANGLE,
               color: Microsoft.Xna.Framework.Color.White,
               rotation: 0,
               origin: Vector2.Zero,
               scale: 2.0f,
               effects: SpriteEffects.None,
               layerDepth: 0.6f
            );

            spriteBatch.DrawString(game.GameElements.Font, String.Format("{0:D8}", Value), POSITION + new Vector2(2, 16), Color.WhiteSmoke);
        }
    }
}
