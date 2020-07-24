using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AI_GridSpace : MonoBehaviour
{

    public Button button;
    public Text buttonText;

    private AI_Controller AIcontroller;


    public void SetGameControllerReference(AI_Controller controller)
    {
        AIcontroller = controller;
    }

    //Gets Triggered every time a button is pressed, fills the button with X or O
    public void SetSpace()
    {
        if (AIcontroller.playermove == true)
        {
            buttonText.text = AIcontroller.GetPlayerSide();
            button.interactable = false;
            AIcontroller.EndTurn();
        }
    }

}