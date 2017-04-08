using System;
using System.Collections;
using System.Collections.Generic;
using thelab.mvc;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WorkerView : View<Application>
{
    [SerializeField]
    private Animator animatorOneWorker;
    [SerializeField]
    private Animator animatorTwoWorker;
    [SerializeField]
    private SpriteRenderer[] img_BridgeElement;
    [SerializeField]
    private GameObject[] objectsForMove;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float distance;


    private bool isMoving;
    
    void Update()
    {
        if (isMoving)
        {
            Move();
        }
    }
    private void Move()
    {

        for (int i = 0; i < objectsForMove.Length; i++)
            objectsForMove[i].transform.Translate(Vector3.right * Time.deltaTime * speed);
    }

    public void StartMoving()
    {
        isMoving = true;
        AnimWorkerOne();
        animatorTwoWorker.SetTrigger("Anim_Worker2");
        StartCoroutine(StopMoving(distance / speed));
    }
  
    IEnumerator StopMoving(float time)
    {
        yield return new WaitForSeconds(time);
        AnimWorkerOne();
        isMoving = false;
    }
    public override void Awake()
    {
        base.Awake();
      
        app.model.Game.Events.StartMoving += StartMoving;
        app.model.Game.Events.PaintBridge += PaintBridge;
    }
    public void AnimWorkerOne()
    {
        animatorOneWorker.SetTrigger("Run");
    }
    private void PaintBridge(int questionIndex)
    {
        img_BridgeElement[questionIndex].GetComponent<SpriteRenderer>().color = Color.green;
    }
    public void AnimationWorkers()
    {
        animatorOneWorker.SetTrigger("Work");
        animatorTwoWorker.SetTrigger("Anim_Worker2");
    }
    public void StartedAnimDialogueWorker()
    {
        StartCoroutine(delay());
    }
    IEnumerator delay()
    {
        yield return new WaitForSeconds(2.5f);
        animatorOneWorker.SetTrigger("AnimWorker");
    }
    public void AnimDialogueWorker()
    {
        animatorOneWorker.SetTrigger("AnimWorker");
    }


}