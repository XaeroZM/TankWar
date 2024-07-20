using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;
using 坦克大战.Properties;

namespace 坦克大战
{
    internal class Explosion : GameObject
    {
        private int playSpeed = 1;
        private int playCount = 0;
        private int index = 0;
        private Bitmap[] bmpArray = new Bitmap[] {
            Resources.EXP1,
            Resources.EXP2,
            Resources.EXP3,
            Resources.EXP4,
            Resources.EXP5 
        };

        public Explosion(int x, int y)
        {
            foreach (Bitmap bmp in bmpArray)
            {
                bmp.MakeTransparent(Color.Black);
            }
            this.x = x - bmpArray[0].Width / 2;
            this.y = y - bmpArray[0].Height / 2; 
        }       
        protected override Image GetImage()
        {
            if(index>4) return bmpArray[4];
            return bmpArray[index];
        }
        public override void Update()
        {
            playCount++;
            index = (playCount - 1) / playSpeed;
            base.Update();
        }
        public override void DrawSelf()
        {
            // base.DrawSelf();
                   
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return -1;
        }

        public override string ToString()
        {
            return "111";
        }

       

        
    }
}
