using RahatWebAppication.Models;
using RahatWebAppication.ViewModels;

namespace RahatWebAppication.ModelMappers
{
    public static class ProductMapper
    {
        public static ItemModel MapFromServerToClient(this Item source)
        {
            return new ItemModel
            {
                Name = source.Name,
                Description = source.Description,
                Price = source.Price,
                Photo = source.Photo,
                PreviousPrice = source.PreviousPrice ?? 0,
                CategoryId = source.CategoryId,
                ItemCategoryName = source.ItemCategory?.Name,
                Discount = source.Discount,
                Id = source.Id,
                IsLatest = source.IsLatest
            };
        }

        public static Item MapFromClientToServer(this ItemModel source)
        {
            return new Item
            {
                Name = source.Name,
                Description = source.Description,
                Price = source.Price,
                Photo = source.Photo,
                PreviousPrice = source.PreviousPrice,
                CategoryId = source.CategoryId,
                Discount = source.Discount,
                Id = source.Id,
                IsLatest = source.IsLatest
            };
        }
    }
}