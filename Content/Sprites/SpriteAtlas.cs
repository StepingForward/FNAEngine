using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace FNAEngine.Content.Sprites {
    public class SpriteAtlas : IContent{
        public static void SpritesFromTexture(ContentManager cm, Sprite Atlas, int width, int height, int columns, int rows, string name) {
            List<Sprite> sprites = new List<Sprite>();

            for(int y = 0; y < rows; y++) {
                for (int x = 0; x < columns; x++) {
                    sprites.Add(new Sprite(Atlas.Texture, new Rectangle(y*width, x*height, width, height)));
                }
            }

            GameContent.Atlases.Add(name, sprites);
        }

        
        public static List<Sprite> GetSpritesFromTexture(ContentManager cm, Sprite Atlas, int tileWidth, int tileHeight, int columns, int rows) {
            List<Sprite> sprites = new List<Sprite>();
            for (int x = 0; x < rows; x++) {
                for (int y = 0; y < columns; y++) {
                    sprites.Add(new Sprite(Atlas.Texture, new Rectangle(y * tileWidth, x * tileHeight, tileWidth, tileHeight))); //How?
                }
            }
            Console.WriteLine(sprites.Count());
            return sprites; //it just works
        }
    }

}
