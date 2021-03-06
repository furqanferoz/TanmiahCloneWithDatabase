﻿using System;
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
    public class CartController : Controller
    {

        private SqlCommand sqlCommand;
        private IUpdateCart _updateCart;
        private ICreateCart _createCart;
        private IGetCart _getCart;
        private ICartService _cartService;
        private CartEditModel CartEditModel;

        public CartController(IUpdateCart updateCart, ICreateCart createCart, IGetCart getCart, ICartService cartService, CartEditModel cartModel)
        {
            this.CartEditModel = cartModel;
            this._updateCart = updateCart;
            this._createCart = createCart;
            this._getCart = getCart;
            this._cartService = cartService;
        }

        // GET: Cart
        public ActionResult Index()
        {
            int ID = 1;
            DataTable dataTable = new DataTable();
            dataTable = this._getCart.GetCartData(ID);
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
            sqlCommand = new SqlCommand();
            try
            {
                if (Image != null)
                {
                    string filename = System.IO.Path.GetFileName(Image.FileName);
                    string path = System.IO.Path.Combine(Server.MapPath("~/UploadedFiles"), filename);
                    cartEditModel.Image = Image.FileName;
                    Image.SaveAs(path);
                    ViewBag.ImageUrl = "~/UploadedFiles" + filename;
                }
                ViewBag.FileStatus = "File uploaded successfully.";
                sqlCommand = this._createCart.CreateCartData(cartEditModel);
               
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
            
            this.CartEditModel = this._cartService.FillData(id);
            if (this.CartEditModel.Image != null && 
                this.CartEditModel.Title != null && 
                this.CartEditModel.Description != null)
            {
                return View(this.CartEditModel);
            }
            else
            {
                return PartialView("_404");
            }
            
        }

        // POST: Cart/Edit/5
        [HttpPost]
        public ActionResult Edit(CartEditModel cartEditModel, HttpPostedFileBase Image)
        {
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
                sqlCommand = this._updateCart.UpdateCartData(cartEditModel, type);
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
            this.CartEditModel = this._cartService.FillData(id);
            if (this.CartEditModel.Title != null && 
                this.CartEditModel.Image != null && 
                this.CartEditModel.Description != null)
            {
                return View(this.CartEditModel);
            }
            else
            {
                return PartialView("_404");
            }
        }

        // POST: Cart/Delete/5
        [HttpPost]
        public ActionResult Delete(CartEditModel cartEditModel)
        {
            sqlCommand = new SqlCommand();
            try
            {
                string type = "Delete";
                sqlCommand = this._updateCart.UpdateCartData(cartEditModel, type);
            }
            catch (Exception ex)
            {
                ViewBag.FileStatus = ex;
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
