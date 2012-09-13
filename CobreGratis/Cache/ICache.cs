
namespace BielSystems.Cache
{
    public interface ICache
    {
        void StoreData(string key, object data);
        object LoadData(string key);
        void ClearAll();
        void ClearKey(string key);
        bool ContainsKey(string key);
    }
}
