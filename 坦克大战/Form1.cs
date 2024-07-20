using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 坦克大战
{
    public partial class Form1 : Form
    {
        private static Graphics Wingraphics;
        private Thread thread;
        private static Bitmap TempBmp;
        public Form1()
        {   
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
    
            Wingraphics =this.CreateGraphics();

            thread = new Thread(new ThreadStart(GameMainThread));
            thread.Start();
        }  
        
        private static void GameMainThread()
        {
            //做一个临时画布
            TempBmp = new Bitmap(450, 450);
            Graphics bitmapG = Graphics.FromImage(TempBmp);
            GameFramework.graphics = bitmapG;

            int sleepTime = 1000/60;
            GameFramework.Start();
            while (true)
            {
                GameFramework.graphics.Clear(Color.Black);      
                //清除画布指定颜色，只会在循环里有效，主要是可以进行一个画面实时更新——也可以设置窗体的背景颜色
                GameFramework.Update();
                Wingraphics.DrawImage(TempBmp, 0, 0);
                Thread.Sleep(sleepTime);
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e) //sender事件消息,e是返回的关闭信息(如关闭的时间等)
        {
               thread.Abort();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)           //这里的e是按键信息
        {
            GameObjectManager.KeyDown(e);
        }


        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            GameObjectManager.KeyUp(e);
        }
    }
}
