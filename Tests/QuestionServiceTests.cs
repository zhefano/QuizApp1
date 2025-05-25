using NUnit.Framework;
using QuizApp.Services;

namespace QuizApp.Tests
{
    [TestFixture]
    public class QuestionServiceTests
    {
        private QuestionService questionService;

        [SetUp]
        public void SetUp()
        {
            questionService = new QuestionService();
        }

        [Test]
        public void AddQuestionToQuiz_ShouldAddQuestion()
        {
            // TODO: Implement test
        }

        [Test]
        public void UpdateQuestion_ShouldUpdateQuestion()
        {
            // TODO: Implement test
        }

        [Test]
        public void DeleteQuestion_ShouldRemoveQuestion()
        {
            // TODO: Implement test
        }

        [Test]
        public void GetQuestionsForQuiz_ShouldReturnQuestions()
        {
            // TODO: Implement test
        }
    }
} 