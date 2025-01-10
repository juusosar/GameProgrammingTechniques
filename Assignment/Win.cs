using RC_Framework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Assignment
{
    public class GameLevel_Win : RC_GameStateParent
    {
        static public Texture2D texWin = null;

        ImageBackground win = null;
        ColorField trans = null;

        public override void LoadContent()
        {
            texWin = Util.texFromFile(graphicsDevice, Levels.dir + "winning.png");
            win = new ImageBackground(texWin, Color.White, graphicsDevice);
            trans = new ColorField(new Color(255, 255, 255, 100), new Rectangle(0, 0, 1400, 900));
        }

        public override void Update(GameTime gameTime)
        {
            if (keyState.IsKeyDown(Keys.R) && prevKeyState.IsKeyUp(Keys.R))
            {
                Levels.levelManager.getCurrentLevel().ExitLevel();
                Levels.levelManager.getLevel(1).LoadContent();
                Levels.levelManager.setLevel(0);  
            }
        }

        public override void Draw(GameTime gameTime)
        {   

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);

            trans.Draw(spriteBatch);
            win.Draw(spriteBatch);
            spriteBatch.End();
        }
    }

}

