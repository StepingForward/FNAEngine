using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FNAEngine {
    public interface IEntity { }
    public interface IContent { }
    public interface IUpdate { public void Update(); }
    public interface IDraw { public void Draw(SpriteBatch spriteBatch); }
}
