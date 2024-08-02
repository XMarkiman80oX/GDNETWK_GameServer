using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace GDNETWK_GameServer
{
    class Program
    {
        public static bool isRunning = false;
        public static bool isRPSGame = true;
        static void Main(string[] args)
        {
            Console.Title = "Game Server";
            isRunning = true;

            Thread mainThread = new Thread(new ThreadStart(MainThread));
            mainThread.Start();
            if(isRPSGame)
                GameServer.Start(2, 26951);
            else
                Server.Start(50, 26951);

           
        }

        private  static void MainThread()
        {
            Console.WriteLine($"Main thread started. Running at {Constants.TICKS_PER_SEC} ticks per second");
            DateTime _nextLoop = DateTime.Now;
            DateTime time1 = DateTime.Now;
            DateTime time2 = DateTime.Now;


            while (isRunning)
            {

                while (_nextLoop <DateTime.Now)
                {
                    time2 = DateTime.Now;
                    float deltaTime = (time2.Ticks - time1.Ticks) / 10000000f;
                    //Console.WriteLine(deltaTime);  // *float* output {0,2493331}
                                                  
                    time1 = time2;

                    if(isRPSGame)
                        GameGameLogic.Update(deltaTime);
                    else
                        GameLogic.Update(deltaTime);

                    _nextLoop = _nextLoop.AddMilliseconds(Constants.MS_PER_TICK);

                    if(_nextLoop > DateTime.Now)
                    {
                        Thread.Sleep(_nextLoop - DateTime.Now);
                    }
                }
            }
        }
    }
}
