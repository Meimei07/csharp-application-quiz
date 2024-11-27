using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice_exam2
{
    public class TeacherUtil
    {
        public string Username = "meimei";
        public string Password = "1004";

        public TeacherUtil() { }
        public TeacherUtil(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }

        public bool Login(string username, string password)
        {
            if(this.Username == username && this.Password == password)
            {
                return true;
            }
            return false;
        }

        public void addSubject(Subject subject, List<Subject> subjects)
        {
            bool subjectExist = false;
            foreach(Subject sub in subjects)
            {
                if(sub.SubjectName == subject.SubjectName)
                {
                    subjectExist = true;
                }
            }

            if(subjectExist != true)
            {
                subjects.Add(subject);
                Console.WriteLine("subject added success");
            }
            else
            {
                Console.WriteLine("subject already exist");
            }
        }

        public void addQuiz(Subject subj)
        {
            Console.Write("Enter question: ");
            string question = Console.ReadLine();

            Console.WriteLine("Enter answer(s)...");
            List<Answer> answers = new List<Answer>();
            string answer;
            do
            {
                Console.Write($"Enter answer ('e' to finish adding answers): ");
                answer = Console.ReadLine();

                if (answer == "e")
                {
                    break;
                }

                Console.Write("Write 'true' if it's correct answer, else write 'false': ");
                string isCorrect = Console.ReadLine();

                answers.Add(new Answer(answer, bool.Parse(isCorrect)));
            } while (answer != "e");

            subj.addQuestion(new QAndA(question, answers), subj.SubjectName);
        }

        public void addAnswer(Subject subj)
        {
            subj.addAnswer(subj.SubjectName);
        }

        public void editQuestion(Subject subj)
        {
            subj.editQuestion(subj.SubjectName);
        }

        public void editAnswers(Subject subj)
        {
            subj.editAnswer(subj.SubjectName);
        }

        public void removeQuestion(Subject subj)
        {
            subj.removeQuestion(subj.SubjectName);
        }

        public void removeAnswer(Subject subj)
        {
            subj.removeAnswer(subj.SubjectName);
        }
    }
}