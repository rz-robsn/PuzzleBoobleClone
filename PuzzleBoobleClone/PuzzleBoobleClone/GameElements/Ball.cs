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
        private static int SRC_RECTANGLE_HEIGHT = 16;

        public static int RECTANGLE_WIDTH = 2 * SRC_RECTANGLE_WIDTH;
        public static int RECTANGLE_HEIGHT = 2 * SRC_RECTANGLE_HEIGHT-5;

        public enum BallColor { Blue, Green, Red, Yellow, Orange, Purple, Silver, DarkGrey }

        public Vector2 Position;
        public BallColor Color;
        public Vector2 Direction;
        public float Speed;
        public Rectangle Rectangle;

        private BallAnimationHelper AnimationHelper;

        public Ball(Vector2 position, BallColor color)
        {
            Position = position;
            Color = color;

            AnimationHelper = new BallAnimationHelper(this);

            Direction = new Vector2(0, 0);
            Speed = 0;
        }

        public void Update(GameTime gameTime, Game1 game)
        {
            Position += Speed * Direction;
            Rectangle = new Rectangle((int)Position.X, (int)Position.Y, RECTANGLE_WIDTH, RECTANGLE_HEIGHT);

            AnimationHelper.Update(gameTime, game);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Game1 game)
        {
            AnimationHelper.Draw(gameTime, spriteBatch, game);
        }

        public bool IsMoving() 
        {
            return Speed > 0;
        }

        public void Destroy() 
        {
            AnimationHelper.Destroy();
        }

        public void FallDown() 
        {
            AnimationHelper.FallDown();
        }

        public void Load() 
        {
            AnimationHelper.Load();
        }
    }
}
