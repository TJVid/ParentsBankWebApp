using System;
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
    public class AccountsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        private List<Account> GetAccountsForRole()
        {
            IQueryable<Account> filteredAccounts;
            if (User.IsInRole("Parent"))
            {
                filteredAccounts = db.Accounts.Where(a => a.ownerMailID == User.Identity.Name);
                return filteredAccounts.ToList<Account>();
            }

            if (User.IsInRole("Child"))
            {
                filteredAccounts = db.Accounts.Where(a => a.recipientMailID == User.Identity.Name);
                return filteredAccounts.ToList<Account>();
            }
            return db.Accounts.ToList<Account>();
        }

        public AccountsController()
        {

        }

        // GET: Accounts
        public ActionResult Index()
        {
            var accounts = GetAccountsForRole();
            if (User.IsInRole("Child"))
                return RedirectToAction("Details", accounts.FirstOrDefault());
            return View(accounts);
        }

        // GET: Accounts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Account account = new Account();
            account = GetAccountsForRole().FirstOrDefault(a => a.Id == id);
            //account = GetAccountsForRole().Find(id);

            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // GET: Accounts/Create
        [Authorize(Roles = "Parent")]
        public ActionResult Create()
        {
            var account = new Account();
            account.ownerMailID = User.Identity.Name;
            return View(account);
        }

        // POST: Accounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Parent")]
        public ActionResult Create([Bind(Include = "Id,ownerMailID,recipientMailID,name,openDate,interestRate,principal")] Account account)
        {
            #region Validations
            //Owner and recipient cannot be same email address
            if (account.ownerMailID.Equals(account.recipientMailID))
                ModelState.AddModelError("recipientMailID", "Owner and recipient cannot be same email address");
            //A recipient can have only one recipient registration
            if (GetAccountsForRole().Count(a => a.recipientMailID == account.recipientMailID) > 0)
                ModelState.AddModelError("recipientMailID", "A recipient can have only one recipient registration");
            //An owner cannot be a recipient of another account
            if (GetAccountsForRole().Count(a => a.recipientMailID == account.ownerMailID) > 0)
                ModelState.AddModelError("ownerMailID", "An owner cannot be a recipient of another account");
            //A recipient cannot be an owner of another account
            if (GetAccountsForRole().Count(a => a.ownerMailID == account.recipientMailID) > 0)
                ModelState.AddModelError("recipientMailID", "A recipient can have only one recipient registration");

            #endregion
            if (ModelState.IsValid)
            {
                db.Accounts.Add(account);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(account);
        }

        // GET: Accounts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = GetAccountsForRole().FirstOrDefault(a => a.Id == id);
            if (account == null)
            {
                return HttpNotFound();
            }
            account.openDate = DateTime.Today;
            return View(account);
        }

        // POST: Accounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Parent")]
        public ActionResult Edit([Bind(Include = "Id,ownerMailID,recipientMailID,name,openDate,interestRate,principal")] Account account)
        {
            if (account.ownerMailID.Equals(account.recipientMailID))
            {
                ModelState.AddModelError("recipientMailID", "Owner and recipient cannot be same email address");
            }

            if (ModelState.IsValid)
            {
                db.Entry(account).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(account);
        }

        // GET: Accounts/Delete/5
        [Authorize(Roles = "Parent")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = GetAccountsForRole().FirstOrDefault(a => a.Id == id);

            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // POST: Accounts/Delete/5
        [Authorize(Roles = "Parent")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Parent")]
        public ActionResult DeleteConfirmed(int id)
        {
            Account account = GetAccountsForRole().FirstOrDefault(a => a.Id == id);
            if (account.principal > 0)
            {
                ModelState.AddModelError("principal", "Principal ");
            }
            if (ModelState.IsValid)
            {
                db.Accounts.Remove(account);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(account);
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
