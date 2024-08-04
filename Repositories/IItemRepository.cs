
using System.Collections.Generic;

namespace valmet_cadastro_item.Models
{
    public interface IItemRepository
    {
        List<ItemModel> BuscarTodos();
       ItemModel Add(ItemModel item);



    }
}