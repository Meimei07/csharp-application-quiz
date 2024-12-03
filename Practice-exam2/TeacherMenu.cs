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
        private string subjectPath = Directory.GetCurrentDirectory() + @"\Subjects";
        private static List<Subject> subjects = new List<Subject>();
        public static List<Subject> Subjects => subjects; //getter
        private TeacherUtil teacher = new TeacherUtil();
        private IOManager io = new IOManager();

        public void StartTeacherMenu()
        {
            Console.WriteLine();
            Console.WriteLine(@"========== Teacher Menu ==========
1. Add subject
2. Add question/answer
3. Edit question/answer
4. Remove question/answer
5. View students' result
0. Back to main menu");
            Console.Write("Enter: ");
            int option = int.Parse(Console.ReadLine());

            switch(option)
            {
                case 1: AddSubject(); break;
                case 2: AddQAndA(); break;
                case 3: EditQAndA(); break;
                case 4: RemoveQAndA(); break;
                case 5: ViewStudentResult(); break;
                case 0: BackToMainMenu(); break;
                default: Console.WriteLine("invalid"); StartTeacherMenu(); break;
            }
        }

        public string showSubject()
        {
            //go to path Subjects folder, retrieve all files and display as menu to select
            List<FileInfo> SubjectFiles = io.LoadFiles(subjectPath);

            if(SubjectFiles.Count == 0)
            {
                Console.WriteLine("no available subjects");
                return null;
            }

            string selectedFile = "";
            int index = 1;
            foreach (FileInfo subject in SubjectFiles)
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
            Console.WriteLine();
            return selectedFile;
        }

        public void AddSubject()
        {
            Console.Write("Enter subject: ");
            string subjectName = Console.ReadLine();

            string fullPath = Path.Combine(subjectPath, subjectName + ".json");
            if(File.Exists(fullPath))
            {
                Console.WriteLine($"Subject {subjectName} already exist");
                StartTeacherMenu();
            }

            //teacher.addSubject(new Subject(subjectName), subjects);
            subjects.Add(new Subject(subjectName));

            //create file name 'subject name' in Subjects folder, with empty content
            io.WriteJson(subjectPath, subjectName, new List<QAndA>());

            Console.WriteLine("subject added success");
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
            string selected = showSubject();

            if (!string.IsNullOrEmpty(selected))
            {
                teacher.addQuiz(new Subject(selected));
            }

            List<FileInfo> SubjectFiles = io.LoadFiles(subjectPath);
            Console.WriteLine("count"+SubjectFiles.Count);
            if (SubjectFiles.Count > 1)
            {
                //call method to create mix quiz
                teacher.addMixQuiz(new Subject(selected), SubjectFiles);            
            }
            AddQAndA();
        }

        public void AddAnswer()
        {
            string selected = showSubject();

            if (!string.IsNullOrEmpty(selected))
            {
                teacher.addAnswer(new Subject(selected));
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
            string selected = showSubject();

            if (!string.IsNullOrEmpty(selected))
            {
                teacher.editQuestion(new Subject(selected));
            }
            EditQAndA();
        }

        public void EditAnswer()
        {
            string selected = showSubject();

            if (!string.IsNullOrEmpty(selected))
            {
                teacher.editAnswers(new Subject(selected));
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
            string selected = showSubject();

            if (!string.IsNullOrEmpty(selected))
            {
                teacher.removeQuestion(new Subject(selected));
            }
            RemoveQAndA();
        }

        public void RemoveAnswer()
        {
            string selected = showSubject();

            if (!string.IsNullOrEmpty(selected))
            {
                teacher.removeAnswer(new Subject(selected));
            }
            RemoveQAndA();
        }

        public void ViewStudentResult()
        {
            string selected = showSubject();

            if (!string.IsNullOrEmpty(selected))
            {
                teacher.StudentResult(new Subject(selected));
            }
            StartTeacherMenu();
        }

        public void BackToMainMenu() 
        {
            MainMenu menu = new MainMenu();
            Console.WriteLine();
            menu.StartMenu();
        }
    }
}