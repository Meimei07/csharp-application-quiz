﻿using System;
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
        //private string subjectPath = "D:\\C# term2\\Exam github clone\\csharp-application-quiz\\Practice-exam2\\bin\\Debug\\Subjects";
        private string subjectPath = Directory.GetCurrentDirectory() + @"\Subjects";
        //private string resultPath = "D:\\C# term2\\Exam github clone\\csharp-application-quiz\\Practice-exam2\\bin\\Debug\\Result";
        private string resultPath = Directory.GetCurrentDirectory() + @"\Result";
        private List<QAndA> Quizzes = new List<QAndA>();
        private List<Result> Results = new List<Result>();
        private IOManager io = new IOManager();

        public Subject() { }
        public Subject(string subjectName)
        {
            this.SubjectName = subjectName;
        }

        public List<Result> FindSubject(string subject)
        {
            return Results.Where(r => r.SubjectName == subject).ToList();
        }

        public void Display(string username)
        {
            int TotalScore = 0;
            Console.WriteLine($"Subject: {SubjectName}\n");
            int index = 1;

            //read from subject name file, assign to Quizzes
            string fullPath1 = Path.Combine(subjectPath, SubjectName + ".json");
            if(File.Exists(fullPath1))
            {
                Quizzes = io.ReadJson<List<QAndA>>(subjectPath, SubjectName);
            }

            foreach(QAndA quiz in Quizzes)
            {
                Console.Write(index);
                quiz.Display();
                Console.Write("There may be more than 1 correct answer...");

                TotalScore += quiz.CheckAnswer();
                index++;             
            }

            Console.WriteLine($"\nTotal score: {TotalScore}pts");

            //read from Results file
            string fullPath2 = Path.Combine(resultPath, "Results.json");
            if(File.Exists(fullPath2))
            {
                Results = io.ReadJson<List<Result>>(resultPath, "Results");
            }

            Results.Add(new Result(username, SubjectName, TotalScore));

            //write to Results file
            io.WriteJson(resultPath, "Results", Results);

            //show her place among others player in that subject
            List<Result> sortedResult = Top20(SubjectName);
            int matchingIndex = sortedResult.FindIndex(r => r.Username == username);
            Result result = sortedResult[matchingIndex];
            Console.WriteLine($"Top{matchingIndex + 1}: {result.Username} -> {result.Score}pts");
        }

        public void DisplayResult(string subject, string username)
        {
            string fullPath = Path.Combine(resultPath, "Results.json");
            if (File.Exists(fullPath))
            {
                //read from subjectName_Result file         
                Results = io.ReadJson<List<Result>>(resultPath, "Results");
            }
            else
            {
                Console.WriteLine("No result");
                return;
            }

            List<Result> groupResult = FindSubject(subject);
            if (groupResult.Count != 0)
            {
                List<Result> re = groupResult.Where(r => r.Username == username).ToList();
                if(re.Count != 0)
                {
                    foreach (var result in re)
                    {
                        Console.WriteLine($"Score: {result.Score}");
                    }
                }
                else
                {
                    Console.WriteLine("no result");
                }
            }
            else
            {
                Console.WriteLine("no result");
            }
        }

        public void TeacherViewResult(string subject)
        {
            string fullPath = Path.Combine(resultPath, "Results.json");
            if (File.Exists(fullPath))
            {
                //read from subjectName_Result file         
                Results = io.ReadJson<List<Result>>(resultPath, "Results");
            }
            else
            {
                Console.WriteLine("no result yet");
            }

            //List<Result> subjectResult = Results.Where(re => re.SubjectName == subject).ToList();
            List<Result> subjectResult = FindSubject(subject);

            if(subjectResult.Count != 0)
            {
                foreach (Result result in subjectResult)
                {
                    result.Display();
                }
            }
            else
            {
                Console.WriteLine("No result");
            }

        }

        public List<Result> Top20(string subjectName)
        {
            //read from Results file, assign to Results
            string fullPath = Path.Combine(resultPath, "Results.json");
            if(File.Exists(fullPath))
            {
                Results = io.ReadJson<List<Result>>(resultPath, "Results");
            }
            //else
            //{
            //    Console.WriteLine("none of students have done the test");
            //    return null;
            //}

            List<Result> groupResult = FindSubject(subjectName);

            if(groupResult.Count == 0)
            {
                Console.WriteLine("none of students have done this subject test");
                return null;
            } 

            //this list may have result of the same user 2,3,...times, becuz a user can take the same test multiple times
            groupResult.Sort((a, b) => b.Score.CompareTo(a.Score));

            List<Result> finalResults = new List<Result>();
            List<string> names = new List<string>(); 

            foreach (Result result in groupResult) 
            {
                if (!names.Contains(result.Username))
                {
                    names.Add(result.Username);
                    finalResults.Add(result); //this list makes sure to show there's no repeated username, will take the highest score
                }
            }

            return finalResults;
        }

        public int showQuiz(string subjectName)
        {
            //read from subject name file
            string fullPath = Path.Combine(subjectPath, subjectName + ".json");
            if(File.Exists(fullPath))
            {
                Quizzes = io.ReadJson<List<QAndA>>(subjectPath, subjectName); 
            }

            if(Quizzes.Count == 0)
            {
                Console.WriteLine("no available question\n");
                return -1;
            }

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
            List<QAndA> allQuizzes = new List<QAndA>(); //store all subjects' quizzes
            foreach (FileInfo file in files)
            {
                if(io.GetFileName(file) != "MixTest")
                {
                    List<QAndA> quizzes = io.ReadJson<List<QAndA>>(subjectPath, io.GetFileName(file));

                    if (quizzes != null)
                    {
                        allQuizzes.AddRange(quizzes);
                    }
                }
            }

            Random random = new Random();
            List<QAndA> mixQuizzes = new List<QAndA>();
            
            while(allQuizzes.Count > 0 && mixQuizzes.Count < 20)
            {
                int randomIndex = random.Next(0, allQuizzes.Count);
                mixQuizzes.Add(allQuizzes[randomIndex]);
                allQuizzes.RemoveAt(randomIndex); //remove so that in the next,next loop, there won't be repeat quiz in the mix
            }

            io.WriteJson(subjectPath, "MixTest", mixQuizzes);
        }

        public void addQuestion(QAndA quiz, string subjectName)
        {
            string fullPath = Path.Combine(subjectPath, subjectName + ".json");
            if(File.Exists(fullPath))
            {
                Quizzes = io.ReadJson<List<QAndA>>(subjectPath, subjectName);
            }

            Quizzes.Add(quiz);
            io.WriteJson(subjectPath, subjectName, Quizzes);
            Console.WriteLine("\nquestion added success");
        }

        public void addAnswer(string subjectName)
        {
            int selected = showQuiz(subjectName);

            //read from subject name file, to re-assure info is up-to-date
            string fullPath = Path.Combine(subjectPath, subjectName + ".json");
            if (File.Exists(fullPath))
            {
                Quizzes = io.ReadJson<List<QAndA>>(subjectPath, subjectName);
            }

            if (selected > 0 && selected <= Quizzes.Count)
            {
                QAndA quiz = Quizzes[selected - 1];

                Console.Write("Add answer: ");
                string answer = Console.ReadLine();

                if(quiz.Answers.Find(ans => ans.Element == answer) == null)
                {
                    Console.Write("Write 'true' if it's correct answer, else write 'false': ");
                    string isCorrect = Console.ReadLine();

                    quiz.Answers.Add(new Answer(answer, bool.Parse(isCorrect)));

                    //write to subject name file
                    io.WriteJson(subjectPath, subjectName, Quizzes);
                    
                    Console.WriteLine("answer added success\n");
                }
                else
                {
                    Console.WriteLine("answer already exist");
                }
            }
            //else
            //{
            //    Console.WriteLine("invalid selection\n");
            //}
        }

        public void editQuestion(string subjectName)
        {
            int selected = showQuiz(subjectName);

            //read from subject name file, to re-assure info is up-to-date
            string fullPath = Path.Combine(subjectPath, subjectName + ".json");
            if (File.Exists(fullPath))
            {
                Quizzes = io.ReadJson<List<QAndA>>(subjectPath, subjectName);
            }

            if (selected > 0 && selected <= Quizzes.Count)
            {
                QAndA quiz = Quizzes[selected - 1];

                Console.Write("Update the question: ");
                string newQuestion = Console.ReadLine();

                quiz.Question = newQuestion;

                //write to subject name file
                io.WriteJson(subjectPath, subjectName, Quizzes);
                
                Console.WriteLine("question updated success\n");
            }
            //else
            //{
            //    Console.WriteLine("invalid selection\n");
            //}
        }

        public void editAnswer(string subjectName)
        {
            int selected = showQuiz(subjectName);

            //read from subject name file, to re-assure info is up-to-date
            string fullPath = Path.Combine(subjectPath, subjectName + ".json");
            if (File.Exists(fullPath))
            {
                Quizzes = io.ReadJson<List<QAndA>>(subjectPath, subjectName);
            }

            if (selected > 0 && selected <= Quizzes.Count)
            {
                QAndA quiz = Quizzes[selected - 1];

                Console.WriteLine();
                quiz.Display();
                Console.Write("Select answer to update: ");
                int selectedAnswer = int.Parse(Console.ReadLine());

                if(selectedAnswer > 0 && selectedAnswer <= quiz.Answers.Count)
                {
                    Answer answer = quiz.Answers[selectedAnswer - 1];
                    answer.editAns(quiz.Answers);

                    //write to subject name file
                    io.WriteJson(subjectPath, subjectName, Quizzes);
                }
                else
                {
                    Console.WriteLine("invalid selection\n");
                }
            }
        }

        public void removeQuestion(string subjectName)
        {
            int selected = showQuiz(subjectName);

            //read from subject name file, to re-assure info is up-to-date
            string fullPath = Path.Combine(subjectPath, subjectName + ".json");
            if (File.Exists(fullPath))
            {
                Quizzes = io.ReadJson<List<QAndA>>(subjectPath, subjectName);
            }

            if (selected > 0 && selected <= Quizzes.Count)
            {
                Quizzes.RemoveAt(selected - 1);

                //write to subject name file
                io.WriteJson(subjectPath, subjectName, Quizzes);
                
                Console.WriteLine("question removed success\n");
            }
            //else
            //{
            //    Console.WriteLine("invalid selection\n");
            //}
        }

        public void removeAnswer(string subjectName)
        {
            int selected = showQuiz(subjectName);

            //read from subject name file, to re-assure info is up-to-date
            string fullPath = Path.Combine(subjectPath, subjectName + ".json");
            if (File.Exists(fullPath))
            {
                Quizzes = io.ReadJson<List<QAndA>>(subjectPath, subjectName);
            }

            if (selected > 0 && selected <= Quizzes.Count)
            {
                QAndA quiz = Quizzes[selected - 1];

                quiz.Display();
                Console.Write("Select answer to remove: ");
                int selectedAnswer = int.Parse(Console.ReadLine());

                if (selectedAnswer > 0 && selectedAnswer <= quiz.Answers.Count)
                {
                    quiz.Answers.RemoveAt(selectedAnswer - 1);

                    //write to subject name file
                    io.WriteJson(subjectPath, subjectName, Quizzes);
                    
                    Console.WriteLine("answer removed success\n");
                }
                else
                {
                    Console.WriteLine("invalid selection\n");
                }
            }
            //else
            //{
            //    Console.WriteLine("invalid selection\n");
            //}
        }
    }
}