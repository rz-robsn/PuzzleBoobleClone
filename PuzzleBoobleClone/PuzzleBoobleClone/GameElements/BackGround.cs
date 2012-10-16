using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace PuzzleBoobleClone.GameElements
{
    public class BackGround : GameElement
    {
        public void Update(GameTime gameTime, Game1 game) { }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Game1 game)
        {
            spriteBatch.Draw(game.GameElements.BackGround, new Vector2(0, 0), null, Color.White, 0.0f, new Vector2(0, 0), 2f, SpriteEffects.None, 1);
        }
    }
}
