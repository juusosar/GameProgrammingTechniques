using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RC_Framework;
using System;

namespace MT2
{
    public class GameLevel_Intro : RC_GameStateParent
    {
        public override void LoadContent()
        {
            font1 = Content.Load<SpriteFont>("spritefont1");
        }

        public override void Update(GameTime gameTime)
        {

            if (Game1.keyState.IsKeyDown(Keys.N) && !Game1.prevKeyState.IsKeyDown(Keys.N))
            {
                gameStateManager.setLevel(1);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            graphicsDevice.Clear(Color.Aqua);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);
            spriteBatch.DrawString(font1, "level 0 - press n to go to next level", new Vector2(100, 100), Color.Brown);
            spriteBatch.End();
        }
    }


}
