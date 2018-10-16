using System.Collections.Generic;
using RahatWebAppication.Models;

namespace RahatWebAppication.ViewModels
{
    public class ItemViewModel
    {
        public ItemViewModel()
        {
            ItemCategories = new List<ItemCategory>();
            Item = new ItemModel();
        }
        public ItemModel Item { get; set; }
        public IEnumerable<ItemModel> Items { get; set; }
        public IEnumerable<ItemCategory> ItemCategories { get; set; }
        public MailViewModel MailModel { get; set; }
        public User User { get; set; }
    }
}