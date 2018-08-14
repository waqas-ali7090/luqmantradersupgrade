using System.Collections.Generic;
using RahatWebAppication.Models;
using RahatWebAppication.ViewModels;

namespace RahatWebAppication.Interfaces
{
    public interface IItemRepository
    {
        bool AddItem(Item item);
        bool UpdateItem(Item item);
        bool DeleteItem(Item item);
        Item FindItemById(int itemId);
        IEnumerable<Item> GetAllItems();
        List<ItemModel> SearchItems(ItemSearchRequest itemSearchRequest);
        IEnumerable<ItemModel> GetlatestItems();
    }
}
