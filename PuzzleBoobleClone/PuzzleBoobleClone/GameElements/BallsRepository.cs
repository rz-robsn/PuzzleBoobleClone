using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PuzzleBoobleClone.GameElements
{
    public class BallsRepository : GameElement
    {
        /// <summary>
        /// The Next Ball that is going to be thrown
        /// </summary>
        public Ball CurrentBall;
        private static readonly Vector2 CURRENT_BALL_POSITION = new Vector2(301, 365);

        /// <summary>
        /// The Next Ball to throw After the Current Ball
        /// </summary>
        public Ball NextBall;
        private static readonly Vector2 NEXT_BALL_POSITION = new Vector2(232, 400);

        public BallsRepository() 
        {
            CurrentBall = new Ball(CURRENT_BALL_POSITION, GetRandomBallColor());
            NextBall = new Ball(NEXT_BALL_POSITION, GetRandomBallColor());
        }

        public void Update(GameTime gameTime, Game1 game)
        {
            CurrentBall.Update(gameTime, game);
            NextBall.Update(gameTime, game);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Game1 game)
        {
            CurrentBall.Draw(gameTime, spriteBatch, game);
            NextBall.Draw(gameTime, spriteBatch, game);
        }

        private static Ball.BallColor GetRandomBallColor() 
        {
            return Ball.BallColor.Blue;
        }
    }
}
