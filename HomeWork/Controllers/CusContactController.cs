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
            return View(data);
        }
        [HttpPost]
        public ActionResult Index(int? searchState, string search_str)
        {
            if (search_str != null)
            {
                var data = db.客戶聯絡人.Where(q => q.姓名.Contains(search_str));
                if (searchState == 1)
                { data = db.客戶聯絡人.Where(q => q.客戶資料.客戶名稱.Contains(search_str)); }
                if (searchState == 2)
                { data = db.客戶聯絡人.Where(q => q.手機.Contains(search_str)); }
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

        
    }
}