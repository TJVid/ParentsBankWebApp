﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ParentsBankWebApp_Group.Models;

namespace ParentsBankWebApp_Group.Controllers
{
    [Authorize]
    public class WishListItemsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: WishListItems
        public ActionResult Index()
        {
            var wishListItems = db.WishListItems.Include(w => w.Account);
            return View(wishListItems.ToList());
        }

        // GET: WishListItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WishListItem wishListItem = db.WishListItems.Find(id);
            if (wishListItem == null)
            {
                return HttpNotFound();
            }
            return View(wishListItem);
        }

        // GET: WishListItems/Create
        public ActionResult Create()
        {
            ViewBag.AccountID = new SelectList(db.Accounts, "Id", "recipientMailID");
            WishListItem wsItem = new WishListItem();
            wsItem.dateAdded = DateTime.Today;
            return View(wsItem);
        }

        // POST: WishListItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,AccountID,dateAdded,cost,description,link,purchased")] WishListItem wishListItem)
        {
            if (ModelState.IsValid)
            {
                db.WishListItems.Add(wishListItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AccountID = new SelectList(db.Accounts, "Id", "recipientMailID", wishListItem.AccountID);
            return View(wishListItem);
        }

        // GET: WishListItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WishListItem wishListItem = db.WishListItems.Find(id);
            if (wishListItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.AccountID = new SelectList(db.Accounts, "Id", "recipientMailID", wishListItem.AccountID);
            return View(wishListItem);
        }

        // POST: WishListItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,AccountID,dateAdded,cost,description,link,purchased")] WishListItem wishListItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(wishListItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AccountID = new SelectList(db.Accounts, "Id", "recipientMailID", wishListItem.AccountID);
            return View(wishListItem);
        }

        // GET: WishListItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WishListItem wishListItem = db.WishListItems.Find(id);
            if (wishListItem == null)
            {
                return HttpNotFound();
            }
            return View(wishListItem);
        }

        // POST: WishListItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WishListItem wishListItem = db.WishListItems.Find(id);
            db.WishListItems.Remove(wishListItem);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
