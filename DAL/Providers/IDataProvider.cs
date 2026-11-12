using System.Collections.Generic;
namespace DAL.Providers
{
    public interface IDataProvider<T>
    {
        void Serialize(string filePath, List<T> data);
        List<T> Deserialize(string filePath);
    }
}