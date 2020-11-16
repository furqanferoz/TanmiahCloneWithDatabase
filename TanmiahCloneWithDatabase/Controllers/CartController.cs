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
    public class CartController : Controller
    {
        string connectionString = ConfigurationManager.ConnectionStrings["TanmiahClone"].ConnectionString;
        private SqlCommand sqlCommand;

        // GET: Cart
        public ActionResult Index()
        {
            GetCart getCart= new GetCart();
            int ID = 1;
            DataTable dataTable = new DataTable();
            dataTable = getCart.GetCartData(ID);
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
        public ActionResult Create(CartEditModel cartEditModel, HttpPostedFileBase Image)
        {
            CreateCart createCart= new CreateCart();

            sqlCommand = new SqlCommand();
            try
            {
                if (Image != null)

                {
                    string filename = System.IO.Path.GetFileName(Image.FileName);
                    string path = System.IO.Path.Combine(Server.MapPath("~/UploadedFiles"), filename);
                    cartEditModel.Image = Image.FileName;
                    Image.SaveAs(path);
                }
                ViewBag.FileStatus = "File uploaded successfully.";
                sqlCommand = createCart.CreateCartData(cartEditModel);
            }
            catch (Exception)
            {
                ViewBag.FileStatus = "Error while file uploading.";
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        // GET: Cart/Edit/5
        public ActionResult Edit(int id)
        {
            CartEditModel CartModel = new CartEditModel();
            CartService CartService = new CartService();
            CartModel = CartService.FillData(id);
            if (CartModel != null)
            {
                return View(CartModel);
            }
            return RedirectToAction("Index");
        }

        // POST: Cart/Edit/5
        [HttpPost]
        public ActionResult Edit(CartEditModel cartEditModel, HttpPostedFileBase Image)
        {
            UpdateCart updateCart = new UpdateCart();
            string type = "Update";
            sqlCommand = new SqlCommand();
            try
            {
                if (Image != null)

                {
                    string filename = System.IO.Path.GetFileName(Image.FileName);
                    string path = System.IO.Path.Combine(Server.MapPath("~/UploadedFiles"), filename);
                    cartEditModel.Image = Image.FileName;
                    Image.SaveAs(path);
                }
                ViewBag.FileStatus = "File uploaded successfully.";
                sqlCommand = updateCart.UpdateCartData(cartEditModel, type);
            }
            catch (Exception)
            {
                ViewBag.FileStatus = "Error while file uploading.";
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: Cart/Delete/5
        public ActionResult Delete(int id)
        {
            CartEditModel CartModel = new CartEditModel();
            CartService cartService = new CartService();
            CartModel = cartService.FillData(id);
            if (CartModel != null)
            {
                return View(CartModel);
            }
            return RedirectToAction("Index");
        }

        // POST: Cart/Delete/5
        [HttpPost]
        public ActionResult Delete(CartEditModel cartEditModel)
        {
            UpdateCart updateCart = new UpdateCart();

            sqlCommand = new SqlCommand();
            try
            {
                string type = "Delete";
                sqlCommand = updateCart.UpdateCartData(cartEditModel, type);
            }
            catch (Exception ex)
            {
                ViewBag.FileStatus = ex;
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
