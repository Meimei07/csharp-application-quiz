using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace Practice_exam2
{
    public class Subject
    {
        public string SubjectName;
        private string path = "D:\\C# term2\\Exam github clone\\csharp-application-quiz\\Practice-exam2\\bin\\Debug\\Subjects";
        private string extention = ".json";
        private List<QAndA> Quizzes = new List<QAndA>();
        private List<Result> Results = new List<Result>();
        private IOManager io = new IOManager();

        public Subject() { }
        public Subject(string subjectName)
        {
            this.SubjectName = subjectName;
        }

        public void Display(string username)
        {
            int TotalScore = 0;
            Console.WriteLine($"Subject: {SubjectName}");
            int index = 1;

            foreach(QAndA quiz in Quizzes)
            {
                Console.Write(index);
                quiz.Display();
                Console.Write("There may be more than 1 correct answer...");

                TotalScore += quiz.CheckAnswer();
                index++;             
            }

            Console.WriteLine($"Total score: {TotalScore}");
            //show her place among others player in that subject
            Results.Add(new Result(username, SubjectName, TotalScore));
        }

        public void DisplayResult(string username, string subject)
        {
            bool exist = false;
            foreach(Result result in Results)
            {
                if(result.Username == username && result.SubjectName == subject)
                {
                    result.Display();
                    exist = true;
                }
            }

            if (exist == false)
            {
                Console.WriteLine("no result");
            }
        }

        public void Top20()
        {
            //should create result file base on subject
            //e.g. EnglishResult to store all students' English result
            Results.Sort((a, b) => b.Score.CompareTo(a.Score));

            for(int i=0; i<Results.Count; i++)
            {
                Console.WriteLine($"Top{i+1}: {Results[i].Username} -> {Results[i].Score}pts");
            }
        }

        public int showQuiz(string subjectName)
        {
            //read from subject name file
            Quizzes = io.ReadJson<List<QAndA>>(path, subjectName);

            for (int i = 0; i < Quizzes.Count; i++)
            {
                Console.Write(i + 1);
                Quizzes[i].Display();
            }
            Console.Write("Select question: ");
            int selected = int.Parse(Console.ReadLine());

            return selected;
        }

        public void addQuestion(QAndA quiz, string subjectName)
        {
            string fullPath = Path.Combine(path, subjectName + ".json");
            if(File.Exists(fullPath))
            {
                Quizzes = io.ReadJson<List<QAndA>>(path, subjectName);
            }

            Quizzes.Add(quiz);
            io.WriteJson(path, subjectName, Quizzes);
            Console.WriteLine("question added success");
        }

        public void addAnswer(string subjectName)
        {
            int selected = showQuiz(subjectName);

            //read from subject name file, to re-assure info is up-to-date
            Quizzes = io.ReadJson <List<QAndA>>(path, subjectName);

            if (selected > 0 && selected <= Quizzes.Count)
            {
                QAndA quiz = Quizzes[selected - 1];

                Console.Write("Add answer: ");
                string answer = Console.ReadLine();
                Console.Write("Write 'true' if it's correct answer, else write 'false': ");
                string isCorrect = Console.ReadLine();

                quiz.Answers.Add(new Answer(answer, bool.Parse(isCorrect)));
                Console.WriteLine("answer added success");

                //write to subject name file
                io.WriteJson(path, subjectName, Quizzes);
            }
            else
            {
                Console.WriteLine("invalid selection");
            }
        }

        public void editQuestion(string subjectName)
        {
            int selected = showQuiz(subjectName);

            //read from subject name file, to re-assure info is up-to-date
            Quizzes = io.ReadJson<List<QAndA>>(path, subjectName);

            if (selected > 0 && selected <= Quizzes.Count)
            {
                QAndA quiz = Quizzes[selected - 1];

                Console.Write("Update the question: ");
                string newQuestion = Console.ReadLine();

                quiz.Question = newQuestion;
                Console.WriteLine("question updated success");

                //write to subject name file
                io.WriteJson(path, subjectName, Quizzes);
            }
            else
            {
                Console.WriteLine("invalid selection");
            }
        }

        public void editAnswer(string subjectName)
        {
            int selected = showQuiz(subjectName);

            //read from subject name file, to re-assure info is up-to-date
            Quizzes = io.ReadJson<List<QAndA>>(path, subjectName);

            if (selected > 0 && selected <= Quizzes.Count)
            {
                QAndA quiz = Quizzes[selected - 1];

                quiz.Display();
                Console.Write("Select answer to update: ");
                int selectedAnswer = int.Parse(Console.ReadLine());

                if(selectedAnswer > 0 && selectedAnswer <= quiz.Answers.Count)
                {
                    Answer answer = quiz.Answers[selectedAnswer - 1];
                    answer.editAns();

                    //write to subject name file
                    io.WriteJson(path, subjectName, Quizzes);
                }
                else
                {
                    Console.WriteLine("invalid selection");
                }
            }
        }

        public void removeQuestion(string subjectName)
        {
            int selected = showQuiz(subjectName);

            //read from subject name file, to re-assure info is up-to-date
            Quizzes = io.ReadJson<List<QAndA>>(path, subjectName);

            if (selected > 0 && selected <= Quizzes.Count)
            {
                Quizzes.RemoveAt(selected - 1);
                Console.WriteLine("question removed");

                //write to subject name file
                io.WriteJson(path, subjectName, Quizzes);
            }
            else
            {
                Console.WriteLine("invalid selection");
            }
        }

        public void removeAnswer(string subjectName)
        {
            int selected = showQuiz(subjectName);

            //read from subject name file, to re-assure info is up-to-date
            Quizzes = io.ReadJson<List<QAndA>>(path, subjectName);

            if (selected > 0 && selected <= Quizzes.Count)
            {
                QAndA quiz = Quizzes[selected - 1];

                quiz.Display();
                Console.Write("Select answer to remove: ");
                int selectedAnswer = int.Parse(Console.ReadLine());

                if (selectedAnswer > 0 && selectedAnswer <= quiz.Answers.Count)
                {
                    quiz.Answers.RemoveAt(selectedAnswer - 1);
                    Console.WriteLine("answer removed sucess");

                    //write to subject name file
                    io.WriteJson(path, subjectName, Quizzes);
                }
                else
                {
                    Console.WriteLine("invalid selection");
                }

            }
            else
            {
                Console.WriteLine("invalid selection");
            }
        }
    }
}