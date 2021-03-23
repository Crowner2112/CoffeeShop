using Models.DAO;
using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoffeeShop.Areas.Admin.Controllers
{
    public class IngredientController : BaseController
    {
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new IngredientDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Edit(int id)
        {
            var ingredient = new IngredientDao().ViewDetail(id);
            return View(ingredient);
        }
        [HttpPost]
        public ActionResult Create(Ingredient ingredient)
        {
            if (ModelState.IsValid)
            {
                var dao = new IngredientDao();
                ingredient.CreatedDate = DateTime.Now;
                long id = dao.Insert(ingredient);
                if (id > 0)
                {
                    return RedirectToAction("Index", "Ingredient");
                }
                else
                {
                    ModelState.AddModelError("", "Add Ingredient successfully");
                }
            }
            return View("Index");
        }
        [HttpPost]
        public ActionResult Edit(Ingredient ingredient)
        {
            if (ModelState.IsValid)
            {
                var dao = new IngredientDao();
                var result = dao.Update(ingredient);
                if (result)
                {
                    return RedirectToAction("Index", "Ingredient");
                }
                else
                {
                    ModelState.AddModelError("", "Modify Ingredient successfully");
                }
            }
            return View("Index");
        }
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new IngredientDao().Delete(id);
            return RedirectToAction("Index");
        }
        public ActionResult Add(int id)
        {
            IngredientDao.AddQuantity(id);
            return RedirectToAction("Index", "Ingredient");
        }
    }
}