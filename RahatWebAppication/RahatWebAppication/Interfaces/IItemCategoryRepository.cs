using System.Collections.Generic;
using RahatWebAppication.Models;

namespace RahatWebAppication.Interfaces
{
    public interface IItemCategoryRepository
    {
        IEnumerable<ItemCategory> GetAllCategories();
        IEnumerable<ItemCategory> GetCategoriesUsedInItems();
    }
}
