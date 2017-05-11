using HomeWork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HomeWork.Controllers
{
    public class CusBankController : Controller
    {
        // GET: CusBank
        客戶資料Entities db = new 客戶資料Entities();
        public ActionResult Index()
        {
            var data = db.客戶銀行資訊.OrderBy(p => p.客戶Id).Where(p => p.Is刪除 == false);
            return View(data);
        }
        [HttpPost]
        public ActionResult Index(int? searchState, string search_str)
        {
            if (search_str != null)
            {
                var data = db.客戶銀行資訊.Where(q => q.客戶資料.客戶名稱.Contains(search_str));
                if (searchState == 1)
                { data = db.客戶銀行資訊.Where(q => q.銀行名稱.Contains(search_str)); }
                if (searchState == 2)
                { data = db.客戶銀行資訊.Where(q => q.帳戶名稱.Contains(search_str)); }
                return View("Index", data);
            }
            return View();
        }
        public ActionResult Create(string 客戶名稱)
        {          
            List<SelectListItem> selectlist = new List<SelectListItem>();
            foreach(var q in db.客戶資料)
            {
                selectlist.Add(new SelectListItem() { Text = q.客戶名稱, Value = q.客戶名稱});
            }           
            ViewBag.客戶名稱 = new SelectList(selectlist, "Text", "Value", 客戶名稱);
            return View();
        }

        [HttpPost]
        public ActionResult GetCreate(string 客戶名稱, 客戶銀行資訊 CusData)
        {
            var item = db.客戶資料.First(a=>a.客戶名稱 == 客戶名稱);       
            CusData.客戶Id = item.Id;
            db.客戶銀行資訊.Add(CusData);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var item = db.客戶銀行資訊.Find(id);
            return View(item);
        }

        [HttpPost]
        public ActionResult Edit(int id, 客戶銀行資訊 CusData)
        {
            if (ModelState.IsValid)
            {
                var item = db.客戶銀行資訊.Find(id);
                item.分行代碼 = CusData.分行代碼;
                item.客戶Id = CusData.客戶Id;               
                item.帳戶名稱 = CusData.帳戶名稱;
                item.帳戶號碼 = CusData.帳戶號碼;
                item.銀行代碼 = CusData.銀行代碼;
                item.銀行名稱 = CusData.銀行名稱;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(CusData);
        }
        public ActionResult Delete(int id)
        {
            客戶銀行資訊 item = db.客戶銀行資訊.Find(id);
            return View(item);
        }

        [HttpPost]
        public ActionResult Delete(int id, 客戶銀行資訊 cusData)
        {
            var item = db.客戶銀行資訊.Find(id);
            item.Is刪除 = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var data = db.客戶銀行資訊.Find(id);

            return View(data);
        }
    }
}