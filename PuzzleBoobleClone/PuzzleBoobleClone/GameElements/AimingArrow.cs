using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace PuzzleBoobleClone.GameElements
{
    public class AimingArrow : GameElement
    {
        private static readonly Rectangle SPRITE_RECTANGLE = new Rectangle(0, 514, 23, 56);
        private static readonly Vector2 ROTATION_CENTER = new Vector2(11, 32.5f);

        private static float ANGLE_LOWER_BOUND = (float)Math.PI / 12;
        private static float ANGLE_HIGHER_BOUND = 11 * (float)Math.PI / 12;

        private Vector2 Position = new Vector2(317, 384);
        private float angle = (float)Math.PI/2;
        private float RotationSpeed = (float)Math.PI/90;

        public AimingArrow() { }

        public void Update(GameTime gameTime, Game1 game)
        {
            KeyboardState keyBoardState = Keyboard.GetState();

            if (keyBoardState.IsKeyDown(Keys.Right))
            {
                angle = MathHelper.Clamp(angle + RotationSpeed, ANGLE_LOWER_BOUND, ANGLE_HIGHER_BOUND);
            }

            if (keyBoardState.IsKeyDown(Keys.Left))
            {
                angle = MathHelper.Clamp(angle - RotationSpeed, ANGLE_LOWER_BOUND, ANGLE_HIGHER_BOUND);
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Game1 game)
        {
            Texture2D spriteSheet = game.GameElements.SpriteSheet;

            spriteBatch.Draw(texture: spriteSheet,
                        position: Position,
                        sourceRectangle: SPRITE_RECTANGLE,
                        color: Color.White,
                        rotation: angle - (float)Math.PI/2, 
                        origin: ROTATION_CENTER,
                        scale: 2.0f,
                        effects: SpriteEffects.None,
                        layerDepth: 0.8f);
        }

        /// <summary>
        /// Returns a unit Vector that has the same direction as the
        /// Arrow
        /// </summary>
        public Vector2 GetDirectionVector()
        {
            return new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
        }
    }
}