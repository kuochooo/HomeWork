using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HomeWork.Models;

namespace HomeWork.Controllers
{
    public class CusManageController : Controller
    {
        // GET: 客戶管理
        客戶資料Entities db = new 客戶資料Entities();
        public ActionResult Index()
        {
            var Alldata = db.客戶資料.OrderBy(p => p.Id).Where(p=>p.Is刪除 == false);
            return View(Alldata);
        }

        [HttpPost]
        public ActionResult Index(int? searchState, string search_str)
        {
            if (search_str != null)
            {
                var data = db.客戶資料.Where(q => q.客戶名稱.Contains(search_str));
                if (searchState == 1)
                { data = db.客戶資料.Where(q => q.統一編號.Contains(search_str)); }
                if (searchState == 2)
                { data = db.客戶資料.Where(q => q.電話.Contains(search_str)); }
                return View("Index", data);
            }
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(客戶資料 CusData)
        {
            if (ModelState.IsValid)
            {
                db.客戶資料.Add(CusData);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Edit(int id)
        {
            var item = db.客戶資料.Find(id);
            return View(item);
        }

        [HttpPost]
        public ActionResult Edit(int id, 客戶資料 CusData)
        {
            if (ModelState.IsValid)
            {
                var item = db.客戶資料.Find(id);
                item.客戶名稱 = CusData.客戶名稱;
                item.客戶聯絡人 = CusData.客戶聯絡人;
                item.傳真 = CusData.傳真;
                item.地址 = CusData.地址;
                item.客戶銀行資訊 = CusData.客戶銀行資訊;
                item.統一編號 = CusData.統一編號;
                item.電話 = CusData.電話;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(CusData);
        }
        public ActionResult Delete(int id)
        {
            客戶資料 item = db.客戶資料.Find(id);
            return View(item);
        }

        [HttpPost]
        public ActionResult Delete(int id,客戶資料 cusData)
        {       
                var item = db.客戶資料.Find(id);
                item.Is刪除 = true;
                db.SaveChanges();
                return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var data = db.客戶資料.Find(id);

            return View(data);
        }
   
        
    }
}