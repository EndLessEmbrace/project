using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using thelab.mvc;
public class AnswerButton :Controller<Application> {

    public Text answerText;

    private AnswerData answerData;
    private GameControllers gameController;

    void Start()
    {
        gameController = FindObjectOfType<GameControllers>();
    }
    public void Setup(AnswerData data)
    {
        answerData = data;
        answerText.text = answerData.answerText;
    }
    public void HandleClick()
    {
        app.model.Game.AnswerButtonClicker(answerData.isCorrect);
    }  
}
