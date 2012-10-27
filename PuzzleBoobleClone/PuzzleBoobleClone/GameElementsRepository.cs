using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using PuzzleBoobleClone.GameElements;

namespace PuzzleBoobleClone
{   
    public class GameElementsRepository : GameElement, HangingBallsObserver
    {

        public enum Level {LEVELONE, LEVELTWO }

        private List<GameElement> Elements;

        public Texture2D BackGround;
        public Texture2D SpriteSheet;
        public Texture2D PoppedBallsSpriteSheet;
        public Texture2D BoundsWall;

        public SpriteFont Font;

        public GameElementsRepository()
        {
            AimingArrow arrow = new AimingArrow();
            Bounds bounds = new Bounds();
            Score score = new Score();
            HangingBalls hangingBalls = new HangingBalls(bounds, this, score, Level.LEVELONE);

            this.Elements = new List<GameElement>();
            this.Elements.Add(new BackGround());
            this.Elements.Add(new BagAndLauncherMachine());
            this.Elements.Add(new Bobbles()); 
            this.Elements.Add(arrow);
            this.Elements.Add(bounds);
            this.Elements.Add(score);
            this.Elements.Add(new BallsRepository(arrow, bounds, hangingBalls));
        }

        public void LoadContent(Game1 game)
        {
            SpriteSheet = game.Content.Load<Texture2D>("Images/sprites");
            BackGround = game.Content.Load<Texture2D>("Images/background");
            BoundsWall = game.Content.Load<Texture2D>("Images/bounds");
            PoppedBallsSpriteSheet = game.Content.Load<Texture2D>("Images/sprites_poppedballsonly");
            Font = game.Content.Load<SpriteFont>("Linds");
        }
        
        public void Update(GameTime gameTime, Game1 game)
        {
            Elements.ForEach(e => e.Update(gameTime, game));
        }
            
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Game1 game)
        {
            Elements.ForEach(e => e.Draw(gameTime, spriteBatch, game));
        }

        public void OnPlayerWins()
        {
            // Load Level 2
            AimingArrow arrow = new AimingArrow();
            Bounds bounds = new Bounds();
            Score score = new Score();
            HangingBalls hangingBalls = new HangingBalls(bounds, this, score, Level.LEVELTWO);

            this.Elements = new List<GameElement>();
            this.Elements.Add(new BackGround());
            this.Elements.Add(new BagAndLauncherMachine());
            this.Elements.Add(new Bobbles());
            this.Elements.Add(arrow);
            this.Elements.Add(bounds);
            this.Elements.Add(score);
            this.Elements.Add(new BallsRepository(arrow, bounds, hangingBalls));
        }

        public void OnPlayerLoses()
        {
            throw new NotImplementedException();
        }
    }
}
