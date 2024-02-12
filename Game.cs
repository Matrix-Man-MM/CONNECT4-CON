using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CONNECT4_CON
{
    public class Game
    {
        const int NUM_ROWS = 6;
        const int NUM_COLS = 7;

        const char PLAYER_ONE = 'O';
        const char PLAYER_TWO = '@';

        char[,] game_board = new char[NUM_ROWS, NUM_COLS];

        ConsoleColor original_foreground_color = Console.ForegroundColor;

        public Game()
        {
            for (int i = 0; i < NUM_ROWS; i++)
                for (int j = 0; j < NUM_COLS; j++)
                    game_board[i, j] = '.';
        }

        public void PlayGame()
        {
            bool is_player_one_turn = true;
            bool has_game_been_won = false;

            while (!IsBoardFull() && !has_game_been_won)
            {
                Console.Clear();
                PrintBoard();
                has_game_been_won = PlayerMove(is_player_one_turn ? PLAYER_ONE : PLAYER_TWO);

                if (!has_game_been_won)
                    is_player_one_turn = !is_player_one_turn;
            }

            Console.Clear();
            PrintBoard();

            if (has_game_been_won)
                Console.WriteLine("Player {0} wins!", (is_player_one_turn ? "1" : "2"));
            else
                Console.WriteLine("Game is a tie!");
        }

        private bool PlayerMove(char player)
        {
            Console.Write("Player {0}, enter a column number (0 to {1}): ", player, NUM_COLS - 1);
            int col;

            while (!int.TryParse(Console.ReadLine(), out col) || col < 0 || col >= NUM_COLS || !IsColAvailable(col))
                Console.WriteLine("INVALID COLUMN! Try Again!");

            int row = DropPiece(col, player);

            return CheckForWin(player, row, col);
        }

        private bool IsColAvailable(int col)
        {
            return game_board[0, col] == '.';
        }

        private int DropPiece(int col, char player)
        {
            for (int i = NUM_ROWS - 1; i >= 0; i--)
                if (game_board[i, col] == '.')
                {
                    game_board[i, col] = player;
                    return i;
                }

            return -1;
        }

        private bool CheckForWin(char player, int last_row, int last_col)
        {
            return CheckDir(player, last_row, last_col, 1, 0) || CheckDir(player, last_row, last_col, 0, 1) || CheckDir(player, last_row, last_col, 1, 1) || CheckDir(player, last_row, last_col, 1, -1);
        }

        private bool CheckDir(char player, int row, int col, int d_row, int d_col)
        {
            int cnt = 1;
            int r, c;

            r = row + d_row;
            c = col + d_col;

            while (r >= 0 && r < NUM_ROWS && c >= 0 && c < NUM_COLS && game_board[r, c] == player)
            {
                cnt++;
                r += d_row;
                c += d_col;
            }

            r = row - d_row;
            c = col - d_col;

            while (r >= 0 && r < NUM_ROWS && c >= 0 && c < NUM_COLS && game_board[r,c] == player)
            {
                cnt++;
                r -= d_row;
                c -= d_col;
            }

            return cnt >= 4;
        }

        private bool IsBoardFull()
        {
            for (int i = 0; i < NUM_COLS; i++)
                if (game_board[0, i] == '.')
                    return false;

            return true;
        }

        private void PrintBoard()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;

            for (int i = 0; i < NUM_ROWS; i++)
            {
                for (int j = 0; j < NUM_COLS; j++)
                {
                    if (game_board[i, j] == PLAYER_ONE)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(game_board[i, j]);
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    else if (game_board[i, j] == PLAYER_TWO)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write(game_board[i, j]);
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    else
                        Console.Write(game_board[i, j]);
                }
                    

                Console.WriteLine();
            }

            Console.ForegroundColor = original_foreground_color;
        }
    }
}
