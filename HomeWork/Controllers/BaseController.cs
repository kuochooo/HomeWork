using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HomeWork.Controllers
{
    public abstract class BaseController : Controller
    {
        // GET: Base
        public ActionResult Debug()
        {
            return View();
        }
    }
}