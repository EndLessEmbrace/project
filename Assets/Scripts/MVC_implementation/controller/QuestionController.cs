using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestionController:MonoBehaviour
{
    public QuestionModel[] questionModel;
    public QuestionModel GetCurrentQuestionData()
    {
        return questionModel[0];
    }
  
}