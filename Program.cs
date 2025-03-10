using QuizApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuizApp
{
    class Program
    {
        static void Main()
        {
            using var db = new AppDbContext();
            db.Database.EnsureCreated();

            while (true)
            {
                Console.WriteLine("\n1. Create Quiz\n2. Play Quiz\n3. View High Scores\n4. Exit");
                Console.Write("Choose an option: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CreateQuiz();
                        break;
                    case "2":
                        PlayQuiz();
                        break;
                    case "3":
                        ViewHighScores();
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Invalid choice, try again.");
                        break;
                }
            }
        }

        static void CreateQuiz()
        {
            using var db = new AppDbContext();

            Console.Write("Enter quiz title: ");
            string title = Console.ReadLine();

            var quiz = new Quiz { Title = title };
            db.Quizzes.Add(quiz);
            db.SaveChanges();

            while (true)
            {
                Console.Write("Enter question text (or 'done' to finish): ");
                string text = Console.ReadLine();
                if (text.ToLower() == "done") break;

                Console.Write("Enter correct answer: ");
                string answer = Console.ReadLine();

                quiz.Questions.Add(new Question { Text = text, CorrectAnswer = answer });
                db.SaveChanges();
            }

            Console.WriteLine("Quiz created successfully!");
        }

        static void PlayQuiz()
        {
            using var db = new AppDbContext();

            var quizzes = db.Quizzes.ToList();
            if (!quizzes.Any())
            {
                Console.WriteLine("No quizzes available.");
                return;
            }

            Console.WriteLine("Available quizzes:");
            foreach (var q in quizzes) Console.WriteLine($"{q.Id}. {q.Title}");

            Console.Write("Enter quiz ID to play: ");
            if (!int.TryParse(Console.ReadLine(), out int quizId)) return;

            var quiz = db.Quizzes
                .Where(q => q.Id == quizId)
                .Select(q => new { q.Title, q.Questions })
                .FirstOrDefault();

            if (quiz == null)
            {
                Console.WriteLine("Quiz not found.");
                return;
            }

            Console.WriteLine($"\nStarting Quiz: {quiz.Title}");
            int score = 0;

            foreach (var question in quiz.Questions)
            {
                Console.WriteLine($"\n{question.Text}");
                Console.Write("Your answer: ");
                string userAnswer = Console.ReadLine();

                if (userAnswer.Equals(question.CorrectAnswer, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Correct!");
                    score++;
                }
                else
                {
                    Console.WriteLine($"Wrong! Correct answer: {question.CorrectAnswer}");
                }
            }

            Console.Write("Enter your name for the high score: ");
            string playerName = Console.ReadLine();

            db.HighScores.Add(new HighScore { PlayerName = playerName, Score = score, QuizId = quizId });
            db.SaveChanges();

            Console.WriteLine($"Quiz finished! Your score: {score}");
        }

        static void ViewHighScores()
        {
            using var db = new AppDbContext();

            var quizzes = db.Quizzes.ToList();
            if (!quizzes.Any())
            {
                Console.WriteLine("No quizzes available.");
                return;
            }

            Console.WriteLine("Available quizzes:");
            foreach (var q in quizzes) Console.WriteLine($"{q.Id}. {q.Title}");

            Console.Write("Enter quiz ID to view high scores: ");
            if (!int.TryParse(Console.ReadLine(), out int quizId)) return;

            var scores = db.HighScores
                .Where(h => h.QuizId == quizId)
                .OrderByDescending(h => h.Score)
                .ToList();

            if (!scores.Any())
            {
                Console.WriteLine("No scores recorded for this quiz.");
                return;
            }

            Console.WriteLine("\nHigh Scores:");
            foreach (var score in scores)
            {
                Console.WriteLine($"{score.PlayerName}: {score.Score}");
            }
        }
    }
}
