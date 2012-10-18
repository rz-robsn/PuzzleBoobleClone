using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PuzzleBoobleClone.GameElements
{
    public class BallsRepository : GameElement
    {
        private static readonly Vector2 CURRENT_BALL_POSITION = new Vector2(303, 365);
        private static readonly Vector2 NEXT_BALL_POSITION = new Vector2(232, 400);

        private static float MOVING_BALL_SPEED = 18;

        /// <summary>
        /// The Bounds Of the Ball Field.
        /// </summary>
        private static Rectangle RectangleBounds = new Rectangle(190, 45, 259, 322);

        /// <summary>
        /// The Next Ball that is going to be thrown
        /// </summary>
        public Ball CurrentBall;

        /// <summary>
        /// The Next Ball to throw After the Current Ball
        /// </summary>
        public Ball NextBall;

        private HangingBalls HangingBalls;

        private AimingArrow Arrow;
        private KeyboardState PreviousKeyState;

        public BallsRepository(AimingArrow arrow) 
        {
            Arrow = arrow;

            SetCurrentBall(new Ball(CURRENT_BALL_POSITION, GetRandomBallColor()));
            SetNextBall(new Ball(NEXT_BALL_POSITION, GetRandomBallColor()));
            HangingBalls = new HangingBalls(RectangleBounds);
        }

        public void Update(GameTime gameTime, Game1 game)
        {
            KeyboardState state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.Space) && !PreviousKeyState.IsKeyDown(Keys.Space)
                && !CurrentBall.IsMoving())
            {
                ThrowCurrentBall();
            }
            PreviousKeyState = state;

            if (BallCollideWithSideBounds(CurrentBall))
            {
                CurrentBall.Direction.X *= -1;
            }

            HangingBalls.BallSlot interSectingSlot = HangingBalls.BallsIntersectingWithBall(CurrentBall);
            if (CurrentBall.IsMoving() && (interSectingSlot != null || HangingBalls.BallIntersectsWithUpperBounds(CurrentBall)) )
            {
                HangingBalls.SetBallToNearestSlot(CurrentBall, interSectingSlot);
                SetCurrentBall(NextBall);
                SetNextBall(new Ball(NEXT_BALL_POSITION, GetRandomBallColor()));
            }

            CurrentBall.Update(gameTime, game);
            NextBall.Update(gameTime, game);
            HangingBalls.Update(gameTime, game);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Game1 game)
        {
            CurrentBall.Draw(gameTime, spriteBatch, game);
            NextBall.Draw(gameTime, spriteBatch, game);
            HangingBalls.Draw(gameTime, spriteBatch, game);
        }

        public void ThrowCurrentBall() 
        {
            CurrentBall.Direction = Arrow.GetDirectionVector();
            CurrentBall.Speed = MOVING_BALL_SPEED;
        }

        private void SetCurrentBall(Ball ball) 
        {
            CurrentBall = ball;
            CurrentBall.Position = CURRENT_BALL_POSITION;
        }

        private void SetNextBall(Ball ball) 
        {
            NextBall = ball;
            NextBall.Position = NEXT_BALL_POSITION;
        }

        private static bool BallCollideWithSideBounds(Ball ball) 
        {
            Rectangle ballRectangle = ball.Rectangle;

            return (ballRectangle.Left < RectangleBounds.Left && RectangleBounds.Left < ballRectangle.Right)
                    || (ballRectangle.Left < RectangleBounds.Right && RectangleBounds.Right < ballRectangle.Right);
        }

        private static bool BallRectangleCollidesWithBottomBound(Ball ball)
        {
            return ball.Rectangle.Top < RectangleBounds.Bottom && RectangleBounds.Bottom < ball.Rectangle.Bottom;
        }

        private static Ball.BallColor GetRandomBallColor() 
        {
            return Ball.BallColor.Blue;
        }
    }
}
