using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice_exam2
{
    public class Result
    {
        public string Username;
        public string SubjectName;
        public int Score; 

        public Result() { }
        public Result(string username, string subjectName, int score)
        {
            this.Username = username;
            this.SubjectName = subjectName;
            this.Score = score;
        }

        public void Display()
        {
            Console.WriteLine($"Student: {Username}, Score: {Score}");
        }
    }
}