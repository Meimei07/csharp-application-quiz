using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice_exam2
{
    public class StudentMenu
    {
        private StudentManager studentManager = new StudentManager();
        private static TeacherMenu teacherMenu = new TeacherMenu();

        public void StartMenu()
        {
            Console.WriteLine(@"========== Menu ==========
1. Register
2. Login
0. Back to main menu");
            Console.Write("Enter: ");
            int option = int.Parse(Console.ReadLine());

            switch(option)
            {
                case 1: Register(); break;
                case 2: Login(); break;
                case 0: BackToMainMenu(); break;
                default: StartMenu(); break;
            }
        }

        public void Register()
        {
            Console.Write("Enter username: ");
            string username = Console.ReadLine();

            if(studentManager.FindName(username) == null)
            {
                Console.Write("Enter password: ");
                string password = Console.ReadLine();

                Console.WriteLine("Enter date of birth...");
                Console.Write("Day: ");
                int day = int.Parse(Console.ReadLine());
                Console.Write("Month: ");
                int month = int.Parse(Console.ReadLine());
                Console.Write("Year: ");
                int year = int.Parse(Console.ReadLine());

                DateTime dob = new DateTime(year, month, day);
                studentManager.register(new Student(username, password, dob));
            }
            else
            {
                Console.WriteLine("username already exist");
            }
            StartMenu();
        }

        public void Login()
        {
            Console.Write("Enter username: ");
            string username = Console.ReadLine();
            Console.Write("Enter password: ");
            string password = Console.ReadLine();

            if (studentManager.login(username, password) == true)
            {
                TestMenu testMenu = new TestMenu();
                testMenu.StartTestMenu(username);
            }
            else
            {
                Console.WriteLine("incorrect username or password");
                StartMenu();
            }
        }

        public void BackToMainMenu()
        {
            MainMenu menu = new MainMenu();
            menu.StartMenu();
        }
    }
}