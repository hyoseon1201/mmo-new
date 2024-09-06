using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
    [Serializable]
    public class ServerConfig
    {
        public string dataPath;
        public string ip;
        public int port;
        public string connectionString;
    }

    public class ConfigManager
    {
        public static ServerConfig Config { get; private set; }

        public static void LoadConfig(string path = "./config.json")
        {
            // ServerConfig 클래스 타입으로 게임서버 루트디렉토리에 존재하는 config.json를 역직렬화 해준다음, Config에 저장해준다.
            string text = File.ReadAllText(path);
            Config = Newtonsoft.Json.JsonConvert.DeserializeObject<ServerConfig>(text);
        }
    }
}
