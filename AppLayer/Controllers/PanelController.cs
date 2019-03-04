using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using HMSEntity;
using HMSService;

namespace AppLayer.Controllers
{
    public class PanelController : Controller
    {
        // GET: Panel
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
        private EmployeeService emprepo = new EmployeeService();
        private RoomService roomService = new RoomService();
        private HistoryService HService = new HistoryService();
       
        public ActionResult Index()
        {
            //Session["username"] = userName;
            string userName = Session["username"].ToString();
            var usr= emprepo.Get(userName);
            Session["userType"] = usr.UserType;
            if(usr.Picture!= null)
            {
                var base64 = Convert.ToBase64String(usr.Picture);
                Session["img"] = string.Format("data:image/gif;base64,{0}", base64);
            }
            return View();
        }
        public ActionResult Profile()
        {
            var emp = emprepo.Get(Session["username"].ToString());
            return View(emp);
        }
        public ActionResult EditProfile()
        {
            var emp = emprepo.Get(Session["username"].ToString());
            ViewBag.data = emp;
            return View();
        }
        [HttpPost]
        public ActionResult EditProfile(FormCollection collection, HttpPostedFileBase file1)
        {
            Employee emp = new Employee();
            if (file1 != null)
            {
                emp.Picture = new byte[file1.ContentLength];
                file1.InputStream.Read(emp.Picture, 0, file1.ContentLength);
            }
            emp.FirstName = collection["FirstName"].ToString();
            emp.LastName = collection["LastName"].ToString();
            emp.Email = collection["Email"].ToString();
            emp.MobileNo = collection["MobileNo"].ToString();
            emp.Password = encryption(collection["Password"].ToString());
            emp.Address = collection["Address"].ToString();
            if (emprepo.Update(emp) == 1)
            {
                Response.Write("<script>alert('Account Successfully Updated')</script>");
                return RedirectToAction("Profile", "Panel");
            }
            else
            {
                Response.Write("<script>alert('There was a problem Updating Account')</script>");
                return RedirectToAction("EditProfile", "Panel");
            }
        }
        public ActionResult Logout()
        {
           
            Session.Clear();
            
            return RedirectToAction("Login", "Home");
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        private static readonly Random random = new Random();
        private static readonly object syncLock = new object();
        public static int RandomNumber(int min, int max)
        {
            lock (syncLock)
            { // synchronize
                return random.Next(min, max);
            }
        }
        [HttpPost]
        public ActionResult Register(FormCollection collection, HttpPostedFileBase file1)
        {
            Employee emp = new Employee();
            if (file1!=null)
            {
                emp.Picture = new byte[file1.ContentLength];
                file1.InputStream.Read(emp.Picture, 0, file1.ContentLength);
            }
            
            emp.FirstName = collection["firstName"].ToString();
            emp.LastName = collection["lastName"].ToString();
            emp.Email = collection["email"].ToString();
            emp.Address = collection["address"].ToString();
            emp.MobileNo = collection["mobileNo"].ToString();
            emp.DOB = DateTime.Parse(collection["DOB"]);
            emp.NID = collection["NID"].ToString();
            emp.Password = encryption(collection["password"].ToString());
            emp.UserType = collection["userType"].ToString();
            emp.WorkingSince = DateTime.Now;
            string str = DateTime.Now.Minute + "-" + RandomNumber(100, 900).ToString() + "-" + DateTime.Now.Second;
            emp.UserId = str;
            emprepo.Insert(emp);
            Response.Write("<script>alert('Account Created Successfully')</script>");
            return RedirectToAction("Index","Panel");
        }
        public ActionResult Delete()
        {
            var emp = this.emprepo.GetAll();
            List<Employee> EmpList = new List<Employee>();
            foreach(var item in emp)
            {
                if(item.UserId!= Session["username"].ToString())
                {
                    EmpList.Add(item);
                }
            }
            return View(EmpList);
        }
        public ActionResult ConfirmDelete(string id)
        {
            var emp = this.emprepo.Get(id);
            Session["dlt"] = emp.UserId;
            if (emp.Picture != null)
            {
                var base64 = Convert.ToBase64String(emp.Picture);
                Session["image2"] = string.Format("data:image/gif;base64,{0}", base64);
            }
            else
            {
                Session["image2"] = null;
            }
            return View(emp);
        }
        public ActionResult Deleted(string id)
        {
            //var emp = this.emprepo.Get(id);
            //Response.Write("<script>alert('@emp.UserId')</script>");
            /*if (this.emprepo.Delete(emp)==1)
            {
                Response.Write("<script>alert('Account Deleted Successfully')</script>");
            }
            else
            {
                Response.Write("<script>alert('There is a problem Deleting this Account')</script>");
            }*/
            emprepo.Delete(id);
            return RedirectToAction("Delete", "Panel");
        }
        public ActionResult AddRoom()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddRoom(FormCollection collection, HttpPostedFileBase file1)
        {
            Room room = new Room();
            if (file1 != null)
            {
                room.Picture = new byte[file1.ContentLength];
                file1.InputStream.Read(room.Picture, 0, file1.ContentLength);
            }
            room.RoomType = collection["roomType"].ToString();
            room.RoomNo = collection["RoomNo"].ToString();
            room.NoOfBed = collection["NoOfBed"].ToString();
            room.Price = Convert.ToDouble(collection["Price"].ToString());
            room.BedType = collection["BedType"].ToString();
            room.MaxPerson = collection["MaxPerson"].ToString();
            room.Status = "Available";
           
            roomService.Insert(room);
            Response.Write("<script>alert('Room Created Successfully')</script>");
            return RedirectToAction("ViewRoom","Panel");
        }
        public ActionResult ViewRoom()
        {
            var room = this.roomService.GetAll();
            List<Room> RoomList = new List<Room>();
            foreach (var item in room)
            {
            
                RoomList.Add(item);
          
            }
            return View(RoomList);
            
        }
        public ActionResult RoomDetails(string id)
        {
            if (Session["check"] != null)
            {
                var room = roomService.Get(Session["check"].ToString());
                if (room.Picture != null)
                {
                    var base64 = Convert.ToBase64String(room.Picture);
                    Session["image2"] = string.Format("data:image/gif;base64,{0}", base64);
                }
                else
                {
                    Session["image2"] = null;
                }
                return View(room);
            }
            else
            {
                var room = roomService.Get(id);
                if (room.Picture != null)
                {
                    var base64 = Convert.ToBase64String(room.Picture);
                    Session["image2"] = string.Format("data:image/gif;base64,{0}", base64);
                }
                else
                {
                    Session["image2"] = null;
                }
                return View(room);
            }
            

        }
        public ActionResult Booking(string id)
        {
            var roomservice = roomService.Get(id);
            ViewBag.Data = roomservice;
            if (roomservice.Picture != null)
            {
                var base64 = Convert.ToBase64String(roomservice.Picture);
                Session["image2"] = string.Format("data:image/gif;base64,{0}", base64);
            }
            else
                Session["image2"] = null;
            return View();
        }
        public ActionResult Clear(string id)
        {
            var roomservice = roomService.Get(id);
            roomservice.Payment = roomservice.Price.ToString();
            roomservice.PaymentStatus = "Paid";
            roomService.Update(roomservice);
            //Session["check"] = id;
            return RedirectToAction("RoomDetails", "Panel",new {id=roomservice.RoomNo });
        }
        public ActionResult Generic(string id)
        {
            var roomservice = roomService.Get(id);
            var hService = HService.Get(id);
            if (roomservice.Status == "Booked")
            {
                roomservice.CheckInDate = (DateTime.Now).ToString();
                hService.CheckInDate= (DateTime.Now).ToString();
                roomservice.Status = "Checked In";
            }
            else
            {
                
                //roomservice.CheckOutDate = (DateTime.Now).ToString();
                hService.CheckOutDate= (DateTime.Now).ToString();
                roomservice.Status = "Available";
                roomservice.BookedDate = null;
                roomservice.CheckInDate = null;
                roomservice.ClientName = null;
                roomservice.ClientEmail = null;
                roomservice.Payment = null;
                roomservice.PaymentStatus = null;
                roomservice.ClientPhone = null;
                roomservice.ClientNIDPicture = null;
                roomservice.TotalPerson = null;
            }
            roomService.Update(roomservice);
            HService.Update(hService);
        
            return RedirectToAction("ViewRoom", "Panel");
        }
        [HttpPost]
        public ActionResult Booking(FormCollection collection, HttpPostedFileBase file1)
        {
            var history = new History();
            var roomservice = roomService.Get(collection["RoomNo"].ToString());
            history.RoomNo = roomservice.RoomNo;
            history.RoomType = roomservice.RoomType;
            if (roomservice.Status == "Available")
            {
                if (file1 != null)
                {
                    roomservice.ClientNIDPicture = new byte[file1.ContentLength];
                    file1.InputStream.Read(roomservice.ClientNIDPicture, 0, file1.ContentLength);
                    history.ClientNidPicture = new byte[file1.ContentLength];
                    file1.InputStream.Read(history.ClientNidPicture, 0, file1.ContentLength);
                }
                roomservice.ClientName = collection["ClientName"].ToString();
                history.ClientName = collection["ClientName"].ToString();
                roomservice.ClientEmail = collection["ClientEmail"].ToString();
                history.ClientEmail = collection["ClientEmail"].ToString();
                roomservice.Payment = collection["Payment"].ToString();
                roomservice.TotalPerson = collection["TotalPerson"].ToString();
                history.Person = collection["TotalPerson"].ToString();
                roomservice.ClientPhone = collection["ClientNumber"].ToString();
                history.ClientNumber = collection["ClientNumber"].ToString();
                if (roomservice.Payment != null)
                {
                    double payment = Convert.ToDouble(roomservice.Payment);
                    if (roomservice.Price - payment == 0)
                    {
                        roomservice.PaymentStatus = "Paid";
                    }
                    else
                    {
                        roomservice.PaymentStatus = "due " + (roomservice.Price - payment) + " Tk";
                    }
                }
                roomservice.BookedDate = (DateTime.Now).ToString();
                history.BookedDate = (DateTime.Now).ToString();
                roomservice.Status = "Booked";

            }
            HService.Insert(history);
            roomService.Update(roomservice);
            return RedirectToAction("ViewRoom", "Panel");
        }
        
    }
}