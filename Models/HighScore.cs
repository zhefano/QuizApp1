using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp.Models
{
    /// <summary>
    /// Represents a high score entry for a quiz.
    /// </summary>
    public class HighScore
    {
        /// <summary>
        /// Unique identifier for the high score entry.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Name of the player who achieved the score.
        /// </summary>
        public string PlayerName { get; set; }
        /// <summary>
        /// The score achieved by the player.
        /// </summary>
        public int Score { get; set; }
        /// <summary>
        /// Foreign key to the related quiz.
        /// </summary>
        public int QuizId { get; set; }
        /// <summary>
        /// Reference to the related quiz.
        /// </summary>
        public Quiz Quiz { get; set; }
    }
}
