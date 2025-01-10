using RC_Framework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;



namespace Assignment
{
    public class help : RC_GameStateParent
    {
        static public Texture2D texPause = null;

        ImageBackground pause1 = null;
        ColorField trans = null;

        public override void LoadContent()
        {
            texPause = Util.texFromFile(graphicsDevice, Levels.dir + "help.png");
            pause1 = new ImageBackground(texPause, Color.White, graphicsDevice);
            trans = new ColorField(new Color(255, 255, 255, 100), new Rectangle(0, 0, 1400, 900));
        }

        public override void Update(GameTime gameTime)
        {
            if (keyState.IsKeyDown(Keys.I) && prevKeyState.IsKeyUp(Keys.I))
            {
                Levels.levelManager.popLevel();
            }
        }

        public override void Draw(GameTime gameTime)
        {

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);

            trans.Draw(spriteBatch);
            pause1.Draw(spriteBatch);
            spriteBatch.End();
        }
    }

}

