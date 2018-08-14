using System.Collections.Generic;
using System.Linq;
using RahatWebAppication.Interfaces;
using RahatWebAppication.Models;

namespace RahatWebAppication.Repositories
{
    public class ItemCategoryRepository : IItemCategoryRepository
    {
        #region Private
        private readonly RahatDBEntities db = new RahatDBEntities();
        #endregion

        #region Public
        public IEnumerable<ItemCategory> GetAllCategories()
        {
            return db.ItemCategories.Where(x => x.Items.Count != 0);
        }
        #endregion
    }
}