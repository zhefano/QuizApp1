using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp.Models
{
    /// <summary>
    /// Represents a question in a quiz.
    /// </summary>
    public class Question
    {
        /// <summary>
        /// Unique identifier for the question.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The text of the question.
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// The correct answer to the question.
        /// </summary>
        public string CorrectAnswer { get; set; }
        /// <summary>
        /// Foreign key to the parent quiz.
        /// </summary>
        public int QuizId { get; set; }
        /// <summary>
        /// Reference to the parent quiz.
        /// </summary>
        public Quiz Quiz { get; set; }
    }
}
