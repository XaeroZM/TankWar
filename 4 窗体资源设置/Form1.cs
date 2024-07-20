using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 坦克大战.Properties;

namespace 坦克大战
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            //this.StartPosition = FormStartPosition.Manual;
            //this.Location = new Point(1550, 400);

            
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //GDI Graphics Device Interface
            Graphics g = this.CreateGraphics();
            #region 怎么绘制线和字符串(region用法)  
            //Pen pen = new Pen(Color.FromArgb(0, 0, 0));                   画笔  
            //g.DrawLine(pen, new Point(0, 0), new Point(255, 255));        画线
            //g.DrawString("春风若有怜花意，可否许我少年时",                     写字
            //    new Font("隶书",12),                                      字体
            //    new SolidBrush(Color.FromArgb(255, 0, 0)),               粗刷子  
            //    new Point(0, 0));                                        位置点
            #endregion                      
            //Resources.Boss         using 坦克大战.Properties，也使用这个空间后直接使用资源类 
            Image image = Properties.Resources.Boss;

            Bitmap bitmap = Properties.Resources.Star1;
            bitmap.MakeTransparent(Color.Black);
            g.DrawImage(bitmap, 50, 50);

            g.DrawImage(image, new Point(255, 255));
        }
    }
}
