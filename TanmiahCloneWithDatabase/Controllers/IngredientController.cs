using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TanmiahCloneWithDatabase.Models;

namespace TanmiahCloneWithDatabase.Controllers
{
    public class IngredientController : Controller
    {
        string ConnectionString = ConfigurationManager.ConnectionStrings["TanmiahClone"].ConnectionString;
        // GET: Ingredient
        public ActionResult Index()
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection Conn = new SqlConnection(ConnectionString))
            {
                Conn.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("Select * from IngredientsDetail", Conn);
                sqlDataAdapter.Fill(dataTable);
            }
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
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        [HttpGet]
        // GET: Ingredient/Edit/5
        public ActionResult Edit(int id)
        {
            IngredientEditModel ingredientEditModel = new IngredientEditModel();
            DataTable dataTableEdit = new DataTable();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                string query = "Select * from IngredientsDetail where ID = @ID";
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, con);
                sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@ID", id);
                sqlDataAdapter.Fill(dataTableEdit);
            }
            if (dataTableEdit.Rows.Count == 1)
            {
                ingredientEditModel.ID = Convert.ToInt32(dataTableEdit.Rows[0][0].ToString());
                ingredientEditModel.Title = dataTableEdit.Rows[0][1].ToString();
                ingredientEditModel.DescriptionOne = dataTableEdit.Rows[0][3].ToString();
                ingredientEditModel.DescriptionSec = dataTableEdit.Rows[0][4].ToString();
                ingredientEditModel.Protein = dataTableEdit.Rows[0][5].ToString();
                ingredientEditModel.Carbohydrate = dataTableEdit.Rows[0][6].ToString();
                ingredientEditModel.Fat = dataTableEdit.Rows[0][7].ToString();
                return View(ingredientEditModel);
            }

            return RedirectToAction("Index");
        }

        // POST: Ingredient/Edit/5
        [HttpPost]
        public ActionResult Edit(IngredientEditModel ingredientEditModel)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                string updateQuery = "Update IngredientsDetail Set IngredientTitle = @Title, IngredientDesc = @Desc," +
                    " IngredientDescSec = @DescSec, IngredientProtein = @Protein, IngredientCarbohydrate = @Carbohydrate, IngredientFat = @Fat";
                SqlCommand sqlCommand = new SqlCommand(updateQuery,conn);
                sqlCommand.Parameters.AddWithValue("@Title",ingredientEditModel.Title);
                sqlCommand.Parameters.AddWithValue("@Desc", ingredientEditModel.DescriptionOne);
                sqlCommand.Parameters.AddWithValue("@DescSec", ingredientEditModel.DescriptionSec);
                sqlCommand.Parameters.AddWithValue("@Protein",ingredientEditModel.Protein);
                sqlCommand.Parameters.AddWithValue("@Carbohydrate", ingredientEditModel.Carbohydrate);
                sqlCommand.Parameters.AddWithValue("@Fat", ingredientEditModel.Fat);
                sqlCommand.ExecuteNonQuery();

            }
            return RedirectToAction("Index","Home");
        }

        // GET: Ingredient/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Ingredient/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
