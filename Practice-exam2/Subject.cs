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
        private string path = "D:\\C# term2\\Exam github clone\\csharp-application-quiz\\Practice-exam2\\bin\\Debug\\Data";
        private string resultPath = "D:\\C# term2\\Exam github clone\\csharp-application-quiz\\Practice-exam2\\bin\\Debug";
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

            //read from subject name file, assign to Quizzes
            Quizzes = io.ReadJson<List<QAndA>>(path, SubjectName);

            foreach(QAndA quiz in Quizzes)
            {
                Console.Write(index);
                quiz.Display();
                Console.Write("There may be more than 1 correct answer...");

                TotalScore += quiz.CheckAnswer();
                index++;             
            }

            Console.WriteLine($"Total score: {TotalScore}");

            //read from subjectname_Result file
            string fullPath = Path.Combine(resultPath, SubjectName + "Result.json");
            if(File.Exists(fullPath))
            {
                Results = io.ReadJson<List<Result>>(resultPath, SubjectName + "Result");
            }

            Results.Add(new Result(username, SubjectName, TotalScore));

            //write to subjectname_Result file
            io.WriteJson(resultPath, SubjectName + "Result", Results);
            
            //show her place among others player in that subject
            List<Result> sortedrResult = Top20(SubjectName);
            int matchingIndex = sortedrResult.FindIndex(r => r.Username == username);
            Result result = sortedrResult[matchingIndex];
            Console.WriteLine($"Top{matchingIndex+1}: {result.Username} -> {result.Score}");
        }

        public void DisplayResult(string username, string subject)
        {
            string fullPath = Path.Combine(resultPath, subject + "Result" + ".json");
            if(File.Exists(fullPath))
            {
                //read from subjectName_Result file         
                Results = io.ReadJson<List<Result>>(resultPath, subject + "Result");
            }
            else
            {
                Console.WriteLine("No result");
                return;
            }

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

        public List<Result> Top20(string subjectName)
        {
            //should create result file base on subject
            //e.g. EnglishResult to store all students' English result

            //read from subjectName_Result file, assign to Results
            Results = io.ReadJson<List<Result>>(resultPath, subjectName + "Result");

            Results.Sort((a, b) => b.Score.CompareTo(a.Score));

            //write to file back after sort
            io.WriteJson(resultPath, subjectName + "Result", Results);

            List<Result> results = new List<Result>();
            List<string> names = new List<string>(); //aa, mei=3, mei=2, ju

            foreach (Result result in Results)
            {
                if (!names.Contains(result.Username))
                {
                    names.Add(result.Username);
                    results.Add(result);
                }
            }

            return results;
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

        public void mixQuestion(List<FileInfo> files)
        {
            List<QAndA> allQuizzes = new List<QAndA>();
            foreach (FileInfo file in files)
            {
                if(io.GetFileName(file) != "MixTest")
                {
                    List<QAndA> quizzes = io.ReadJson<List<QAndA>>(path, io.GetFileName(file));

                    if (quizzes != null)
                    {
                        allQuizzes.AddRange(quizzes);
                    }
                }

            }

            Random random = new Random();
            List<QAndA> mixQuizzes = new List<QAndA>();
            
            while(allQuizzes.Count > 0 && mixQuizzes.Count < 5)
            {
                int randomIndex = random.Next(allQuizzes.Count);
                mixQuizzes.Add(allQuizzes[randomIndex]);
                allQuizzes.RemoveAt(randomIndex);

            }
            Console.WriteLine("remain quiz in allQuizzes");
            foreach (QAndA q in allQuizzes)
            {
                q.Display();
            }

            io.WriteJson(path, "MixTest", mixQuizzes);

            //Console.WriteLine("mix quiz");
            //foreach (QAndA quiz in mixQuizzes)
            //{
            //    quiz.Display();
            //}
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