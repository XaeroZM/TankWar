    using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 坦克大战.Properties;

namespace 坦克大战
{
    internal class GameObjectManager
    {
        private static List<NotMoveThing> wallList=new List<NotMoveThing>();
        private static List<NotMoveThing> steelList = new List<NotMoveThing>();
        private static List<EnemyTank> tankList = new List<EnemyTank>();
        private static List<Bullet> bulletList = new List<Bullet>();
        private static List<Explosion> explosionList = new List<Explosion>();
        private static NotMoveThing boss;
        private static MyTank mytank;

        private static int enemyBornSpeed = 60;
        private static int enemyBornCount = 60;
        private static Point[] points = new Point[3];
        public static void Start()
        {
            
            points[0].X = points[0].Y = 0;
            points[1].X = 7 * 30; points[1].Y = 0;
            points[2].X = 14 * 30; points[2].Y = 0; 
        }
        public static void Update()
        {
            foreach (NotMoveThing thing in wallList)
            {
                thing.Update();
            }
            foreach (NotMoveThing thing in steelList)
            {
                thing.Update();
            }
            foreach (MoveThing thing in tankList)
            {
                thing.Update();
            }
            //这里foreach出错是因为遍历的同时又被销毁了，故可以将须销毁的收集起来(bool)之后集中销毁
            for(int i = 0; i < bulletList.Count; i++)
                bulletList[i].Update();
            foreach (Explosion explosion in explosionList)  
                explosion.Update(); 
            boss.Update();
            mytank.Update();
            EnemyBorn();
        }

        public static void CreateBullet(int x,int y,Direction direction,Tag tag)
        {
            Bullet bullet = new Bullet(x, y, 5, direction, tag);
            bulletList.Add(bullet);
        }

        public static void CreateExplosion(int x,int y)
        {
            Explosion explosion =new Explosion(x, y);
            explosionList.Add(explosion);
        }

        public static void DestroyBullet(Bullet bullet)
        {
            bulletList.Remove(bullet);
        }
        public static void DestroyWall(NotMoveThing wall)
        {
            wallList.Remove(wall);
        }
        public static void DestroyTank(EnemyTank tank)
        {
            tankList.Remove(tank);
        }
        private static void EnemyBorn()
        {
            enemyBornCount++;
            if (enemyBornCount < enemyBornSpeed) return;
            SoundMananger.PlayAdd();
            Random rd=new Random();
            int index=rd.Next(0,3);
            Point position = points[index];
            int enemyType=rd.Next(1,5);
            switch(enemyType)
            {
                case 1: CreateEnemyTank1(position.X, position.Y); break;
                case 2: CreateEnemyTank2(position.X, position.Y); break;
                case 3: CreateEnemyTank3(position.X, position.Y); break;
                case 4: CreateEnemyTank4(position.X, position.Y); break;
            }
            enemyBornCount = 0;
        }
        
        private static void CreateEnemyTank1(int x,int y)
        {
            EnemyTank tank=new EnemyTank(x,y,2,Resources.GreenUp,Resources.GreenDown, Resources.GreenLeft, Resources.GreenRight);
            tankList.Add(tank);
        }
        private static void CreateEnemyTank2(int x,int y) 
        {
            EnemyTank tank = new EnemyTank(x, y, 2, Resources.YellowUp, Resources.YellowDown, Resources.YellowLeft, Resources.YellowRight);
            tankList.Add(tank);
        }
        private static void CreateEnemyTank3(int x,int y) 
        {
            EnemyTank tank = new EnemyTank(x, y, 1, Resources.SlowUp, Resources.SlowDown, Resources.SlowLeft, Resources.SlowRight);
            tankList.Add(tank);
        }
        private static void CreateEnemyTank4(int x,int y) 
        {
            EnemyTank tank = new EnemyTank(x, y, 2, Resources.QuickUp, Resources.QuickDown, Resources.QuickLeft, Resources.QuickRight);
            tankList.Add(tank);
        }
        public static NotMoveThing IsCollidedWall(Rectangle rect)
        {   
            foreach (NotMoveThing thing in wallList)
            { 
                if (thing.GetRectangle().IntersectsWith(rect))
                    return thing;
            }
            return null;
        }
        public static NotMoveThing IsCollidedSteel(Rectangle rect)
        {
            foreach (NotMoveThing thing in steelList)
            {
                if (thing.GetRectangle().IntersectsWith(rect))
                    return thing;
            }
            return null;
        }
        public static NotMoveThing IsCollidedBoss(Rectangle rect)
        {
            if(boss.GetRectangle().IntersectsWith(rect)) return boss;
            return null;
        }
        public static EnemyTank IsCollidedEnemyTank(Rectangle rectangle)
        {
            foreach (EnemyTank thing in tankList)
                if (thing.GetRectangle().IntersectsWith(rectangle)) return thing;
            return null;
        }
        //public static void DrawMap()
        //{
        //    foreach (NotMoveThing thing in wallList) 
        //    {
        //        thing.DrawSelf();
        //    }
        //    foreach (NotMoveThing thing in steelList)
        //    {
        //        thing.DrawSelf();
        //    }
        //    boss.DrawSelf();
        //}
        //public static void DrawMyTank()
        //{
        //    mytank.DrawSelf();
        //}
        public static void CreateMap()
        {
            #region 地图创建
            CreateWall(1, 1, 5, Resources.wall, wallList);
            CreateWall(3, 1, 5, Resources.wall, wallList);
            CreateWall(5, 1, 4, Resources.wall, wallList);
            CreateWall(7, 1, 3, Resources.wall, wallList);
            CreateWall(9, 1, 4, Resources.wall, wallList);
            CreateWall(11, 1, 5, Resources.wall, wallList);
            CreateWall(13, 1, 5, Resources.wall, wallList);

            CreateWall(7, 5, 1, Resources.steel, steelList);

            CreateWall(0, 7, 1, Resources.steel, steelList);

            CreateWall(2, 7, 1, Resources.wall, wallList);
            CreateWall(3, 7, 1, Resources.wall, wallList);
            CreateWall(4, 7, 1, Resources.wall, wallList);
            CreateWall(6, 7, 1, Resources.wall, wallList);
            CreateWall(7, 6, 2, Resources.wall, wallList);
            CreateWall(8, 7, 1, Resources.wall, wallList);
            CreateWall(10, 7, 1, Resources.wall, wallList);
            CreateWall(11, 7, 1, Resources.wall, wallList);
            CreateWall(12, 7, 1, Resources.wall, wallList);

            CreateWall(14, 7, 1, Resources.steel, steelList);

            CreateWall(1, 9, 5, Resources.wall, wallList);
            CreateWall(3, 9, 5, Resources.wall, wallList);
            CreateWall(5, 9, 3, Resources.wall, wallList);

            CreateWall(6, 10, 1, Resources.wall, wallList);
            CreateWall(7, 10, 2, Resources.wall, wallList);
            CreateWall(8, 10, 1, Resources.wall, wallList);

            CreateWall(9, 9, 3, Resources.wall, wallList);
            CreateWall(11, 9, 5, Resources.wall, wallList);
            CreateWall(13, 9, 5, Resources.wall, wallList);


            CreateWall(6, 13, 2, Resources.wall, wallList);
            CreateWall(7, 13, 1, Resources.wall, wallList);
            CreateWall(8, 13, 2, Resources.wall, wallList);

            CreateBoss(7, 14, Resources.Boss);
            #endregion
        }
        public static void CreateMyTank()
        {
            int x = 5 * 30;
            int y = 14 * 30;
            mytank=new MyTank(x,y,2);
        }
        private static void CreateWall(int x, int y, int count,Image image,List<NotMoveThing> List)
        {
            int xPosition = x * 30;
            int yPosition = y * 30;
            for (int i = yPosition; i < yPosition + count * 30; i += 15)
            {
                NotMoveThing wall1 = new NotMoveThing(xPosition, i, image);
                NotMoveThing wall2 = new NotMoveThing(xPosition+15, i, image); 
                List.Add(wall1);
                List.Add(wall2);
            }
        }

        private static void CreateBoss(int x,int y,Image image)
        {
            int xPosition = x * 30; int yPosition = y * 30;
            boss = new NotMoveThing(xPosition, yPosition, image);
        }
        public static void KeyUp(KeyEventArgs args)
        {
            mytank.KeyUp(args);
        }
        public static void KeyDown(KeyEventArgs args)
        {
            mytank.KeyDown(args);
        }


    }
}
