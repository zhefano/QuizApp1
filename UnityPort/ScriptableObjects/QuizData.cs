using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuizData", menuName = "Quiz/Quiz Data", order = 1)]
public class QuizData : ScriptableObject
{
    public string Title;
    public string Category; // Optional: for filtering quizzes
    public List<QuestionData> Questions = new List<QuestionData>();
} 