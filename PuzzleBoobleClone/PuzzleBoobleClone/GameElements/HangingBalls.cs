using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PuzzleBoobleClone.GameElements
{
    public class HangingBalls : GameElement
    {
        /// <summary>
        /// Represents a Slot in the Ball Field
        /// </summary>
        public class BallSlot
        {
            public int RowIndex;
            public int ColumnIndex;

            public BallSlot(int rowIndex, int columnIndex)
            {
                RowIndex = rowIndex;
                ColumnIndex = columnIndex;
            }
        }

        private static int NUMBER_OF_ROWS = 12;
        private static int NUMBER_OF_COLUMNS_EVEN = 8;
        private static int NUMBER_OF_COLUMNS_ODD = 7;
        private static int ODD_ROW_OFFSET = 16;

        /// <summary>
        /// Top Left Position of the Top Left Slot
        /// </summary>
        public Vector2 Position
        {
            get
            {
                return new Vector2(Bounds.Left, Bounds.Top);
            }
        }

        private List<List<Ball>> Balls;

        private Rectangle Bounds;

        public HangingBalls(Rectangle bounds)
        {
            Bounds = bounds;

            Balls = new List<List<Ball>>(NUMBER_OF_ROWS);
            for (int i = 0; i < NUMBER_OF_ROWS; i++)
            {
                Balls.Insert(i, new List<Ball>(i % 2 == 0 ? NUMBER_OF_COLUMNS_EVEN : NUMBER_OF_COLUMNS_ODD));
                if (i % 2 == 0)
                {
                    for (int j = 0; j < NUMBER_OF_COLUMNS_EVEN; j++)
                    {
                        Balls.ElementAt(i).Add(null);
                    }
                }
                else
                {
                    for (int j = 0; j < NUMBER_OF_COLUMNS_ODD; j++)
                    {
                        Balls.ElementAt(i).Add(null);
                    }
                }
            }

            // Adding Balls
            SetBallAtPosition(0, 0, new Ball(Vector2.Zero, Ball.BallColor.Blue));
            SetBallAtPosition(0, 1, new Ball(Vector2.Zero, Ball.BallColor.Blue));
            SetBallAtPosition(0, 2, new Ball(Vector2.Zero, Ball.BallColor.Blue));
            SetBallAtPosition(0, 3, new Ball(Vector2.Zero, Ball.BallColor.Blue));
            SetBallAtPosition(0, 4, new Ball(Vector2.Zero, Ball.BallColor.Blue));
            SetBallAtPosition(0, 5, new Ball(Vector2.Zero, Ball.BallColor.Blue));
            SetBallAtPosition(0, 6, new Ball(Vector2.Zero, Ball.BallColor.Blue));
            SetBallAtPosition(0, 7, new Ball(Vector2.Zero, Ball.BallColor.Blue));
            SetBallAtPosition(1, 0, new Ball(Vector2.Zero, Ball.BallColor.Blue));
            SetBallAtPosition(1, 1, new Ball(Vector2.Zero, Ball.BallColor.Blue));
            SetBallAtPosition(1, 2, new Ball(Vector2.Zero, Ball.BallColor.Blue));
            SetBallAtPosition(1, 3, new Ball(Vector2.Zero, Ball.BallColor.Blue));
            SetBallAtPosition(1, 4, new Ball(Vector2.Zero, Ball.BallColor.Blue));
            SetBallAtPosition(1, 5, new Ball(Vector2.Zero, Ball.BallColor.Blue));
            SetBallAtPosition(1, 6, new Ball(Vector2.Zero, Ball.BallColor.Blue));
        }

        public void Update(GameTime gameTime, Game1 game)
        {
            foreach (List<Ball> list in Balls)
            {
                foreach (Ball ball in list)
                {
                    if (ball != null)
                    {
                        ball.Update(gameTime, game);
                    }
                }
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Game1 game)
        {
            foreach (List<Ball> list in Balls)
            {
                foreach (Ball ball in list)
                {
                    if (ball != null)
                    {
                        ball.Draw(gameTime, spriteBatch, game);
                    }
                }
            }
        }

        public void SetBallAtPosition(int rowIndex, int colIndex, Ball ball)
        {
            if (Balls.ElementAt(rowIndex).ElementAt(colIndex) != null)
            {
                throw new Exception(String.Format("There is already a ball {0} at position ({1},{2})",
                                                    ball, rowIndex.ToString(), colIndex.ToString()));
            }
            Balls.ElementAt(rowIndex).Insert(colIndex, ball);

            if (rowIndex % 2 == 0)
            {
                ball.Position = Position + new Vector2(colIndex * Ball.RECTANGLE_WIDTH, rowIndex * Ball.RECTANGLE_HEIGHT);
            }
            else
            {
                ball.Position = Position + new Vector2(ODD_ROW_OFFSET + colIndex * Ball.RECTANGLE_WIDTH, rowIndex * Ball.RECTANGLE_HEIGHT);
            }
            ball.Speed = 0;
        }

        public bool AnyBallIntersectsWithBall(Ball ball)
        {
            foreach (List<Ball> list in Balls)
            {
                foreach (Ball b in list)
                {
                    if (b != null && b.Rectangle.Intersects(ball.Rectangle))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void SetBallToNearestSlot(Ball ball)
        {
            float nearestRowRatio = Math.Abs(ball.Rectangle.Center.Y - Position.Y) / Bounds.Height;
            int nearestRowIndex = (int)Math.Floor(nearestRowRatio * NUMBER_OF_ROWS);

            int nearestColumnIndex;
            if (nearestRowIndex % 2 == 0)
            {
                float nearestColumnRatio = Math.Abs(ball.Rectangle.Center.X - Position.X) / Bounds.Width;
                nearestColumnIndex = (int)Math.Floor(nearestColumnRatio * NUMBER_OF_COLUMNS_EVEN);
            }
            else
            {
                float nearestColumnRatio = Math.Abs(ball.Position.X - Position.X + ODD_ROW_OFFSET) / (Bounds.Width - ODD_ROW_OFFSET);
                nearestColumnIndex = (int)MathHelper.Clamp((float)Math.Floor(nearestColumnRatio * NUMBER_OF_COLUMNS_EVEN), 0, ODD_ROW_OFFSET);
            }

            SetBallAtPosition(nearestRowIndex, nearestColumnIndex, ball);
        }
    }
}
