using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Test421_DBFirst.Models;

namespace Test421_DBFirst.Controllers
{
    public class PeopleController : Controller
    {
        private AdventureModel db = new AdventureModel();

        // GET: People 
        public ActionResult Index()
        {

            SortingPagingInfo info = new SortingPagingInfo();
            info.SortDirection = "ascending";
            info.SortField = "LastName";
            info.PageSize = 10;
            info.PageCount = Convert.ToInt32(Math.Ceiling((double)db.People.Count()/info.PageSize));
            info.CurrentPageIndex = 0;


            var personList =  db.People.OrderBy(c=>c.LastName).Take(info.PageSize).ToList();
            ViewBag.SortingPagingInfo = info;
            // return View(db.People.ToList());
            return View(personList); ////
        }

        [HttpPost]
        public ActionResult Index(SortingPagingInfo info, string searchName)
        {
            if (searchName != null && searchName != "")
                return Search(info, searchName);

            IQueryable<Person> personList = null;

            bool bSortingAscending = info.SortDirection == "descending" ? false : true;
            switch(info.SortField)
            {
                case "LastName":
                    personList = bSortingAscending ? db.People.OrderBy(c => c.LastName) : db.People.OrderByDescending(c => c.LastName);
                    break;
                case "FirstName":
                    personList = bSortingAscending ? db.People.OrderBy(c => c.FirstName) : db.People.OrderByDescending(c => c.FirstName);
                    break;
                case "PersonType":
                    personList = bSortingAscending ? db.People.OrderBy(c => c.PersonType) : db.People.OrderByDescending(c => c.PersonType);
                    break;
                case "Title":
                    personList = bSortingAscending ? db.People.OrderBy(c => c.Title) : db.People.OrderByDescending(c => c.Title);
                    break;
                case "NameStyle":
                    personList = bSortingAscending ? db.People.OrderBy(c => c.NameStyle) : db.People.OrderByDescending(c => c.NameStyle);
                    break;
                case "MiddleName":
                    personList = bSortingAscending ? db.People.OrderBy(c => c.MiddleName) : db.People.OrderByDescending(c => c.MiddleName);
                    break;
                case "Suffix":
                    personList = bSortingAscending ? db.People.OrderBy(c => c.Suffix) : db.People.OrderByDescending(c => c.Suffix);
                    break;
                case "EmailPromotion":
                    personList = bSortingAscending ? db.People.OrderBy(c => c.EmailPromotion) : db.People.OrderByDescending(c => c.EmailPromotion);
                    break;
                case "ModifiedDate":
                    personList = bSortingAscending ? db.People.OrderBy(c => c.ModifiedDate) : db.People.OrderByDescending(c => c.ModifiedDate);
                    break;
            }

            personList = personList.Skip(info.CurrentPageIndex * info.PageSize).Take(info.PageSize);
            ViewBag.SortingPagingInfo = info;

            return View(personList.ToList());
        }

        public ActionResult Search(SortingPagingInfo info , string searchName)
        {
            IQueryable<Person> personList = null;
            personList = from r in db.People
                             where r.FirstName.Contains(searchName) || r.LastName.Contains(searchName)
                            select r;
            
            if (info.PageSize ==0) // no page information, need to create one
            {
                //personList = from r in db.People
                //             where r.FirstName.Contains(searchName) || r.LastName.Contains(searchName)
                //             orderby r.LastName
                //             select r;
    
                info = new SortingPagingInfo();
                // SortingPagingInfo info = new SortingPagingInfo();
                info.SortDirection = "ascending";
                info.SortField = "LastName";
                info.PageSize = 10;
                info.PageCount = Convert.ToInt32(Math.Ceiling((double)personList.Count() / info.PageSize));
                info.CurrentPageIndex = 0;
            }

            bool bSortingAscending = info.SortDirection == "descending" ? false : true;
            switch (info.SortField)
            {
                case "LastName":
                    personList = bSortingAscending ? personList.OrderBy(c => c.LastName) : personList.OrderByDescending(c => c.LastName);
                    break;
                case "FirstName":
                    personList = bSortingAscending ? personList.OrderBy(c => c.FirstName) : personList.OrderByDescending(c => c.FirstName);
                    break;
                case "PersonType":
                    personList = bSortingAscending ? personList.OrderBy(c => c.PersonType) : personList.OrderByDescending(c => c.PersonType);
                    break;
                case "Title":
                    personList = bSortingAscending ? personList.OrderBy(c => c.Title) : personList.OrderByDescending(c => c.Title);
                    break;
                case "NameStyle":
                    personList = bSortingAscending ? personList.OrderBy(c => c.NameStyle) : personList.OrderByDescending(c => c.NameStyle);
                    break;
                case "MiddleName":
                    personList = bSortingAscending ? personList.OrderBy(c => c.MiddleName) : personList.OrderByDescending(c => c.MiddleName);
                    break;
                case "Suffix":
                    personList = bSortingAscending ? personList.OrderBy(c => c.Suffix) : personList.OrderByDescending(c => c.Suffix);
                    break;
                case "EmailPromotion":
                    personList = bSortingAscending ? personList.OrderBy(c => c.EmailPromotion) : personList.OrderByDescending(c => c.EmailPromotion);
                    break;
                case "ModifiedDate":
                    personList = bSortingAscending ? personList.OrderBy(c => c.ModifiedDate) : personList.OrderByDescending(c => c.ModifiedDate);
                    break;
            }




            ViewBag.SortingPagingInfo = info;
                ViewBag.SearchName = searchName;
                // return View(db.People.ToList());
                return View("Index", personList.Skip(info.CurrentPageIndex * info.PageSize).Take(info.PageSize));
            

        }

        // GET: People/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.People.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // GET: People/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: People/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BusinessEntityID,PersonType,NameStyle,Title,FirstName,MiddleName,LastName,Suffix,EmailPromotion,AdditionalContactInfo,Demographics,rowguid,ModifiedDate")] Person person)
       // public ActionResult Create([Bind(Include = "BusinessEntityID,PersonType,NameStyle,Title,FirstName,MiddleName,LastName,Suffix,EmailPromotion,ModifiedDate")] Person person)
        {
            if (ModelState.IsValid)
            {
                db.People.Add(person);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(person);
        }

        // GET: People/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.People.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // POST: People/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "BusinessEntityID,PersonType,NameStyle,Title,FirstName,MiddleName,LastName,Suffix,EmailPromotion,AdditionalContactInfo,Demographics,rowguid,ModifiedDate")] Person person)
        {
            if (ModelState.IsValid)
            {
                db.Entry(person).State = EntityState.Modified;
                person.ModifiedDate = DateTime.Now;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(person);
        }

        // GET: People/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.People.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Person person = db.People.Find(id);
            db.People.Remove(person);
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
