using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice_exam2
{
    public class TestMenu
    {
        private string path = "D:\\C# term2\\Exam github clone\\csharp-application-quiz\\Practice-exam2\\bin\\Debug\\Data";
        private StudentManager studentManager = new StudentManager();
        private IOManager io = new IOManager();
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

        public string ShowSubject()
        {
            List<FileInfo> SubjectFiles = io.LoadFiles(path);

            string selectedFile = "";
            int index = 1;
            foreach(FileInfo subject in SubjectFiles)
            {
                Console.WriteLine($"{index}. {io.GetFileName(subject)}");
                index++;
            }
            Console.Write("Select subject: ");
            int selected = int.Parse(Console.ReadLine());

            if(selected > 0 && selected <= SubjectFiles.Count)
            {
                selectedFile = io.GetFileName(SubjectFiles[selected - 1]);
            } 

            return selectedFile;
        }

        public void StartNewTest(string username)
        {
            string selected = ShowSubject();

            if(!string.IsNullOrEmpty(selected))
            {
                Subject subject = new Subject(selected);
                subject.Display(username);
            }
            StartTestMenu(username);
        }

        public void SeePreviousResult(string username)
        {
            string selected = ShowSubject();

            if (!string.IsNullOrEmpty(selected))
            {
                Subject subject = new Subject(selected);
                subject.DisplayResult(username, selected);
            }
            StartTestMenu(username);
        }

        public void ViewTop20(string username)
        {
            string selected = ShowSubject();

            if (!string.IsNullOrEmpty(selected))
            {
                Subject subject = new Subject(selected);
                List<Result> Top20Results = subject.Top20(selected);

                //subject.Top20(selected);

                int index = 1;
                foreach (Result result in Top20Results)
                {
                    Console.WriteLine($"Top{index}: {result.Username} -> {result.Score}pts");
                    index++;
                }
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