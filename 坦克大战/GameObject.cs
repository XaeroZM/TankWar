using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 坦克大战
{
    internal abstract class GameObject
    {
        public int x { get; set; }      //属性的简写
        public int y { get; set; }

        public int Width { get; set; } 
        public int Height { get; set; }
        protected abstract Image GetImage();
        public virtual void DrawSelf()
        {
            Graphics graphics = GameFramework.graphics;
            graphics.DrawImage(GetImage(), x, y);    
        }
        public virtual void Update()
        {
            DrawSelf();
        }
        public Rectangle GetRectangle()
        {
            Rectangle rectangle=new Rectangle(x,y,Width,Height) ;
            return rectangle;
        }
    }
}
