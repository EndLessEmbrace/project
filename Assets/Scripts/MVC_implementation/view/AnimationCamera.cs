using UnityEngine;
using System.Collections;
using System;

public class AnimationCamera : MonoBehaviour {
    public static AnimationCamera Instance;
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private GameObject[] showItemsUI;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    public void Started ()
    {
        StartCoroutine(delay());
    }
    IEnumerator delay()
    {
        yield return new WaitForSeconds(0.5f);
        anim.SetTrigger("MoveCloser");
        yield return new WaitForSeconds(2.5f);
        for(int i=0; i < showItemsUI.Length; i++) { 
        showItemsUI[i].SetActive(true);
        }
    }
    IEnumerator PauseForReturnCamera()
    {
        yield return new WaitForSeconds(4.0f);
        ReturnCam();
        yield return new WaitForSeconds(1.0f);
        anim.SetTrigger("Wait");
    }
    public void ReturnCam()
    {
        anim.SetTrigger("OriginalScale");
        
    }
    public void FinalAnimationCamera()
    {
            anim.SetTrigger("ShowBridge");
            StartCoroutine(PauseForReturnCamera());
    }
    
}
