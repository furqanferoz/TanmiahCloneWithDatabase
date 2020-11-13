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
        SqlCommand sqlCommand;

        [HttpGet]
        // GET: HeaderCart
        public ActionResult Index()
        {
            GetHeader getHeader = new GetHeader();
            int ID = 1;
            DataTable dataTable = new DataTable();
            dataTable = getHeader.GetHeaderData(ID);
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
            CreateHeader createHeader = new CreateHeader();

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
                sqlCommand = createHeader.CreateHeaderData(headerCartEditModel);
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
            HeaderCartEditModel headerCartModel = new HeaderCartEditModel();
            HeaderCartService headerCartService = new HeaderCartService();
            headerCartModel = headerCartService.FillData(id);
            if (headerCartModel != null)
            {
                return View(headerCartModel);
            }
            return RedirectToAction("Index");

        }

        // POST: HeaderCart/Edit/5
        [HttpPost]
        public ActionResult Edit(HeaderCartEditModel headerCartEditModel, HttpPostedFileBase Image)
        {
            UpdateHeader updateHeader = new UpdateHeader();
            string type = "Update";
            sqlCommand = new SqlCommand();
            try
            {
                if (Image != null)
                    
                {
                    string filename = System.IO.Path.GetFileName(Image.FileName);
                    string path = System.IO.Path.Combine(Server.MapPath("~/UploadedFiles"),filename);
                    headerCartEditModel.Image = Image.FileName;
                    Image.SaveAs(path);
                }
                ViewBag.FileStatus = "File uploaded successfully.";
                sqlCommand = updateHeader.UpdateHeaderData(headerCartEditModel, type);
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
            HeaderCartEditModel headerCartModel = new HeaderCartEditModel();
            HeaderCartService headerCartService = new HeaderCartService();
            headerCartModel = headerCartService.FillData(id);
            if (headerCartModel != null)
            {
                return View(headerCartModel);
            }
            return RedirectToAction("Index");
        }

        // POST: HeaderCart/Delete/5
        [HttpPost]
        public ActionResult Delete(HeaderCartEditModel headerCartEditModel)
        {
            UpdateHeader updateHeader = new UpdateHeader();

            sqlCommand = new SqlCommand();
            try
            {
                string type = "Delete";
                sqlCommand = updateHeader.UpdateHeaderData(headerCartEditModel, type);
            }
            catch (Exception ex)
            {
                ViewBag.FileStatus = ex;
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
