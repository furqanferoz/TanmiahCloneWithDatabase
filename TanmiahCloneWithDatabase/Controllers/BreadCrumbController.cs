using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TanmiahCloneWithDatabase.Controllers
{
    public class BreadCrumbController : Controller
    {
        string connectionString = ConfigurationManager.ConnectionStrings["TanmiahClone"].ConnectionString;

        // GET: BreadCrumb
        public ActionResult Index()
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("Select * from BreadCrum", sqlConnection);
                sqlDataAdapter.Fill(dataTable);
            }
            return PartialView("_BreadCrumb", dataTable);
        }

        // GET: BreadCrumb/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BreadCrumb/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BreadCrumb/Create
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

        // GET: BreadCrumb/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BreadCrumb/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: BreadCrumb/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BreadCrumb/Delete/5
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
