using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 坦克大战
{
     enum Direction
    {
        Up,
        Down, 
        Left, 
        Right
    }
    internal class MoveThing:GameObject
    {
        private Object _lock = new Object();
        //private Object _lock =new Object();
        //lock (_lock){}
        public Bitmap BitmapUp {  get; set; }
        public Bitmap BitmapDown { get; set;}
        public Bitmap BitmapLeft { get; set; }
        public Bitmap BitmapRight { get; set; }
        public int Speed { get; set; }
        private Direction direction;
        public Direction Direction { 
            get { return direction; }
            set 
            {
                direction = value; Bitmap bitmap=null;
                switch (Direction)
                {
                    case Direction.Up: bitmap = BitmapUp; break;
                    case Direction.Down: bitmap = BitmapDown; break;
                    case Direction.Left: bitmap = BitmapLeft; break;
                    case Direction.Right: bitmap = BitmapRight; break;
                }
                lock (_lock)
                {
                    Width = bitmap.Width; Height = bitmap.Height;
                }
            }
        }

        protected override Image GetImage()
        {
            Bitmap bitmap = null;
            switch (Direction)
            {
                case Direction.Up:
                    bitmap = BitmapUp;
                    break;
                case Direction.Down:
                    bitmap = BitmapDown;
                    break;
                case Direction.Left: 
                    bitmap = BitmapLeft;
                    break;
                case Direction.Right:
                    bitmap = BitmapRight;
                    break;
            }
            bitmap.MakeTransparent(Color.Black);
            return bitmap;
        }
        public override void DrawSelf()
        {
            lock (_lock)
            {
                base.DrawSelf();
            }
        }
    }
}
