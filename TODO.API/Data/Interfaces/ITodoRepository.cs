using TODO.API.Models;

namespace TODO.API.Data.Interfaces
{
    public interface ITodoRepository
    {
        IEnumerable<ItemData> GetAllData();
        ItemData GetItemById(int id);
        bool ItemExist(int ItemId);
        bool CreateItem(ItemData item);
        bool UpdateItem(ItemData item);
        bool DeleteItem(ItemData item);

        bool Save();


    }
}
