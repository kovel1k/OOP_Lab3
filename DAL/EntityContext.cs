using System.Collections.Generic;
using DAL.Providers;
namespace DAL
{
    public class EntityContext<T>
    {
        private IDataProvider<T> _provider;
        
        public void SetProvider(IDataProvider<T> provider)
        {
            _provider = provider;
        }

        public void Save(string filePath, List<T> data)
        {
            if (_provider != null) _provider.Serialize(filePath, data);
        }

        public List<T> Load(string filePath)
        {
            if (_provider != null) return _provider.Deserialize(filePath);
            return new List<T>();
        }
    }
}