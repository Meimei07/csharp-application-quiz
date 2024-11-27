using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice_exam2
{
    public class TeacherMenu
    {
        private string path = "D:\\C# term2\\Exam github clone\\csharp-application-quiz\\Practice-exam2\\bin\\Debug\\Subjects";

        private static List<Subject> subjects = new List<Subject>();
        public static List<Subject> Subjects => subjects; //getter

        private List<QAndA> Quizzes = new List<QAndA>();

        private TeacherUtil teacher = new TeacherUtil();

        private IOManager io = new IOManager();

        public void StartTeacherMenu()
        {
            Console.WriteLine(@"========== Teacher Menu ==========
1. Add subject
2. Add question/answer
3. Edit question/answer
4. Remove question/answer
0. Back to main menu");
            Console.Write("Enter: ");
            int option = int.Parse(Console.ReadLine());

            switch(option)
            {
                case 1: AddSubject(); break;
                case 2: AddQAndA(); break;
                case 3: EditQAndA(); break;
                case 4: RemoveQAndA(); break;
                case 0: BackToMainMenu(); break;
                default: StartTeacherMenu(); break;
            }
        }

        public string GetFileName(FileInfo file)
        {
            //file.Name.Substring(file.Name.ToCharArray().Length - 4, file.Name.ToCharArray().Length);
            return file.Name.Replace(file.Extension, "");
            //return file.Name.Replace(".txt", "");
        }

        public int showSubject()
        {
            //go to path Subjects folder, retrieve all files and display as menu to select
            //List<FileInfo> Subjects = io.LoadFiles(path);
            
            subjects = io.ReadJson<List<Subject>>(path, "Subject");

            int index = 1;
            foreach (Subject subject in subjects)
            {
                Console.WriteLine($"{index}. {subject.SubjectName}");
                index++;
            }
            Console.Write("Select subject: ");
            int selected = int.Parse(Console.ReadLine());

            return selected;
        }

        public void AddSubject()
        {
            string fullPath = Path.Combine(path, "Subject.json");
            if(File.Exists(fullPath))
            {
                subjects = io.ReadJson<List<Subject>>(path, "Subject");
            }

            Console.Write("Enter subject: ");
            string subjectName = Console.ReadLine();

            teacher.addSubject(new Subject(subjectName), subjects);
            io.WriteJson(path, "Subject", subjects);

            //create file name 'subject name' in Subjects folder
            //Subject emptySubject = new Subject(subjectName);
            StartTeacherMenu();
        }

        public void AddQAndA()
        {
            Console.WriteLine(@"1. Add question
2. Add answer to existing question
0. <- Back");
            Console.Write("Enter: ");
            int option = int.Parse(Console.ReadLine());

            switch(option)
            {
                case 1: AddQuestion(); break;
                case 2: AddAnswer(); break;
                case 0: StartTeacherMenu(); break;
                default: AddQAndA(); break;
            }
        }


        public void AddQuestion()
        {
            int selected = showSubject();   

            //if(!string.IsNullOrEmpty(selected))
            //{
            //    //Subject subject = new Subject(selected);
            //    //Subject subject = Subjects.Where(s => s.SubjectName == selected).FirstOrDefault();
                
            //    if(subject != null)
            //    {
            //    }
            //    io.WriteJson(path, subject.SubjectName, subject);
            //}

            if (selected > 0 && selected <= Subjects.Count)
            {
                Subject subject = Subjects[selected - 1];
                //Quizzes = io.ReadJson<List<QAndA>>(path, subject.SubjectName);

                Quizzes = teacher.addQuiz(subject);
                //foreach(QAndA q in Quizzes)
                //{
                //    q.Display();
                //}
                io.WriteJson(path, subject.SubjectName, Quizzes);
            }
            else
            {
                Console.WriteLine("invalid selection");
            }
            AddQAndA();
        }

        public void AddAnswer()
        {
            int selected = showSubject();

            //if(!string.IsNullOrEmpty(selected))
            //{
            //    Subject subject = new Subject(selected);
            //    teacher.addAnswer(subject);
            //}

            if (selected > 0 && selected <= Subjects.Count)
            {
                Subject subject = Subjects[selected - 1];
            }
            else
            {
                Console.WriteLine("invalid selection");
            }
            AddQAndA();
        }

        public void EditQAndA()
        {
            Console.WriteLine(@"1. Edit question
2. Edit answer
0. Back");
            Console.Write("Enter: ");
            int option = int.Parse(Console.ReadLine());

            switch(option)
            {
                case 1: EditQuestion(); break;
                case 2: EditAnswer(); break;
                case 0: StartTeacherMenu(); break;
                default: EditQAndA(); break;
            }
        }

        public void EditQuestion()
        {
            int selected = showSubject();

            //if(!string.IsNullOrEmpty(selected))
            //{
            //    Subject subject = new Subject(selected);
            //    teacher.editQuestion(subject);
            //}

            if (selected > 0 && selected <= Subjects.Count)
            {
                Subject subject = Subjects[selected - 1];
            }
            else
            {
                Console.WriteLine("invalid selection");
            }
            EditQAndA();
        }

        public void EditAnswer()
        {
            int selected = showSubject();

            //if (!string.IsNullOrEmpty(selected))
            //{
            //    Subject subject = new Subject(selected);
            //    teacher.editAnswers(subject);
            //}

            if (selected > 0 && selected <= Subjects.Count)
            {
                Subject subject = Subjects[selected - 1];
                teacher.editAnswers(subject);
            }
            else
            {
                Console.WriteLine("invalid selection");
            }
            EditQAndA();
        }

        public void RemoveQAndA()
        {
            Console.WriteLine(@"1. Remove question
2. Remove answer in existing question
0. <- Back");
            Console.Write("Enter: ");
            int option = int.Parse(Console.ReadLine());

            switch(option)
            {
                case 1: RemoveQuestion(); break;
                case 2: RemoveAnswer(); break;
                case 0: StartTeacherMenu(); break;
                default: RemoveQAndA(); break;
            }
        }

        public void RemoveQuestion()
        {
            int selected = showSubject();

            //if (!string.IsNullOrEmpty(selected))
            //{
            //    Subject subject = new Subject(selected);
            //    teacher.removeQuestion(subject);
            //}

            if (selected > 0 && selected <= Subjects.Count)
            {
                Subject subject = Subjects[selected - 1];
                teacher.removeQuestion(subject);
            }
            else
            {
                Console.WriteLine("invalid selection");
            }
            RemoveQAndA();
        }

        public void RemoveAnswer()
        {
            int selected = showSubject();

            //if (!string.IsNullOrEmpty(selected))
            //{
            //    Subject subject = new Subject(selected);
            //    teacher.removeAnswer(subject);
            //}

            if (selected > 0 && selected <= Subjects.Count)
            {
                Subject subject = Subjects[selected - 1];
                teacher.removeAnswer(subject);
            }
            else
            {
                Console.WriteLine("invalid selection");
            }
            RemoveQAndA();
        }

        public void BackToMainMenu() 
        {
            MainMenu menu = new MainMenu();
            menu.StartMenu();
        }
    }
}