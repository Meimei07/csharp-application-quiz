using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace Practice_exam2
{
    public class StudentManager
    {
        private List<Student> Students = new List<Student>();
        private IOManager io = new IOManager();
        private string resultPath = "D:\\C# term2\\Exam github clone\\csharp-application-quiz\\Practice-exam2\\bin\\Debug";

        public Student FindName(string username)
        {
            //read from Students file
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
            //read from Students file
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
                string fullPath = Path.Combine(resultPath, "Students.json");
                if(File.Exists(fullPath))
                {
                    //read from Students file
                    Students = io.ReadJson<List<Student>>(resultPath, "Students");
                }

                Students.Add(student);
                Console.WriteLine("student added success; please login afterware");

                //write to Students file
                io.WriteJson(resultPath, "Students", Students);
            }
            else
            {
                Console.WriteLine("username already exist");
            }
        }

        public bool login(string username, string password)
        {
            string fullPath = Path.Combine(resultPath, "Students.json");
            if (File.Exists(fullPath))
            {
                //read from Students file
                Students = io.ReadJson<List<Student>>(resultPath, "Students");
            }

            foreach (Student student in Students)
            {
                if(student.Username == username && student.Password == password)
                {
                    Console.WriteLine("Login success");
                    return true;
                }
            }
            return false;
        }

        public void modifyPassword(string username, string oldPassword)
        {
            Student student = Find(username, oldPassword);
            //read from Students file
            if(student != null)
            {
                Console.Write("Enter new password: ");
                string newPassword = Console.ReadLine();

                student.Password = newPassword;
                Console.WriteLine("password modified success");
                //write to Students file, write the Students list to file
            }
            else
            {
                Console.WriteLine("student doesn't exist");
            }
        }

        public void modifyDob(string username, string password)
        {
            Student student = Find(username, password);
            //read from Students file
            if(student != null)
            {
                Console.Write("Enter day:");
                int day = int.Parse(Console.ReadLine());
                Console.Write("Enter month:");
                int month = int.Parse(Console.ReadLine());
                Console.Write("Enter year:");
                int year = int.Parse(Console.ReadLine());

                student.D = new DateTime(year, month, day);
                Console.WriteLine("d.o.b modified success");
                //write to Students file, write the Students list to file
            }
            else
            {
                Console.WriteLine("student doesn't exist");
            }
        }
    }
}