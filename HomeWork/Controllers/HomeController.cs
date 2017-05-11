using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HomeWork.Models;

namespace HomeWork.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            客戶資料Entities db = new 客戶資料Entities();
            var Alldata = db.客戶數量資訊.OrderBy(p => p.客戶ID);
            var list = new List<客戶數量資訊>();
            int ID = 0;
            foreach (var q in Alldata)
            {
                var item = new 客戶數量資訊();
                if (!q.客戶刪除)
                {
                    if (ID != q.客戶ID)
                    {
                        ID = q.客戶ID;
                        item.客戶ID = q.客戶ID;
                        item.客戶名稱 = q.客戶名稱;
                        item.帳戶數量 = 0;
                        item.聯絡人數量 = 0;
                        if (db.客戶數量資訊.Where(a => a.客戶ID == q.客戶ID && a.帳戶刪除 == false).Select(a => a.帳戶ID).Count() > 0)
                        { item.帳戶數量 = db.客戶數量資訊.Where(a => a.客戶ID == q.客戶ID && a.帳戶刪除 == false).Select(a => a.帳戶ID).Distinct().Count(); }
                        if (db.客戶數量資訊.Where(a => a.客戶ID == q.客戶ID && a.聯絡人刪除 == false).Select(a => a.聯絡人ID).Count() > 0)
                        { item.聯絡人數量 = db.客戶數量資訊.Where(a => a.客戶ID == q.客戶ID && a.帳戶刪除 == false).Select(a => a.聯絡人ID).Distinct().Count(); }
                        list.Add(item);
                    }
                }
            }
            return View(list);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

    
    }
}