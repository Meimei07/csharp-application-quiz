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
        private string dataPath = "D:\\C# term2\\Exam github clone\\csharp-application-quiz\\Practice-exam2\\bin\\Debug\\Data";

        public Student FindName(string username)
        {
            //read from Students file
            Students = io.ReadJson<List<Student>>(dataPath, "Students");
            Student student = Students.Find(s => s.Username == username);

            if (student != null)
            {
                return student;
            }
            else
            {
                return null;
            }
        }

        public Student StudentExist(string username, string password)
        {
            Student student = Students.Find(s => s.Username == username && s.Password == password);
            if (student != null)
            {
                return student;      
            }
            return null;
        }

        public void register(Student student)
        {
            if (FindName(student.Username) == null)
            {
                string fullPath = Path.Combine(dataPath, "Students.json");
                if(File.Exists(fullPath))
                {
                    //read from Students file
                    Students = io.ReadJson<List<Student>>(dataPath, "Students");
                }

                Students.Add(student);
                Console.WriteLine("student added success; please login afterware");

                //write to Students file
                io.WriteJson(dataPath, "Students", Students);
            }
            else
            {
                Console.WriteLine("username already exist");
            }
        }

        public bool login(string username, string password)
        {
            string fullPath = Path.Combine(dataPath, "Students.json");
            if (File.Exists(fullPath))
            {
                //read from Students file
                Students = io.ReadJson<List<Student>>(dataPath, "Students");
            }

            if(StudentExist(username, password) != null)
            {
                Console.WriteLine("Login success");
                return true;
            }
            else
            {
                return false;
            }
        }

        public void modifyPassword(string username, string oldPassword)
        {
            //read from Students file
            Students = io.ReadJson<List<Student>>(dataPath, "Students");

            Student student = StudentExist(username, oldPassword);

            if(student != null)
            {
                Console.Write("Enter new password: ");
                string newPassword = Console.ReadLine();

                student.Password = newPassword;          
                Console.WriteLine("password modified success\n");

                //write to Students file, write the Students list to file
                io.WriteJson(dataPath, "Students", Students);
            }
            else
            {
                Console.WriteLine("incorrect username or password\n");
            }
        }

        public void modifyDob(string username, string password)
        {
            //read from Students file
            Students = io.ReadJson<List<Student>>(dataPath, "Students");

            Student student = StudentExist(username, password);

            if (student != null)
            {
                Console.Write("Enter day:");
                int day = int.Parse(Console.ReadLine());
                Console.Write("Enter month:");
                int month = int.Parse(Console.ReadLine());
                Console.Write("Enter year:");
                int year = int.Parse(Console.ReadLine());

                student.D = new DateTime(year, month, day);
                Console.WriteLine("d.o.b modified success\n");

                //write to Students file, write the Students list to file
                io.WriteJson(dataPath, "Students", Students);
            }
            else
            {
                Console.WriteLine("incorrect username or password\n");
            }
        }
    }
}