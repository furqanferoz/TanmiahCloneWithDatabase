using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Mvc;
using TanmiahCloneWithDatabase.Models;
using TanmiahCloneWithDatabase.Services;

namespace TanmiahCloneWithDatabase.Controllers
{
    public class IngredientController : Controller
    {
        SqlCommand sqlCommand;
        // GET: Ingredient
        public ActionResult Index()
        {
            GetIngredient getIngredient = new GetIngredient();
            int ID = 1;
            DataTable dataTable = new DataTable();
            dataTable = getIngredient.GetIngredientData(ID);
            return PartialView("_IngredientDetail", dataTable);
        }

        // GET: Ingredient/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Ingredient/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Ingredient/Create
        [HttpPost]
        public ActionResult Create(IngredientEditModel ingredientEditModel, HttpPostedFileBase Image)
        {
            CreateIngredient createIngredient = new CreateIngredient();

            sqlCommand = new SqlCommand();

            sqlCommand = createIngredient.CreateIngredientData(ingredientEditModel);


            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        // GET: Ingredient/Edit/5
        public ActionResult Edit(int id)
        {
            IngredientEditModel ingredientModel = new IngredientEditModel();
            IngredientService ingredentService = new IngredientService();
            ingredientModel = ingredentService.FillData(id);
            if (ingredientModel != null)
            {
                return View(ingredientModel);
            }
            return RedirectToAction("Index");
        }

        // POST: Ingredient/Edit/5
        [HttpPost]
        public ActionResult Edit(IngredientEditModel ingredientEditModel)
        {
            UpdateIngredient updateIngredient = new UpdateIngredient();
            string type = "Update";
            sqlCommand = new SqlCommand();

            sqlCommand = updateIngredient.UpdateIngredientData(ingredientEditModel,type);

            return RedirectToAction("Index", "Home");
        }

        // GET: Ingredient/Delete/5
        public ActionResult Delete(int id)
        {
            IngredientEditModel IngredientModel = new IngredientEditModel();
            IngredientService ingredientService = new IngredientService();
            IngredientModel = ingredientService.FillData(id);
            if (IngredientModel != null)
            {
                return View(IngredientModel);
            }
            return RedirectToAction("Index");
        }

        // POST: Ingredient/Delete/5
        [HttpPost]
        public ActionResult Delete(IngredientEditModel ingredientEditModel)
        {
            UpdateIngredient updateIngredient = new UpdateIngredient();

            sqlCommand = new SqlCommand();
            try
            {
                string type = "Delete";
                sqlCommand = updateIngredient.UpdateIngredientData(ingredientEditModel, type);
            }
            catch (Exception ex)
            {
                ViewBag.FileStatus = ex;
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
