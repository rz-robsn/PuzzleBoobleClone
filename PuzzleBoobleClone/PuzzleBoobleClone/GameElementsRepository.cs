using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using PuzzleBoobleClone.GameElements;
using System.Timers;

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

        public Level CurrentLevel;

        public GameElementsRepository()
        {
            CurrentLevel = Level.LEVELONE;
            LoadLevel(CurrentLevel);
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
            CurrentLevel = CurrentLevel == Level.LEVELONE ? Level.LEVELTWO : Level.LEVELONE;

            Timer timer = new Timer();
            timer.AutoReset = false;
            timer.Interval = 5000;
            timer.Elapsed += new ElapsedEventHandler(delegate(object source, ElapsedEventArgs e)
            {
                LoadLevel(CurrentLevel);
                timer.Stop();
            });
            timer.Start();
        }

        public void OnPlayerLoses()
        {
            Timer timer = new Timer();
            timer.AutoReset = false;
            timer.Interval = 5000;
            timer.Elapsed += new ElapsedEventHandler(delegate(object source, ElapsedEventArgs e)
                {
                    LoadLevel(CurrentLevel);
                    timer.Stop();
                });
            timer.Start();
        }

        private void LoadLevel(Level level) 
        {
            AimingArrow arrow = new AimingArrow();
            Bounds bounds = new Bounds();
            Score score = new Score();
            HangingBalls hangingBalls = new HangingBalls(bounds, score, level);
            hangingBalls.Observer.Add(this);
            hangingBalls.Observer.Add(bounds);

            this.Elements = new List<GameElement>();
            this.Elements.Add(new BackGround());
            this.Elements.Add(new BagAndLauncherMachine());
            this.Elements.Add(new Bobbles());
            this.Elements.Add(arrow);
            this.Elements.Add(bounds);
            this.Elements.Add(score);
            this.Elements.Add(new BallsRepository(arrow, bounds, hangingBalls));
        }
    }
}
