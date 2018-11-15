using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class QuestionResult
{
    public int test_type = 0;
    public bool isDone = false;
    public int question_order = 0;
    public string user_answer = "";
    public bool correct = false;
}