using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice_exam2
{
    public class MainMenu
    {
        //public List<Subject> Subjects = new List<Subject>();

        public void StartMenu()
        {
            Console.WriteLine(@"========== Menu ==========
1. Teacher
2. Student");
            Console.Write("Enter: ");
            int option = int.Parse(Console.ReadLine());

            switch(option)
            {
                case 1: Teacher(); break;
                case 2: Student(); break;
                default: StartMenu(); break;
            }
        }

        public void Teacher()
        {
            Console.Write("Enter username: ");
            string username = Console.ReadLine();
            Console.Write("Enter password: ");
            string password = Console.ReadLine();

            TeacherUtil teacher = new TeacherUtil();
            if(teacher.Login(username, password) == true)
            {
                TeacherMenu teacherMenu = new TeacherMenu();
                teacherMenu.StartTeacherMenu();
            }
            else
            {
                Console.WriteLine("incorrect username or password");
                Teacher();
            }
        }

        public void Student()
        {
            StudentMenu studentMenu = new StudentMenu();
            studentMenu.StartMenu();
        }
    }
}