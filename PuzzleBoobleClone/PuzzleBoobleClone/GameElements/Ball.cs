using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PuzzleBoobleClone.GameElements
{
    public class Ball : GameElement
    {
        private static int SRC_RECTANGLE_WIDTH = 16;
        private static int SRC_RECTANGLE_HEIGHT = 17;

        /// <summary>
        ///  Coordinate of the Top Left corner of the top Ball (= Blue Ball) on the SpriteSheet
        /// </summary>
        private static Point SPRITE_BALL_TOP_LEFT = new Point(18, 260);

        private static int SPRITE_BALL_ROW_HEIGHT = 26;
        private static int SPRITE_BALL_COLUMN_WIDTH = 306;


        public static int RECTANGLE_WIDTH = 2 * SRC_RECTANGLE_WIDTH;
        public static int RECTANGLE_HEIGHT = 2 * SRC_RECTANGLE_HEIGHT-5;

        public enum BallColor { Blue, Green, Red, Yellow, Orange, Purple, Silver, DarkGrey }

        public Vector2 Position;
        public BallColor Color;
        public Vector2 Direction;
        public float Speed;
        public Rectangle Rectangle;

        private Rectangle SourceRectangle;

        public Ball(Vector2 position, BallColor color)
        {
            Position = position;
            Color = color;

            SourceRectangle = GetColorRectangle(color);

            Direction = new Vector2(0, 0);
            Speed = 0;
        }

        public void Update(GameTime gameTime, Game1 game)
        {
            Position += Speed * Direction;
            Rectangle = new Rectangle((int)Position.X, (int)Position.Y, RECTANGLE_WIDTH, RECTANGLE_HEIGHT);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Game1 game)
        {
            spriteBatch.Draw(
                texture: game.GameElements.SpriteSheet,
                position: Position,
                sourceRectangle: SourceRectangle,
                color: Microsoft.Xna.Framework.Color.White,
                rotation: 0,
                origin: Vector2.Zero,
                scale: 2.0f,
                effects: SpriteEffects.None,
                layerDepth: 0.6f
            );
        }

        public bool IsMoving() 
        {
            return Speed > 0;
        }

        private static Rectangle GetColorRectangle(BallColor color)
        {
            int top = 0;
            int left = 0;

            switch (color)
            {
                case BallColor.Blue:
                    top = 0;
                    left = 0;
                    break;

                case BallColor.DarkGrey:
                    top = 1;
                    left = 1;
                    break;

                case BallColor.Green:
                    top = 0;
                    left = 1;
                    break;

                case BallColor.Orange:
                    top = 2;
                    left = 1;
                    break;

                case BallColor.Purple:
                    top = 3;
                    left = 1;
                    break;

                case BallColor.Red:
                    left = 0;
                    top = 2;
                    break;

                case BallColor.Silver:
                    top = 1;
                    left = 0;
                    break;

                case BallColor.Yellow:
                    top = 3;
                    left = 0;
                    break;

                default:
                    throw new Exception(String.Format("color {0} does not exist", color));
            }
            return new Rectangle(SPRITE_BALL_TOP_LEFT.X + left*SPRITE_BALL_COLUMN_WIDTH, SPRITE_BALL_TOP_LEFT.Y + top*SPRITE_BALL_ROW_HEIGHT, SRC_RECTANGLE_WIDTH, SRC_RECTANGLE_HEIGHT);
        }
    }
}
