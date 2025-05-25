using NUnit.Framework;
using QuizApp.Services;

namespace QuizApp.Tests
{
    [TestFixture]
    public class QuizServiceTests
    {
        private QuizService quizService;

        [SetUp]
        public void SetUp()
        {
            quizService = new QuizService();
        }

        [Test]
        public void CreateQuiz_ShouldCreateQuiz()
        {
            // TODO: Implement test
        }

        [Test]
        public void UpdateQuiz_ShouldUpdateQuizTitle()
        {
            // TODO: Implement test
        }

        [Test]
        public void DeleteQuiz_ShouldRemoveQuiz()
        {
            // TODO: Implement test
        }

        [Test]
        public void GetAllQuizzes_ShouldReturnQuizzes()
        {
            // TODO: Implement test
        }
    }
} 