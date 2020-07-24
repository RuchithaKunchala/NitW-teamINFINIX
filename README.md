# Tic-Tac-Toe Using Minimax
Implementation of the Minimax Algotithm to build an unbeatable TIC TAC TOE game powered by AI.

# About the Game
Tic-tac-toe, also known as noughts and crosses or Xs and Os is a game for two players, X and O, who take turns marking the spaces in a 3×3 grid. In order to win the game, a player must place three of their marks in a horizontal, vertical, or diagonal row. Players soon discover that the best play from both players leads to draw. This game is very well known as we all used to play in childhood. Some may call it " SOS " game.
# Basic Approach
Block the opponent and win!!
# What is Minimax ?
Mini-max algorithm is a recursive or backtracking algorithm which is used in decision-making and game theory. It provides an optimal move for the player assuming that opponent is also playing optimally. This Algorithm computes the minimax decision for the current state. This artificial intelligence algorithm applied in two player games, such as tic-tac-toe, checkers, chess etc. This games are known as zero-sum games, because in a mathematical representation: one player wins (+1) and other player loses (-1) or both of anyone not to win (0).
# Understanding the Algorithm
Two players: 
* **Maximizer**
* **Minimizer** <br/>
Maximizer tries to maximize the chances of winning and Minimizer tries to minimize the chances of Maximizer's winning.<br/>
If player X can win in one move, their best move is that winning move. If player O knows that one move will lead to the situation where player X can win in one move, while another move will lead to the situation where player X can, at best, draw, then player B's best move is the one leading to a draw. Later in the game, it's easy to see what the "best" move is. The Minimax algorithm helps find the best move, by working backwards from the end of the game. At each step it assumes that player X is trying to maximize the chances of X winning, while on the next turn player O is trying to minimize the chances of X winning (i.e., to maximize O's own chances of winning).<br/>
With respect to the implementation of this project, when it’s computer’s turn, with every move it adds +1 if win, -1 if lose and 0 if draw, and selects the move with maximum score.<br/>

*function minimax(count, depth, current_player)<br/>
&nbsp;&nbsp;&nbsp;&nbsp;if computer won*<br/>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;*return +1;<br/>
&nbsp;&nbsp;&nbsp;&nbsp;else if player won<br/>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;return -1;<br/>
&nbsp;&nbsp;&nbsp;&nbsp;else<br/>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;return 0;<br/>
&nbsp;&nbsp;&nbsp;&nbsp;if current_player is computer&nbsp;&nbsp;&nbsp;&nbsp;//Max block <br/>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;maxValue = −∞<br/>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;for each move left <br/>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;score = minimax(count, depth+1, player)<br/>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;maxValue = max(maxValue, score)<br/>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;return maxValue<br/>
&nbsp;&nbsp;&nbsp;&nbsp;else &nbsp;&nbsp;&nbsp;&nbsp;//player’s turn; min block<br/>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;minValue = ∞<br/>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;for each move left<br/>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;score = minimax(count, depth+1, computer)<br/>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;minValue = min(minValue, score)<br/>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;return min value<br/>*

Consider the following state of game (first grid) and assuming its computer’s turn (i.e. in this case, Computer is X), the best move according to the algorithm will be the first move (Move 1)<br/>
![Minimax](https://user-images.githubusercontent.com/60230072/88391922-bc0fbf00-cdd8-11ea-916f-1bab4998b297.png)

Let’s understand it further that why Move 1 is the best move:
* Given the state, moves 1, 2, and 3 are generated and function Minimax is called recursively further on those moves.
* As move 1 leads to computer’s win i.e. end of the game, giving +1(maximum score) as its score, on the other hand, move 2 and 3 generate 2.1, 2.2, 3.1 and 3.3 moves respectively and recursively call Minimax.
* Move 2.1 adds -1 to move 2’s score and move 3.1 adds -1 to move 3’s score as the opposite wins(minimum score).
* Moves 2.2 and 3.2 generate the last possible moves 2.3 and 3.3 respectively which adds +1 to moves 2 and 3 respectively (as computer wins in both cases).
* And since 2.1 and 2.2 are opposite player’s turn, it selects minimum score from (-1, +1) and same goes for 3.1 and 3.2. <br/>
**So, the ultimate scores for 1, 2, and 3 are +1, -1, and -1 respectively; therefore, the best move for the given state of game is move 1.** <br/><br/>
In simple words, a list of every possible moves and the ultimate score is created given a state of game like above; and the move with the maximum ultimate score is selected. <br/>
# Further Improvement In Minimax
The Minimax Algorithm can be further improved by adding **Alpha-Beta Pruning** to the code. It is an optimization technique almost same as the minimax algotithm. With this, the search time can be limited to the 'more promising' subtree, and a deeper search can be performed in the same time. But at the end both Minimax and Alpha-Beta pruning gives the same result.

# Extra features added to the Game
* Hints to make decisions on next move.
* Score board to add competition between players.
* Play with AI in 5 different levels.
* Alpha-Beta pruning is added along with the minimax code.
* Back button is provided to navigate through the game menu.


## Team Members
* Alisha Vinod
* Kunchala Ruchitha
* Tanvi Shekar
* Yashoda Devi Sree
<br/>
## Resources and Reading Materials<br/>
* https://learn.unity.com/tutorial/creating-a-tic-tac-toe-game-using-only-ui-components#5c7f8528edbc2a002053b4be<br/>
* https://www.geeksforgeeks.org/minimax-algorithm-in-game-theory-set-3-tic-tac-toe-ai-finding-optimal-move/<br/>
* https://www.youtube.com/watch?v=trKjYdBASyQ&vl=en<br/>
* https://towardsdatascience.com/how-a-chess-playing-computer-thinks-about-its-next-move-8f028bd0e7b1<br/>
* https://www.javatpoint.com/mini-max-algorithm-in-ai<br/>
* https://channel9.msdn.com/Blogs/cdndevs/Hosting-Unity-WebGL-games-on-Azure<br/>
* https://techcommunity.microsoft.com/t5/educator-developer-blog/hosting-your-unity-game-on-azure/ba-p/379490<br/>
* https://www.youtube.com/watch?v=7aP-sjFcVHY<br/>
* https://www.youtube.com/playlist?list=PLkzh1bySTmYB83ybePBUtsP4t0DAdspiw <br/>
