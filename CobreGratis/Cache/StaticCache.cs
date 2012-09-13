using System.Collections.Generic;

namespace BielSystems.Cache
{
    public class StaticCache : ICache
    {
        protected static Dictionary<string, object> Data = new Dictionary<string, object>();

        public void StoreData(string key, object data)
        {
            StaticCache.Data.Add(key, data);
        }

        public object LoadData(string key)
        {
            if (StaticCache.Data.ContainsKey(key))
                return StaticCache.Data[key];

            return string.Empty;
        }

        public void ClearAll()
        {
            StaticCache.Data.Clear();
        }

        public void ClearKey(string key)
        {
            if (StaticCache.Data.ContainsKey(key))
                StaticCache.Data.Remove(key);
        }

        public bool ContainsKey(string key)
        {
            return StaticCache.Data.ContainsKey(key);
        }
    }
}
