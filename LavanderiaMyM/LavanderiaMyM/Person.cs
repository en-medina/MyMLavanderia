using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LavanderiaMyM
{
    class Person
    {
        int id;
        string name;
        string nationalID;
        DateTime birthday;
        string telephone;
        string email;
        string address;
        string Celphone;
        
        protected Person(int pid, string pname)
        {
            Id = pid;
            Name = pname;
        }

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string NationalID { get => nationalID; set => nationalID = value; }
        public DateTime Birthday { get => birthday; set => birthday = value; }
        public string Telephone { get => telephone; set => telephone = value; }
        public string Email { get => email; set => email = value; }
        public string Address { get => address; set => address = value; }
        public string Celphone1 { get => Celphone; set => Celphone = value; }
    }
    class Employee : Person
    {
        string username;
        double maxDiscount;
        bool isAdmin;

        bool couldModifyEmp;
        bool couldSearchProcess;
        bool couldModifyServices;
        bool couldModifyPayments;
        bool couldModifyExpenses;

        bool isActive;
        public Employee(int pid, string pname, string pusername, double pmaxDiscount) : base (pid, pname)
        {
            username = pusername;
            maxDiscount = pmaxDiscount;
        }

        public bool CouldModifyEmp { get => couldModifyEmp; set => couldModifyEmp = value; }
        public bool CouldSearchProcess { get => couldSearchProcess; set => couldSearchProcess = value; }
        public bool CouldModifyServices { get => couldModifyServices; set => couldModifyServices = value; }
        public bool CouldModifyPayments { get => couldModifyPayments; set => couldModifyPayments = value; }
        public bool CouldModifyExpenses { get => couldModifyExpenses; set => couldModifyExpenses = value; }

        public void setPrivilege(bool couldModifyEmp, bool couldSearchProcess,
            bool couldModifyServices, bool couldModifyPayments, bool couldModifyExpenses)
        {
            CouldModifyEmp = couldModifyEmp;
            CouldSearchProcess= couldSearchProcess;
            CouldModifyServices = couldModifyServices;
            CouldModifyPayments = couldModifyPayments;
            CouldModifyExpenses = couldModifyExpenses;
        }
    }
}
