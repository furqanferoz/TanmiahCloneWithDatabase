﻿using System;
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
        DataTable dataTable;
        SqlCommand sqlCommand;
        BreadCrumbModel BreadCrumbModel;

        private IBreadCrumbService _breadCrumbService;
        private IUpdateBreadCrumb _updateBreadCrumb;
        private IGetBreadCrumb _getBreadCrumb;
        private ICreateBreadCrumb _createBreadCrumb;
        public BreadCrumbController(ICreateBreadCrumb createBreadCrumb, IGetBreadCrumb getBreadCrumb, IUpdateBreadCrumb updateBreadCrumb, IBreadCrumbService breadCrumbService, BreadCrumbModel breadCrumbModel)
        {
            this.BreadCrumbModel = breadCrumbModel;
            this._breadCrumbService = breadCrumbService;
            this._getBreadCrumb = getBreadCrumb;
            this._createBreadCrumb = createBreadCrumb;
            this._updateBreadCrumb = updateBreadCrumb;
        }

        // GET: BreadCrumb
        public ActionResult Index()
        {
            int id = 1;
            dataTable = new DataTable();
            dataTable = this._getBreadCrumb.GetBreadCrumbData(id);
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
            sqlCommand = new SqlCommand();
            sqlCommand = this._createBreadCrumb.CreateBreadCrumbData(breadCrumbModel);
            return RedirectToAction("Index", "Home");
        }

        // GET: BreadCrumb/Edit/5
        public ActionResult Edit(int id)
        {
           
            this.BreadCrumbModel = this._breadCrumbService.FillData(id);
            if (this.BreadCrumbModel != null)
            {
                return View(this.BreadCrumbModel);
            }
            return RedirectToAction("Index");
        }

        // POST: BreadCrumb/Edit/5
        [HttpPost]
        public ActionResult Edit(BreadCrumbModel breadCrumb)
        {
            string type = "Update";
            sqlCommand = new SqlCommand();
            sqlCommand = this._updateBreadCrumb.UpdateBreadCrumbData(breadCrumb, type);
            return RedirectToAction("Index", "Home");
        }

        // GET: BreadCrumb/Delete/5
        public ActionResult Delete(int id)
        {
            this.BreadCrumbModel = this._breadCrumbService.FillData(id);
            if (this.BreadCrumbModel != null)
            {
                return View(this.BreadCrumbModel);
            }
            return RedirectToAction("Index");
        }

        // POST: BreadCrumb/Delete/5
        [HttpPost]
        public ActionResult Delete(BreadCrumbModel breadCrumbModel)
        {
            sqlCommand = new SqlCommand();
            try
            {
                string type = "Delete";
                sqlCommand = this._updateBreadCrumb.UpdateBreadCrumbData(breadCrumbModel,type);
            }
            catch (Exception ex)
            {
                ViewBag.FileStatus = ex;
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
