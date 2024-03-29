﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Timers;

namespace PuzzleBoobleClone.GameElements
{
    public class Bounds : GameElement , HangingBallsObserver
    {
        public static int ROW_HEIGHT = 32;
        private static int NUMBER_OF_ROWS = 12;

        private static Rectangle SRC_RECTANGLE = new Rectangle(0, 0, 128, 16);

        /// <summary>
        /// The Bounds Of the Ball Field.
        /// </summary>
        public Rectangle Rectangle = new Rectangle(190, 45, 259, 322);

        public BoundsObserver Observer = null;

        private Vector2 InitialPosition = new Vector2(190, 45);
        public int CurrentNumOfRowRemoved;

        private Timer RowRemovalTimer;

        public Bounds() 
        {
            CurrentNumOfRowRemoved = 0;

            RowRemovalTimer = new Timer();
            RowRemovalTimer.AutoReset = true;
            RowRemovalTimer.Interval = 15000;
            RowRemovalTimer.Elapsed += new ElapsedEventHandler(
                delegate(object source, ElapsedEventArgs e) 
                {
                        RemoveOneRow();    
                });
            RowRemovalTimer.Start();
        }

        public void Update(GameTime gameTime, Game1 game)
        {
            if (CurrentNumOfRowRemoved == NUMBER_OF_ROWS) 
            {
                RowRemovalTimer.Stop();
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Game1 game)
        {
            Texture2D wall = game.GameElements.BoundsWall;

            for (int i = 0; i < CurrentNumOfRowRemoved; i++ )
            {
                spriteBatch.Draw(
                    texture: wall,
                    position: InitialPosition + new Vector2(0, i * ROW_HEIGHT),
                    sourceRectangle: null,
                    color: Microsoft.Xna.Framework.Color.White,
                    rotation: 0,
                    origin: Vector2.Zero,
                    scale: 2.0f,
                    effects: SpriteEffects.None,
                    layerDepth: 0.9f
                );
            }

        }

        public void RemoveOneRow() 
        {
            CurrentNumOfRowRemoved = (int)MathHelper.Clamp(CurrentNumOfRowRemoved + 1, 0, NUMBER_OF_ROWS);

            Rectangle = new Rectangle(
                Rectangle.Left,
                Rectangle.Top + ROW_HEIGHT,
                Rectangle.Width,
                Rectangle.Height - ROW_HEIGHT);

            if (Observer != null) 
            {
                Observer.OnOneRowRemoved(this);
            }
        }

        public void OnPlayerWins()
        {
            RowRemovalTimer.Stop();
        }

        public void OnPlayerLoses()
        {
            RowRemovalTimer.Stop();
        }
    }
}
