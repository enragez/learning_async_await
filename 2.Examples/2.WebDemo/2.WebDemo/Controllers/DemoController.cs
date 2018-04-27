using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace _2.WebDemo.Controllers
{
    public class DemoController : Controller
    {
        public ActionResult GetSync()
        {
            Thread.Sleep(5000);
            return Json("response", JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetAsync()
        {
            await Task.Delay(5000);
            return Json("response", JsonRequestBehavior.AllowGet);
        }
    }
}