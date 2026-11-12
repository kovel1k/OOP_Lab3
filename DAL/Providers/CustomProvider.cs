using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
namespace DAL.Providers
{
    public class CustomProvider<T> : IDataProvider<T>
    {
        // Custom серіалізація в C# також найчастіше базується на BinaryFormatter, 
        // але вимагає від класу реалізації ISerializable (що ми зробили у StudentDAL).
        public void Serialize(string filePath, List<T> data)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                formatter.Serialize(fs, data);
            }
        }
        public List<T> Deserialize(string filePath)
        {
            if (!File.Exists(filePath)) return new List<T>();
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                return (List<T>)formatter.Deserialize(fs);
            }
        }
    }
}