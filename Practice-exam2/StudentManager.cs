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
        private string dataPath = Directory.GetCurrentDirectory() + @"\Data";
        private Subject subject = new Subject();

        public void FileExist()
        {
            //read from Students file
            string fullPath = Path.Combine(dataPath, "Students.json");
            if (File.Exists(fullPath))
            {
                Students = io.ReadJson<List<Student>>(dataPath, "Students");
            }
        }

        public Student FindName(string username)
        {
            FileExist();

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
            FileExist();

            Student student = Students.Find(s => s.Username == username && s.Password == password);
            if (student != null)
            {
                return student;      
            }
            else
            {
                return null;
            }
        }

        public void register(Student student)
        {
            FileExist();

            Students.Add(student);

            //write to Students file
            io.WriteJson(dataPath, "Students", Students);

            Console.WriteLine("student added success");
        }

        public bool login(string username, string password)
        {
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

        public void modifyUsername(string username, string password)
        {
            Student student = StudentExist(username, password);

            if (student != null)
            {
                Console.Write("Enter new username: ");
                string newUsername = Console.ReadLine();

                Student stud = Students.Find(s => s.Username == newUsername);
                if (stud != null)
                {
                    Console.WriteLine("username already exist");
                }
                else
                {
                    student.Username = newUsername;
                    io.WriteJson(dataPath, "Students", Students);

                    subject.FindResult(username, newUsername);

                    Console.WriteLine("username modified success");
                }
            }
            else
            {
                Console.WriteLine("incorrect username or password");
            }
        }

        public void modifyPassword(string username, string oldPassword)
        {
            Student student = StudentExist(username, oldPassword);

            if(student != null)
            {
                Console.Write("Enter new password: ");
                string newPassword = Console.ReadLine();

                student.Password = newPassword;          

                //write to Students file, write the Students list to file
                io.WriteJson(dataPath, "Students", Students);
                
                Console.WriteLine("password modified success");
            }
            else
            {
                Console.WriteLine("incorrect username or password");
            }
        }

        public void modifyDob(string username, string password)
        {
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

                //write to Students file, write the Students list to file
                io.WriteJson(dataPath, "Students", Students);
                
                Console.WriteLine("d.o.b modified success");
            }
            else
            {
                Console.WriteLine("incorrect username or password");
            }
        }

        public void displayStudents()
        {
            FileExist();

            if(Students.Count == 0)
            {
                Console.WriteLine("no student yet");
                return;
            }

            int index = 1;
            foreach (Student student in Students)
            {
                Console.Write($"{index}. ");
                student.Display();
                index++;
            }
        }
    }
}