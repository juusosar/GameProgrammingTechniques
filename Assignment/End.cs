using RC_Framework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Assignment
{
    public class GameLevel_End : RC_GameStateParent
    {
        static public Texture2D texEnd = null;

        ImageBackground end = null;
        ColorField trans = null;

        public override void LoadContent()
        {
            texEnd = Util.texFromFile(graphicsDevice, Levels.dir + "gameover.png");
            end = new ImageBackground(texEnd, Color.Green, graphicsDevice);
            trans = new ColorField(new Color(255, 255, 255, 100), new Rectangle(0, 0, 1400, 900));
        }

        public override void Update(GameTime gameTime)
        {
            if (keyState.IsKeyDown(Keys.R) && prevKeyState.IsKeyUp(Keys.R))
            {
                Levels.levelManager.getCurrentLevel().ExitLevel();
                Levels.levelManager.getLevel(1).LoadContent();
                Levels.level2flag = false;
                Levels.levelManager.setLevel(0);
                
            }
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);

            trans.Draw(spriteBatch);
            end.Draw(spriteBatch);
            spriteBatch.End();
        }
    }

}

