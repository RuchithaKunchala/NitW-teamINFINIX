using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class gridspace : MonoBehaviour
{
    public Button button;
    public Text buttonText;

    private GameController gameController;

    public void SetGameControllerReference(GameController controller)
    {
        gameController = controller;
    }

    //Gets Triggered every time a button is pressed, fills the button with X or O
    public void SetSpace()
    {
        buttonText.text = gameController.GetPlayerSide();
        button.interactable = false;
        gameController.EndTurn();
    }
}