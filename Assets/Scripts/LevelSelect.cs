using UnityEngine;


//Sets difficulty level parameter to implement the minimax function
public class LevelSelect : MonoBehaviour
{
    public void GetLevel(int AIlevel)
    {
        PlayerPrefs.SetInt("level", AIlevel);
    }
}