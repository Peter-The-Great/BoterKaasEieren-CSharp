namespace Simple
{
    using System;

    namespace TicTacToe
    {
        class Program
        {
            static char[] board = { '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            static int playerTurn = 1; // 1 for Player 1, 2 for Player 2

            static void Main(string[] args)
            {
                bool gameRunning = true;

                while (gameRunning)
                {
                    DrawBoard();
                    int choice = GetPlayerChoice();
                    UpdateBoard(choice);
                    if (CheckForWin())
                    {
                        DrawBoard();
                        Console.WriteLine($"Player {playerTurn} wins!");
                        gameRunning = false;
                    }
                    else if (IsBoardFull())
                    {
                        DrawBoard();
                        Console.WriteLine("It's a draw!");
                        gameRunning = false;
                    }
                    else
                    {
                        playerTurn = (playerTurn == 1) ? 2 : 1;
                    }
                }
            }

            static void DrawBoard()
            {
                Console.Clear();
                Console.WriteLine($" {board[0]} | {board[1]} | {board[2]} ");
                Console.WriteLine("---+---+---");
                Console.WriteLine($" {board[3]} | {board[4]} | {board[5]} ");
                Console.WriteLine("---+---+---");
                Console.WriteLine($" {board[6]} | {board[7]} | {board[8]} ");
            }

            static int GetPlayerChoice()
            {
                int choice = -1;
                while (choice < 1 || choice > 9 || board[choice - 1] == 'X' || board[choice - 1] == 'O')
                {
                    Console.Write($"Player {playerTurn}, choose a position (1-9): ");
                    int.TryParse(Console.ReadLine(), out choice);
                }
                return choice;
            }

            static void UpdateBoard(int choice)
            {
                char symbol = (playerTurn == 1) ? 'X' : 'O';
                board[choice - 1] = symbol;
            }

            static bool CheckForWin()
            {
                // Check for winning combinations: rows, columns, diagonals
                if ((board[0] == board[1] && board[1] == board[2]) ||
                    (board[3] == board[4] && board[4] == board[5]) ||
                    (board[6] == board[7] && board[7] == board[8]) ||
                    (board[0] == board[3] && board[3] == board[6]) ||
                    (board[1] == board[4] && board[4] == board[7]) ||
                    (board[2] == board[5] && board[5] == board[8]) ||
                    (board[0] == board[4] && board[4] == board[8]) ||
                    (board[2] == board[4] && board[4] == board[6]))
                {
                    return true;
                }
                return false;
            }

            static bool IsBoardFull()
            {
                foreach (char position in board)
                {
                    if (position != 'X' && position != 'O')
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }
}
