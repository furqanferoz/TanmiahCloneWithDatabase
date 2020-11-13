using SqlItmes;
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
    public class RelatedItemsController : Controller
    {
        // GET: RelatedItems
        public ActionResult Index()
        {
            DataTable dataTable = new DataTable();
            SqlDataAdapter sqlDataAdapter;
            using (SqlConnection conn = new SqlConnection(SqlConn.ConnectionString))
            {
                conn.Open();
                sqlDataAdapter = new SqlDataAdapter("Select * from RelatedItems", conn);
                sqlDataAdapter.Fill(dataTable);
            }
            return PartialView("_RelatedItems", dataTable);
        }

        // GET: RelatedItems/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RelatedItems/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RelatedItems/Create
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
        // GET: RelatedItems/Edit/5
        public ActionResult Edit(int id)
        {
            RelatedItemsEditModel relatedItemsEditModel = new RelatedItemsEditModel();
            DataTable dataTableEdit = new DataTable();
            using (SqlConnection con = new SqlConnection(SqlConn.ConnectionString))
            {
                con.Open();
                string editQuery = "Select * from RelatedItems where ID = @ID";
                SqlDataAdapter sqlDataAdapterEdit = new SqlDataAdapter(editQuery, con);
                sqlDataAdapterEdit.SelectCommand.Parameters.AddWithValue("@ID", id);
                sqlDataAdapterEdit.Fill(dataTableEdit);

            }
            if (dataTableEdit.Rows.Count == 1)
            {
                relatedItemsEditModel.ID = Convert.ToInt32(dataTableEdit.Rows[0][0].ToString());
                relatedItemsEditModel.Image = dataTableEdit.Rows[0][1].ToString();
                relatedItemsEditModel.Name = dataTableEdit.Rows[0][2].ToString();
                relatedItemsEditModel.Title = dataTableEdit.Rows[0][3].ToString();
                relatedItemsEditModel.Description = dataTableEdit.Rows[0][4].ToString();
                return View(relatedItemsEditModel);
            }

            return RedirectToAction("Index");
        }

        // POST: RelatedItems/Edit/5
        [HttpPost]
        public ActionResult Edit(RelatedItemsEditModel relatedItemsEditModel)
        {
            using (SqlConnection con = new SqlConnection(SqlConn.ConnectionString))
            {
                con.Open();
                string updateQuery = "Update RelatedItems Set RelatedImage = @Image, RelatedName = @Name, RelatedTitle = @Title, RelatedDesc = @Description where ID = @ID";
                SqlCommand sqlCommand = new SqlCommand(updateQuery, con);
                sqlCommand.Parameters.AddWithValue("@ID", relatedItemsEditModel.ID);
                sqlCommand.Parameters.AddWithValue("@Image", relatedItemsEditModel.Image);
                sqlCommand.Parameters.AddWithValue("@Name", relatedItemsEditModel.Name);
                sqlCommand.Parameters.AddWithValue("@Title", relatedItemsEditModel.Title);
                sqlCommand.Parameters.AddWithValue("@Description", relatedItemsEditModel.Description);
                sqlCommand.ExecuteNonQuery();

            }
            return RedirectToAction("Index", "Home");
        }

        // GET: RelatedItems/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RelatedItems/Delete/5
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
