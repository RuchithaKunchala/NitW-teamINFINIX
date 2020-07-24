using UnityEngine;
using UnityEngine.SceneManagement;

// Manages the scene transitions from current to the received scene name taken in as a string parameter 
public class SceneManager_UI : MonoBehaviour
{
    public void MoveToScene(string scenename)
    {
        SceneManager.LoadScene(scenename);
    }
}