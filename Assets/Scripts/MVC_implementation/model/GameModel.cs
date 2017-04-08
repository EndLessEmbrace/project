using System;
using System.Collections;
using thelab.mvc;
using UnityEngine;

public class GameModel : Model<Application>
{
    
    [SerializeField]
    private int totalTimeSecondsPerQuestion;
    [SerializeField]
    private int totalTimeSeconds;
   
    public int TotalTimeSecondsPerQuestion
    {
        get
        { return totalTimeSecondsPerQuestion; } 
    }
    public int TotalTimeSeconds
    {
        get
        { return totalTimeSeconds; }
    }
    public GameEventsModel Events;
    private int questionIndex = 0;

    public QuestionData[] questionPool { get; set; }
    public QuestionController dataController { get; private set; }
    public QuestionModel currentQuestionData { get;  set; }

    public void AnswerButtonClicker(bool isCorrect)
    {
        if (isCorrect)
        {
            Events.CallScore();
       
        }
        if (questionPool.Length > questionIndex + 1)
        {
            Events.CallPaintBridge(questionIndex);
            Events.CallSetActiveTimeForQuestion(false);
            Events.CallStopTime();
            Events.CallAnimationQuestionPanel();
            Events.CallStartMoving();
            ShowNextQuestion();
        }
        else
        {
            Events.CallGameOver();
            Events.CallScore();
            Events.CallPaintBridge(questionIndex);
        }
    }

    private void ShowNextQuestion()
    {
        NextQuestion();
        StartCoroutine(pauseForShowQuestion(2.25f));
    }
    public void ShowNextQuestionAfterTimeOver()
    {
        if (questionPool.Length > questionIndex + 1)
        {
            Events.CallSetActiveTimeForQuestion(false);
            Events.CallStopTime();
            Events.CallAnimationQuestionPanel();
            Events.CallStartMoving();
            NextQuestion();
            StartCoroutine(pauseForShowQuestion(2.25f));
        }
        else
        {
            Events.CallGameOver();
        }
           
    }

    public IEnumerator pauseForShowQuestion(float delay)
    {
        yield return new WaitForSeconds(delay);
        ShowQuestion();
        Events.CallUpdateTimeForQuestion();
        Events.CallSetActiveTimeForQuestion(true);
        Events.CallTimeIsOver();
    }
    public void ShowQuestion()
    {
        Events.CallShowQuestion(questionPool[questionIndex], questionIndex+1);
    }
    public QuestionData NextQuestion()
    {
        return questionPool[questionIndex++];

    }


}