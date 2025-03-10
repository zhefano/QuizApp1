﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp.Models
{
    public class Quiz
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<Question> Questions { get; set; } = new List<Question>();
        public List<HighScore> HighScores { get; set; } = new List<HighScore>();
    }
}
