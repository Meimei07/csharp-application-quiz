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
        public static List<Subject> Subjects => subjects; //getter

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

        public int showSubject()
        {
            for (int i = 0; i < Subjects.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Subjects[i].SubjectName}");
            }
            Console.Write("Select subject: ");
            int selected = int.Parse(Console.ReadLine());

            return selected;
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
            int selected = showSubject();   

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
            int selected = showSubject();

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
            int selected = showSubject();

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

            if(selected > 0 && selected <= Subjects.Count)
            {
                Subject subject = Subjects[selected - 1];
                TeacherUtil.removeQuestion(subject);
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

            if (selected > 0 && selected <= Subjects.Count)
            {
                Subject subject = Subjects[selected - 1];
                TeacherUtil.removeAnswer(subject);
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