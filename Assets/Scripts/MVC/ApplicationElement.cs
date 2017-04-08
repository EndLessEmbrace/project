using UnityEngine;

public class ApplicationElement:MonoBehaviour
{
    public Application app { get { return GameObject.FindObjectOfType<Application>(); } }
}