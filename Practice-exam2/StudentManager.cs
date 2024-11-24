using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice_exam2
{
    public class StudentManager
    {
        List<Student> Students = new List<Student>();

        public Student FindName(string username)
        {
            foreach(Student student in Students)
            {
                if(student.Username == username)
                {
                    return student;
                }
            }
            return null;
        }

        public Student Find(string username, string password)
        {
            foreach(Student student in Students)
            {
                if(student.Username == username && student.Password == password)
                {
                    return student;
                }
            }
            return null;
        }

        public void register(Student student)
        {
            if (FindName(student.Username) == null)
            {
                Students.Add(student);
                Console.WriteLine("student added success; please login afterware");
            }
            else
            {
                Console.WriteLine("username already exist");
            }
        }

        public bool login(string username, string password)
        {
            //int status = 0;
            foreach(Student student in Students)
            {
                if(student.Username == username && student.Password == password)
                {
                    //status = 1;
                    Console.WriteLine("Login success");
                    return true;
                }
            }
            Console.WriteLine("incorrect username or password");
            return false;
        }

        public void modifyPassword(string username, string oldPassword)
        {
            Student student = Find(username, oldPassword);
            if(student != null)
            {
                Console.Write("Enter new password: ");
                string newPassword = Console.ReadLine();

                student.Password = newPassword;
                Console.WriteLine("password modified success");
            }
            else
            {
                Console.WriteLine("student doesn't exist");
            }
        }

        public void modifyDob(string username, string password)
        {
            Student student = Find(username, password);
            if(student != null)
            {
                Console.Write("Enter day:");
                int day = int.Parse(Console.ReadLine());
                Console.Write("Enter month:");
                int month = int.Parse(Console.ReadLine());
                Console.Write("Enter year:");
                int year = int.Parse(Console.ReadLine());

                student.D = new DateTime(year, month, day);
                Console.WriteLine("dob modified success");
            }
            else
            {
                Console.WriteLine("student doesn't exist");
            }
        }
    }
}