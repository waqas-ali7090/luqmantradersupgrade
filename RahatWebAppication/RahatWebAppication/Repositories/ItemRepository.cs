using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using RahatWebAppication.Interfaces;
using RahatWebAppication.Models;
using RahatWebAppication.ViewModels;

namespace RahatWebAppication.Repositories
{
    public class ItemRepository : IItemRepository
    {
        #region Private
        private readonly RahatDBEntities db = new RahatDBEntities();
        #endregion

        public bool AddItem(Item item)
        {
            db.Items.Add(item);
            db.SaveChanges();
            return true;
        }

        public bool UpdateItem(Item item)
        {
            db.Entry(item).State = EntityState.Modified;
            db.SaveChanges();
            return true;
        }

        public bool DeleteItem(Item item)
        {
            db.Entry(item).State = EntityState.Deleted;
            db.SaveChanges();
            return true;
        }
        public Item FindItemById(int itemId)
        {
            return db.Items.Find(itemId);
        }

        public IEnumerable<Item> GetAllItems()
        {
            return db.Items.ToList();
        }

        public List<ItemModel> SearchItems(ItemSearchRequest itemSearchRequest)
        {
            //as contains method do not work with null values
            itemSearchRequest.CategoryIds = itemSearchRequest.CategoryIds ?? new List<int>();
            itemSearchRequest.PriceRange = itemSearchRequest.PriceRange ?? new PriceRangeModel();
            int fromRow = (itemSearchRequest.PageNo - 1) * itemSearchRequest.PageSize;
            int toRow = itemSearchRequest.PageSize;
            var items =
                db.Items.Where(
                    x =>
                        (itemSearchRequest.CategoryIds.Count == 0 || itemSearchRequest.CategoryIds.Contains(x.CategoryId)))
                    .Select(
                        x =>
                            new ItemModel
                            {
                                Id = x.Id,
                                Name = x.Name,
                                Price = x.Price,
                                Description = x.Description,
                                Discount = x.Discount,
                                Photo = x.Photo,
                                ItemCategoryName = x.ItemCategory.Name,
                                PreviousPrice = x.PreviousPrice ?? 0
                            }).OrderBy(x => x.Name).Skip(fromRow).Take(toRow).ToList();
            return items;
        }

        public IEnumerable<ItemModel> GetlatestItems()
        {
            var items = db.Items.Where(x => x.IsLatest).Select(x => new ItemModel
            {
                Name = x.Name,
                Photo = x.Photo,
                Price = x.Price,
                Description = x.Description,
                Discount = x.Discount,
                ItemCategoryName = x.ItemCategory.Name,
                PreviousPrice = x.PreviousPrice ?? 0
            }).Take(4);
            return items;
        }
    }
}