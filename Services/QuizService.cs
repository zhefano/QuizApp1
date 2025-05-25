using QuizApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.Json;

namespace QuizApp.Services
{
    /// <summary>
    /// Service for managing quizzes.
    /// </summary>
    public class QuizService
    {
        /// <summary>
        /// Creates a new quiz with the specified title.
        /// </summary>
        public Quiz CreateQuiz(string title)
        {
            try
            {
                using var db = new AppDbContext();
                var quiz = new Quiz { Title = title };
                db.Quizzes.Add(quiz);
                db.SaveChanges();
                return quiz;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Error] Failed to create quiz: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Retrieves all quizzes.
        /// </summary>
        public List<Quiz> GetAllQuizzes()
        {
            try
            {
                using var db = new AppDbContext();
                return db.Quizzes.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Error] Failed to retrieve quizzes: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Retrieves a quiz by its ID.
        /// </summary>
        public Quiz GetQuizById(int id)
        {
            try
            {
                using var db = new AppDbContext();
                return db.Quizzes.FirstOrDefault(q => q.Id == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Error] Failed to retrieve quiz: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Updates the specified quiz's title.
        /// </summary>
        public void UpdateQuiz(Quiz quiz)
        {
            try
            {
                using var db = new AppDbContext();
                var existing = db.Quizzes.FirstOrDefault(q => q.Id == quiz.Id);
                if (existing != null)
                {
                    existing.Title = quiz.Title;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Error] Failed to update quiz: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Deletes the quiz and all related questions and high scores.
        /// </summary>
        public void DeleteQuiz(int quizId)
        {
            try
            {
                using var db = new AppDbContext();
                var quiz = db.Quizzes.FirstOrDefault(q => q.Id == quizId);
                if (quiz != null)
                {
                    db.Questions.RemoveRange(db.Questions.Where(q => q.QuizId == quizId));
                    db.HighScores.RemoveRange(db.HighScores.Where(h => h.QuizId == quizId));
                    db.Quizzes.Remove(quiz);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Error] Failed to delete quiz: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Exports all quizzes to a JSON file at the specified path.
        /// </summary>
        /// <param name="path">File path to export to.</param>
        public void ExportQuizzesToJson(string path)
        {
            try
            {
                using var db = new AppDbContext();
                var quizzes = db.Quizzes.ToList();
                var json = JsonSerializer.Serialize(quizzes, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(path, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Error] Failed to export quizzes: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Imports quizzes from a JSON file at the specified path.
        /// </summary>
        /// <param name="path">File path to import from.</param>
        public void ImportQuizzesFromJson(string path)
        {
            try
            {
                if (!File.Exists(path))
                    throw new FileNotFoundException($"File not found: {path}");
                var json = File.ReadAllText(path);
                var quizzes = JsonSerializer.Deserialize<List<Quiz>>(json);
                using var db = new AppDbContext();
                foreach (var quiz in quizzes)
                {
                    if (!db.Quizzes.Any(q => q.Title == quiz.Title))
                    {
                        db.Quizzes.Add(quiz);
                    }
                }
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Error] Failed to import quizzes: {ex.Message}");
                throw;
            }
        }
    }
} 