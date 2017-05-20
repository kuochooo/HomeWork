using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HomeWork.Controllers
{
    public class testActionFilter : ActionFilterAttribute
    {
        public string name { get; set; }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var date = Convert.ToString(DateTime.Now.Month);
            System.Diagnostics.Debug.WriteLine(name+date);
        }
    }
}