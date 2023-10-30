using Microsoft.EntityFrameworkCore;
using TODO.API.Data;
using TODO.API.Data.Interfaces;
using TODO.API.Models;

namespace TODO.API.Repository
{
    public class TodoRepository : ITodoRepository
    {
        private readonly TodoDbContext _context;

        public TodoRepository(TodoDbContext context)
        {
            _context = context;
        }
        public bool CreateItem(ItemData item)
        {
            _context.Add(item);
            return Save();
        }

        public bool DeleteItem(ItemData item)
        {
            _context.Remove(item);
            return Save();
        }

        public IEnumerable<ItemData> GetAllData()
        {
            var Items =  _context.Items.ToList();
            return Items;
        }

        public ItemData GetItemById(int id)
        {
            if (id == null) throw new ArgumentNullException("No id matching this");
            var itemRetreieval = _context.Items.FirstOrDefault(p => p.Id == id);
            if (itemRetreieval is null) throw new KeyNotFoundException("Item not found");
            return itemRetreieval;
        }

        public bool ItemExist(int ItemId)
        {
            var itemExistance = _context.Items.Any(p=>p.Id  == ItemId);
            return itemExistance;
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateItem(ItemData item)
        {
            _context.Add(item);
            return Save();
            
        }
    }
}
