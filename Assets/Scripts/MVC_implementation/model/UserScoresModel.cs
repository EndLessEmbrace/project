using System;
using UnityEngine;

public class UserScoresModel: MonoBehaviour
{
    [SerializeField]
    private float pointsAddedForCorrectAnswer;

    public float PointsAddedForCorrectAnswer
    {
        get { return pointsAddedForCorrectAnswer; }
        
    }
   // public event Action<float> OnScoreUpdated = delegate { };
  
    //public void Reset()
    //{
    //    pointsAddedForCorrectAnswer = 0;
    //}
  
}