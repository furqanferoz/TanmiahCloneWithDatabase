using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TanmiahCloneWithDatabase.Models;
using TanmiahCloneWithDatabase.Services;
using static TanmiahCloneWithDatabase.Services.GetBreadCrumb;

namespace TanmiahCloneWithDatabase.Controllers
{
    public class BreadCrumbController : Controller
    {
        GetBreadCrumb getBreadCrumb;
        DataTable dataTable;
        SqlCommand sqlCommand;
        // GET: BreadCrumb
        public ActionResult Index()
        {
            int id = 1;
            getBreadCrumb = new GetBreadCrumb();
            dataTable = new DataTable();
            dataTable = getBreadCrumb.GetBreadCrumbData(id);
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
        public ActionResult Create(BreadCrumbModel breadCrumbModel)
        {
            CreateBreadCrumb createBreadCrumb = new CreateBreadCrumb();
            sqlCommand = new SqlCommand();
            sqlCommand = createBreadCrumb.CreateBreadCrumbData(breadCrumbModel);
            return RedirectToAction("Index", "Home");
        }

        // GET: BreadCrumb/Edit/5
        public ActionResult Edit(int id)
        {
            BreadCrumbModel breadCrumbModel = new BreadCrumbModel();
            BreadCrumbService breadCrumbService = new BreadCrumbService();
            breadCrumbModel = breadCrumbService.FillData(id);
            if (breadCrumbModel != null)
            {
                return View(breadCrumbModel);
            }
            return RedirectToAction("Index");
        }

        // POST: BreadCrumb/Edit/5
        [HttpPost]
        public ActionResult Edit(BreadCrumbModel breadCrumb)
        {
            UpdateBreadCrumb updateBreadCrum = new UpdateBreadCrumb();

            string type = "Update";
            sqlCommand = new SqlCommand();

            sqlCommand = updateBreadCrum.UpdateBreadCrumbData(breadCrumb, type);

            return RedirectToAction("Index", "Home");
        }

        // GET: BreadCrumb/Delete/5
        public ActionResult Delete(int id)
        {
            BreadCrumbModel breadCrumbModel = new BreadCrumbModel();
            BreadCrumbService breadCrumbService = new BreadCrumbService();
            breadCrumbModel = breadCrumbService.FillData(id);
            if (breadCrumbModel != null)
            {
                return View(breadCrumbModel);
            }
            return RedirectToAction("Index");
        }

        // POST: BreadCrumb/Delete/5
        [HttpPost]
        public ActionResult Delete(BreadCrumbModel breadCrumbModel)
        {
            UpdateBreadCrumb updateBreadCrumb = new UpdateBreadCrumb();
            sqlCommand = new SqlCommand();
            try
            {
                string type = "Delete";
                sqlCommand = updateBreadCrumb.UpdateBreadCrumbData(breadCrumbModel,type);
            }
            catch (Exception ex)
            {
                ViewBag.FileStatus = ex;
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
