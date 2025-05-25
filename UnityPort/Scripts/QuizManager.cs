using System;
using System.Collections.Generic;
using UnityEngine;

public class QuizManager : MonoBehaviour
{
    public QuizData currentQuiz;
    public int CurrentQuestionIndex { get; private set; }
    public int Score { get; private set; }
    public event Action<QuestionData> OnQuestionChanged;
    public event Action<int> OnQuizCompleted;

    private List<QuestionData> randomizedQuestions;

    public void StartQuiz(QuizData quiz, bool randomize = true)
    {
        currentQuiz = quiz;
        CurrentQuestionIndex = 0;
        Score = 0;
        randomizedQuestions = new List<QuestionData>(quiz.Questions);
        if (randomize)
            Shuffle(randomizedQuestions);
        ShowCurrentQuestion();
    }

    public void ShowCurrentQuestion()
    {
        if (HasMoreQuestions())
            OnQuestionChanged?.Invoke(GetCurrentQuestion());
        else
            OnQuizCompleted?.Invoke(Score);
    }

    public QuestionData GetCurrentQuestion()
    {
        if (currentQuiz == null || CurrentQuestionIndex >= randomizedQuestions.Count)
            return null;
        return randomizedQuestions[CurrentQuestionIndex];
    }

    public bool SubmitAnswer(string answer)
    {
        var question = GetCurrentQuestion();
        if (question == null) return false;
        bool correct = answer.Trim().Equals(question.CorrectAnswer.Trim(), StringComparison.OrdinalIgnoreCase);
        if (correct) Score++;
        CurrentQuestionIndex++;
        ShowCurrentQuestion();
        return correct;
    }

    public bool HasMoreQuestions()
    {
        return currentQuiz != null && CurrentQuestionIndex < randomizedQuestions.Count;
    }

    public void ResetQuiz()
    {
        CurrentQuestionIndex = 0;
        Score = 0;
        ShowCurrentQuestion();
    }

    private void Shuffle(List<QuestionData> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int j = UnityEngine.Random.Range(i, list.Count);
            var temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }
} 