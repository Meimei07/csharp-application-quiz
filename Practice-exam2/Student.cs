using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice_exam2
{
    public class Student
    {
        public string Username;
        public string Password;
        public DateTime D;
        private int Day;
        private int Month;
        private int Year;

        public Student() { }
        public Student(string username, string password, DateTime d)
        {
            
            this.Username= username;
            this.Password= password;
            this.D = d;
        }

        public void Display()
        {
            Console.Write($"Username: {Username}, Password: {Password}, ");
            Console.WriteLine("DOB: " + D.ToString("dd/MM/yyyy"));
        }
    }
}