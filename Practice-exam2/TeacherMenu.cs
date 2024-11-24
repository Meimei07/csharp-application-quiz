using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice_exam2
{

    public class TeacherMenu
    {
        private static List<Subject> subjects = new List<Subject>();
        public static List<Subject> Subjects => subjects;

        //public TeacherMenu(List<Subject> subjects)
        //{
        //    this.Subjects = subjects;
        //}

        public void StartTeacherMenu()
        {
            Console.WriteLine(@"========== Teacher Menu ==========
1. Add subject
2. Add question/answer
3. Edit question/answer
0. Back to main menu");
            Console.Write("Enter: ");
            int option = int.Parse(Console.ReadLine());

            switch(option)
            {
                case 1: AddSubject(); break;
                case 2: AddQAndA(); break;
                case 3: EditQAndA(); break;
                case 0: BackToMainMenu(); break;
                default: StartTeacherMenu(); break;
            }
        }

        public void AddSubject()
        {
            Console.Write("Enter subject: ");
            string subjectName = Console.ReadLine();

            TeacherUtil.addSubject(new Subject(subjectName), Subjects);
            Console.WriteLine($"count:{Subjects.Count}");
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
            for (int i = 0; i < Subjects.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Subjects[i].SubjectName}");
            }
            Console.Write("Select subject to add question: ");
            int selected = int.Parse(Console.ReadLine());

            if (selected > 0 && selected <= Subjects.Count)
            {
                Subject subject = Subjects[selected - 1];
                TeacherUtil.addQuiz(subject);
            }
            else
            {
                Console.WriteLine("invalid selection");
            }
            AddQAndA();
        }

        public void AddAnswer()
        {
            for (int i = 0; i < Subjects.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Subjects[i].SubjectName}");
            }
            Console.Write("Select subject to add question: ");
            int selected = int.Parse(Console.ReadLine());

            if (selected > 0 && selected <= Subjects.Count)
            {
                Subject subject = Subjects[selected - 1];
                TeacherUtil.addAnswer(subject);
            }
            else
            {
                Console.WriteLine("invalid selection");
            }
            AddQAndA();
        }

        public void EditQAndA()
        {
            Console.WriteLine(@"1. Edit question" +
                "2. Edit answer" +
                "0. Back");
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
            for (int i = 0; i < Subjects.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Subjects[i].SubjectName}");
            }
            Console.Write("Select subject to add question: ");
            int selected = int.Parse(Console.ReadLine());

            if (selected > 0 && selected <= Subjects.Count)
            {
                Subject subject = Subjects[selected - 1];
                TeacherUtil.editQuestion(subject);
            }
            else
            {
                Console.WriteLine("invalid selection");
            }
            EditQAndA();
        }

        public void EditAnswer()
        {
            for (int i = 0; i < Subjects.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Subjects[i].SubjectName}");
            }
            Console.Write("Select subject to add question: ");
            int selected = int.Parse(Console.ReadLine());

            if (selected > 0 && selected <= Subjects.Count)
            {
                Subject subject = Subjects[selected - 1];
                TeacherUtil.editAnswers(subject);
            }
            else
            {
                Console.WriteLine("invalid selection");
            }
            EditQAndA();
        }

        public void BackToMainMenu() 
        {
            MainMenu menu = new MainMenu();
            menu.StartMenu();
        }
    }
}