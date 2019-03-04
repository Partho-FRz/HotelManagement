using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using HMSEntity;
using HMSService;

namespace AppLayer.Controllers
{
    public class HomeController : Controller
    {
        private EmployeeService emprepo = new EmployeeService();
        public string encryption(String password)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] encrypt;
            UTF8Encoding encode = new UTF8Encoding();
            //encrypt the given password string into Encrypted data  
            encrypt = md5.ComputeHash(encode.GetBytes(password));
            StringBuilder encryptdata = new StringBuilder();
            //Create a new string by using the encrypted data  
            for (int i = 0; i < encrypt.Length; i++)
            {
                encryptdata.Append(encrypt[i].ToString());
            }
            return encryptdata.ToString();
        }
        public ActionResult Index()
        {
            using (var db = new DataContext())
            {
                if(!db.Employees.Any())
                {
                    var usr = emprepo.Get(1);
                    if(usr==null)
                    {
                        Employee emp = new Employee();
                        emp.FirstName = "Admin";
                        emp.LastName = "Admin";
                        emp.WorkingSince = DateTime.Now;
                        emp.Email = "Admin@Admin.com";
                        emp.Address = "Company";
                        emp.MobileNo = "Admin";
                        emp.DOB = DateTime.Now;
                        emp.UserId = "Admin";
                        emp.UserType = "Admin";
                        emp.NID = "Admin";
                        emp.Password = encryption("Admin");
                        emp.Picture = null;
                        emprepo.InsertEmp(emp);
                    }
                }
            }
                return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            string userName = collection["userName"].ToString();
            string password = collection["password"].ToString();
            var emp = emprepo.Get(userName);
            if (emprepo.Get(userName) != null)
            {

                if (emp.Password == encryption(password))
                {
                    Response.Write("<script>alert('Welcome')</script>");
                    Session["username"] = userName;
                    return RedirectToAction("Index", "Panel");
                }
                else
                {
                    Response.Write("<script>alert('Wrong Password')</script>");
                    return View();
                }

            }
            else
            {
                Response.Write("<script>alert('Invalid Username')</script>");
                return View();
            }
        }
        public ActionResult ForgetPass()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForgetPass(FormCollection collection)
        {
            string UserName = collection["userName"].ToString();
            string Email = collection["email"].ToString();
            var usr = emprepo.Get(UserName);
            if (UserName == usr.UserId)
            {
                if (Email == usr.Email)
                {
                    Session["UserName"] = UserName;
                    return RedirectToAction("NewPassword", "Home");
                }
                else
                {
                    Response.Write("<script>alert('Invalid Email')</script>");
                    return View();
                }
            }
            else
            {
                Response.Write("<script>alert('Invalid Username')</script>");
                
                return View();
            }
        }
        public ActionResult NewPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult NewPassword(FormCollection collection)
        {
            string Pass = collection["Password"].ToString();
            var usr = emprepo.Get(Session["UserName"].ToString());
            usr.Password = encryption(Pass);
            emprepo.Update(usr);
            return RedirectToAction("Login", "Home");
        }
    }
}