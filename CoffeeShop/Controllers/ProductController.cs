using Models.DAO;
using Models.EF;
using System.Linq;
using System.Web.Mvc;

namespace CoffeeShop.Controllers
{
    public class ProductController : Controller
    {
        CoffeeShopDbContext db = new CoffeeShopDbContext();
        public ActionResult Index()
        {
            var modelCategory = (from c in db.Categories select c.CategoryName).ToList();
            ViewBag.Category = modelCategory;
            ViewBag.Product = new ProductDao().ListAllProduct();
            return View();
        }

    }
}