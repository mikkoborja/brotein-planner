using Newtonsoft.Json;

namespace BroTeinPlanner.Utils {
    public static class JsonFileHandler {
        public static List<T> Load<T>(string path) {
            if(File.Exists(path)) {
                string accounts = File.ReadAllText(path);
                return JsonConvert.DeserializeObject<List<T>>(accounts) ?? new List<T>();
            }else {
                return new List<T>();
            }
        }

        public static void Save<T>(string path, List<T> data) {
            string json = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(path, json);
        }
    }
}