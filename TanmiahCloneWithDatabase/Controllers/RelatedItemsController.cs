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
        // GET: RelatedItems
        public ActionResult Index()
        {
            GetRelatedItems getItems = new GetRelatedItems();
            DataTable dataTable = new DataTable();
            dataTable = getItems.GetRelatedItemsData();
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
            CreateItems createItems = new CreateItems();

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
                sqlCommand = createItems.CreateHeaderData(relatedItemsEditModel);
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
            RelatedItemsEditModel relatedItemsEditModel = new RelatedItemsEditModel();
            RelatedItemsService relatedItemsService = new RelatedItemsService();
            relatedItemsEditModel = relatedItemsService.FillData(id);
            if (relatedItemsEditModel != null)
            {
                return View(relatedItemsEditModel);
            }
            return RedirectToAction("Index");
        }

        // POST: RelatedItems/Edit/5
        [HttpPost]
        public ActionResult Edit(RelatedItemsEditModel relatedItemsEditModel, HttpPostedFileBase Image) 
        {
            UpdateItems updateItem = new UpdateItems();
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
                sqlCommand = updateItem.UpdateItemsData(relatedItemsEditModel, type);
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
