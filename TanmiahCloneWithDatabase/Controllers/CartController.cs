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
    public class CartController : Controller
    {
        string connectionString = ConfigurationManager.ConnectionStrings["TanmiahClone"].ConnectionString;
        // GET: Cart
        public ActionResult Index()
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("Select * from Cart", sqlConnection);
                sqlDataAdapter.Fill(dataTable);
            }
            return PartialView("_Cart", dataTable);
        }

        // GET: Cart/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Cart/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cart/Create
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
        // GET: Cart/Edit/5
        public ActionResult Edit(int id)
        {
            CartEditModel cartEditModel = new CartEditModel();
            DataTable dataTableEdit = new DataTable();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                string query = "select * from Cart where ID = @ID";
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, sqlConnection);
                sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@ID",id);
                sqlDataAdapter.Fill(dataTableEdit);
            }
            if(dataTableEdit.Rows.Count == 1)
            {
                cartEditModel.ID =Convert.ToInt32(dataTableEdit.Rows[0][0].ToString());
                cartEditModel.Title = dataTableEdit.Rows[0][1].ToString();
                cartEditModel.Description = dataTableEdit.Rows[0][2].ToString();
                cartEditModel.Image = dataTableEdit.Rows[0][3].ToString();
                return View(cartEditModel);
            }
                return RedirectToAction("Index");
        }

        // POST: Cart/Edit/5
        [HttpPost]
        public ActionResult Edit(CartEditModel cartEditModel)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string editQuery = "Update Cart Set CartTitle = @Title, CartDesc = @Description, CartImage = @Image";
                SqlCommand sqlCommand = new SqlCommand(editQuery,conn);
                sqlCommand.Parameters.AddWithValue("@Title",cartEditModel.Title);
                sqlCommand.Parameters.AddWithValue("@Description",cartEditModel.Description);
                sqlCommand.Parameters.AddWithValue("@Image",cartEditModel.Image);
                sqlCommand.ExecuteNonQuery();
            }
                return RedirectToAction("Index", "Home");
        }

        // GET: Cart/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Cart/Delete/5
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
