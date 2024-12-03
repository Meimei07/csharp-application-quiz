using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice_exam2
{
    public class Answer
    {
        public string Element;
        public bool IsCorrect;

        public Answer() { }
        public Answer(int selected)
        {

        }
        public Answer(string element, bool isCorrect)
        {
            this.Element = element;
            this.IsCorrect = isCorrect;
        }

        public void Display()
        {
            Console.WriteLine($"{Element}: {IsCorrect}");
        }

        public void editAns(List<Answer> Answers)
        {
            Console.Write("Update your answer: ");
            string newAnswer = Console.ReadLine();

            if(Answers.Find(ans => ans.Element == newAnswer) == null)
            {
                Console.Write("Write 'true' if it's correct answer, else write 'false': ");
                string isCorrect = Console.ReadLine();

                Element = newAnswer;
                IsCorrect = bool.Parse(isCorrect);
                Console.WriteLine("answer updated success\n");
            }
            else
            {
                Console.WriteLine("answer already exist");
            }
        }
    }
}