using System.Collections;
using System.Collections.Generic;
using thelab.mvc;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameView : View<Application>
{
    public WorkerView gameView;
    private List<GameObject> answerButtonGameObjects = new List<GameObject>();

    [SerializeField]
    private Animator animatorQuestionPanel;
    [SerializeField]
    private Transform questionParentDestroyExtraObject;

    public Text numberQuestionText;
    public Text questionDisplayText;
    public Text scoreDisplayText;
    public Text timeRemainingDisplayText;
    public Text timeRemainingDisplayTextPerQuestion;

    private bool timeTick = true;
    private bool timeIsOver = true;

    private float playerScore;
    public GameObject timeRemainingDisplay;
    public GameObject questionDisplay;
    public SimpleObjectPool answerButtonObjectPool;
    public Transform answerButtonParent;
    private float totalTimeSeconds;
    private float totalTimeSecondsPerQuestion;

    void Start()
    {
        ResetScore();
    }

    public override void Awake()
    {
        base.Awake();
        app.model.Game.Events.ShowQuestion += ShowQuestion;
        app.model.Game.Events.UpdateTimeForQuestion += UpdateTimeForQuestion;
        app.model.Game.Events.SetActiveTimeForQuestion += SetActiveTimeForQuestion;
        app.model.Game.Events.Score += Score;
        app.model.Game.Events.StartTime += StartTime;
        app.model.Game.Events.StopTime += StopTime;
        app.model.Game.Events.TimeIsOver += TimeIsOver;
        app.model.Game.Events.AnimationQuestionPanel += AnimationQuestionPanel;

        //app.model.User.OnScoreUpdated += i => scoreDisplayText.text = i.ToString();

    }
    private void Score()
    {
        playerScore += app.model.User.PointsAddedForCorrectAnswer;
        scoreDisplayText.text = Mathf.Round(playerScore).ToString() + " %";
    }
    private void ResetScore()
    {
        playerScore = 0;
    }
    private void StartTime()
    {
        totalTimeSeconds = app.model.Game.TotalTimeSeconds;
        UpdateTimeForQuestion();
        StartCoroutine(AsUpdate());
    }
    private void StopTime()
    {
        timeTick = false;
        StartCoroutine(RunTime(2.0f));
    }
    IEnumerator RunTime(float delay)
    {
        yield return new WaitForSeconds(delay);
        timeTick = true;
    }
    private void ShowQuestion(QuestionData questionData, int number)
    {
     
        questionDisplayText.text = questionData.questionText;
        DestroyExtraObject();
        RemoveAnswerButtons();
        gameView.AnimationWorkers();

        for (int i = 0; i < questionData.answers.Length; i++)
        {
            numberQuestionText.text = number.ToString() + "  вопрос";
            GameObject answerButtonGameObject = answerButtonObjectPool.GetObject();
            answerButtonGameObject.transform.SetParent(answerButtonParent);
            answerButtonGameObjects.Add(answerButtonGameObject);
            AnswerButton answerButton = answerButtonGameObject.GetComponent<AnswerButton>();
            answerButton.Setup(questionData.answers[i]);
        }
        for (int i = 0; i < questionData.extraObjects.Length; i++)
        {
            Instantiate(questionData.extraObjects[i], questionParentDestroyExtraObject.transform.position,
                questionParentDestroyExtraObject.transform.rotation, questionParentDestroyExtraObject);
        }
    }
    private void UpdateTimeForQuestion()
    {
        totalTimeSecondsPerQuestion = app.model.Game.TotalTimeSecondsPerQuestion;
    }
    private void SetActiveTimeForQuestion(bool boolean)
    {
        timeRemainingDisplay.SetActive(boolean);
    }
    private void AnimationQuestionPanel()
    {
        animatorQuestionPanel.SetTrigger("Anim");
    }

    private void DestroyExtraObject()
    {
        foreach (Transform child in questionParentDestroyExtraObject)
        {
            Destroy(child.gameObject);
        }
    }
    private void RemoveAnswerButtons()
    {
        while (answerButtonGameObjects.Count > 0)
        {
            answerButtonObjectPool.ReturnObject(answerButtonGameObjects[0]);
            answerButtonGameObjects.RemoveAt(0);
        }
    }
    private void TimeIsOver()
    {
        timeIsOver = true;
    }
    void UpdateTimeRemainingDisplay()
    {
        timeRemainingDisplayTextPerQuestion.text = Mathf.Round(totalTimeSecondsPerQuestion).ToString();
        timeRemainingDisplayText.text = Mathf.Round(totalTimeSeconds).ToString();
    }
    private IEnumerator AsUpdate()
    {
        while (totalTimeSeconds > 0f)
        {
            if (timeTick) totalTimeSeconds-= Time.deltaTime;
            totalTimeSecondsPerQuestion -= Time.deltaTime;
            UpdateTimeRemainingDisplay();

            if (totalTimeSecondsPerQuestion <= 0f)
            {
                if(timeIsOver)
                { 
                app.model.Game.ShowNextQuestionAfterTimeOver();
                    timeIsOver = false;
                }
            }
            if (totalTimeSeconds <= 0f)
            {
                app.view.UIView.GameOver();
            }
            yield return null;

        }
        yield return null;
    }

    
   
}