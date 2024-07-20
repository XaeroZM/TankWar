using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 坦克大战  
{
    internal class NotMoveThing:GameObject         //不可移动的物体
    {
        public Image Image { get; set; }
        protected override Image GetImage()
        {
            Width = Image.Width;
            Height = Image.Height;
            return Image;
        }

        public NotMoveThing(int x, int y, Image image)
        {
            this.x = x; this.y = y; this.Image = image;
        }

    }
}
