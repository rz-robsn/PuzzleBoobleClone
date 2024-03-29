﻿using System;
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
        public static readonly Vector2 CURRENT_BALL_POSITION_UNIT;

        private static int SRC_RECTANGLE_WIDTH = 16;
        private static int SRC_RECTANGLE_HEIGHT = 16;

        /// <summary>
        ///  Coordinate of the Top Left corner of the top Ball (= Blue Ball) on the SpriteSheet
        /// </summary>
        private static Point SPRITE_BALL_TOP_LEFT = new Point(18, 261);

        private static int SPRITE_BALL_ROW_HEIGHT = 26;
        private static int SPRITE_BALL_COLUMN_WIDTH = 306;

        /// <summary>
        ///  Coordinate of the Top-Leftmost frame of the first destroyed Ball (= Blue Ball) on the PoppedBallSpriteSheet
        /// </summary>
        private static Vector2 TOP_LEFT_DESTROYED = new Vector2(4, 6);
        private static int DESTROYED_BALL_WIDTH = 35;
        private static int DESTROYED_BALL_HEIGHT = 30;
        private static int SPRITE_DISTANCE_DESTROYED_BALL_WIDTH = 40;
        private static double DESTROYED_BALL_FRAME_INTERVAL = 100;
        private static int DESTROYED_BALL_NUMBER_OF_FRAMES = 5;
        private static int SPRITE_DISTANCE_DESTROYED_BALL_ROW_HEIGHT = 45;
        private static int SPRITE_DISTANCE_DESTROYED_BALL_COLUMN_WIDTH = 200;

        private static int FALLING_SPEED = 10;

        private static float LOADING_SPEED = 5;

        public enum BallState { Normal, Loading, Destroyed, Falling, Dark };
        private Rectangle SourceRectangle;
        private BallState State;

        private int DestroyedFrameIndex;
        private double DestroyedGamedTime;    

        private Ball Ball;
        
        static BallAnimationHelper()
        {
            CURRENT_BALL_POSITION_UNIT = new Vector2(303, 365) - new Vector2(232, 400);
            CURRENT_BALL_POSITION_UNIT.Normalize();
        }

        public BallAnimationHelper(Ball ball) 
        {
           Ball = ball;
           State = BallState.Normal;
           SourceRectangle = GetColorNormalRectangle(ball.Color);
        }

        public void Destroy() 
        {
            State = BallState.Destroyed;
            DestroyedGamedTime = 0;
        }

        public void FallDown() 
        {
            State = BallState.Falling;
        }

        public void Load() 
        {
            State = BallState.Loading;
        }

        public void GoDark() 
        {
            State = BallState.Dark;
        }

        public void Update(GameTime gameTime, Game1 game)
        {
            switch (State) 
            {
                case BallState.Falling:
                    Ball.Position.Y += FALLING_SPEED;
                    break;

                case BallState.Destroyed:
                    if (DestroyedGamedTime == 0) 
                    {
                        Vector2 newTopLeft = GetSpriteDestroyedBallTopLeftForColor(Ball.Color);
                        SourceRectangle = new Rectangle(
                                            (int)newTopLeft.X,
                                            (int)newTopLeft.Y,
                                            DESTROYED_BALL_WIDTH,
                                            DESTROYED_BALL_HEIGHT);
                        DestroyedGamedTime = gameTime.TotalGameTime.TotalMilliseconds;
                        DestroyedFrameIndex = 0;
                    }
                    else if (DestroyedGamedTime + DESTROYED_BALL_FRAME_INTERVAL*(DestroyedFrameIndex)
                                < gameTime.TotalGameTime.TotalMilliseconds)
                    {
                        DestroyedFrameIndex++;
                        if (DestroyedFrameIndex < DESTROYED_BALL_NUMBER_OF_FRAMES)
                        {
                            SourceRectangle = new Rectangle(
                                SourceRectangle.Left + SPRITE_DISTANCE_DESTROYED_BALL_WIDTH,
                                SourceRectangle.Top,
                                SourceRectangle.Width,
                                SourceRectangle.Height
                                );
                        }
                        else 
                        {
                            SourceRectangle = new Rectangle(0,0,0,0);
                        }
                    }
                    break;

                case BallState.Loading:
                    if (Ball.Position.X >= BallsRepository.CURRENT_BALL_POSITION.X
                        && Ball.Position.Y <= BallsRepository.CURRENT_BALL_POSITION.Y)
                    {
                        State = BallState.Normal;
                        Ball.Position = BallsRepository.CURRENT_BALL_POSITION;
                    }
                    Ball.Position += CURRENT_BALL_POSITION_UNIT * LOADING_SPEED;
 
                    break;
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Game1 game)
        {
            switch (State) 
            {
                case BallState.Falling:
                case BallState.Loading:
                case BallState.Normal:
                    spriteBatch.Draw(
                        texture: game.GameElements.SpriteSheet,
                        position: Ball.Position,
                        sourceRectangle: SourceRectangle,
                        color: Color.White,
                        rotation: 0,
                        origin: Vector2.Zero,
                        scale: 2.0f,
                        effects: SpriteEffects.None,
                        layerDepth: 0.6f
                    );
                    break;

                case BallState.Destroyed:
                    spriteBatch.Draw(
                        texture: game.GameElements.PoppedBallsSpriteSheet,
                        position: GetPositionWithCenterOnBallPosition(SourceRectangle),
                        sourceRectangle: SourceRectangle,
                        color: Microsoft.Xna.Framework.Color.White,
                        rotation: 0,
                        origin: Vector2.Zero,
                        scale: 2.0f,
                        effects: SpriteEffects.None,
                        layerDepth: 0.6f
                    );
                    break;

                case BallState.Dark:
                    spriteBatch.Draw(
                        texture: game.GameElements.SpriteSheet,
                        position: Ball.Position,
                        sourceRectangle: SourceRectangle,
                        color: Color.Gray,
                        rotation: 0,
                        origin: Vector2.Zero,
                        scale: 2.0f,
                        effects: SpriteEffects.None,
                        layerDepth: 0.6f
                    );
                    break;
            }
        }

        private Vector2 GetPositionWithCenterOnBallPosition(Rectangle rectangle) 
        {
            Point BallCenter = Ball.Rectangle.Center;
            return new Vector2(BallCenter.X - rectangle.Width / 2 -15, BallCenter.Y - rectangle.Height / 2 - 10);
        }

        private static Rectangle GetColorNormalRectangle(Ball.BallColor color)
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

        private static Vector2 GetSpriteDestroyedBallTopLeftForColor(Ball.BallColor color) 
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
                    top = 1;
                    left = 0;
                    break;

                case Ball.BallColor.Orange:
                    top = 1;
                    left = 2;
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
                    top = 0;
                    left = 1;
                    break;

                case Ball.BallColor.Yellow:
                    top = 0;
                    left = 2;
                    break;

                default:
                    throw new Exception(String.Format("color {0} does not exist", color));            
            }
            return TOP_LEFT_DESTROYED + new Vector2(
                                            left * SPRITE_DISTANCE_DESTROYED_BALL_COLUMN_WIDTH,
                                            top * SPRITE_DISTANCE_DESTROYED_BALL_ROW_HEIGHT);
        }
    }
}
