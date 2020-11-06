using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using TanmiahCloneWithDatabase.Models;

namespace TanmiahCloneWithDatabase.Controllers
{
    public class HeaderCartController : Controller
    {
        string connectionString = ConfigurationManager.ConnectionStrings["TanmiahClone"].ConnectionString;


        [HttpGet]
        // GET: HeaderCart
        public ActionResult Index()
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("Select * from BannerDetail", sqlConnection);
                sqlDataAdapter.Fill(dataTable);
            }
            return PartialView("_HeaderCart", dataTable);
        }

        // GET: HeaderCart/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HeaderCart/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HeaderCart/Create
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
        // GET: HeaderCart/Edit/5
        public ActionResult Edit(int id)
        {
            HeaderCartEditModel headerCartModel = new HeaderCartEditModel();
            DataTable dataTableEdit = new DataTable();
            using (SqlConnection Conn = new SqlConnection(connectionString))
            {
                Conn.Open();
                string query = "Select * from BannerDetail Where ID = @ID";
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, Conn);
                sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@ID", id);
                sqlDataAdapter.Fill(dataTableEdit);
            }
            if (dataTableEdit.Rows.Count == 1)
            {
                headerCartModel.ID = Convert.ToInt32(dataTableEdit.Rows[0][0].ToString());
                headerCartModel.Tile = (dataTableEdit.Rows[0][1].ToString());
                headerCartModel.Name = (dataTableEdit.Rows[0][2].ToString());
                headerCartModel.Description = (dataTableEdit.Rows[0][3].ToString());
                headerCartModel.Image = (dataTableEdit.Rows[0][4].ToString());
                return View(headerCartModel);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // POST: HeaderCart/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            return View();   
        }

        // GET: HeaderCart/Delete/5
        public ActionResult Delete(int id)
        {

            return View();

        }

        // POST: HeaderCart/Delete/5
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
