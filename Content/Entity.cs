using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace FNAEngine.Content {
    public class PhysicsEntity {
        public string Name { get; set; }
        public bool[] RectCollisions = new bool[4] {false, false, false, false}; //left, right, down, up
        public bool CircleColliding;
        public Rect? RectangleCol;
        public Circle? CircleCol;

        public PhysicsEntity() { }

        public PhysicsEntity(Rect rectangle) {
            RectangleCol = rectangle;
        }

        public PhysicsEntity(Circle circle) {
            CircleCol = circle;
        }

        public void UpdateCollision() {
            RectCollisions = new bool[4] { false, false, false, false };
        }

        public void IsColliding(Collision collider) {
            if (collider.GetType() == typeof(Circle) && this.RectangleCol != null && this.CircleCol == null)  //Rect colliding with Circle
                CircleWithRect(RectangleCol, (Circle)collider);
            else if (collider.GetType() == typeof(Rect) && this.RectangleCol != null && this.CircleCol == null) { //Rect colliding with rect
                if (collider.rect.Intersects(RectangleCol.rect)) {
                    if (collider.rect.Intersects(RectangleCol.top)) RectCollisions[3] = true;
                    if (collider.rect.Intersects(RectangleCol.bottom)) RectCollisions[2] = true;
                    if (collider.rect.Intersects(RectangleCol.left)) RectCollisions[0] = true;
                    if (collider.rect.Intersects(RectangleCol.right)) RectCollisions[1] = true;
                }
            } 
            else if (collider.GetType() == typeof(Circle) && this.RectangleCol == null && this.CircleCol != null) { //Circle colliding with Circle
                Circle col = (Circle)collider;
                var radius = col.Radius + CircleCol.Radius;
                var deltaX = col.Center.X - CircleCol.Center.X;
                var deltaY = col.Center.Y - CircleCol.Center.Y;
                CircleColliding =  deltaX * deltaX + deltaY * deltaY <= radius * radius;
            }
            else if (collider.GetType() == typeof(Rect) && this.RectangleCol == null && this.CircleCol != null) //Circle colliding with Rect
                CircleWithRect((Rect)collider, CircleCol);
        }

        public void IsCollidingWithPhysicsEntity(PhysicsEntity ent) {
            if (ent.RectangleCol == null && ent.CircleCol != null) IsColliding(ent.CircleCol);
            else if (ent.RectangleCol != null && ent.CircleCol == null) IsColliding(ent.RectangleCol);
        }

        void CircleWithRect(Rect RectangleCol, Circle collider) {
            Rectangle rect = RectangleCol.rect;
            Circle cir = (Circle)collider;
            float rW = (rect.Width) / 2;
            float rH = (rect.Height) / 2;

            float distX = Math.Abs(cir.Center.X - (rect.Left + rW));
            float distY = Math.Abs(cir.Center.Y - (rect.Top + rH));

            if (distX >= cir.Radius + rW || distY >= cir.Radius + rH)
                CircleColliding = false;

            if (distX < rW || distY < rH) 
                CircleColliding = true; // touching

            distX -= rW;
            distY -= rH;

            if (distX * distX + distY * distY < cir.Radius * cir.Radius) 
                CircleColliding = true;
            
            CircleColliding = false;
        }

        public void IsCollidingWithColliders(List<Collision> colliders) {
            foreach (Collision collider in colliders) 
                IsColliding(collider);
        }

        bool AreInRange(float range, Vector2 v1, Vector2 v2) {
            var dx = v1.X - v2.X;
            var dy = v1.Y - v2.Y;
            return dx * dx + dy * dy < range * range;
        }
    }

    public class Circle : Collision{
        public Circle(int x, int y, int radius) {
            Center = new Point(x, y);
            Radius = radius;
        }

        public Point Center { get; private set; }
        public int Radius { get; private set; }

    }

    public class Rect : Collision{
        public Rect(int x, int y, int width, int height) {
            rect = new Rectangle(x, y, width, height);
            right = new Rectangle(rect.Right, y, 1, rect.Height - 2);
            left = new Rectangle(rect.Left, y, 1, rect.Height - 2);
            bottom = new Rectangle(x, rect.Bottom, rect.Width - 2, 1);
            top = new Rectangle(x, rect.Top, rect.Width - 2, 1);
            rectRect = rect;
        }

        public void Update() {
            rect = rectRect;
            right = new Rectangle(rect.Right, rect.Y, 1, rect.Height - 2);
            left = new Rectangle(rect.Left, rect.Y, 1, rect.Height - 2);
            bottom = new Rectangle(rect.X, rect.Bottom, rect.Width - 2, 1);
            top = new Rectangle(rect.X, rect.Top, rect.Width - 2, 1);
        }

        public Rectangle rectRect; //funny name
        public Rectangle right;
        public Rectangle left;
        public Rectangle bottom;
        public Rectangle top;
    }

    public class Collision {
        public Rectangle rect;

        public Collision() { }
    }
}
