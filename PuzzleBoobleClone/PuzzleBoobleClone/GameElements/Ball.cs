﻿using System;
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

        private Rectangle BallRectangle;

        public Ball(Vector2 position, BallColor color)
        {
            Position = position;
            Color = color;

            BallRectangle = GetColorRectangle(color);

            Direction = new Vector2(0, 0);
            Speed = 0;
        }

        public void Update(GameTime gameTime, Game1 game)
        {
            Position += Speed * Direction;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Game1 game)
        {
            spriteBatch.Draw(
                texture: game.GameElements.SpriteSheet,
                position: Position,
                sourceRectangle: BallRectangle,
                color: Microsoft.Xna.Framework.Color.White,
                rotation: 0,
                origin: Vector2.Zero,
                scale: 2.0f,
                effects: SpriteEffects.None,
                layerDepth: 0.6f
            );
        }

        private static Rectangle GetColorRectangle(BallColor color)
        {
            // Coordinate of the Top Left corner of the top Ball (= Blue Ball) on the SpriteSheet
            Point ballTopLeft = new Point(17, 260);

            int ball_width = 18;
            int ball_height = 17;

            switch (color)
            {
                case BallColor.Blue: return new Rectangle(ballTopLeft.X, ballTopLeft.Y, ball_width, ball_height);
                case BallColor.DarkGrey: return new Rectangle();
                case BallColor.Green: return new Rectangle();
                case BallColor.Orange: return new Rectangle();
                case BallColor.Purple: return new Rectangle();
                case BallColor.Red: return new Rectangle();
                case BallColor.Silver: return new Rectangle();
                case BallColor.Yellow: return new Rectangle();

                default:
                    throw new Exception(String.Format("color {0} does not exist", color));
            }
        }
    }
}