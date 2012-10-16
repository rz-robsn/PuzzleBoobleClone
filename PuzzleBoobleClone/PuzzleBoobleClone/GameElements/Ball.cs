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
        public enum BallColor { Blue, Green, Red, Yellow, Orange, Purple, Silver, DarkGrey }

        public Vector2 Position;
        public BallColor Color;
        public Vector2 Direction;
        public float Speed;

        public Ball(Vector2 position, BallColor color) 
        {
            Position = position;
            Color = color;

            Direction = new Vector2(0, 0);
            Speed = 0;
        }

        public void Update(GameTime gameTime, Game1 game)
        {
            Position += Speed * Direction;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Game1 game)
        {
            
        }
    }
}
