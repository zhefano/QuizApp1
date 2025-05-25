using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class QuestionData
{
    public string Text;
    public List<string> Choices = new List<string>();
    public string CorrectAnswer;
    public Sprite Image; // Optional: for image questions
} 