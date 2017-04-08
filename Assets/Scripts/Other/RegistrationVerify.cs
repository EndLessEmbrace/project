using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class RegistrationVerify : MonoBehaviour
{
   
    public List<InputField> inputFieldList = new List<InputField>();
    [SerializeField]
    private GameObject PanelManual;
  
    public void CheckingFields()
    {
        bool variable = true;
       
        for (int i = 0; i < inputFieldList.Count; i++)
        {
            if (inputFieldList[i].text.Length == 0)
            {
                variable = false; 
            }
        }
        if (variable)
        {
            PanelManual.SetActive(true);
        }
    }
}
