using System;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web.Mvc;
using RahatWebAppication.Interfaces;
using RahatWebAppication.ModelMappers;
using RahatWebAppication.ViewModels;

namespace RahatWebAppication.Controllers
{
    public class HomeController : Controller
    {
        #region Private Repository
        private readonly IItemRepository itemRepository;
        private readonly IItemCategoryRepository itemCategoryRepository;
        #endregion

        #region Constructor
        public HomeController(IItemRepository itemRepository, IItemCategoryRepository itemCategoryRepository)
        {
            this.itemRepository = itemRepository;
            this.itemCategoryRepository = itemCategoryRepository;
        }
        #endregion

        #region Public Methods

        public ActionResult Index()
        {
            System.Web.HttpContext.Current.Session["menu"] = "HOME";
            var items = itemRepository.GetlatestItems();
            return View(items);
        }

        public ActionResult Product()
        {
            System.Web.HttpContext.Current.Session["menu"] = "PRODUCT";
            var model = new ItemViewModel
            {
                Items = itemRepository.GetAllItems().Select(x => x.MapFromServerToClient()).ToList(),
                ItemCategories = itemCategoryRepository.GetAllCategories().ToList()
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult GetProduct(ItemSearchRequest itemSearchRequest)
        {
            var items = itemRepository.SearchItems(itemSearchRequest);
            var model = new ItemViewModel { Items = items };
            return PartialView("_GetProduct", model);
        }

        public ActionResult Service()
        {
            System.Web.HttpContext.Current.Session["menu"] = "SERVICE";
            return View();
        }

        public ActionResult About()
        {
            System.Web.HttpContext.Current.Session["menu"] = "ABOUT";
            return View();
        }

        public ActionResult Contact()
        {
            System.Web.HttpContext.Current.Session["menu"] = "CONTACT";
            return View();
        }

        public ActionResult SocialWork()
        {
            System.Web.HttpContext.Current.Session["menu"] = "SOCIAL";
            return View();
        }

        [HttpPost]
        public ActionResult SendEmail(ItemViewModel model)
        {
            string email = ConfigurationManager.AppSettings["MyEmail"];
            string password = ConfigurationManager.AppSettings["MyPassword"];
            string host = ConfigurationManager.AppSettings["Host"];
            int port = Convert.ToInt32(ConfigurationManager.AppSettings["Port"]);

            MailMessage mail = new MailMessage(email, email)
            {
                Subject = ConfigurationManager.AppSettings["Subject"],
                Body =
                    "Name : " + model.MailModel.Name + "<br> Email : " + model.MailModel.From + "<br>Message : " +
                    model.MailModel.Body,
                IsBodyHtml = true
            };
            SmtpClient smtp = new SmtpClient
            {
                Host = host,
                Port = port,
                UseDefaultCredentials = true,
                Credentials = new System.Net.NetworkCredential(email, password),
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network
            };
            smtp.Send(mail);
            return RedirectToAction("Contact");
        }
        #endregion
    }
}