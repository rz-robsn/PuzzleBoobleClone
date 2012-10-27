using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PuzzleBoobleClone.GameElements
{
    public class HangingBalls : GameElement, BoundsObserver
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
                return new Vector2(Bounds.Rectangle.Left, Bounds.Rectangle.Top);
            }
        }

        private List<List<Ball>> Balls;
        private List<Ball> DeletedBalls;

        private Bounds Bounds;
        private HangingBallsObserver Observer;
        private Score CurrentScore;

        public HangingBalls(Bounds bounds, HangingBallsObserver observer, Score score, GameElementsRepository.Level level)
        {
            Bounds = bounds;
            bounds.Observer = this;
            CurrentScore = score;

            Observer = observer;

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

            DeletedBalls = new List<Ball>();

            // Adding Balls
            switch(level)
            {
                case GameElementsRepository.Level.LEVELONE:
                    SetBallAtPosition(0, 0, new Ball(Vector2.Zero, Ball.BallColor.Red));
                    SetBallAtPosition(0, 1, new Ball(Vector2.Zero, Ball.BallColor.Red));
                    SetBallAtPosition(0, 2, new Ball(Vector2.Zero, Ball.BallColor.Yellow));
                    SetBallAtPosition(0, 3, new Ball(Vector2.Zero, Ball.BallColor.Yellow));
                    SetBallAtPosition(0, 4, new Ball(Vector2.Zero, Ball.BallColor.Blue));
                    SetBallAtPosition(0, 5, new Ball(Vector2.Zero, Ball.BallColor.Blue));
                    SetBallAtPosition(0, 6, new Ball(Vector2.Zero, Ball.BallColor.Green));
                    SetBallAtPosition(0, 7, new Ball(Vector2.Zero, Ball.BallColor.Green));
                    SetBallAtPosition(1, 0, new Ball(Vector2.Zero, Ball.BallColor.Red));
                    SetBallAtPosition(1, 1, new Ball(Vector2.Zero, Ball.BallColor.Red));
                    SetBallAtPosition(1, 2, new Ball(Vector2.Zero, Ball.BallColor.Yellow));
                    SetBallAtPosition(1, 3, new Ball(Vector2.Zero, Ball.BallColor.Yellow));
                    SetBallAtPosition(1, 4, new Ball(Vector2.Zero, Ball.BallColor.Blue));
                    SetBallAtPosition(1, 5, new Ball(Vector2.Zero, Ball.BallColor.Blue));
                    SetBallAtPosition(1, 6, new Ball(Vector2.Zero, Ball.BallColor.Green));
                    SetBallAtPosition(2, 0, new Ball(Vector2.Zero, Ball.BallColor.Blue));
                    SetBallAtPosition(2, 1, new Ball(Vector2.Zero, Ball.BallColor.Blue));
                    SetBallAtPosition(2, 2, new Ball(Vector2.Zero, Ball.BallColor.Green));
                    SetBallAtPosition(2, 3, new Ball(Vector2.Zero, Ball.BallColor.Green));
                    SetBallAtPosition(2, 4, new Ball(Vector2.Zero, Ball.BallColor.Red));
                    SetBallAtPosition(2, 5, new Ball(Vector2.Zero, Ball.BallColor.Red));
                    SetBallAtPosition(2, 6, new Ball(Vector2.Zero, Ball.BallColor.Yellow));
                    SetBallAtPosition(2, 7, new Ball(Vector2.Zero, Ball.BallColor.Yellow));
                    SetBallAtPosition(3, 0, new Ball(Vector2.Zero, Ball.BallColor.Blue));
                    SetBallAtPosition(3, 1, new Ball(Vector2.Zero, Ball.BallColor.Green));
                    SetBallAtPosition(3, 2, new Ball(Vector2.Zero, Ball.BallColor.Green));
                    SetBallAtPosition(3, 3, new Ball(Vector2.Zero, Ball.BallColor.Red));
                    SetBallAtPosition(3, 4, new Ball(Vector2.Zero, Ball.BallColor.Red));
                    SetBallAtPosition(3, 5, new Ball(Vector2.Zero, Ball.BallColor.Yellow));
                    SetBallAtPosition(3, 6, new Ball(Vector2.Zero, Ball.BallColor.Yellow));
                    break;

                case GameElementsRepository.Level.LEVELTWO:
                    SetBallAtPosition(0, 3, new Ball(Vector2.Zero, Ball.BallColor.DarkGrey));
                    SetBallAtPosition(0, 4, new Ball(Vector2.Zero, Ball.BallColor.DarkGrey));
                    SetBallAtPosition(1, 3, new Ball(Vector2.Zero, Ball.BallColor.Blue));
                    SetBallAtPosition(2, 4, new Ball(Vector2.Zero, Ball.BallColor.Green));
                    SetBallAtPosition(3, 3, new Ball(Vector2.Zero, Ball.BallColor.Blue));
                    SetBallAtPosition(4, 4, new Ball(Vector2.Zero, Ball.BallColor.Purple));
                    SetBallAtPosition(5, 3, new Ball(Vector2.Zero, Ball.BallColor.Green));
                    SetBallAtPosition(6, 4, new Ball(Vector2.Zero, Ball.BallColor.Blue));
                    SetBallAtPosition(7, 3, new Ball(Vector2.Zero, Ball.BallColor.Silver));
                    break;
            }  
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

            DeletedBalls.ForEach(ball => ball.Update(gameTime, game));
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
            DeletedBalls.ForEach(ball => ball.Draw(gameTime, spriteBatch, game));
        }

        public void SetBallAtPosition(int rowIndex, int colIndex, Ball ball)
        {
            if (Balls.ElementAt(rowIndex).ElementAt(colIndex) != null)
            {
                throw new SlotOccupiedException(rowIndex, colIndex);
            }
            Balls[rowIndex][colIndex] = ball;

            RefreshBallPosition(rowIndex, colIndex, ball);
        }

        private void RefreshBallPosition(int rowIndex, int colIndex, Ball ball)
        {
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
            return ball.Rectangle.Center.Y < Bounds.Rectangle.Top;
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
            int nearestRowIndex;
            int nearestColumnIndex;

            if (interSectingSlot != null)
            {
                // Get the nearest empty slot.
                List<BallSlot> emptySlots = GetAllAdjacentLowerSlots(interSectingSlot);
                emptySlots.AddRange(GetAdjacentSlotsOnSameRow(interSectingSlot));
                emptySlots.RemoveAll(slot => GetBallAtSlot(slot) != null);

                Vector2 ballCenter = new Vector2(ball.Rectangle.Center.X, ball.Rectangle.Center.Y);
                emptySlots.Sort(delegate(BallSlot slot, BallSlot otherSlot)
                {
                    float length1 = (GetSlotCenter(slot) - ballCenter).Length();
                    float length2 = (GetSlotCenter(otherSlot) - ballCenter).Length();
                    return length1.CompareTo(length2);
                });

                if (emptySlots.Count > 0)
                {
                    nearestRowIndex = emptySlots.ElementAt(0).RowIndex;
                    nearestColumnIndex = emptySlots.ElementAt(0).ColumnIndex;
                }
                else 
                {
                    // There is no room left to place that ball.
                    Observer.OnPlayerLoses();
                    nearestColumnIndex = interSectingSlot.ColumnIndex;
                    nearestRowIndex = interSectingSlot.RowIndex;
                }
            }
            else // The ball reached the ceiling
            {
                float nearestRowRatio = Math.Abs(ball.Rectangle.Center.Y - Position.Y) / (Ball.RECTANGLE_HEIGHT * NUMBER_OF_ROWS);
                nearestRowIndex = (int)MathHelper.Clamp((float)Math.Floor(nearestRowRatio * NUMBER_OF_ROWS), 0, NUMBER_OF_ROWS - 1);

                if (nearestRowIndex % 2 == 0)
                {
                    float nearestColumnRatio = Math.Abs(ball.Rectangle.Center.X - Position.X) / Bounds.Rectangle.Width;

                    nearestColumnIndex = (int)MathHelper.Clamp((float)Math.Floor(nearestColumnRatio * NUMBER_OF_COLUMNS_EVEN), 0, NUMBER_OF_COLUMNS_EVEN - 1);
                }
                else
                {
                    float nearestColumnRatio = Math.Abs(ball.Rectangle.Center.X + ODD_ROW_OFFSET - Position.X) / (Bounds.Rectangle.Width - 2 * ODD_ROW_OFFSET);
                    nearestColumnIndex = (int)MathHelper.Clamp((float)Math.Floor(nearestColumnRatio * NUMBER_OF_COLUMNS_ODD), 0, NUMBER_OF_COLUMNS_ODD - 1);
                }            
            }

            try
            {
                if (GetAllBallsAdjacentToSlot(new BallSlot(nearestRowIndex, nearestColumnIndex)).Count == 0) 
                {
                     nearestColumnIndex = (ball.Direction.X > 0) ? GetClampledColumnIndex(nearestRowIndex, nearestColumnIndex + 1) 
                                                                : GetClampledColumnIndex(nearestRowIndex, nearestColumnIndex - 1);

                     if (GetAllBallsAdjacentToSlot(new BallSlot(nearestRowIndex, nearestColumnIndex)).Count == 0) 
                     {
                         nearestColumnIndex = (ball.Direction.X > 0) ? GetClampledColumnIndex(nearestRowIndex, nearestColumnIndex - 2)
                                                                    : GetClampledColumnIndex(nearestRowIndex, nearestColumnIndex + 2);
                     
                     }
                }
                 
                SetBallAtPosition(nearestRowIndex, nearestColumnIndex, ball);
                DestroyAlignedPieceAtSlot(nearestRowIndex, nearestColumnIndex);

                int numOfBallsFallen = FallDownAllBallsWithNoUpperAdjacentBalls();

                CurrentScore.Value += (numOfBallsFallen > 0) ? (int)Math.Pow(2, FallDownAllBallsWithNoUpperAdjacentBalls()) * 10
                                                             : 0;  
                CheckIfPlayerWins();
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
            // Pick One color out of those in the grid.
            IEnumerable<Ball> BallPool = Balls.SelectMany(list => list).Where(ball => ball != null);

            if (BallPool.Count() > 0)
            {
                Random random = new Random();
                int randomNumber = random.Next(0, BallPool.Count());
                return BallPool.ElementAt(randomNumber).Color;
            }
            else 
            {
                return Ball.BallColor.Blue;
            }
        }

        public void OnOneRowRemoved(Bounds bound)
        {
            // Reposition All Rows
            for (int i = 0; i < NUMBER_OF_ROWS; i++)
            {
                List<BallSlot> slotListCandidate = new List<BallSlot>();
                for (int j = 0; j < GetNumberOfColumnForRow(i); j++)
                {
                    Ball ball = GetBallAtSlot(i, j);
                    if (ball != null)
                    {
                        RefreshBallPosition(i, j, ball);
                    }
                }
            }

            CheckIfPlayerLost();
        }

        /// <summary>
        /// "Pops" the ball as well as any other ball of the same color next to it 
        /// if there are 3 or more of these balls, and updates the score.
        /// </summary>
        /// <param name="rowIndex">The RowIndex of the ball to pop.</param>
        /// <param name="columnIndex">The ColumnIndex of the ball to pop.</param>
        private void DestroyAlignedPieceAtSlot(int rowIndex, int columnIndex)
        {
            List<BallSlot> alignedSlots = GetAllPieceOfSameColorAlignedAtSlot(rowIndex, columnIndex);

            if (alignedSlots.Count >= 3) 
            {
                alignedSlots.ForEach(slot => DestroyBallAtSlot(slot));
                CurrentScore.Value += alignedSlots.Count * Score.POINTS_PER_BALLS_POPPED;
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

        /// <summary>
        /// Falls all balls that have no Upper adjacent Balls.
        /// </summary>
        /// <returns>The Number of balls that fell.</returns>
        private int FallDownAllBallsWithNoUpperAdjacentBalls() 
        {
            List<BallSlot> slots = GetAllBallsWithNoUpperAndSameRowAdjacentBalls();
            if(slots.Count > 0)
            {
                slots.ForEach(slot => FallBallAtSlot(slot));
                return slots.Count + FallDownAllBallsWithNoUpperAdjacentBalls();
            }
            return 0;
        }

        private void DestroyBallAtSlot(BallSlot slot) 
        {
            Ball ball = GetBallAtSlot(slot);
            if (ball != null)
            {
                ball.Destroy();
                DeletedBalls.Add(ball);
            }
            Balls[slot.RowIndex][slot.ColumnIndex] = null;
        }

        private void FallBallAtSlot(BallSlot slot) 
        {
            Ball ball = GetBallAtSlot(slot);
            if (ball != null) 
            {
                ball.FallDown();
                DeletedBalls.Add(ball);
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

        private List<BallSlot> GetAllBallsAdjacentToSlot(BallSlot ballSlot)
        {
            List<BallSlot> slots = new List<BallSlot>(6);

            foreach (BallSlot slot in GetAllAdjacentSlots(ballSlot))
            {
                if (GetBallAtSlot(slot) != null)
                {
                    slots.Add(slot);
                }
            }

            return slots;
        }

        private static List<BallSlot> GetAllAdjacentUpperSlots(BallSlot slot) 
        {
            List<BallSlot> slots = new List<BallSlot>(6);

            if (slot.RowIndex % 2 == 0)
            {
                slots.Add(new BallSlot(GetClampedRowIndex(slot.RowIndex - 1), GetClampledColumnIndex(slot.RowIndex - 1, slot.ColumnIndex - 1)));
                slots.Add(new BallSlot(GetClampedRowIndex(slot.RowIndex - 1), GetClampledColumnIndex(slot.RowIndex - 1, slot.ColumnIndex)));
            }
            else
            {
                slots.Add(new BallSlot(GetClampedRowIndex(slot.RowIndex - 1), GetClampledColumnIndex(slot.RowIndex - 1, slot.ColumnIndex)));
                slots.Add(new BallSlot(GetClampedRowIndex(slot.RowIndex - 1), GetClampledColumnIndex(slot.RowIndex - 1, slot.ColumnIndex + 1)));
            }

            return slots;
        }

        private List<BallSlot> GetAllBallsWithNoUpperAndSameRowAdjacentBalls()
        {
            List<BallSlot> slots = new List<BallSlot>();

            for (int i = 1; i < NUMBER_OF_ROWS; i++)
            {
                List<BallSlot> slotListCandidate = new List<BallSlot>();
                for (int j = 0; j < GetNumberOfColumnForRow(i); j++)
                {
                    BallSlot slot = new BallSlot(i, j);

                    if(GetBallAtSlot(slot) != null)
                    {
                        if(GetBallsForSlots(GetAllAdjacentUpperSlots(slot)).All(ball => ball == null))
                        {
                            slotListCandidate.Add(slot);
                            if (j + 1 >= GetNumberOfColumnForRow(i))
                            {
                                slots.AddRange(slotListCandidate);
                            }
                        }
                        else
                        {
                            slotListCandidate = new List<BallSlot>();
                        }
                    }
                    else if (slotListCandidate.Count > 0)
                    {
                        slots.AddRange(slotListCandidate);
                    }
                }
            }

            return slots;
        }

        private List<Ball> GetBallsForSlots(List<BallSlot> slots)
        {
            return slots.ConvertAll<Ball>(slot => GetBallAtSlot(slot));
        }

        private Vector2 GetSlotCenter(BallSlot slot) 
        {
            return (slot.RowIndex % 2 == 0) ? Position + new Vector2(slot.ColumnIndex * Ball.RECTANGLE_WIDTH + Ball.RECTANGLE_WIDTH / 2, slot.RowIndex * Ball.RECTANGLE_HEIGHT + Ball.RECTANGLE_HEIGHT / 2)
                                            : Position + new Vector2(ODD_ROW_OFFSET + slot.ColumnIndex * Ball.RECTANGLE_WIDTH + Ball.RECTANGLE_WIDTH / 2, slot.RowIndex * Ball.RECTANGLE_HEIGHT + Ball.RECTANGLE_HEIGHT / 2);
            
        }

        private void CheckIfPlayerWins()
        {
            if (Balls.All(list => list.All(ball => ball == null)))
            {
                Observer.OnPlayerWins();
            }
        }

        private void CheckIfPlayerLost()
        {
            if (GetLowestOccupiedRowIndex() + 1 > NUMBER_OF_ROWS - Bounds.CurrentNumOfRowRemoved) 
            {
                 Observer.OnPlayerLoses();
            }
        }

        private int GetLowestOccupiedRowIndex() 
        {
            for (int i = NUMBER_OF_ROWS-1; i >= 0; i--)
            {
                if (Balls[i].Any(ball => ball != null))
                {
                    return i;
                }
            }
            return 0;
        }

        private static List<BallSlot> GetAllAdjacentLowerSlots(BallSlot slot)
        {
            List<BallSlot> slots = new List<BallSlot>(6);

            if (slot.RowIndex % 2 == 0)
            {
                slots.Add(new BallSlot(GetClampedRowIndex(slot.RowIndex + 1), GetClampledColumnIndex(slot.RowIndex + 1, slot.ColumnIndex - 1)));
                slots.Add(new BallSlot(GetClampedRowIndex(slot.RowIndex + 1), GetClampledColumnIndex(slot.RowIndex + 1, slot.ColumnIndex)));
            }
            else
            {
                slots.Add(new BallSlot(GetClampedRowIndex(slot.RowIndex + 1), GetClampledColumnIndex(slot.RowIndex + 1, slot.ColumnIndex)));
                slots.Add(new BallSlot(GetClampedRowIndex(slot.RowIndex + 1), GetClampledColumnIndex(slot.RowIndex + 1, slot.ColumnIndex + 1)));
            }

            return slots;
        }

        private static List<BallSlot> GetAdjacentSlotsOnSameRow(BallSlot slot)
        {
            List<BallSlot> slots = new List<BallSlot>(6);

            slots.Add(new BallSlot(GetClampedRowIndex(slot.RowIndex), GetClampledColumnIndex(slot.RowIndex, slot.ColumnIndex - 1)));
            slots.Add(new BallSlot(GetClampedRowIndex(slot.RowIndex), GetClampledColumnIndex(slot.RowIndex, slot.ColumnIndex + 1)));

            return slots;
        }

        private static List<BallSlot> GetAllAdjacentSlots(BallSlot slot) 
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
            return GetClampedRowIndex(rowIndex) % 2 == 0 ? NUMBER_OF_COLUMNS_EVEN : NUMBER_OF_COLUMNS_ODD;
        }
    }
}
