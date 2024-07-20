using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 坦克大战.Properties;

namespace 坦克大战
{
    enum Tag
    {
        Mytank,EnemyTank
    }
    internal class Bullet : MoveThing
    {
        Tag Tag;
        public Bullet(int x, int y, int speed, Direction direction, Tag tag)
        {
            this.x = x;
            this.y = y;
            this.Speed = speed;
            BitmapUp = Resources.BulletUp;
            BitmapDown = Resources.BulletDown;
            BitmapLeft = Resources.BulletLeft;
            BitmapRight = Resources.BulletRight;
            this.Direction = direction;
            this.Tag = tag;
            this.x -= Width / 2;
            this.y -= Height / 2;
        }
        public override void DrawSelf()
        {
            base.DrawSelf();
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
        private void MoveCheck()
        {
            Rectangle rectangle = GetRectangle();
            rectangle.X = x + Width / 2 - 3;
            rectangle.Y = y + Height / 2 - 3;
            rectangle.Width = rectangle.Height = 3;
            //墙：销毁、钢墙：自毁、坦克：击毁
            int xExplosion=this.x+Width / 2;
            int yExplosion=this.y+Height / 2;
            NotMoveThing wall = null;
            if ((wall = GameObjectManager.IsCollidedWall(rectangle)) != null)
            {
                GameObjectManager.DestroyWall(wall); GameObjectManager.DestroyBullet(this);
                GameObjectManager.CreateExplosion(xExplosion, yExplosion);
                SoundMananger.PlayBlast();
                return;
            }
            if (GameObjectManager.IsCollidedSteel(rectangle) != null)
            {
                GameObjectManager.DestroyBullet(this);
                 return;
            }
           // if (GameObjectManager.IsCollidedBoss(rectangle) != null)
            //{
           //     ChangeDirection(); return;
           // }
           if(Tag==Tag.Mytank)
            {
                EnemyTank enemyTank = null;
                
                if ((enemyTank=GameObjectManager.IsCollidedEnemyTank(rectangle))!=null)
                {
                    GameObjectManager.DestroyTank(enemyTank);
                    GameObjectManager.DestroyBullet(this);
                    SoundMananger.PlayBlast();
                    return;
                }
            }
            switch (Direction)
            {
                case Direction.Up: if (y + Height / 2 + 3 < 0) GameObjectManager.DestroyBullet(this); return;
                case Direction.Down: if (y + Height / 2 - 3 > 450) GameObjectManager.DestroyBullet(this); return;
                case Direction.Left: if (x + Width / 2 - 3 < 0) GameObjectManager.DestroyBullet(this); return;
                case Direction.Right: if (x + Width / 2 + 3 > 450) GameObjectManager.DestroyBullet(this); return;
            }
        }
    }
}
