using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMSEntity;

namespace HMSRepo
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {

        private DataContext context;


        public EmployeeRepository()
        {
            this.context = new DataContext();
        }



        public Employee Get(string id)
        {
            return this.context.Employees.SingleOrDefault(e => e.UserId == id);
        }

        public Employee Get(Employee emp)
        {
            return this.context.Employees.SingleOrDefault(e => e.UserId == emp.UserId && e.Password == emp.Password && e.UserType == emp.UserType);
        }


        public int InsertEmp(Employee emp)
        {
            //      var usr= this.context.Employees.SingleOrDefault(e => e.Id == emp.Id);
            //        emp.LoginId = (emp.LoginId + "-");
            this.context.Employees.Add(emp);
            return this.context.SaveChanges();
        }



        public int UpdateEmp(Employee emp)
        {
            Employee empToUpdate = this.context.Employees.SingleOrDefault(e => e.Id == emp.Id);
            empToUpdate.FirstName = emp.FirstName;
            empToUpdate.LastName = emp.LastName;
            empToUpdate.Email = emp.Email;
            empToUpdate.Salary = emp.Salary;
            empToUpdate.Address = emp.Address;
            empToUpdate.MobileNo = emp.MobileNo;
            empToUpdate.DOB = emp.DOB;
            //       empToUpdate.LoginId = emp.LoginId;
            empToUpdate.UserType = emp.UserType;
            empToUpdate.NID = emp.NID;
            empToUpdate.Password = emp.Password;
            empToUpdate.Picture = emp.Picture;
            return this.context.SaveChanges();
        }

        public int Delete(string id)
        {
            Employee empToDelete = this.context.Employees.SingleOrDefault(e => e.UserId == id);
            this.context.Employees.Remove(empToDelete);

            return this.context.SaveChanges();
        }




    }
}
