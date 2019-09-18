using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using prjCustomer.Models;  //step1. 要先using
using PagedList;

namespace prjCustomer.Controllers
{
    public class HomeController : Controller
    {
        dbCustomerEntities1 db = new dbCustomerEntities1();
        private int pageSize = 2;   //一頁顯示幾筆資料
        // GET: Home
        /*public ActionResult Index()
        {
            //var customer = db.tCustomer.OrderBy(m => m.fId).ToList();     //Lamda
           // var customer = (from s in db.tCustomer orderby s.Sex select s).ToList();   //LinQ
            //return View(customer);

        }*/

        public ActionResult Index(int page=1)
        {
            int currentPage = page < 1 ? 1 : page;
            var customers = db.tCustomer.OrderBy(m => m.fId);
            var result = customers.ToPagedList(currentPage, pageSize);
            return View(result);
        } 

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]  //代表用表單的submit做傳送，如果沒有寫表示是用get的呼叫方式走網址
        public ActionResult Create(tCustomer vCustomer)
        {
            
            db.tCustomer.Add(vCustomer);
            db.SaveChanges();
            TempData["ResultMsg"] = string.Format("顧客[{0}]成功新增", vCustomer.fName);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int fid)
        {
            var customer = (from s in db.tCustomer where s.fId == fid select s).FirstOrDefault();
            db.tCustomer.Remove(customer);
            db.SaveChanges();
            TempData["ResultMsg"] = string.Format("顧客[{0}]成功刪除", customer.fName);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int fid)
        {
            //var customer = (from s in db.tCustomer where s.fId == fid select s).FirstOrDefault();  //LinQ
            var customer = db.tCustomer.Where(m => m.fId == fid).FirstOrDefault();   //Lamda
            return  View(customer);
        }

        [HttpPost]
        public ActionResult Edit(tCustomer vCustomer)
        {
            int fid = vCustomer.fId;
            //var customer = (from s in db.tCustomer where s.fId == fid select s).FirstOrDefault();
            var customer = db.tCustomer.Where(m => m.fId == fid).FirstOrDefault();
            customer.fName = vCustomer.fName;
            customer.Sex = vCustomer.Sex;
            customer.Birthday = vCustomer.Birthday;
            customer.fPhone = vCustomer.fPhone;
            customer.fAddress = vCustomer.fAddress;
            db.SaveChanges();
            TempData["ResultMsg"] = string.Format("顧客[{0}]成功編輯", customer.fName);
            return RedirectToAction("Index");

        }

        public string ShowImage2()
        {
            string show = "";
            for (int i=1; i<=5; i++)
            {
                show += string.Format("<img src='../Images/img{0}.jpg' width='150'>", i);
            }
            return show;
        }

        public string ShowImage(int index)
        {
            string[] name = new string[] { "Enoch I love you", "Kiss u", "Smile", "Cute", "Good boy" };
            string show = string.Format("<p align='center'><img src='../Images/img{0}.jpg' width='250'><br>{1}</p>", index, name[index-1]);
            return show;
        }


    }
}


