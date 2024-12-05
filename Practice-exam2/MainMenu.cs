using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice_exam2
{
    public class MainMenu
    {
        public void StartMenu()
        {
            Console.WriteLine(@"========== Main Menu ==========
Select role:
1. I am a teacher
2. I am a student");
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
            Console.Write("\nEnter username: ");
            string username = Console.ReadLine();
            Console.Write("Enter password: ");
            string password = Console.ReadLine();

            TeacherUtil teacher = new TeacherUtil();
            IOManager io = new IOManager();

            //read from teacher file
            string path = Directory.GetCurrentDirectory() + @"\Data";
            teacher = io.ReadJson<TeacherUtil>(path, "teacher");

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