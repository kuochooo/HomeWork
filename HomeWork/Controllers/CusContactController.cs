using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HomeWork.Models;
using System.Data.Entity.Validation;
using System.Data.Entity.Infrastructure;

namespace HomeWork.Controllers
{
    public class CusContactController : Controller
    {
        // GET: CusContact
        客戶資料Entities db = new 客戶資料Entities();
        public ActionResult Index()
        {          
            var data = db.客戶聯絡人.OrderBy(p => p.客戶Id).Where(p => p.Is刪除 == false);
            List<SelectListItem> selectlist = new List<SelectListItem>();
            var searchstate = true;
            foreach (var q in db.客戶聯絡人)
            {
                string 職稱 = q.職稱;
                selectlist.Add(new SelectListItem() { Text =職稱, Value = 職稱,Selected=searchstate});
                searchstate = false;
            }

            ViewBag.客戶職稱 = new SelectList(selectlist, "Text", "Value");
            return View(data);
        }
        [HttpPost]
        public ActionResult Index(string 客戶職稱)
        {
            if (客戶職稱 != null)
            {
                var data = db.客戶聯絡人.Where(q => q.職稱 == 客戶職稱);
                ViewBag.客戶職稱 = new SelectList(db.客戶聯絡人, "職稱", "職稱", 客戶職稱);
                return View("Index", data);
            }
            return View();

        }

        public ActionResult ISearchndex(客戶聯絡人 data)
        {          
            return View(data);
        }

        public ActionResult Create(string 客戶名稱)
        {
            List<SelectListItem> selectlist = new List<SelectListItem>();
            foreach (var q in db.客戶資料)
            {
                selectlist.Add(new SelectListItem() { Text = q.客戶名稱, Value = q.客戶名稱 });
            }
            ViewBag.客戶名稱 = new SelectList(selectlist, "Text", "Value", 客戶名稱);
            return View();
        }

        [HttpPost]
        public ActionResult Create(string 客戶名稱,客戶聯絡人 CusData)
        {
            var item = db.客戶資料.First(a => a.客戶名稱 == 客戶名稱);
            CusData.客戶Id = item.Id;
            var data = db.客戶聯絡人.Where(p => p.客戶Id== item.Id);
            if (ModelState.IsValid && data.Where(p => p.Email == CusData.Email).Count() <= 0)
            {
                db.客戶聯絡人.Add(CusData);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            List<SelectListItem> selectlist = new List<SelectListItem>();
            foreach (var q in db.客戶資料)
            {
                selectlist.Add(new SelectListItem() { Text = q.客戶名稱, Value = q.客戶名稱 });
            }
            ViewBag.客戶名稱 = new SelectList(selectlist, "Text", "Value", 客戶名稱);
            ModelState.AddModelError("Email", "有相同的E-mail");
            return View(CusData);
        }

        public ActionResult Edit(int id)
        {
            var item = db.客戶聯絡人.Find(id);
            return View(item);
        }

        [HttpPost]
        public ActionResult Edit(int id, 客戶聯絡人 CusData)
        {
            if (ModelState.IsValid)
            {
                var item = db.客戶聯絡人.Find(id);
                item.姓名 = CusData.姓名;                          
                item.手機 = CusData.手機;
                item.職稱 = CusData.職稱;              
                item.電話 = CusData.電話;
                item.Email = CusData.Email;
                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateException ex)
                {
                    throw ex;
                }
                return RedirectToAction("Index");
            }

            return View(CusData);
        }
        public ActionResult Delete(int id)
        {
            客戶聯絡人 item = db.客戶聯絡人.Find(id);
            return View(item);
        }

        [HttpPost]
        public ActionResult Delete(int id, 客戶聯絡人 cusData)
        {
            var item = db.客戶聯絡人.Find(id);
            item.Is刪除 = true;
            
            try
            {
                db.SaveChanges();
                            }
                        catch (DbEntityValidationException ex)
             {
                               throw ex;
                           }
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var data = db.客戶聯絡人.Find(id);

            return View(data);
        }

        public ActionResult 客戶聯絡人List()
        {
            var data = db.客戶聯絡人.OrderBy(p => p.Id);         
            return View(data);
        }
        [HttpPost]
        [HandleError(ExceptionType = typeof(DbUpdateException), View = "Error_DbUpdateException")]
        public ActionResult 客戶聯絡人List(UpdateVM[] items)
        {
            if (items !=null)
            {
                foreach (var item in items)
                {
                    var Cus = db.客戶聯絡人.Find(item.Id);
                    Cus.職稱 = item.職稱;
                    Cus.手機 = item.手機;
                    Cus.電話 = item.電話;
                }

                db.Configuration.ValidateOnSaveEnabled = false;
                db.SaveChanges();

                return RedirectToAction("客戶聯絡人List");
            }
          
            return View("客戶聯絡人List");
        }
    }
}