using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using TanmiahCloneWithDatabase.Models;
using TanmiahCloneWithDatabase.Services;
using System.IO;

namespace TanmiahCloneWithDatabase.Controllers
{
    public class HeaderCartController : Controller
    {
        DataTable dataTable;
        SqlCommand sqlCommand;
        private IGetHeader _getHeader;
        private ICreateHeader _createHeader;
        private IUpdateHeader _updateHeader;
        private IHeaderCartService _headerCartService;
        private HeaderCartEditModel HeaderCartEditModel;



        public HeaderCartController(IGetHeader getHeader, ICreateHeader createHeader, IUpdateHeader updateHeader,
            IHeaderCartService headerCartService, HeaderCartEditModel headerCartEditModel)
        {
            this._createHeader = createHeader;
            this._getHeader = getHeader;
            this._updateHeader = updateHeader;
            this._headerCartService = headerCartService;
            this.HeaderCartEditModel = headerCartEditModel;

        }

        [HttpGet]
        // GET: HeaderCart
        public ActionResult Index()
        {
            int ID = 1;
            dataTable = new DataTable();
            dataTable = this._getHeader.GetHeaderData(ID);
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
        public ActionResult Create(HeaderCartEditModel headerCartEditModel, HttpPostedFileBase Image)
        {
            sqlCommand = new SqlCommand();
            try
            {
                if (Image != null)

                {
                    string filename = System.IO.Path.GetFileName(Image.FileName);
                    string path = System.IO.Path.Combine(Server.MapPath("~/UploadedFiles"), filename);
                    headerCartEditModel.Image = Image.FileName;
                    Image.SaveAs(path);
                }
                ViewBag.FileStatus = "File uploaded successfully.";
                sqlCommand = this._createHeader.CreateHeaderData(headerCartEditModel);
            }
            catch (Exception)
            {
                ViewBag.FileStatus = "Error while file uploading.";
            }
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        // GET: HeaderCart/Edit/5
        public ActionResult Edit(int id)
        {

            this.HeaderCartEditModel = this._headerCartService.FillData(id);
            if (this.HeaderCartEditModel != null)
            {
                return View(this.HeaderCartEditModel);
            }
            return RedirectToAction("Index");

        }

        // POST: HeaderCart/Edit/5
        [HttpPost]
        public ActionResult Edit(HeaderCartEditModel headerCartEditModel, HttpPostedFileBase Image)
        {
            string type = "Update";
            sqlCommand = new SqlCommand();
            try
            {
                if (Image != null)

                {
                    string filename = System.IO.Path.GetFileName(Image.FileName);
                    string path = System.IO.Path.Combine(Server.MapPath("~/UploadedFiles"), filename);
                    headerCartEditModel.Image = Image.FileName;
                    Image.SaveAs(path);
                }
                ViewBag.FileStatus = "File uploaded successfully.";
                sqlCommand = this._updateHeader.UpdateHeaderData(headerCartEditModel, type);
            }
            catch (Exception)
            {
                ViewBag.FileStatus = "Error while file uploading.";
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: HeaderCart/Delete/5
        public ActionResult Delete(int id)
        {
            this.HeaderCartEditModel = this._headerCartService.FillData(id);
            if (this.HeaderCartEditModel != null)
            {
                return View(this.HeaderCartEditModel);
            }
            return RedirectToAction("Index");
        }

        // POST: HeaderCart/Delete/5
        [HttpPost]
        public ActionResult Delete(HeaderCartEditModel headerCartEditModel)
        {
            sqlCommand = new SqlCommand();
            try
            {
                string type = "Delete";
                sqlCommand = this._updateHeader.UpdateHeaderData(headerCartEditModel, type);
            }
            catch (Exception ex)
            {
                ViewBag.FileStatus = ex;
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
