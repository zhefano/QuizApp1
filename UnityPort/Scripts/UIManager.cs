using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject quizPanel;
    public GameObject resultPanel;
    public GameObject highScorePanel;

    public Text questionText;
    public Button[] choiceButtons;
    public Text feedbackText;
    public Text scoreText;

    private QuizManager quizManager;

    void Start()
    {
        quizManager = FindObjectOfType<QuizManager>();
        quizManager.OnQuestionChanged += UpdateQuestionUI;
        quizManager.OnQuizCompleted += ShowResultPanel;
    }

    public void ShowMainMenu()
    {
        mainMenuPanel.SetActive(true);
        quizPanel.SetActive(false);
        resultPanel.SetActive(false);
        highScorePanel.SetActive(false);
    }

    public void ShowQuizPanel()
    {
        mainMenuPanel.SetActive(false);
        quizPanel.SetActive(true);
        resultPanel.SetActive(false);
        highScorePanel.SetActive(false);
    }

    public void ShowResultPanel(int score)
    {
        resultPanel.SetActive(true);
        quizPanel.SetActive(false);
        mainMenuPanel.SetActive(false);
        highScorePanel.SetActive(false);
        scoreText.text = $"Score: {score}";
    }

    public void ShowHighScorePanel()
    {
        mainMenuPanel.SetActive(false);
        quizPanel.SetActive(false);
        resultPanel.SetActive(false);
        highScorePanel.SetActive(true);
    }

    public void UpdateQuestionUI(QuestionData question)
    {
        questionText.text = question.Text;
        for (int i = 0; i < choiceButtons.Length; i++)
        {
            if (i < question.Choices.Count)
            {
                choiceButtons[i].gameObject.SetActive(true);
                choiceButtons[i].GetComponentInChildren<Text>().text = question.Choices[i];
                int index = i;
                choiceButtons[i].onClick.RemoveAllListeners();
                choiceButtons[i].onClick.AddListener(() => OnChoiceSelected(question.Choices[index]));
            }
            else
            {
                choiceButtons[i].gameObject.SetActive(false);
            }
        }
        feedbackText.text = "";
    }

    public void OnChoiceSelected(string selectedAnswer)
    {
        bool correct = quizManager.SubmitAnswer(selectedAnswer);
        feedbackText.text = correct ? "Correct!" : "Wrong!";
    }
} 