using System.Collections;
using thelab.mvc;
using UnityEngine;
public class UIView : View<Application>
{

    [SerializeField]
    private GameObject viewPeople;
    [SerializeField]
    private GameObject[] hideAfterGameOver;
    [SerializeField]
    private GameObject viewGameOver;
    

    [SerializeField]
    private GameObject[] showItems;
    [SerializeField]
    private GameObject[] hideItems;

    public void HideAndShowItems()

    {
        HideItems();
        ShowItems();
    }
    private void ShowItems()
    {
        for (int i = 0; i < showItems.Length; i++)
        {
            showItems[i].SetActive(true);
        }
    }
    private void HideItems()
    {
        for (int i = 0; i < hideItems.Length; i++)
        {
            hideItems[i].SetActive(false);
        }
    }
    public override void Awake()
    {
        base.Awake();
        app.model.Game.Events.StartGame += StartGame;
        app.model.Game.Events.GameOver += GameOver;
    }
    private void StartGame()
    {
       app.model.Game.ShowQuestion();
    }

    public void GameOver()
    {
        app.view.GameView.questionDisplay.SetActive(false);
        StartCoroutine(PauseForView(viewPeople, 1.0f));
        StartCoroutine(PauseForView(viewGameOver,6.5f ));
        AnimationCamera.Instance.FinalAnimationCamera();
         for (int i = 0; i < hideAfterGameOver.Length; i++)
            hideAfterGameOver[i].SetActive(false);
    }
    IEnumerator PauseForView(GameObject gameObject, float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(true);
    }
}