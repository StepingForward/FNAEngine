using FNAEngine.Content.Sprites;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Text.Json;
using Microsoft.Xna.Framework;
using System.Security.Cryptography.X509Certificates;

namespace FNAEngine.Content {
    public class Tilemap<LevelValue> {
        public Root<LevelValue> Level;
        public List<Sprite> Images;
        public Layer EntityLayer = new Layer();
        public Layer ObjectLayer = new Layer();

        public int offsetX;
        public int offsetY;

        public Tilemap(string path, ContentManager cm, int offsetX, int offsetY) {
            this.offsetX = offsetX;
            this.offsetY = offsetY;
            Level = JSONRead<Root<LevelValue>>(Environment.CurrentDirectory + "\\" + path);

            foreach (Layer layer in Level.Layers) {
                if (layer.Data2D != null) {
                    ObjectLayer = layer;
                } else if (layer.Entities != null) {
                    EntityLayer = layer;
                } else {
                    //todo! if there are new types of layers
                }
            }

            string s = path.Remove(path.LastIndexOf("\\") + 1);
            Console.WriteLine(Environment.CurrentDirectory + s + ObjectLayer.Tileset);
            Texture2D sprite = cm.Load<Texture2D>(Environment.CurrentDirectory + s + ObjectLayer.Tileset);
            Images = SpriteAtlas.GetSpritesFromTexture(cm, new Sprite(sprite, Rectangle.Empty), ObjectLayer.GridCellWidth,
                ObjectLayer.GridCellHeight, sprite.Width/ObjectLayer.GridCellWidth, sprite.Height / ObjectLayer.GridCellHeight);
        }

        public List<Collision> GetTileRecs(Layer layer) {
            List<Collision> tiles = new List<Collision>();

            for (int j = 0; j < layer.Data2D.Count; j++) {
                for (int i = 0; i < layer.Data2D[j].Count; i++) {
                    if (layer.Data2D[j][i] != -1) {
                        tiles.Add(new Rect(layer.GridCellWidth * i + offsetX,
                            layer.GridCellHeight * j + offsetY,
                            layer.GridCellWidth, layer.GridCellHeight));
                    }
                }
            }

            return tiles;
        }

        public void Draw(SpriteBatch spriteBatch, Layer layer) {
            for (int j = 0; j < layer.Data2D.Count; j++) {
                for (int i = 0; i < layer.Data2D[j].Count; i++) {
                    if (layer.Data2D[j][i] != -1) {
                        spriteBatch.Draw(Images[layer.Data2D[j][i]].Texture, 
                            new Vector2(layer.GridCellWidth * i + offsetX
                                , layer.GridCellHeight * j + offsetY),
                            Images[layer.Data2D[j][i]].sourceRectangle, Color.White);
                    }
                }
            }
        }

        private T JSONRead<T>(string filePath) {
            var serializeOptions = new JsonSerializerOptions {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
            string text = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<T>(text, serializeOptions);
        }
    }

    public class TilemapEntity {
        public string Name { get; set; }
        public int Id { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }

    public class Layer {
        public string? Name { get; set; }
        public int OffsetX { get; set; }
        public int OffsetY { get; set; }
        public int GridCellWidth { get; set; }
        public int GridCellHeight { get; set; }
        public int GridCellsX { get; set; }
        public int GridCellsY { get; set; }
        public string Tileset { get; set; }
        public List<List<int>> Data2D { get; set; }
        public List<TilemapEntity> Entities { get; set; }

        public Layer() { }
    }

    public class Root<Value> {
        public int Width { get; set; }
        public int Height { get; set; }
        public Value? Values { get; set; }
        public List<Layer>? Layers { get; set; }
    }
}
