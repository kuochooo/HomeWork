using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HomeWork.Models;
using ClosedXML.Excel;
using System.IO;
using PagedList;


namespace HomeWork.Controllers
{
    [Authorize]
    public class CusManageController : BaseController
    {
        // GET: 客戶管理

        客戶資料Repository res= RepositoryHelper.Get客戶資料Repository();
        [testActionFilter(name ="Index")]
        public ActionResult Index(int? page)
        {
            List<SelectListItem> 客戶管理清單 = res.客戶管理清單();
            ViewBag.客戶分類 = new SelectList(客戶管理清單, "Text", "Value");
            var pageNum = page ?? 1;
            var Alldata = res.GetProduct列表頁所有資料().OrderBy(a=>a.Id);
            var onePageOfProducts = Alldata.ToPagedList(pageNum, 1);
            ViewBag.onePageOfProducts = onePageOfProducts;
            return View(onePageOfProducts);
        }

        [HttpPost]
        public ActionResult Index(string 客戶分類)
        {
            List<SelectListItem> 客戶管理清單 = res.客戶管理清單();
            ViewBag.客戶分類 = new SelectList(客戶管理清單, "Text", "Value");
            if (客戶分類 != null)
            {
                var data = res.search客戶分類(客戶分類);
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
                res.Add(CusData);
                res.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Edit(int id)
        {
            var item = res.Get單筆資料ByProductId(id);
            return View(item);
        }

        [HttpPost]
        public ActionResult Edit(int id, 客戶資料 CusData)
        {
            var item = res.Get單筆資料ByProductId(id);
            if (TryUpdateModel(CusData,
            new string[] { "客戶名稱", "客戶分類", "傳真", "地址", "統一編號", "電話", "Email" }))
            {
              res.UnitOfWork.Commit();
              return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult Delete(int id)
        {
            客戶資料 item = res.Get單筆資料ByProductId(id);
            return View(item);
        }

        [HttpPost]
        public ActionResult Delete(int id,客戶資料 cusData)
        {       
                var item = res.Get單筆資料ByProductId(id);
                item.Is刪除 = true;
                res.UnitOfWork.Commit();
                return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var data = res.Get單筆資料ByProductId(id);

            return View(data);
        }

        public ActionResult cusDataExport()
        {
            using (XLWorkbook wb = new XLWorkbook())
            {
                var data = res.All().Select(a=>new {a.客戶分類,a.客戶名稱,a.統一編號,a.電話,a.傳真,a.地址});
                var ws = wb.Worksheets.Add("cusdata", 1);
                ws.Cell(1, 1).InsertData(data);
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    wb.SaveAs(memoryStream);
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    return this.File(memoryStream.ToArray(), "application/vnd.ms-excel", "Download.xlsx");
                }
            }
        }

    }
}