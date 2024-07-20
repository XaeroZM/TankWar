using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 坦克大战
{
    internal class GameFramework
    {
        public static Graphics graphics;
        public static void Start()
        {
            SoundMananger.InitSound();
            GameObjectManager.CreateMap();
            GameObjectManager.CreateMyTank();
            GameObjectManager.Start();
            SoundMananger.PlaySound(); 
        }
        public static void Update() 
        {
            GameObjectManager.Update();
        }
        
    }
}
