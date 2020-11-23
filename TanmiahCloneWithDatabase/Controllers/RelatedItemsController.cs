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
using TanmiahCloneWithDatabase.Services;

namespace TanmiahCloneWithDatabase.Controllers
{
    public class RelatedItemsController : Controller
    {
        SqlCommand sqlCommand;
        private ICreateItems _createItems;
        private IUpdateItems _updateItems;
        private IGetRelatedItems _getRelatedItems;
        private IRelatedItemsService _RelatedItemsService;
        private RelatedItemsEditModel RelatedItemsEditModel;

        public RelatedItemsController(ICreateItems createItems, IUpdateItems updateItems, IGetRelatedItems getRelatedItems,
            IRelatedItemsService relatedItemsService, RelatedItemsEditModel relatedItemsEditModel)
        {
            this._createItems = createItems;
            this._updateItems = updateItems;
            this._getRelatedItems = getRelatedItems;
            this._RelatedItemsService = relatedItemsService;
            this.RelatedItemsEditModel = relatedItemsEditModel;
        }

        // GET: RelatedItems
        public ActionResult Index()
        {
            DataTable dataTable = new DataTable();
            dataTable = this._getRelatedItems.GetRelatedItemsData();
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
        public ActionResult Create(RelatedItemsEditModel relatedItemsEditModel, HttpPostedFileBase Image)
        {
            sqlCommand = new SqlCommand();
            try
            {
                if (Image != null)
                {
                    string filename = System.IO.Path.GetFileName(Image.FileName);
                    string path = System.IO.Path.Combine(Server.MapPath("~/UploadedFiles"), filename);
                    relatedItemsEditModel.Image = Image.FileName;
                    Image.SaveAs(path);
                }
                ViewBag.FileStatus = "File uploaded successfully.";
                sqlCommand = this._createItems.CreateHeaderData(relatedItemsEditModel);
            }
            catch (Exception)
            {
                ViewBag.FileStatus = "Error while file uploading.";
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        // GET: RelatedItems/Edit/5
        public ActionResult Edit(int id)
        {
            this.RelatedItemsEditModel = this._RelatedItemsService.FillData(id);
            if (this.RelatedItemsEditModel.Description != null && 
                this.RelatedItemsEditModel.Image != null && 
                this.RelatedItemsEditModel.Name != null && 
                this.RelatedItemsEditModel.Title != null)
            {
                return View(this.RelatedItemsEditModel);
            }
            else
            {
                return PartialView("_404");
            }
        }

        // POST: RelatedItems/Edit/5
        [HttpPost]
        public ActionResult Edit(RelatedItemsEditModel relatedItemsEditModel, HttpPostedFileBase Image) 
        {
            string type = "Update";
            sqlCommand = new SqlCommand();
            try
            {
                if (Image != null)
                {
                    string filename = System.IO.Path.GetFileName(Image.FileName);
                    string path = System.IO.Path.Combine(Server.MapPath("~/UploadedFiles"), filename);
                    relatedItemsEditModel.Image = Image.FileName;
                    Image.SaveAs(path);
                }
                ViewBag.FileStatus = "File uploaded successfully.";
                sqlCommand = this._updateItems.UpdateItemsData(relatedItemsEditModel, type);
            }
            catch (Exception)
            {
                ViewBag.FileStatus = "Error while file uploading.";
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: RelatedItems/Delete/5
        public ActionResult Delete(int id)
        {
            this.RelatedItemsEditModel = this._RelatedItemsService.FillData(id);
            if (this.RelatedItemsEditModel.Description != null &&
                this.RelatedItemsEditModel.Image != null &&
                this.RelatedItemsEditModel.Name != null &&
                this.RelatedItemsEditModel.Title != null)
            {
                return View(this.RelatedItemsEditModel);
            }
            else
            {
                return PartialView("_404");
            }
        }

        // POST: RelatedItems/Delete/5
        [HttpPost]
        public ActionResult Delete(RelatedItemsEditModel itemsEditModel)
        {
            sqlCommand = new SqlCommand();
            try
            {
                string type = "Delete";
                sqlCommand = this._updateItems.UpdateItemsData(itemsEditModel, type);
            }
            catch (Exception ex)
            {
                ViewBag.FileStatus = ex;
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
