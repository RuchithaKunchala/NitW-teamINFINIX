using UnityEngine;
using UnityEngine.UI;
//Class for X and O selection button
[System.Serializable]
public class Player3
{
    public Image panel;
    public Text text;
    public Button button;
}
//Class for X and O selection button color
[System.Serializable]
public class PlayerColor3
{
    public Color panelColor;
    public Color textColor;
}

public class AI_Controller : MonoBehaviour
{
    public Text[] buttonList;
    public Text[] list;
    public GameObject gameOverPanel;
    public Text gameOverText;
    public GameObject restartButton;
    public Player3 playerX;
    public Player3 playerO;
    public PlayerColor3 activePlayerColor;
    public PlayerColor3 inactivePlayerColor;
    public GameObject startInfo;

    public GameObject game;
    public GameObject manWon;
    public GameObject manLost;

    public GameObject[] Strike;

    private string playerSide;
    private string robot;
    private string winner;
    public bool playermove;
    private int moveCount;
    private int count;

    private int level;

    //Sets all initial conditions

    void Awake()
    {
        level = PlayerPrefs.GetInt("level");

        SetGameControllerReferenceOnButtons();
        gameOverPanel.SetActive(false);
        moveCount = 0;
        restartButton.SetActive(false);
        playermove = true;
        game.SetActive(true); //animations
        manWon.SetActive(false); //animations
        manLost.SetActive(false); //animations

        for (int i = 0; i < 8; i++)
        {
            Strike[i].SetActive(false);
        }
    }
    //Finds optimal move for AI based on difficulty
    void Update()
    {
        if (playermove == false)
        {
            list = buttonList;
            int move = 0;
            int bestScore = -1000;
            for (int i = 0; i < 9; i++)
            {
                if (list[i].text == "")
                {
                    list[i].text = GetrobotSide();
                    int score = minimax(list, 0, false, -1000, 1000);
                    list[i].text = "";
                    if (score > bestScore)
                    {
                        bestScore = score;
                        move = i;
                    }
                }
            }
            buttonList[move].text = robot;
            buttonList[move].GetComponentInParent<Button>().interactable = false;
            EndTurn();
        }
    }
    //Finds optimal move for AI based on difficulty 
    //Minimax algorithm includes alpha beta pruning for optimization
    int minimax(Text[] blist, int depth, bool isMaximizing, int alpha, int beta)
    {

        string result = checkwinner(blist);
        //Depth of search tree for minimax algorithm is determined by the level of difficulty
        if (result != null || depth == level)
        {
            if (result == GetrobotSide())
            {
                return 10 - depth;
            }
            else if (result == GetPlayerSide())
            {
                return depth - 10;
            }
            else
                return 0;
        }
        //Checks for maximizing player (Player)
        if (isMaximizing)
        {
            int bestScore = -1000;
            for (int i = 0; i < 9; i++)
            {
                if (blist[i].text == "")
                {
                    blist[i].text = GetrobotSide();
                    int score = minimax(blist, depth + 1, false, alpha, beta); //depth is incremented
                    blist[i].text = "";
                    if (score > bestScore)
                    {
                        bestScore = score;
                    }
                    if (bestScore > alpha)
                    {
                        alpha = bestScore;
                    }
                    if (beta <= alpha)
                    {
                        break;
                    }
                }
            }
            return bestScore;
        }
        //Checks for maximizing player (Computer)
        else
        {
            int bestScore = 1000;
            for (int i = 0; i < 9; i++)
            {
                if (blist[i].text == "")
                {
                    blist[i].text = GetPlayerSide();
                    int score = minimax(blist, depth + 1, true, alpha, beta); //depth is incremented
                    blist[i].text = "";
                    if (score < bestScore)
                    {
                        bestScore = score;
                    }
                    if (bestScore < beta)
                    {
                        beta = bestScore;
                    }
                    if (beta <= alpha)
                    {
                        break;
                    }
                }
            }
            return bestScore;
        }
    }
    void SetGameControllerReferenceOnButtons()
    {
        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].GetComponentInParent<AI_GridSpace>().SetGameControllerReference(this);
        }
    }
    //Determines whether the starting player has chosen X or O
    public void SetStartingSide(string startingSide)
    {
        playerSide = startingSide;
        //Activates X button
        if (playerSide == "X")
        {
            robot = "O";
            SetPlayerColors(playerX, playerO);
        }
        //Activates O button
        else
        {
            robot = "X";
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
        game.SetActive(true); // animations
        manWon.SetActive(false); // animations
        manLost.SetActive(false); // animations
        for (int i = 0; i < 8; i++)
        {
            Strike[i].SetActive(false);
        }
    }
    //returns player's side (X or O)
    public string GetPlayerSide()
    {
        return playerSide;
    }
    //returns robot's side (X or O)
    public string GetrobotSide()
    {
        return robot;
    }
    //Checks the winning conditions
    public void EndTurn()
    {
        moveCount++;
        //Horizontal row 1
        if (buttonList[0].text == playerSide && buttonList[1].text == playerSide && buttonList[2].text == playerSide)
        {
            for (int i = 3; i < 9; i++)
            {
                buttonList[i].text = "";
            }
            Strike[0].SetActive(true);
            GameOver(playerSide);
        }
        //Horizontal row 2
        else if (buttonList[3].text == playerSide && buttonList[4].text == playerSide && buttonList[5].text == playerSide)
        {
            for (int i = 0; i < 3; i++)
            {
                buttonList[i].text = "";
            }
            for (int i = 6; i < 9; i++)
            {
                buttonList[i].text = "";
            }
            Strike[1].SetActive(true);
            GameOver(playerSide);
        }
        //Horizontal row 3
        else if (buttonList[6].text == playerSide && buttonList[7].text == playerSide && buttonList[8].text == playerSide)
        {
            for (int i = 0; i < 6; i++)
            {
                buttonList[i].text = "";
            }
            Strike[2].SetActive(true);
            GameOver(playerSide);
        }
        //Vertical row 1
        else if (buttonList[0].text == playerSide && buttonList[3].text == playerSide && buttonList[6].text == playerSide)
        {
            for (int i = 1; i < 3; i++)
            {
                buttonList[i].text = "";
            }
            for (int i = 4; i < 6; i++)
            {
                buttonList[i].text = "";
            }
            for (int i = 7; i < 9; i++)
            {
                buttonList[i].text = "";
            }
            Strike[3].SetActive(true);
            GameOver(playerSide);
        }
        //Vertical row 2
        else if (buttonList[1].text == playerSide && buttonList[4].text == playerSide && buttonList[7].text == playerSide)
        {
            buttonList[0].text = "";
            buttonList[2].text = "";
            buttonList[3].text = "";
            buttonList[5].text = "";
            buttonList[6].text = "";
            buttonList[8].text = "";

            Strike[4].SetActive(true);
            GameOver(playerSide);
        }
        //Vertical row 3
        else if (buttonList[2].text == playerSide && buttonList[5].text == playerSide && buttonList[8].text == playerSide)
        {
            for (int i = 0; i < 2; i++)
            {
                buttonList[i].text = "";
            }
            for (int i = 3; i < 5; i++)
            {
                buttonList[i].text = "";
            }
            for (int i = 6; i < 8; i++)
            {
                buttonList[i].text = "";
            }
            Strike[5].SetActive(true);
            GameOver(playerSide);
        }
        //Diagonal1
        else if (buttonList[0].text == playerSide && buttonList[4].text == playerSide && buttonList[8].text == playerSide)
        {
            for (int i = 1; i < 4; i++)
            {
                buttonList[i].text = "";
            }
            for (int i = 5; i < 8; i++)
            {
                buttonList[i].text = "";
            }
            Strike[6].SetActive(true);
            GameOver(playerSide);
        }
        //Diagonal2
        else if (buttonList[2].text == playerSide && buttonList[4].text == playerSide && buttonList[6].text == playerSide)
        {
            for (int i = 0; i < 2; i++)
            {
                buttonList[i].text = "";
            }
            for (int i = 7; i < 9; i++)
            {
                buttonList[i].text = "";
            }
            buttonList[3].text = "";
            buttonList[5].text = "";

            Strike[7].SetActive(true);
            GameOver(playerSide);
        }
        else if (buttonList[0].text == robot && buttonList[1].text == robot && buttonList[2].text == robot)
        {
            for (int i = 0; i < 6; i++)
            {
                buttonList[i].text = "";
            }

            Strike[0].SetActive(true);
            GameOver(robot);
        }
        else if (buttonList[3].text == robot && buttonList[4].text == robot && buttonList[5].text == robot)
        {
            for (int i = 0; i < 3; i++)
            {
                buttonList[i].text = "";
            }
            for (int i = 6; i < 9; i++)
            {
                buttonList[i].text = "";
            }

            Strike[1].SetActive(true);
            GameOver(robot);
        }
        else if (buttonList[6].text == robot && buttonList[7].text == robot && buttonList[8].text == robot)
        {
            for (int i = 0; i < 6; i++)
            {
                buttonList[i].text = "";
            }

            Strike[2].SetActive(true);
            GameOver(robot);
        }
        else if (buttonList[0].text == robot && buttonList[3].text == robot && buttonList[6].text == robot)
        {
            for (int i = 1; i < 3; i++)
            {
                buttonList[i].text = "";
            }
            for (int i = 4; i < 6; i++)
            {
                buttonList[i].text = "";
            }
            for (int i = 7; i < 9; i++)
            {
                buttonList[i].text = "";
            }

            Strike[3].SetActive(true);
            GameOver(robot);
        }
        else if (buttonList[1].text == robot && buttonList[4].text == robot && buttonList[7].text == robot)
        {
            buttonList[0].text = "";
            buttonList[2].text = "";
            buttonList[3].text = "";
            buttonList[5].text = "";
            buttonList[6].text = "";
            buttonList[8].text = "";

            Strike[4].SetActive(true);
            GameOver(robot);
        }
        else if (buttonList[2].text == robot && buttonList[5].text == robot && buttonList[8].text == robot)
        {
            for (int i = 0; i < 2; i++)
            {
                buttonList[i].text = "";
            }
            for (int i = 3; i < 5; i++)
            {
                buttonList[i].text = "";
            }
            for (int i = 6; i < 8; i++)
            {
                buttonList[i].text = "";
            }

            Strike[5].SetActive(true);
            GameOver(robot);
        }
        else if (buttonList[0].text == robot && buttonList[4].text == robot && buttonList[8].text == robot)
        {
            for (int i = 1; i < 4; i++)
            {
                buttonList[i].text = "";
            }
            for (int i = 5; i < 8; i++)
            {
                buttonList[i].text = "";
            }
            Strike[6].SetActive(true);
            GameOver(robot);
        }
        else if (buttonList[2].text == robot && buttonList[4].text == robot && buttonList[6].text == robot)
        {
            for (int i = 0; i < 2; i++)
            {
                buttonList[i].text = "";
            }
            for (int i = 7; i < 9; i++)
            {
                buttonList[i].text = "";
            }
            buttonList[3].text = "";
            buttonList[5].text = "";

            Strike[7].SetActive(true);
            GameOver(robot);
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
    public string checkwinner(Text[] list)
    {
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
    //Changes playersides from X to O and O to X after each turn
    void ChangeSides()
    {
        playermove = (playermove == true) ? false : true;
        if (playermove == true)
        {
            if (playerSide == "X")
                SetPlayerColors(playerX, playerO);
            else
                SetPlayerColors(playerO, playerX);

        }
        else
        {
            if (robot == "O")
                SetPlayerColors(playerO, playerX);
            else
                SetPlayerColors(playerX, playerO);
        }
    }
    //Sets X and O buttons to active and inactive with respct to who is playing
    void SetPlayerColors(Player3 newPlayer, Player3 oldPlayer)
    {
        newPlayer.panel.color = activePlayerColor.panelColor;
        newPlayer.text.color = activePlayerColor.textColor;
        oldPlayer.panel.color = inactivePlayerColor.panelColor;
        oldPlayer.text.color = inactivePlayerColor.textColor;
    }

    //Triggers animations and sends the parameter of winning player to update the scoreboard
    void GameOver(string winningPlayer)
    {
        SetBoardInteractable(false);
        if (winningPlayer == "draw")
        {
            SetGameOverText("DRAW !");
            SetPlayerColorsInactive();
            game.SetActive(true); //animations
            manWon.SetActive(false); //animations
            manLost.SetActive(false); //animations
        }
        else
        {
            if (winningPlayer == "X")
            {
                SetPlayerColorsInactive();
                game.SetActive(false); //animations
                manWon.SetActive(true); //animations
                manLost.SetActive(false); //animations
                SetGameOverText("Human Wins!");
            }
            else if (winningPlayer == "O")
            {
                SetPlayerColorsInactive();
                game.SetActive(false); //animations
                manWon.SetActive(false); //animations
                manLost.SetActive(true); //animations
                SetGameOverText("Computer Wins!");
            }
        }
        restartButton.SetActive(true);
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
        game.SetActive(true); //animations
        manWon.SetActive(false); //animations
        manLost.SetActive(false); //animations
        gameOverPanel.SetActive(false);
        restartButton.SetActive(false);
        SetPlayerButtons(true);
        SetPlayerColorsInactive();
        startInfo.SetActive(true);
        playermove = true;

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
