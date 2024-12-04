using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice_exam2
{
    public class QAndA
    {
        public string Question;
        public List<Answer> Answers;

        private int Score;

        public QAndA()
        {
            Question = string.Empty;
            Answers = new List<Answer>();
        }
        public QAndA(string question, List<Answer> answers)
        {
            this.Question = question;
            this.Answers = answers;
        }

        public int CheckAnswer()
        {
            Score = 0;
            List<int> selectedAnswers = new List<int>();
            int selected;

            do
            {
                Console.Write("Select (0 to end selection): ");
                selected = int.Parse(Console.ReadLine());

                if (selected == 0)
                {
                    break;
                }

                if (selected > 0 && selected <= Answers.Count)
                {
                    if (!selectedAnswers.Contains(selected))
                    {
                        selectedAnswers.Add(selected);
                    }
                    else
                    {
                        Console.WriteLine("answer already selected");
                    }
                }
                else
                {
                    Console.WriteLine("invalid selection");
                }
            } while (selected != 0);

            int correctAnswerCount = 0;
            foreach(Answer answer in Answers)
            {
                if(answer.IsCorrect == true)
                {
                    correctAnswerCount++;
                }
            }

            if(correctAnswerCount == selectedAnswers.Count)
            {
                bool correctAll = true;
                foreach(int selectedAnswer in selectedAnswers)
                {
                    if (Answers[selectedAnswer-1].IsCorrect == false)
                    {
                        correctAll = false;
                        break;
                    }
                }

                if(correctAll == true)
                {
                    Score = 1;
                }
                else
                {
                    Score = 0;
                }
            }
            else
            {
                Score = 0;
            }

            return Score;
        }

        public void Display()
        {
            Console.Write($"- {Question}?  -->");
            for(int i=0; i<Answers.Count; i++)
            {
                Console.Write($"     {i+1}/. {Answers[i].Element}");
            }
            Console.WriteLine();
        }
    }
}