using QuizApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuizApp.Services
{
    /// <summary>
    /// Service for managing questions.
    /// </summary>
    public class QuestionService
    {
        /// <summary>
        /// Adds a question to a quiz.
        /// </summary>
        public void AddQuestionToQuiz(int quizId, string text, string correctAnswer)
        {
            try
            {
                using var db = new AppDbContext();
                var question = new Question { QuizId = quizId, Text = text, CorrectAnswer = correctAnswer };
                db.Questions.Add(question);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Error] Failed to add question: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Retrieves all questions for a specific quiz.
        /// </summary>
        public List<Question> GetQuestionsForQuiz(int quizId)
        {
            try
            {
                using var db = new AppDbContext();
                return db.Questions.Where(q => q.QuizId == quizId).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Error] Failed to retrieve questions: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Updates the specified question's text and correct answer.
        /// </summary>
        public void UpdateQuestion(Question question)
        {
            try
            {
                using var db = new AppDbContext();
                var existing = db.Questions.FirstOrDefault(q => q.Id == question.Id);
                if (existing != null)
                {
                    existing.Text = question.Text;
                    existing.CorrectAnswer = question.CorrectAnswer;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Error] Failed to update question: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Deletes the question by its ID.
        /// </summary>
        public void DeleteQuestion(int questionId)
        {
            try
            {
                using var db = new AppDbContext();
                var question = db.Questions.FirstOrDefault(q => q.Id == questionId);
                if (question != null)
                {
                    db.Questions.Remove(question);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Error] Failed to delete question: {ex.Message}");
                throw;
            }
        }
    }
} 