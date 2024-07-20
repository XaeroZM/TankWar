using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 坦克大战.Properties;

namespace 坦克大战
{
    internal class MyTank :MoveThing
    {
        public bool IsMoving { get; set; }
        public MyTank(int x,int y,int speed) 
        {
            IsMoving = false;
            this.x = x;
            this.y = y;
            this.Speed = speed;
            BitmapUp = Resources.MyTankUp;
            BitmapDown = Resources.MyTankDown;
            BitmapLeft = Resources.MyTankLeft;
            BitmapRight = Resources.MyTankRight;   
            this.Direction = Direction.Right;    
        }
        public override void Update()
        {
            MoveCheck();
            base.Update();
            Move();
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
                IsMoving = false;
                
                return;
            }
            if(GameObjectManager.IsCollidedSteel(rectangle) != null)
            {
                IsMoving = false;
                return;
            }
            if (GameObjectManager.IsCollidedBoss(rectangle) != null)
            {
                IsMoving = false;
                return;
            }
            switch (Direction)
            {   
                case Direction.Up: if (y - Speed < 0) IsMoving = false; return;
                case Direction.Down: if(y+Speed +Height >450) IsMoving = false; return;
                case Direction.Left: if(x - Speed < 0) IsMoving=false; return;
                case Direction.Right: if(x+Speed +Width >450) IsMoving = false; return;
            }
            
        }
        private void Move()
        {
            if (IsMoving == false) return;
            switch (Direction)
            {
                case Direction.Up: y -= Speed; break;
                case Direction.Down: y += Speed; break;
                case Direction.Left: x -= Speed; break;
                case Direction.Right: x += Speed; break;
            }
        }
            public void KeyDown(KeyEventArgs args)
        {
            switch (args.KeyCode)
            {
                case Keys.W:
                    Direction = Direction.Up;
                    IsMoving = true;    
                    break;
                case Keys.S:
                    Direction = Direction.Down;
                    IsMoving = true;
                    break;
                case Keys.A:
                    Direction = Direction.Left;
                    IsMoving = true;
                    break;
                case Keys.D:
                    Direction = Direction.Right;
                    IsMoving = true;
                    break;
                case Keys.Space:
                    //发射子弹
                    Attack();
                    break;
            }
        }
        private void Attack()
        {
            int x = this.x; int y = this.y;
            SoundMananger.PlayFire();
            switch (Direction)
            {
                case Direction.Up: x += Width / 2; break;
                case Direction.Down: x += Width / 2; y += Height; break;
                case Direction.Left: y += Height / 2; break;
                case Direction.Right: x += Width; y += Height / 2; break;
            }
            GameObjectManager.CreateBullet(x,y, Direction, Tag.Mytank);
        }

        public void KeyUp(KeyEventArgs args)
        {
            switch (args.KeyCode)
            {
                case Keys.W:
                    Direction = Direction.Up;
                    IsMoving = false;
                    break;
                case Keys.S:
                    Direction = Direction.Down;
                    IsMoving = false;
                    break;
                case Keys.A:
                    Direction = Direction.Left;
                    IsMoving = false;
                    break;
                case Keys.D:
                    Direction = Direction.Right;
                    IsMoving = false;
                    break;
            }
        }
    }

}
