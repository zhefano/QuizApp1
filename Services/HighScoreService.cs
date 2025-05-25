using QuizApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.Json;

namespace QuizApp.Services
{
    /// <summary>
    /// Service for managing high scores.
    /// </summary>
    public class HighScoreService
    {
        /// <summary>
        /// Adds a high score entry for a quiz.
        /// </summary>
        /// <param name="playerName">Name of the player.</param>
        /// <param name="score">Score achieved.</param>
        /// <param name="quizId">Quiz ID.</param>
        public void AddHighScore(string playerName, int score, int quizId)
        {
            try
            {
                using var db = new AppDbContext();
                var highScore = new HighScore { PlayerName = playerName, Score = score, QuizId = quizId };
                db.HighScores.Add(highScore);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Error] Failed to add high score: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Retrieves high scores for a specific quiz, ordered by score descending.
        /// </summary>
        /// <param name="quizId">Quiz ID.</param>
        /// <returns>List of high scores.</returns>
        public List<HighScore> GetHighScoresForQuiz(int quizId)
        {
            try
            {
                using var db = new AppDbContext();
                return db.HighScores.Where(h => h.QuizId == quizId).OrderByDescending(h => h.Score).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Error] Failed to retrieve high scores: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Exports all high scores to a JSON file at the specified path.
        /// </summary>
        /// <param name="path">File path to export to.</param>
        public void ExportHighScoresToJson(string path)
        {
            try
            {
                using var db = new AppDbContext();
                var scores = db.HighScores.ToList();
                var json = JsonSerializer.Serialize(scores, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(path, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Error] Failed to export high scores: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Imports high scores from a JSON file at the specified path.
        /// </summary>
        /// <param name="path">File path to import from.</param>
        public void ImportHighScoresFromJson(string path)
        {
            try
            {
                if (!File.Exists(path))
                    throw new FileNotFoundException($"File not found: {path}");
                var json = File.ReadAllText(path);
                var scores = JsonSerializer.Deserialize<List<HighScore>>(json);
                using var db = new AppDbContext();
                foreach (var score in scores)
                {
                    if (!db.HighScores.Any(h => h.PlayerName == score.PlayerName && h.Score == score.Score && h.QuizId == score.QuizId))
                    {
                        db.HighScores.Add(score);
                    }
                }
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Error] Failed to import high scores: {ex.Message}");
                throw;
            }
        }
    }
} 