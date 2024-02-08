using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FNAEngine {
    public class Camera2D {
        protected float zoom; 
        public Matrix transform; 
        public Vector2 pos; 
        protected float rotation; 

        public Camera2D() {
            zoom = 1.0f;
            rotation = 0.0f;
            pos = Vector2.Zero;
        }

        public float Zoom { get; set; }
        public float Rotation { get; set; }
        public Vector2 Pos { get; set; }

        public Matrix get_transformation(GraphicsDevice graphicsDevice) {
            transform = Matrix.CreateTranslation(new Vector3(-pos.X, -pos.Y, 0)) *
                Matrix.CreateRotationZ(Rotation) *
                Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
                Matrix.CreateTranslation(new Vector3(0, 0, 0));
            return transform;
        }
    }
}
