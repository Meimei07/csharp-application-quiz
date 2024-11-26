using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice_exam2
{
    public class TestMenu
    {
        private StudentManager studentManager = new StudentManager();
        private static List<Subject> Subjects = TeacherMenu.Subjects;

        public void StartTestMenu(string username)
        {
            Console.WriteLine(@"========== Test Menu ==========
1. Start new test
2. See previous result
3. View top 20 students
4. Modify password/d.o.b
5. Logout");
            Console.Write("Enter: ");
            int option = int.Parse(Console.ReadLine());

            switch(option)
            {
                case 1: StartNewTest(username); break;
                case 2: SeePreviousResult(username); break;
                case 3: ViewTop20(username); break;
                case 4: ModifyPasswordDob(username); break;
                case 5: Logout(); break;
                default: StartTestMenu(username); break;
            }
        }

        public int ShowSubject()
        {
            for (int i = 0; i < Subjects.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Subjects[i].SubjectName}");
            }
            Console.Write("Select subject: ");
            int selected = int.Parse(Console.ReadLine());

            return selected;
        }

        public void StartNewTest(string username)
        {
            //Console.WriteLine($"count:{Subjects.Count}");
            int selected = ShowSubject();

            if(selected > 0 && selected <= Subjects.Count)
            {
                Subject subject = Subjects[selected - 1];
                subject.Display(username);
            }
            StartTestMenu(username);
        }

        public void SeePreviousResult(string username)
        {
            int selected = ShowSubject();

            if (selected > 0 && selected <= Subjects.Count)
            {
                Subject subject = Subjects[selected - 1];
                subject.DisplayResult(username, subject.SubjectName);
            }
            StartTestMenu(username);
        }

        public void ViewTop20(string username)
        {
            int selected = ShowSubject();

            if (selected > 0 && selected <= Subjects.Count)
            {
                Subject subject = Subjects[selected - 1];
                subject.Top20();
            }
            else
            {
                Console.WriteLine("invalid option");
            }
            StartTestMenu(username);
        }

        public void ModifyPasswordDob(string username)
        {
            Console.WriteLine(@"1. Modify password
2. Modify D.O.B
0. <- Back");
            Console.Write("Enter: ");
            int option = int.Parse(Console.ReadLine());

            switch(option)
            {
                case 1: ModifyPassword(); break;
                case 2: ModifyDob(); break;
                case 0: StartTestMenu(username); break;
                default: ModifyPasswordDob(username); break;
            }
        }

        public void ModifyPassword()
        {
            Console.Write("Enter username: ");
            string username = Console.ReadLine();
            Console.Write("Enter old password: ");
            string oldPassword = Console.ReadLine();

            studentManager.modifyPassword(username, oldPassword);
            ModifyPasswordDob(username);
        }

        public void ModifyDob()
        {
            Console.Write("Enter username: ");
            string username = Console.ReadLine();
            Console.Write("Enter old password: ");
            string password = Console.ReadLine();

            studentManager.modifyDob(username, password);
            ModifyPasswordDob(username);
        }

        public void Logout()
        {
            Console.WriteLine("Logout");
            StudentMenu menu = new StudentMenu();
            menu.StartMenu();
        }
    }
}