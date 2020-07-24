using UnityEngine;
using UnityEngine.UI;

//Class for X and O selection button
[System.Serializable]
public class Player
{
    public Image panel;
    public Text text;
    public Button button;
}

//Class for X and O selection button color
[System.Serializable]
public class PlayerColor
{
    public Color panelColor;
    public Color textColor;
}

public class GameController : MonoBehaviour
{
    public Text[] buttonList;
    public GameObject gameOverPanel;
    public GameObject hint;
    public GameObject hintdigit;
    public Text gameOverText;
    public Text hintText;
    public GameObject restartButton;
    public Player playerX;
    public Player playerO;
    public PlayerColor activePlayerColor;
    public PlayerColor inactivePlayerColor;
    public GameObject startInfo;

    public Score scoreboard;

    private string playerSide;
    private string player1;
    private string player2;
    private int moveCount;
    private string winner;
    private int count;
    public int mov1;
    public int mov2;

    public GameObject boygame;
    public GameObject boyWon;
    public GameObject boyLost;

    public GameObject[] Strike;

    //Sets all initial conditions
    void Awake()
    {
        SetGameControllerReferenceOnButtons();
        gameOverPanel.SetActive(false);
        moveCount = 0;
        restartButton.SetActive(false);
        boygame.SetActive(true); //animations
        boyWon.SetActive(false); //animations
        boyLost.SetActive(false); //animations
        hint.SetActive(false);

        for (int i = 0; i < 8; i++)
        {
            Strike[i].SetActive(false);
        }
    }

    void SetGameControllerReferenceOnButtons()
    {
        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].GetComponentInParent<gridspace>().SetGameControllerReference(this);
        }
    }

    //Determines whether the starting player has chosen X or O
    public void SetStartingSide(string startingSide)
    {
        playerSide = startingSide;
        //Activates X button 
        if (playerSide == "X")
        {
            SetPlayerColors(playerX, playerO);
            player1 = playerSide;
            player2 = "O";
        }
        //Activates O button
        else
        {
            player1 = playerSide;
            player2 = "X";
            SetPlayerColors(playerO, playerX);
        }
        StartGame();
    }

    //Sets all initial conditions at the start of each game
    void StartGame()
    {
        SetBoardInteractable(true);
        SetPlayerButtons(false);
        startInfo.SetActive(false);
        hint.SetActive(true);
        hintdigit.SetActive(true);

        boygame.SetActive(true); // animations
        boyWon.SetActive(false); // animations
        boyLost.SetActive(false); // animations

        for (int i = 0; i < 8; i++)
        {
            Strike[i].SetActive(false);
        }
    }

    //returns current player's side (X or O)
    public string GetPlayerSide()
    {
        return playerSide;
    }

    //Function to give hints to the players using minimax algorithm
    //Returns optimal position
    public void enablehint()
    {
        Text[] list;
        list = buttonList;
        int move = 0;
        int bestScore = -1000;
        for (int i = 0; i < 9; i++)
        {
            if (list[i].text == "")
            {
                list[i].text = GetPlayerSide();
                int score = minimax(list, 0, false);
                list[i].text = "";
                if (score > bestScore)
                {
                    bestScore = score;
                    move = i;
                }
            }
        }
        if (move == 0)
        {
            sethintdigit("1,1");
        }
        else if (move == 1)
        {
            sethintdigit("1,2");
        }
        else if (move == 2)
        {
            sethintdigit("1,3");
        }
        else if (move == 3)
        {
            sethintdigit("2,1");
        }
        else if (move == 4)
        {
            sethintdigit("2,2");
        }
        else if (move == 5)
        {
            sethintdigit("2,3");
        }
        else if (move == 6)
        {
            sethintdigit("3,1");
        }
        else if (move == 7)
        {
            sethintdigit("3,2");
        }
        else if (move == 8)
        {
            sethintdigit("3,3");
        }
    }

    //Implementation of minimax algorithm to find optimal position for hints
    int minimax(Text[] blist, int depth, bool isMaximizing)
    {
        string other;
        if (playerSide == "X")
        {
            other = "O";
        }
        else
        {
            other = "X";
        }

        string result = checkwinner(blist);
        if (result != null)
        {
            if (result == GetPlayerSide())
            {
                return 10 - depth;
            }
            else if (result == other)
            {
                return depth - 10;
            }
            return 0;
        }
        if (isMaximizing)
        {
            int bestScore = -1000;
            for (int i = 0; i < 9; i++)
            {
                if (blist[i].text == "")
                {
                    blist[i].text = GetPlayerSide();
                    int score = minimax(blist, depth + 1, false);
                    blist[i].text = "";
                    if (score > bestScore)
                    {
                        bestScore = score;
                    }

                }
            }
            return bestScore;
        }
        else
        {
            int bestScore = 1000;
            for (int i = 0; i < 9; i++)
            {
                if (blist[i].text == "")
                {
                    blist[i].text = other;
                    int score = minimax(blist, depth + 1, true);
                    blist[i].text = "";
                    if (score < bestScore)
                    {
                        bestScore = score;
                    }
                }
            }
            return bestScore;
        }
    }

    //Returns if X or O won the game or draw
    public string checkwinner(Text[] list)
    {
        string robot;
        if (playerSide == "X")
        {
            robot = "O";
        }
        else
        {
            robot = "X";
        }
        count = 0;
        winner = null;
        //Horizontal row 1
        if (list[0].text == playerSide && list[1].text == playerSide && list[2].text == playerSide)
        {
            winner = playerSide;
        }
        //Horizontal row 2
        else if (list[3].text == playerSide && list[4].text == playerSide && list[5].text == playerSide)
        {
            winner = playerSide;
        }
        //Horizontal row 3
        else if (list[6].text == playerSide && list[7].text == playerSide && list[8].text == playerSide)
        {
            winner = playerSide;
        }
        //Vertical row 1
        else if (list[0].text == playerSide && list[3].text == playerSide && list[6].text == playerSide)
        {
            winner = playerSide;
        }
        //Vertical row 2
        else if (list[1].text == playerSide && list[4].text == playerSide && list[7].text == playerSide)
        {
            winner = playerSide;
        }
        //Vertical row 3
        else if (list[2].text == playerSide && list[5].text == playerSide && list[8].text == playerSide)
        {
            winner = playerSide;
        }
        //Diagonal 1
        else if (list[0].text == playerSide && list[4].text == playerSide && list[8].text == playerSide)
        {
            winner = playerSide;
        }
        //Diagonal 2
        else if (list[2].text == playerSide && list[4].text == playerSide && list[6].text == playerSide)
        {
            winner = playerSide;
        }
        else if (list[0].text == robot && list[1].text == robot && list[2].text == robot)
        {

            winner = robot;
        }
        else if (list[3].text == robot && list[4].text == robot && list[5].text == robot)
        {

            winner = robot;
        }
        else if (list[6].text == robot && list[7].text == robot && list[8].text == robot)
        {

            winner = robot;
        }
        else if (list[0].text == robot && list[3].text == robot && list[6].text == robot)
        {

            winner = robot;
        }
        else if (list[1].text == robot && list[4].text == robot && list[7].text == robot)
        {

            winner = robot;
        }
        else if (list[2].text == robot && list[5].text == robot && list[8].text == robot)
        {

            winner = robot;
        }
        else if (list[0].text == robot && list[4].text == robot && list[8].text == robot)
        {

            winner = robot;
        }
        else if (list[2].text == robot && list[4].text == robot && list[6].text == robot)
        {

            winner = robot;
        }
        else
        {
            for (int i = 0; i < 9; i++)
            {
                if (list[i].text == "")
                {
                    count++;
                }
            }
            if (count == 0)
            {
                return "draw";
            }
        }
        return winner;
    }

    //Sets hint position
    void sethintdigit(string pos)
    {
        hintdigit.SetActive(true);
        hintText.text = pos;
    }

    //Checks the winning conditions
    public void EndTurn()
    {
        moveCount++;
        if (player1 == playerSide)
            mov1++;
        else
            if (player2 == playerSide)
            mov2++;
        //Horizontal row 1
        if (buttonList[0].text == playerSide && buttonList[1].text == playerSide && buttonList[2].text == playerSide)
        {
            Strike[0].SetActive(true);
            GameOver(playerSide);
        }
        //Horizontal row 2
        else if (buttonList[3].text == playerSide && buttonList[4].text == playerSide && buttonList[5].text == playerSide) //H2
        {
            Strike[1].SetActive(true);
            GameOver(playerSide);
        }
        //Horizontal row 3
        else if (buttonList[6].text == playerSide && buttonList[7].text == playerSide && buttonList[8].text == playerSide) //H3
        {
            Strike[2].SetActive(true);
            GameOver(playerSide);
        }
        //Vertical row 1
        else if (buttonList[0].text == playerSide && buttonList[3].text == playerSide && buttonList[6].text == playerSide) //V1
        {
            Strike[3].SetActive(true);
            GameOver(playerSide);
        }
        //Vertical row 2
        else if (buttonList[1].text == playerSide && buttonList[4].text == playerSide && buttonList[7].text == playerSide) //V2
        {
            Strike[4].SetActive(true);
            GameOver(playerSide);
        }
        //Vertical row 3
        else if (buttonList[2].text == playerSide && buttonList[5].text == playerSide && buttonList[8].text == playerSide) //V3
        {
            Strike[5].SetActive(true);
            GameOver(playerSide);
        }
        //Diagonal1
        else if (buttonList[0].text == playerSide && buttonList[4].text == playerSide && buttonList[8].text == playerSide) //D1
        {
            Strike[6].SetActive(true);
            GameOver(playerSide);
        }
        //Diagonal2
        else if (buttonList[2].text == playerSide && buttonList[4].text == playerSide && buttonList[6].text == playerSide) //D2
        {
            Strike[7].SetActive(true);
            GameOver(playerSide);
        }
        else if (moveCount >= 9)
        {
            GameOver("draw");
        }
        else
        {
            ChangeSides();
        }
    }

    //Changes playersides from X to O and O to X after each turn
    void ChangeSides()
    {
        playerSide = (playerSide == "X") ? "O" : "X";
        if (playerSide == "X")
        {
            SetPlayerColors(playerX, playerO);
        }
        else
        {
            SetPlayerColors(playerO, playerX);
        }
    }

    //Sets X and O buttons to active and inactive with respct to who is playing
    void SetPlayerColors(Player newPlayer, Player oldPlayer)
    {
        newPlayer.panel.color = activePlayerColor.panelColor;
        newPlayer.text.color = activePlayerColor.textColor;
        oldPlayer.panel.color = inactivePlayerColor.panelColor;
        oldPlayer.text.color = inactivePlayerColor.textColor;
    }

    //Triggers animations and sends the parameter of winning player to update the scoreboard
    void GameOver(string winningPlayer)
    {
        if (winningPlayer == player1)
        {
            scoreboard.SetScore(mov1, "P1"); //updates player1 score
            boygame.SetActive(false); //animations
            boyWon.SetActive(true); //animations
            boyLost.SetActive(false); //animations
        }
        else if (winningPlayer == player2)
        {
            scoreboard.SetScore(mov2, "P2"); //updates player2 score
            boygame.SetActive(false); //animations
            boyWon.SetActive(false); //animations
            boyLost.SetActive(true); //animations
        }
        SetBoardInteractable(false);
        if (winningPlayer == "draw")
        {
            SetGameOverText("DRAW !");
            SetPlayerColorsInactive();
            boygame.SetActive(true); //animations
            boyWon.SetActive(false); //animations
            boyLost.SetActive(false); //animations
        }
        else
        {
            SetGameOverText(winningPlayer + " Wins!");
        }
        restartButton.SetActive(true);
        hint.SetActive(false);
        hintdigit.SetActive(false);
    }

    //Sets Game Over text
    void SetGameOverText(string value)
    {
        gameOverPanel.SetActive(true);
        gameOverText.text = value;
    }

    //Function to Restart the game and set all initial conditions
    public void RestartGame()
    {
        moveCount = 0;
        mov1 = 0;
        mov2 = 0;
        boygame.SetActive(true); //animations
        boyWon.SetActive(false); //animations
        boyLost.SetActive(false); //animations
        gameOverPanel.SetActive(false);
        restartButton.SetActive(false);
        SetPlayerButtons(true);
        SetPlayerColorsInactive();
        hintdigit.SetActive(false);
        startInfo.SetActive(true);
        for (int i = 0; i < 8; i++)
        {
            Strike[i].SetActive(false);
        }

        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].text = "";
        }
    }
    //Sets all buttons to interactable or non interactable depending on toggle parameter
    void SetBoardInteractable(bool toggle)
    {
        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].GetComponentInParent<Button>().interactable = toggle;
        }
    }
    //Sets X and O selection buttons to interactable or non interactable depending on the state of the game
    void SetPlayerButtons(bool toggle)
    {
        playerX.button.interactable = toggle;
        playerO.button.interactable = toggle;
    }
    //Sets X and O selection buttons colors depending on the state of the game
    void SetPlayerColorsInactive()
    {
        playerX.panel.color = inactivePlayerColor.panelColor;
        playerX.text.color = inactivePlayerColor.textColor;
        playerO.panel.color = inactivePlayerColor.panelColor;
        playerO.text.color = inactivePlayerColor.textColor;
    }
}