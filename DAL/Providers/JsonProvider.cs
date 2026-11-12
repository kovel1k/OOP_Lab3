using System.Collections.Generic;
using System.IO;
using System.Web.Script.Serialization;
namespace DAL.Providers
{
    public class JsonProvider<T> : IDataProvider<T>
    {
        public void Serialize(string filePath, List<T> data)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string json = serializer.Serialize(data);
            File.WriteAllText(filePath, json);
        }
        public List<T> Deserialize(string filePath)
        {
            if (!File.Exists(filePath)) return new List<T>();
            string json = File.ReadAllText(filePath);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Deserialize<List<T>>(json);
        }
    }
}