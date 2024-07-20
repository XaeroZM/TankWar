namespace 多线程
{
    internal class Program
    {
        static void Main(string[] args)                 
        {
            Thread childThread = new Thread(new ThreadStart(childThreadMethod)); 
            childThread.Start(); 
           Thread.Sleep(3000);
            isRun = false;
            //childThread.Abort(); 霸道终止，无论此进程是否结束
        }
        private static bool isRun = true;       //温和的终止
        private static void childThreadMethod()
            {
            while (isRun)
            {
                Console.WriteLine("Child Thread Runing——听歌中");
                Thread.Sleep(1000);     //以ms(豪秒)为单位。1s=1000ms
            }
                }
    }
}
