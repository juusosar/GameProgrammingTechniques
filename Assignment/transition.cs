using RC_Framework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Assignment
{
    public class transition : RC_GameStateParent
    {
        static public Texture2D texPause = null;

        ImageBackground pause1 = null;
        ColorField trans = null;

        public override void LoadContent()
        {
            texPause = Util.texFromFile(graphicsDevice, Levels.dir + "transition.png");
            pause1 = new ImageBackground(texPause, Color.White, graphicsDevice);
            trans = new ColorField(new Color(255, 255, 255, 100), new Rectangle(0, 0, 1400, 900));
        }

        public override void Update(GameTime gameTime)
        {
            if (keyState.IsKeyDown(Keys.N) && prevKeyState.IsKeyUp(Keys.N))
            {
                Levels.levelManager.getLevel(5).LoadContent();
                Levels.levelManager.setLevel(5);
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

