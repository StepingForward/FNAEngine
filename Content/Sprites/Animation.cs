using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using static System.Net.Mime.MediaTypeNames;

namespace FNAEngine.Content.Sprites {
    public class Animation : IContent, IUpdate{
        public List<Frame> Frames = new List<Frame>();
        public int frameTime = 0;
        private int currentFrameTime = 0;
        private int currentFrame = 0;
        private int columns = 0;
        private int rows = 0;

        private Animation(List<Frame> frames, int frameTime, int columns, int rows) {
            this.columns = columns;
            this.rows = rows;
            this.frameTime = frameTime;
            Frames = frames;
        } 

        public static void CreateAnimation(ContentManager cm, Sprite sprite, int width, int height, int columns, int rows, string name, int frameTime) {
            Animation anim = new Animation(Frame.CreateFrames(cm, sprite, width, height, columns, rows), frameTime, columns, rows);
            GameContent.Contents.Add(anim);
            GameContent.Animations.Add(name, anim);
        }

        public void Update() {
            if (currentFrameTime >= frameTime) {
                currentFrame += 1;
                currentFrameTime = 0;
            } currentFrameTime++;
            if (currentFrame == Frames.Count) currentFrame = 0;
        }
        
        public void Draw(SpriteBatch spriteBatch, Vector2 position) {
            spriteBatch.Draw(Frames[currentFrame].sprite.Texture, position, Frames[currentFrame].sprite.sourceRectangle, Color.White);
        }
    }

    public class Frame : IContent {
        public Sprite sprite;
        public int fWidth;
        public int fHeight;

        public Frame(Sprite sprite, int fWidth, int fHeight) {
            this.sprite = sprite;
            this.fWidth = fWidth;
            this.fHeight = fHeight;
        }

        public static List<Frame> CreateFrames(ContentManager cm, Sprite spriteText, int width, int height, int columns, int rows) {
            List<Frame> frames = new List<Frame>();

            foreach(Sprite sprite in SpriteAtlas.GetSpritesFromTexture(cm, spriteText, width, height, columns, rows)) {
                frames.Add(new Frame(sprite, width, height));
            }

            return frames;
        }
    }
}
