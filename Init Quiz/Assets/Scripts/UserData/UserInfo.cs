using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class UserInfo
{
    public string fullname = "";
    public string hospital = "";
    public string major = "";
    public string place = "";
    public float lat = 0f;
    public float lng = 0f;
    public int timeplay = 0;
    public int numberCorrect = 0;
    public int numberIncorrect = 0;
    public DateTime date;
    public bool isDoctor = true;
    public List<QuestionResult> result = new List<QuestionResult>();
}