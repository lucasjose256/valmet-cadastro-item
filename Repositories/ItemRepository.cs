using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using valmet_cadastro_item.Helper; // Ensure this namespace is correct
using valmet_cadastro_item.Models;

namespace valmet_cadastro_item.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly DataBase _context;

        public ItemRepository(DataBase context)
        {
            this._context = context;
        }

        public List<ItemModel> BuscarTodos()
        {
            return _context.Items.ToList();
        }


        public ItemModel Add(ItemModel item)
        {
            _context.Items.Add(item);
            _context.SaveChanges();
            return item;
        }
    }
}
