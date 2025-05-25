using QuizApp.Models;
using QuizApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuizApp
{
    class Program
    {
        static QuizService quizService = new QuizService();
        static QuestionService questionService = new QuestionService();
        static HighScoreService highScoreService = new HighScoreService();

        /// <summary>
        /// Main entry point for the application. Handles the main menu and user input loop.
        /// </summary>
        static void Main()
        {
            using (var db = new AppDbContext())
            {
                db.Database.EnsureCreated();
            }

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\n1. Create Quiz\n2. Play Quiz\n3. View High Scores\n4. Edit Quiz\n5. Delete Quiz\n6. Exit");
                Console.ResetColor();
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
                        EditQuiz();
                        break;
                    case "5":
                        DeleteQuiz();
                        break;
                    case "6":
                        return;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid choice, try again.");
                        Console.ResetColor();
                        break;
                }
            }
        }

        /// <summary>
        /// Handles quiz creation, including input validation and question entry.
        /// </summary>
        static void CreateQuiz()
        {
            Console.Write("Enter quiz title: ");
            string title = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(title))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Quiz title cannot be empty. Please enter a valid title.");
                Console.ResetColor();
                Console.Write("Enter quiz title: ");
                title = Console.ReadLine();
            }

            var quiz = quizService.CreateQuiz(title);

            while (true)
            {
                Console.Write("Enter question text (or 'done' to finish): ");
                string text = Console.ReadLine();
                if (text.ToLower() == "done") break;
                if (string.IsNullOrWhiteSpace(text))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Question text cannot be empty.");
                    Console.ResetColor();
                    continue;
                }

                Console.Write("Enter correct answer: ");
                string answer = Console.ReadLine();
                while (string.IsNullOrWhiteSpace(answer))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Answer cannot be empty. Please enter a valid answer.");
                    Console.ResetColor();
                    Console.Write("Enter correct answer: ");
                    answer = Console.ReadLine();
                }

                questionService.AddQuestionToQuiz(quiz.Id, text, answer);
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Quiz created successfully!");
            Console.ResetColor();
        }

        /// <summary>
        /// Handles quiz playing, including question answering and score tracking.
        /// </summary>
        static void PlayQuiz()
        {
            var quizzes = quizService.GetAllQuizzes();
            if (!quizzes.Any())
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("No quizzes available.");
                Console.ResetColor();
                return;
            }

            Console.WriteLine("Available quizzes:");
            foreach (var q in quizzes) Console.WriteLine($"{q.Id}. {q.Title}");

            int quizId;
            while (true)
            {
                Console.Write("Enter quiz ID to play: ");
                var input = Console.ReadLine();
                if (int.TryParse(input, out quizId) && quizzes.Any(q => q.Id == quizId))
                    break;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid quiz ID. Please enter a valid ID from the list.");
                Console.ResetColor();
            }

            var quiz = quizService.GetQuizById(quizId);
            if (quiz == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Quiz not found.");
                Console.ResetColor();
                return;
            }

            var questions = questionService.GetQuestionsForQuiz(quizId);
            if (!questions.Any())
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("No questions found for this quiz.");
                Console.ResetColor();
                return;
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\nStarting Quiz: {quiz.Title}");
            Console.ResetColor();
            int score = 0;

            foreach (var question in questions)
            {
                Console.WriteLine($"\n{question.Text}");
                Console.Write("Your answer: ");
                string userAnswer = Console.ReadLine();

                if (userAnswer.Equals(question.CorrectAnswer, StringComparison.OrdinalIgnoreCase))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Correct!");
                    Console.ResetColor();
                    score++;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Wrong! Correct answer: {question.CorrectAnswer}");
                    Console.ResetColor();
                }
            }

            Console.Write("Enter your name for the high score: ");
            string playerName = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(playerName))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Name cannot be empty. Please enter your name.");
                Console.ResetColor();
                Console.Write("Enter your name for the high score: ");
                playerName = Console.ReadLine();
            }

            highScoreService.AddHighScore(playerName, score, quizId);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Quiz finished! Your score: {score}");
            Console.ResetColor();
        }

        /// <summary>
        /// Displays high scores for a selected quiz.
        /// </summary>
        static void ViewHighScores()
        {
            var quizzes = quizService.GetAllQuizzes();
            if (!quizzes.Any())
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("No quizzes available.");
                Console.ResetColor();
                return;
            }

            Console.WriteLine("Available quizzes:");
            foreach (var q in quizzes) Console.WriteLine($"{q.Id}. {q.Title}");

            int quizId;
            while (true)
            {
                Console.Write("Enter quiz ID to view high scores: ");
                var input = Console.ReadLine();
                if (int.TryParse(input, out quizId) && quizzes.Any(q => q.Id == quizId))
                    break;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid quiz ID. Please enter a valid ID from the list.");
                Console.ResetColor();
            }

            var scores = highScoreService.GetHighScoresForQuiz(quizId);
            if (!scores.Any())
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("No scores recorded for this quiz.");
                Console.ResetColor();
                return;
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\nHigh Scores:");
            Console.ResetColor();
            foreach (var score in scores)
            {
                Console.WriteLine($"{score.PlayerName}: {score.Score}");
            }
        }

        /// <summary>
        /// Allows editing of a quiz (title, questions, add/delete questions).
        /// </summary>
        static void EditQuiz()
        {
            var quizzes = quizService.GetAllQuizzes();
            if (!quizzes.Any())
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("No quizzes available to edit.");
                Console.ResetColor();
                return;
            }
            Console.WriteLine("Available quizzes:");
            foreach (var q in quizzes) Console.WriteLine($"{q.Id}. {q.Title}");
            int quizId;
            while (true)
            {
                Console.Write("Enter quiz ID to edit: ");
                var input = Console.ReadLine();
                if (int.TryParse(input, out quizId) && quizzes.Any(q => q.Id == quizId))
                    break;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid quiz ID. Please enter a valid ID from the list.");
                Console.ResetColor();
            }
            var quiz = quizService.GetQuizById(quizId);
            if (quiz == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Quiz not found.");
                Console.ResetColor();
                return;
            }
            Console.WriteLine($"Editing Quiz: {quiz.Title}");
            Console.WriteLine("1. Edit Title\n2. Edit Questions\n3. Add Question\n4. Delete Question\n5. Back");
            Console.Write("Choose an option: ");
            var option = Console.ReadLine();
            switch (option)
            {
                case "1":
                    Console.Write("Enter new title: ");
                    var newTitle = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(newTitle))
                    {
                        quiz.Title = newTitle;
                        quizService.UpdateQuiz(quiz);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Quiz title updated.");
                        Console.ResetColor();
                    }
                    break;
                case "2":
                    EditQuestion(quizId);
                    break;
                case "3":
                    Console.Write("Enter question text: ");
                    var text = Console.ReadLine();
                    Console.Write("Enter correct answer: ");
                    var answer = Console.ReadLine();
                    questionService.AddQuestionToQuiz(quizId, text, answer);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Question added.");
                    Console.ResetColor();
                    break;
                case "4":
                    DeleteQuestion(quizId);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Allows editing of a question's text and answer.
        /// </summary>
        static void EditQuestion(int quizId)
        {
            var questions = questionService.GetQuestionsForQuiz(quizId);
            if (!questions.Any())
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("No questions to edit.");
                Console.ResetColor();
                return;
            }
            Console.WriteLine("Questions:");
            foreach (var q in questions) Console.WriteLine($"{q.Id}. {q.Text}");
            int questionId;
            while (true)
            {
                Console.Write("Enter question ID to edit: ");
                var input = Console.ReadLine();
                if (int.TryParse(input, out questionId) && questions.Any(q => q.Id == questionId))
                    break;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid question ID. Please enter a valid ID from the list.");
                Console.ResetColor();
            }
            var question = questions.First(q => q.Id == questionId);
            Console.WriteLine($"Current text: {question.Text}");
            Console.Write("Enter new text (leave blank to keep): ");
            var newText = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newText))
                question.Text = newText;
            Console.WriteLine($"Current answer: {question.CorrectAnswer}");
            Console.Write("Enter new answer (leave blank to keep): ");
            var newAnswer = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newAnswer))
                question.CorrectAnswer = newAnswer;
            questionService.UpdateQuestion(question);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Question updated.");
            Console.ResetColor();
        }

        /// <summary>
        /// Deletes a quiz and all related data.
        /// </summary>
        static void DeleteQuiz()
        {
            var quizzes = quizService.GetAllQuizzes();
            if (!quizzes.Any())
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("No quizzes available to delete.");
                Console.ResetColor();
                return;
            }
            Console.WriteLine("Available quizzes:");
            foreach (var q in quizzes) Console.WriteLine($"{q.Id}. {q.Title}");
            int quizId;
            while (true)
            {
                Console.Write("Enter quiz ID to delete: ");
                var input = Console.ReadLine();
                if (int.TryParse(input, out quizId) && quizzes.Any(q => q.Id == quizId))
                    break;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid quiz ID. Please enter a valid ID from the list.");
                Console.ResetColor();
            }
            quizService.DeleteQuiz(quizId);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Quiz deleted.");
            Console.ResetColor();
        }

        /// <summary>
        /// Deletes a question from a quiz.
        /// </summary>
        static void DeleteQuestion(int quizId)
        {
            var questions = questionService.GetQuestionsForQuiz(quizId);
            if (!questions.Any())
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("No questions to delete.");
                Console.ResetColor();
                return;
            }
            Console.WriteLine("Questions:");
            foreach (var q in questions) Console.WriteLine($"{q.Id}. {q.Text}");
            int questionId;
            while (true)
            {
                Console.Write("Enter question ID to delete: ");
                var input = Console.ReadLine();
                if (int.TryParse(input, out questionId) && questions.Any(q => q.Id == questionId))
                    break;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid question ID. Please enter a valid ID from the list.");
                Console.ResetColor();
            }
            questionService.DeleteQuestion(questionId);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Question deleted.");
            Console.ResetColor();
        }
    }
}
