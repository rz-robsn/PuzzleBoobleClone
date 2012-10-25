using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Timers;

namespace PuzzleBoobleClone.GameElements
{
    public class BallsRepository : GameElement
    {
        private static readonly Vector2 CURRENT_BALL_POSITION = new Vector2(303, 365);
        private static readonly Vector2 NEXT_BALL_POSITION = new Vector2(232, 400);

        private static float MOVING_BALL_SPEED = 12;

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
        private Bounds FieldBounds;

        private KeyboardState PreviousKeyState;

        private Timer ThrowBallTimer;

        public BallsRepository(AimingArrow arrow, Bounds bounds, HangingBalls hangingBalls) 
        {
            Arrow = arrow;
            FieldBounds = bounds;

            HangingBalls = hangingBalls;
            SetCurrentBall(new Ball(CURRENT_BALL_POSITION, HangingBalls.GetRandomColor()));
            SetNextBall(new Ball(NEXT_BALL_POSITION, HangingBalls.GetRandomColor()));

            ThrowBallTimer = new Timer();
            ThrowBallTimer.AutoReset = false;
            ThrowBallTimer.Interval = 10000;
            ResetTimer();
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
                SetNextBall(new Ball(NEXT_BALL_POSITION, HangingBalls.GetRandomColor()));
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
            ThrowBallTimer.Stop();
            ResetTimer();
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

        private bool BallCollideWithSideBounds(Ball ball) 
        {
            Rectangle ballRectangle = ball.Rectangle;

            return (ballRectangle.Left < FieldBounds.Rectangle.Left && FieldBounds.Rectangle.Left < ballRectangle.Right)
                    || (ballRectangle.Left < FieldBounds.Rectangle.Right && FieldBounds.Rectangle.Right < ballRectangle.Right);
        }

        private bool BallRectangleCollidesWithBottomBound(Ball ball)
        {
            return ball.Rectangle.Top < FieldBounds.Rectangle.Bottom && FieldBounds.Rectangle.Bottom < ball.Rectangle.Bottom;
        }

        private void ResetTimer() 
        {
            ThrowBallTimer.Elapsed += new ElapsedEventHandler(delegate(object source, ElapsedEventArgs e)
            {
                ThrowCurrentBall();
            });
            ThrowBallTimer.Start();        
        }
    }
}
