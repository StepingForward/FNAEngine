using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using FNAEngine.Content.Sprites;
using Microsoft.Xna.Framework;
using System.Xml.Linq;

namespace FNAEngine.Content
{
    public class GameContent
    {
        //All Content
        static public List<IContent> Contents = new List<IContent>();

        //Content
        static public Dictionary<string, Sprite> Images = new Dictionary<string, Sprite>(); //Loads here
        static public Dictionary<string, List<Sprite>> Atlases = new Dictionary<string, List<Sprite>>(); //Loads in SpriteAtlas.cs
        static public Dictionary<string, SoundEffect> SoundEffects = new Dictionary<string, SoundEffect>(); //Loads here
        static public Dictionary<string, Animation> Animations = new Dictionary<string, Animation>(); //Loads in Animation.cs
        static public Dictionary<string, PhysicsEntity> PhysicsEntites = new Dictionary<string, PhysicsEntity>(); //Add each one with AddEntity()
        static public List<Rectangle> EntityRecs = new List<Rectangle>();

        static public Dictionary<string, Effect> PresetEffects = new Dictionary<string, Effect>();

        public static void LoadGameContent(ContentManager cm)
        {
            //Loading game content
            var ImageFiles = Directory.GetFiles(Environment.CurrentDirectory + "\\Content\\Sprites", "*.*", SearchOption.AllDirectories);
            var SoundFiles = Directory.GetFiles(Environment.CurrentDirectory + "\\Content\\Audio", "*.*", SearchOption.AllDirectories);

            foreach (string filename in ImageFiles)
            {
                if (filename.EndsWith(".png"))
                {
                    Texture2D phTexture = cm.Load<Texture2D>(filename.Replace(Environment.CurrentDirectory + "\\Content\\", "").Replace(".png", ""));
                    string name = filename.Replace(Environment.CurrentDirectory + "\\Content\\Sprites\\", "").Replace(".png", "");
                    Images.Add(name, new Sprite(phTexture, Microsoft.Xna.Framework.Rectangle.Empty));
                }
            }

            foreach (string filename in SoundFiles)
            {
                if (filename.EndsWith(".wav"))
                {
                    Console.WriteLine(filename.Replace(Environment.CurrentDirectory + "\\Content\\", ""));
                    SoundEffects.Add(filename.Replace(Environment.CurrentDirectory + "\\Content\\Audio\\SoundEffects\\", ""),
                        cm.Load<SoundEffect>(filename.Replace(Environment.CurrentDirectory + "\\Content\\", "")));
                }
            }
        }

        public static void Update() {
            foreach(IUpdate update in Contents) {
                update.Update();
            }
        }

        public static void Dispose() {
            foreach(Sprite texture in Images.Values) texture.Texture.Dispose(); 
            foreach (SoundEffect soundEffect in SoundEffects.Values) soundEffect.Dispose();
            foreach (List<Sprite> sprites in Atlases.Values) 
                foreach (Sprite texture in sprites)  texture.Texture.Dispose(); 
        }
    }

    public class Sprite : IContent {
        public Texture2D Texture;
        public Rectangle sourceRectangle;

        public Sprite(Texture2D texture, Rectangle sourceRectangle) {
            Texture = texture;
            this.sourceRectangle = sourceRectangle;
        }
    }
}
