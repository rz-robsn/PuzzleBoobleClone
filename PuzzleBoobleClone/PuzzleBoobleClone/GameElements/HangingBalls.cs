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
        private static int NUMBER_OF_ROWS = 12;
        private static int NUMBER_OF_COLUMNS_EVEN = 8;
        private static int NUMBER_OF_COLUMNS_ODD = 7;
        private static int ODD_ROW_OFFSET = 10;
        
        /// <summary>
        /// Top Left Position of the Top Left Slot
        /// </summary>
        public Vector2 Position;

        private List<List<Ball>> Balls;


        public HangingBalls(Vector2 position)
        {
            Position = position;

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
            SetBallAtPosition(1, 0, new Ball(Vector2.Zero, Ball.BallColor.Blue));
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
                ball.Position = Position + new Vector2(colIndex * Ball.SRC_RECTANGLE_WIDTH, rowIndex * Ball.SRC_RECTANGLE_HEIGHT);
            }
            else 
            {
                ball.Position = Position + new Vector2(ODD_ROW_OFFSET + colIndex * Ball.SRC_RECTANGLE_WIDTH, rowIndex * Ball.SRC_RECTANGLE_HEIGHT);
            }
        }

    }
}
