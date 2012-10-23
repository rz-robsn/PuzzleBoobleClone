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
        public static int ROW_HEIGHT = 32;
        private static int NUMBER_OF_ROWS = 12;

        private static Rectangle SRC_RECTANGLE = new Rectangle(0, 0, 128, 16);

        /// <summary>
        /// The Bounds Of the Ball Field.
        /// </summary>
        public Rectangle Rectangle = new Rectangle(190, 45, 259, 322);

        private int CurrentNumOfRowToRemove;

        public Bounds() 
        {
            CurrentNumOfRowToRemove = 0;
        }

        public void Update(GameTime gameTime, Game1 game)
        {
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Game1 game)
        {
            Texture2D wall = game.GameElements.BoundsWall;

            for (int i = 0; i < CurrentNumOfRowToRemove; i++ )
            {
                spriteBatch.Draw(
                    texture: wall,
                    position: GetPosition() + new Vector2(0, i * ROW_HEIGHT),
                    sourceRectangle: null,
                    color: Microsoft.Xna.Framework.Color.White,
                    rotation: 0,
                    origin: Vector2.Zero,
                    scale: 2.0f,
                    effects: SpriteEffects.None,
                    layerDepth: 0.9f
                );
            }

        }

        public void RemoveOneRow() 
        {
            CurrentNumOfRowToRemove = (int)MathHelper.Clamp(CurrentNumOfRowToRemove + 1, 0, NUMBER_OF_ROWS);
        }

        private Vector2 GetPosition() 
        {
            return new Vector2(Rectangle.Left, Rectangle.Top); 
        }
    }
}
