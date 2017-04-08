using System;
using thelab.mvc;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControllers : Controller<Application>
{
     void  Start()
    {
        gameModel.currentQuestionData = app.controller.Question.GetCurrentQuestionData();
        gameModel.questionPool = gameModel.currentQuestionData.questions;
     
    }
    public override void Awake()
    {
        base.Awake();
    
    }

    //public void UpdateScore()
    //{
    //    app.model.User.AddPoints();

    //}
    public void GameRestart()

    {
        SceneManager.LoadScene("Game");
    }
    private GameModel gameModel { get { return app.model.Game; } }
}
