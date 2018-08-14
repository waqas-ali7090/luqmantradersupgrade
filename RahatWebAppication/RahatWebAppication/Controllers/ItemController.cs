using System.Web;
using System.Web.Mvc;
using RahatWebAppication.Interfaces;
using RahatWebAppication.Models;
using RahatWebAppication.ViewModels;

namespace RahatWebAppication.Controllers
{
    public class ItemController : Controller
    {
        #region Private Repository
        private readonly IItemRepository itemRepository;
        private readonly IItemCategoryRepository itemCategoryRepository;
        #endregion

        #region Constructor
        public ItemController(IItemRepository itemRepository, IItemCategoryRepository itemCategoryRepository)
        {
            this.itemRepository = itemRepository;
            this.itemCategoryRepository = itemCategoryRepository;
        }
        #endregion

        #region Index
        [Authorize]
        public ActionResult Item()
        {
            var model = new ItemViewModel
            {
                Items = itemRepository.GetAllItems()
            };
            return View(model);
        }
        #endregion

        #region Add/Update/Delete
        [Authorize]
        public ActionResult CreateItem()
        {
            ItemViewModel model = new ItemViewModel
            {
                ItemCategories = itemCategoryRepository.GetAllCategories()
            };
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public ActionResult CreateItem(ItemViewModel model)
        {
            if (ModelState.IsValid)
            {
                HttpPostedFileBase file = model.Item.ImageUpload;
                try
                {
                    model.Item.Photo = model.Item.ImageUpload.FileName;
                    file.SaveAs(Server.MapPath(@"~\ItemImage\" + file.FileName));
                    model.Item.Photo = "~/ItemImage/" + file.FileName;
                    if (itemRepository.AddItem(model.Item))
                    {
                        TempData["Message"] = "Item saved successfully.";
                    }
                    return RedirectToAction("Item");
                }
                catch
                {
                    TempData["Message"] = "Something went wrong. Please Try again!";
                    return View(model);
                }
            }
            else
            {
                HttpPostedFileBase file = model.Item.ImageUpload;
                model.Item.Photo = model.Item.ImageUpload.FileName;
                file.SaveAs(Server.MapPath(@"~\ItemImage\" + file.FileName));
                model.Item.Photo = "~/ItemImage/" + file.FileName;
                return View(model);
            }
        }

        [Authorize]
        public ActionResult EditItem(int id)
        {
            ItemViewModel model = new ItemViewModel
            {
                ItemCategories = itemCategoryRepository.GetAllCategories(),
                Item = itemRepository.FindItemById(id)
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult EditItem(ItemViewModel model)
        {
            if (model.Item.ImageUpload != null)
            {
                HttpPostedFileBase file = model.Item.ImageUpload;
                model.Item.Photo = model.Item.ImageUpload.FileName;
                file.SaveAs(Server.MapPath(@"~\ItemImage\" + file.FileName));
                model.Item.Photo = "~/ItemImage/" + file.FileName;
            }
            try
            {
                if (itemRepository.UpdateItem(model.Item))
                {
                    TempData["Message"] = "Item updated successfully.";
                }
                return RedirectToAction("Item");
            }
            catch
            {
                TempData["Message"] = "Something went wrong. Please Try again!";
                return View(model);
            }
        }

        [Authorize]
        public ActionResult DeleteItem(int id)
        {
            Item item = itemRepository.FindItemById(id);
            try
            {
                if (itemRepository.DeleteItem(item))
                {
                    TempData["Message"] = "Item has been deleted successfully!";
                    return RedirectToAction("Item");
                }
                return View("Item");
            }
            catch
            {
                TempData["Message"] = "Something went wrong. Please Try again!";
                return View("Item");
            }
        }
        #endregion
    }
}
