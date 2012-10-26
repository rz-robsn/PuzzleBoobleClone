using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PuzzleBoobleClone.GameElements;

namespace PuzzleBoobleClone.GameElements
{
    public class BallAnimationHelper : GameElement
    {
        private static int SRC_RECTANGLE_WIDTH = 16;
        private static int SRC_RECTANGLE_HEIGHT = 16;

        /// <summary>
        ///  Coordinate of the Top Left corner of the top Ball (= Blue Ball) on the SpriteSheet
        /// </summary>
        private static Point SPRITE_BALL_TOP_LEFT = new Point(18, 261);

        private static int SPRITE_BALL_ROW_HEIGHT = 26;
        private static int SPRITE_BALL_COLUMN_WIDTH = 306;

        /// <summary>
        ///  Coordinate of the Top-Leftmost frame of the first destroyed Ball (= Blue Ball) on the SpriteSheet
        /// </summary>
        private static Vector2 TOP_LEFT_DESTROYED = new Vector2(4, 6);
        private static int DESTROYED_BALL_WIDTH = 35;
        private static int  DESTROYED_BALL_HEIGHT = 30;
        private static int SPRITE_DISTANCE_DESTROYED_BALL_WIDTH = 40;
        private static int SPRITE_DISTANCE_DESTROYED_BALL_HEIGHT = 45;

        public enum BallState {Normal, Loading, Destroyed, Falling}

        private Rectangle SourceRectangle;
        private int FrameX;
        private int FrameY;
        private BallState State;

        private Ball Ball;
        
        public BallAnimationHelper(Ball ball) 
        {
           Ball = ball;
           State = BallState.Normal;
           SourceRectangle = GetColorRectangle(ball.Color);
        }

        public void Destroy() 
        {
        
        }

        public void FallDown() 
        {
        
        }

        public void Load() { }

        public void Update(GameTime gameTime, Game1 game)
        {
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Game1 game)
        {
            spriteBatch.Draw(
                texture: game.GameElements.SpriteSheet,
                position: Ball.Position,
                sourceRectangle: SourceRectangle,
                color: Microsoft.Xna.Framework.Color.White,
                rotation: 0,
                origin: Vector2.Zero,
                scale: 2.0f,
                effects: SpriteEffects.None,
                layerDepth: 0.6f
            );
        }

        private static Rectangle GetColorRectangle(Ball.BallColor color)
        {
            int top = 0;
            int left = 0;

            switch (color)
            {
                case Ball.BallColor.Blue:
                    top = 0;
                    left = 0;
                    break;

                case Ball.BallColor.DarkGrey:
                    top = 1;
                    left = 1;
                    break;

                case Ball.BallColor.Green:
                    top = 0;
                    left = 1;
                    break;

                case Ball.BallColor.Orange:
                    top = 2;
                    left = 1;
                    break;

                case Ball.BallColor.Purple:
                    top = 3;
                    left = 1;
                    break;

                case Ball.BallColor.Red:
                    left = 0;
                    top = 2;
                    break;

                case Ball.BallColor.Silver:
                    top = 1;
                    left = 0;
                    break;

                case Ball.BallColor.Yellow:
                    top = 3;
                    left = 0;
                    break;

                default:
                    throw new Exception(String.Format("color {0} does not exist", color));
            }
            return new Rectangle(SPRITE_BALL_TOP_LEFT.X + left * SPRITE_BALL_COLUMN_WIDTH, SPRITE_BALL_TOP_LEFT.Y + top * SPRITE_BALL_ROW_HEIGHT, SRC_RECTANGLE_WIDTH, SRC_RECTANGLE_HEIGHT);
        }
    }
}
