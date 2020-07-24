using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text scoretext1;
    public Text scoretext2;

    int score1 = 0;
    int score2 = 0;

    //Sets the score of Player1 and Player2 after each game
    public void SetScore(int move, string player)
    {
        //Updates Player1's score after a win
        if (player == "P1")
        {
            if (move == 3)
                score1 += 30;
            else
                if (move == 4)
                score1 += 20;
            else
                if (move == 5)
                score1 += 10;

        }

        //Updates Player2's score after a win
        if (player == "P2")
        {
            if (move == 3)
                score2 += 30;
            else
                if (move == 4)
                score2 += 20;
            else
                if (move == 5)
                score2 += 10;
        }
        string scor1 = score1.ToString();
        string scor2 = score2.ToString();

        //Displays final score of Player1 and Player2
        scoretext1.text = "Player 1 " + "\n" + scor1;
        scoretext2.text = "Player 2 " + "\n" + scor2;
    }
}