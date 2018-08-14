using System.Web;

namespace RahatWebAppication.ViewModels
{
    public class ItemModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Photo { get; set; }
        public decimal Price { get; set; }
        public decimal PreviousPrice { get; set; }
        public int CategoryId { get; set; }
        public string Discount { get; set; }
        public string Description { get; set; }
        public bool IsLatest { get; set; }

        public HttpPostedFileBase ImageUpload { get; set; }
        public string ItemCategoryName { get; set; }
    }
}