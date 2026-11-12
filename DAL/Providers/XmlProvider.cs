using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
namespace DAL.Providers
{
    public class XmlProvider<T> : IDataProvider<T>
    {
        public void Serialize(string filePath, List<T> data)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<T>));
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                xmlSerializer.Serialize(fs, data);
            }
        }
        public List<T> Deserialize(string filePath)
        {
            if (!File.Exists(filePath)) return new List<T>();
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<T>));
            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                return (List<T>)xmlSerializer.Deserialize(fs);
            }
        }
    }
}