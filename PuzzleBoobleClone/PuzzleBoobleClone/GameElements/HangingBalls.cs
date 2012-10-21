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

            public override bool Equals(object obj)
            {
                if (!(obj is BallSlot))
                {
                    return false;
                }
                else 
                {
                    return this.Equals((BallSlot)obj);
                }
            }

            public bool Equals(BallSlot otherSlot)
            {
                return RowIndex == otherSlot.RowIndex && ColumnIndex == otherSlot.ColumnIndex;
            }

            public override string ToString()
            {
                return "Slot(" + RowIndex + "," + ColumnIndex + ")";
            } 
        }

        public class SlotOccupiedException : Exception
        {
            public int RowIndex;
            public int ColumnIndex;

            public SlotOccupiedException(int rowIndex, int columnIndex)
                : base(String.Format("There is already a ball at Slot ({0},{1})",
                            rowIndex.ToString(), columnIndex.ToString()))
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
            SetBallAtPosition(0, 1, new Ball(Vector2.Zero, Ball.BallColor.DarkGrey));
            SetBallAtPosition(0, 2, new Ball(Vector2.Zero, Ball.BallColor.Green));
            SetBallAtPosition(1, 0, new Ball(Vector2.Zero, Ball.BallColor.Orange));
            SetBallAtPosition(1, 1, new Ball(Vector2.Zero, Ball.BallColor.Purple));
            SetBallAtPosition(1, 2, new Ball(Vector2.Zero, Ball.BallColor.Red));
            SetBallAtPosition(1, 4, new Ball(Vector2.Zero, Ball.BallColor.Silver));
            SetBallAtPosition(1, 5, new Ball(Vector2.Zero, Ball.BallColor.Yellow));
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
                throw new SlotOccupiedException(rowIndex, colIndex);
            }
            Balls[rowIndex][colIndex] = ball;

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

        public bool BallIntersectsWithUpperBounds(Ball ball) 
        {
            return ball.Rectangle.Center.Y < Bounds.Top;
        }

        public BallSlot BallsIntersectingWithBall(Ball ball)
        {
            for (int i = 0; i < NUMBER_OF_ROWS; i++)
            {
                for (int j = 0; j < GetNumberOfColumnForRow(i); j++)
                {
                    Ball b = Balls.ElementAt(i).ElementAt(j);
                    if (b != null && b.Rectangle.Intersects(ball.Rectangle))
                    {
                        return new BallSlot(i, j);
                    }
                }
            }
            return null;
        }

        public void SetBallToNearestSlot(Ball ball, BallSlot interSectingSlot)
        {
            float nearestRowRatio = Math.Abs(ball.Rectangle.Center.Y - Position.Y) / (Ball.RECTANGLE_HEIGHT * NUMBER_OF_ROWS);
            int nearestRowIndex = (int)MathHelper.Clamp( (float)Math.Floor(nearestRowRatio * NUMBER_OF_ROWS), 0, NUMBER_OF_ROWS-1);

            int nearestColumnIndex;
            if (nearestRowIndex % 2 == 0)
            {
                float nearestColumnRatio = Math.Abs(ball.Rectangle.Center.X - Position.X) / Bounds.Width;
                nearestColumnIndex = (int)MathHelper.Clamp((float)Math.Floor(nearestColumnRatio * NUMBER_OF_COLUMNS_EVEN), 0, NUMBER_OF_COLUMNS_EVEN-1);
            }
            else
            {
                float nearestColumnRatio = Math.Abs(ball.Rectangle.Center.X + ODD_ROW_OFFSET - Position.X) / (Bounds.Width - 2*ODD_ROW_OFFSET) - 0.15f;
                nearestColumnIndex = (int)MathHelper.Clamp((float)Math.Floor(nearestColumnRatio * NUMBER_OF_COLUMNS_ODD), 0, NUMBER_OF_COLUMNS_ODD-1);
            }

            try
            {
                SetBallAtPosition(nearestRowIndex, nearestColumnIndex, ball);
                DestroyAlignedPieceAtSlot(nearestRowIndex, nearestColumnIndex);
            }
            catch (SlotOccupiedException ex) 
            {
                //nearestRowIndex = GetClampedRowIndex(nearestRowIndex+1);
                //SetBallAtPosition(GetClampedRowIndex(nearestRowIndex),
                //                  GetClampledColumnIndex(nearestRowIndex, nearestColumnIndex), 
                //                  ball);
            }
        }

        public Ball.BallColor GetRandomColor() 
        {
            return Ball.BallColor.Orange;
            Random random = new Random();
            int randomNumber = random.Next(0, 8);

            switch(randomNumber)
            {
                case 0:
                    return Ball.BallColor.Blue;
                case 1:
                    return Ball.BallColor.DarkGrey;
                case 2:
                    return Ball.BallColor.Green;
                case 3:
                    return Ball.BallColor.Orange;
                case 4:
                    return Ball.BallColor.Purple;
                case 5:
                    return Ball.BallColor.Red;
                case 6:
                    return Ball.BallColor.Silver;
                case 7:
                    return Ball.BallColor.Yellow;
                default:
                    return Ball.BallColor.Blue;
            }
        }

        private void DestroyAlignedPieceAtSlot(int rowIndex, int columnIndex)
        {
            List<BallSlot> alignedSlots = GetAllPieceOfSameColorAlignedAtSlot(rowIndex, columnIndex);

            if (alignedSlots.Count >= 3) 
            {
                alignedSlots.ForEach(slot => DestroyBallAtSlot(slot));
            }
        }

        private List<BallSlot> GetAllPieceOfSameColorAlignedAtSlot(int rowIndex, int columnIndex) 
        {
            List<BallSlot> slots = new List<BallSlot>();
            GetAllPieceOfSameColorAlignedAtSlot(new BallSlot(rowIndex, columnIndex), GetBallAtSlot(rowIndex, columnIndex).Color, slots);
            return slots;
        }

        private void GetAllPieceOfSameColorAlignedAtSlot(BallSlot ballSlot, Ball.BallColor color, List<BallSlot> slots) 
        {
            Ball ball = GetBallAtSlot(ballSlot.RowIndex, ballSlot.ColumnIndex);

            if (ball != null && ball.Color == color && !slots.Contains(ballSlot))
            {
                slots.Add(ballSlot);
                GetAllAdjacentSlots(ballSlot).ForEach(slot => GetAllPieceOfSameColorAlignedAtSlot(slot, color, slots));
            }
        }

        private void DestroyBallAtSlot(BallSlot slot) 
        {
            Ball ball = GetBallAtSlot(slot);
            if (ball != null)
            {
                ball.Destroy();
            }
            Balls[slot.RowIndex][slot.ColumnIndex] = null;
        }

        private Ball GetBallAtSlot(BallSlot slot)
        {
            return GetBallAtSlot(slot.RowIndex, slot.ColumnIndex);
        }

        private Ball GetBallAtSlot(int rowIndex, int columnIndex) 
        {
            return Balls.ElementAt(rowIndex).ElementAt(columnIndex);
        }

        private List<BallSlot> GetAllAdjacentSlots(BallSlot slot) 
        {
            List<BallSlot> slots = new List<BallSlot>(6);

            slots.Add(new BallSlot(GetClampedRowIndex(slot.RowIndex), GetClampledColumnIndex(slot.RowIndex, slot.ColumnIndex-1)));
            slots.Add(new BallSlot(GetClampedRowIndex(slot.RowIndex), GetClampledColumnIndex(slot.RowIndex, slot.ColumnIndex+1)));
            
            if (slot.RowIndex % 2 == 0)
            {
                slots.Add(new BallSlot(GetClampedRowIndex(slot.RowIndex - 1), GetClampledColumnIndex(slot.RowIndex - 1, slot.ColumnIndex - 1)));
                slots.Add(new BallSlot(GetClampedRowIndex(slot.RowIndex - 1), GetClampledColumnIndex(slot.RowIndex - 1, slot.ColumnIndex)));
                slots.Add(new BallSlot(GetClampedRowIndex(slot.RowIndex + 1), GetClampledColumnIndex(slot.RowIndex + 1, slot.ColumnIndex - 1)));
                slots.Add(new BallSlot(GetClampedRowIndex(slot.RowIndex + 1), GetClampledColumnIndex(slot.RowIndex + 1, slot.ColumnIndex)));
            }
            else 
            {
                slots.Add(new BallSlot(GetClampedRowIndex(slot.RowIndex - 1), GetClampledColumnIndex(slot.RowIndex - 1, slot.ColumnIndex)));
                slots.Add(new BallSlot(GetClampedRowIndex(slot.RowIndex - 1), GetClampledColumnIndex(slot.RowIndex - 1, slot.ColumnIndex +1)));
                slots.Add(new BallSlot(GetClampedRowIndex(slot.RowIndex + 1), GetClampledColumnIndex(slot.RowIndex + 1, slot.ColumnIndex)));
                slots.Add(new BallSlot(GetClampedRowIndex(slot.RowIndex + 1), GetClampledColumnIndex(slot.RowIndex + 1, slot.ColumnIndex +1)));            
            }

            return slots;
        }

        private static int GetClampedRowIndex(int rowIndex) 
        {
            return (int)MathHelper.Clamp(rowIndex, 0, NUMBER_OF_ROWS-1);
        }

        private static int GetClampledColumnIndex(int rowIndex, int columnIndex) 
        {
            return (int)MathHelper.Clamp(columnIndex, 0, GetNumberOfColumnForRow(rowIndex)-1);
        }

        private static int GetNumberOfColumnForRow(int rowIndex) 
        {
            return rowIndex % 2 == 0 ? NUMBER_OF_COLUMNS_EVEN : NUMBER_OF_COLUMNS_ODD;
        }
    }
}
