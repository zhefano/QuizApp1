using NUnit.Framework;
using QuizApp.Services;

namespace QuizApp.Tests
{
    [TestFixture]
    public class HighScoreServiceTests
    {
        private HighScoreService highScoreService;

        [SetUp]
        public void SetUp()
        {
            highScoreService = new HighScoreService();
        }

        [Test]
        public void AddHighScore_ShouldAddScore()
        {
            // TODO: Implement test
        }

        [Test]
        public void GetHighScoresForQuiz_ShouldReturnScores()
        {
            // TODO: Implement test
        }
    }
} 