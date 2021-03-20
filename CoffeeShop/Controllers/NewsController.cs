using Models.DAO;
using Models.EF;
using System.Web.Mvc;

namespace CoffeeShop.Controllers
{
    public class NewsController : Controller
    {
        CoffeeShopDbContext db = new CoffeeShopDbContext();
        public ActionResult Index()
        {
            ViewBag.listPost = new NewsDao().ListAllPosts();
            return View();
        }
    }
}