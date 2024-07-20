using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 坦克大战
{
    internal class EnemyTank:MoveThing
    {
        private Random r = new Random();
        public EnemyTank(int x,int y,int speed,Bitmap bmpUp,Bitmap bmpDown,Bitmap bmpLeft,Bitmap bmpRight)
        {
            this.x = x; this.y = y;
            this.Speed = speed;
            BitmapUp = bmpUp;
            BitmapDown = bmpDown;
            BitmapLeft = bmpLeft;
            BitmapRight = bmpRight;
            this.Direction=Direction.Down;
        }
        public override void Update()
        {
            MoveCheck();
            base.Update();
            Move();
        }
        private void Move()
        {
            switch (Direction)
            {
                case Direction.Up: y -= Speed; break;
                case Direction.Down: y += Speed; break;
                case Direction.Left: x -= Speed; break;
                case Direction.Right: x += Speed; break;
            }
        }
        private void MoveCheck()        //检查是否超过窗体
        {
            Rectangle rectangle = GetRectangle();
            switch (Direction)
            {
                case Direction.Up: rectangle.Y -= Speed; break;
                case Direction.Down: rectangle.Y += Speed; break;
                case Direction.Left: rectangle.X -= Speed; break;
                case Direction.Right: rectangle.X += Speed; break;
            }
            if (GameObjectManager.IsCollidedWall(rectangle) != null)
            {
                ChangeDirection();  return;
            }
            if (GameObjectManager.IsCollidedSteel(rectangle) != null)
            {
                ChangeDirection(); return;
            }
            if (GameObjectManager.IsCollidedBoss(rectangle) != null)
            {
                ChangeDirection(); return;
            }
            switch (Direction)
            {
                case Direction.Up: if (y - Speed < 0)               ChangeDirection(); return;
                case Direction.Down: if (y + Speed + Height > 450)  ChangeDirection(); return;
                case Direction.Left: if (x - Speed < 0)             ChangeDirection(); return;
                case Direction.Right: if (x + Speed + Width > 450)  ChangeDirection(); return;
            }

        }
        private void ChangeDirection()
        {
            //Random r=new Random(); 种子。算法，伪随机数，根据当前时间换算成毫秒，通过哈希构造法得到的数
            //最好使用成员变量,这样会是同一个种子,若是局部变量，会每次定义新的种子。主要是同一个种子会尽量使结果更加平均更加随机
            //不同种子之间无法沟通(算法连接)，可能会得到性质相同(即构造方式相同)的随机数
            while (true)
            {

                Direction dir = (Direction)r.Next(0, 4);
                if (dir == Direction)
                {
                    continue;
                }
                else
                {
                    Direction = dir; break;
                }
            }
            MoveCheck();
        }
        
    }
}
