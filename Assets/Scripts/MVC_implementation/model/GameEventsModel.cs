using System;
using UnityEngine;

public class GameEventsModel : MonoBehaviour
{
    public event Action StartGame = delegate { };
    public void CallStartGame()
    {
        StartGame();
    }
    public event Action StartTime = delegate { };
    public void CallStartTime()
    {
        StartTime();
    }
    public event Action StopTime = delegate { };
    public void CallStopTime()
    {
        StopTime();
    }
    public event Action NextQuestion = delegate { };
    public void CallNextQuestion()
    {
        NextQuestion();
    }
    public event Action GameOver = delegate { };
    public void CallGameOver()
    {
        GameOver();
    }
    
    public event Action UpdateTimeForQuestion = delegate { };
    public void CallUpdateTimeForQuestion()
    {
        UpdateTimeForQuestion();
    }
    public event Action<QuestionData,int> ShowQuestion = delegate { };
    public void CallShowQuestion(QuestionData questionData, int number)
    {
        ShowQuestion(questionData, number);
    }
    public event Action AnimationQuestionPanel = delegate { };
    public void CallAnimationQuestionPanel()
    {
        AnimationQuestionPanel();
    }
    public event Action<int> PaintBridge = delegate { };
    public void CallPaintBridge(int questionIndex)
    {
        PaintBridge(questionIndex);
    }
    public event Action StartMoving = delegate { };
    public void CallStartMoving()
    {
        StartMoving();
    }
    public event Action<bool> SetActiveTimeForQuestion = delegate { };
    public void CallSetActiveTimeForQuestion(bool boolean)
    {
        SetActiveTimeForQuestion(boolean);
    }
    public event Action TimeIsOver = delegate { };
    public void CallTimeIsOver()
    {
        TimeIsOver();
    }
    public event Action Score = delegate { };
    public void CallScore()
    {
        Score();
    }
    
    

}