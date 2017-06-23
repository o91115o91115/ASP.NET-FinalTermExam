using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index()
        {
            List<SelectListItem> custdata = new List<SelectListItem>();
            Models.orderService cust = new orderService();
            custdata.Add(new SelectListItem()
            {
                Text = "請選擇",
                Value = ""
            });
            foreach (var item in (List<Models.Customers>)cust.GetCustData())
            {
                custdata.Add(new SelectListItem()
                {
                    Text = item.ContactTitle,
                    Value = Convert.ToString(item.ContactTitle)
                });
            }
            ViewBag.cdata = custdata;
            ViewBag.data = null;
            return View();
        }

        /// <summary>
        /// 搜尋結果
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(orderSeach data)
        {
            List<SelectListItem> custdata = new List<SelectListItem>();
            Models.orderService cust = new orderService();
            custdata.Add(new SelectListItem()
            {
                Text = "請選擇",
                Value = ""
            });
            foreach (var item in (List<Models.Customers>)cust.GetCustData())
            {
                custdata.Add(new SelectListItem()
                {
                    Text = item.ContactTitle,
                    Value = Convert.ToString(item.ContactTitle)
                });
            }
            ViewBag.cdata = custdata;
            Models.orderService order = new orderService();
            ViewBag.data = order.SeachCust(data);
            return View();
        }
    }
}