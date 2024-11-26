using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice_exam2
{
    public class MainMenu
    {
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
            TeacherUtil teacher = new TeacherUtil();
            Console.Write("Enter username: ");
            string username = Console.ReadLine();
            Console.Write("Enter password: ");
            string password = Console.ReadLine();

            //read from teacher file
            //IOManager io = new IOManager();
            //io.ReadJson<TeacherUtil>("teacher.json");

            if(teacher.Login(username, password) == true)
            {
                //save to teacher file
                //io.WriteJson("teacher.json", teacher);

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