using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp.Models
{
    /// <summary>
    /// Represents a quiz containing questions and high scores.
    /// </summary>
    public class Quiz
    {
        /// <summary>
        /// Unique identifier for the quiz.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Title of the quiz.
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// List of questions in the quiz.
        /// </summary>
        public List<Question> Questions { get; set; } = new List<Question>();
        /// <summary>
        /// List of high scores for the quiz.
        /// </summary>
        public List<HighScore> HighScores { get; set; } = new List<HighScore>();
    }
}
