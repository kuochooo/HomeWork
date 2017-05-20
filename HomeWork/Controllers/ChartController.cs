﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using HomeWork.Models;


namespace HomeWork.Controllers
{
    public class ChartController : Controller
    {
        private 客戶資料Entities db = new 客戶資料Entities();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult 客戶銀行資訊_Read()
        {
            return Json(db.客戶銀行資訊.Select(客戶銀行資訊 => new {
                銀行名稱 = 客戶銀行資訊.銀行名稱,
                銀行代碼 = 客戶銀行資訊.銀行代碼,
                分行代碼 = 客戶銀行資訊.分行代碼,
                帳戶名稱 = 客戶銀行資訊.帳戶名稱,
                帳戶號碼 = 客戶銀行資訊.帳戶號碼,
                Is刪除 = 客戶銀行資訊.Is刪除,
            }));
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
