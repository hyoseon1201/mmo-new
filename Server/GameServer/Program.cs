using ServerCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
    class Program
    {
        static Listener _listener = new Listener();
        static Connector _connector = new Connector();

        static void GameLogicTask()
        {
            while (true)
            {
                //GameLogic.Instance.Update();
                Thread.Sleep(0);
            }
        }

        //static void GameDbTask()
        //{
        //    while (true)
        //    {
        //        DBManager.Instance.Flush();
        //        Thread.Sleep(100);
        //    }
        //}

        static void Main(string[] args)
        {
            ConfigManager.LoadConfig();
            DataManager.LoadData();

            // TEMP 방 하나 파두기
            //GameLogic.Instance.Push(() =>
            //{
            //    GameLogic.Instance.Add(1);
            //});

            IPAddress ipAddr = IPAddress.Parse(ConfigManager.Config.ip);
            IPEndPoint endPoint = new IPEndPoint(ipAddr, ConfigManager.Config.port);
            _listener.Init(endPoint, () => { return SessionManager.Instance.Generate(); });

            Console.WriteLine("Listening...");

            //// GameDbTask
            //{
            //    Thread t = new Thread(GameDbTask);
            //    t.Name = "GameDB";
            //    t.Start();
            //}

            //// DB Test
            //DBManager.Instance.Push(DBManager.TestDB);

            // GameLogic
            Thread.CurrentThread.Name = "GameLogic";
            GameLogicTask();
        }
    }
}
