using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
    public interface IValidate
    {
        bool Validate();
    }

    public interface ILoader<Key, Value> : IValidate
    {
        Dictionary<Key, Value> MakeDict();
    }

    public class DataManager
    {
        static HashSet<IValidate> _loaders = new HashSet<IValidate>();

        public static void LoadData()
        {

        }

        static Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
        {
            string text = File.ReadAllText($"{ConfigManager.Config.dataPath}/JsonData/{path}.json");
            Loader loader = Newtonsoft.Json.JsonConvert.DeserializeObject<Loader>(text);
            _loaders.Add(loader);

            return loader;
        }

        static Loader LoadJson<Loader>(string path)
        {
            string text = File.ReadAllText($"{ConfigManager.Config.dataPath}/JsonData/{path}.json");
            return Newtonsoft.Json.JsonConvert.DeserializeObject<Loader>(text);
        }

        static bool Validate()
        {
            bool success = true;

            foreach (var loader in _loaders)
            {
                if (loader.Validate() == false)
                    success = false;
            }

            _loaders.Clear();

            return success;
        }
    }

    
}
