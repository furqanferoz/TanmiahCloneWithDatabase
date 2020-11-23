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
        DataTable dataTable;
        private IGetIngredient _getIngredient;
        private ICreateIngredient _createIngredient;
        private IUpdateIngredient _updateIngredient;
        private IIngredientService _ingredientService;
        private IngredientEditModel IngredientEditModel;

        public IngredientController(IGetIngredient getIngredient, ICreateIngredient createIngredient, IUpdateIngredient updateIngredient, IIngredientService ingredientService, IngredientEditModel ingredientEditModel)
        {
            this._createIngredient = createIngredient;
            this._getIngredient = getIngredient;
            this._ingredientService = ingredientService;
            this._updateIngredient = updateIngredient;
            this.IngredientEditModel = ingredientEditModel;
        }

        // GET: Ingredient
        public ActionResult Index()
        {
            int ID = 1;
            dataTable = new DataTable();
            dataTable = this._getIngredient.GetIngredientData(ID);
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
            sqlCommand = new SqlCommand();
            sqlCommand = this._createIngredient.CreateIngredientData(ingredientEditModel);
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        // GET: Ingredient/Edit/5
        public ActionResult Edit(int id)
        {
            this.IngredientEditModel = this._ingredientService.FillData(id);
            if (this.IngredientEditModel.Carbohydrate != null && 
                this.IngredientEditModel.DescriptionOne != null && 
                this.IngredientEditModel.DescriptionSec != null && 
                this.IngredientEditModel.Fat != null && 
                this.IngredientEditModel.OrderTitle != null && 
                this.IngredientEditModel.Protein != null && 
                this.IngredientEditModel.Title != null && 
                this.IngredientEditModel.TitleOne != null)
            {
                return View(this.IngredientEditModel);
            }
            else
            {
                return PartialView("_404");
            }
           
        }

        // POST: Ingredient/Edit/5
        [HttpPost]
        public ActionResult Edit(IngredientEditModel ingredientEditModel)
        {
            UpdateIngredient updateIngredient = new UpdateIngredient();
            string type = "Update";
            sqlCommand = new SqlCommand();

            sqlCommand = updateIngredient.UpdateIngredientData(ingredientEditModel, type);

            return RedirectToAction("Index", "Home");
        }

        // GET: Ingredient/Delete/5
        public ActionResult Delete(int id)
        {
            this.IngredientEditModel = this._ingredientService.FillData(id);
            if (this.IngredientEditModel.Carbohydrate != null && 
                this.IngredientEditModel.DescriptionOne != null &&
                this.IngredientEditModel.DescriptionSec != null &&
                this.IngredientEditModel.Fat != null && 
                this.IngredientEditModel.OrderTitle != null &&
                this.IngredientEditModel.Protein != null && 
                this.IngredientEditModel.Title != null && 
                this.IngredientEditModel.TitleOne != null)
            {
                return View(this.IngredientEditModel);
            }
            else
            {
                return PartialView("_404");
            }
           
        }

        // POST: Ingredient/Delete/5
        [HttpPost]
        public ActionResult Delete(IngredientEditModel ingredientEditModel)
        {
           sqlCommand = new SqlCommand();
            try
            {
                string type = "Delete";
                sqlCommand = this._updateIngredient.UpdateIngredientData(ingredientEditModel, type);
            }
            catch (Exception ex)
            {
                ViewBag.FileStatus = ex;
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
