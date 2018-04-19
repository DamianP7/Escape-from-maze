using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escape_the_trolls
{
	class Game
	{
        // TODO: Map generator

		private static readonly int height = 23;
		private static readonly int width = 37;
		private string[] board =
			{   "#####################################",
				"# #       #       #     #         # #",
				"# # ##### # ### ##### ### ### ### # #",
				"#       #   # #     #     # # #   # #",
				"##### # ##### ##### ### # # # ##### #",
				"#   # #       #     # # # # #     # #",
				"# # ####### # # ##### ### # ##### # #",
				"# #       # # #   #     #     #   # #",
				"# ####### ### ### # ### ##### # ### #",
				"#     #   # #   # #   #     # #     #",
				"# ### ### # ### # ##### # # # #######",
				"#   #   # # #   #   #   # # #   #   #",
				"####### # # # ##### # ### # ### ### #",
				"#     # #     #   # #   # #   #     #",
				"# ### # ##### ### # ### ### ####### #",
				"# #   #     #     #   # # #       # #",
				"# # ##### # ### ##### # # ####### # #",
				"# #     # # # # #     #       # #   #",
				"# ##### # # # ### ##### ##### # #####",
				"# #   # # #     #     # #   #       #",
				"# # ### ### ### ##### ### # ##### # #",
				"#X#         #     #       #       # #",
				"#####################################" };

		public BoardState[][] boardState = new BoardState[height][];

		public static GameState gameState;

		Player player;

		List<Troll> Enemies = new List<Troll>();


		public Game()
		{
			gameState = GameState.Start;

			for (int i = 0; i < height; i++)
			{
				boardState[i] = new BoardState[width];
			}

			for (int i = 0; i < height; i++)
			{
				for (int j = 0; j < width; j++)
				{
					if (board[i][j] == '#')
						boardState[i][j] = BoardState.Wall;
					else if (board[i][j] == ' ')
						boardState[i][j] = BoardState.Empty;
					else if (board[i][j] == 'X')
						boardState[i][j] = BoardState.Exit;
				}
			}

			GenerateEnemies();

			player = new Player(ref boardState, height, width);

			gameState = GameState.PlayerTurn;
		}

		public void RunningGame()
		{
			bool end = false;

			do
			{
				PrintBoard();

				gameState = GameState.PlayerTurn;

				//Console.WriteLine(board[0].Length);

				player.Move(ref boardState, ref gameState);

				end = CheckGameState(GameState.PlayerTurn);

				if (!end)
				{
					foreach (Troll enemy in Enemies)
					{
						enemy.Move(ref boardState, ref gameState);
						end = CheckGameState(GameState.EnemyTurn);
					}
				}


			} while (!end);
		}

		private void PrintBoard()
		{
			Console.Clear();
			foreach (BoardState[] line in boardState)
			{
				foreach (BoardState point in line)
				{
					Console.Write(' ');
					switch (point)
					{
						case (BoardState.Empty):
							Console.Write(' ');
							break;
						case (BoardState.Exit):
							Console.Write('X');
							break;
						case (BoardState.PlayerU):
							if (gameState != GameState.GameOver)
								Console.Write('^');
							else
								Console.Write('%');
							break;
						case (BoardState.PlayerD):
							if (gameState != GameState.GameOver)
								Console.Write('v');
							else
								Console.Write('%');
							break;
						case (BoardState.PlayerL):
							if (gameState != GameState.GameOver)
								Console.Write('<');
							else
								Console.Write('%');
							break;
						case (BoardState.PlayerR):
							if (gameState != GameState.GameOver)
								Console.Write('>');
							else
								Console.Write('%');
							break;
						case (BoardState.Troll):
							Console.Write('H');
							break;
						case (BoardState.Wall):
							Console.Write('#');
							break;
					}
					Console.Write(' ');
				}
				Console.WriteLine();
			}
		}

		private void PrintCongratulations()
		{
			Console.Clear();
			for (int i = 0; i < height / 2; i++)
				Console.WriteLine();
			Console.WriteLine("\t\t\t\t!!! Congratulations !!!");
		}

		private void PrintGameOver()
		{
			PrintBoard();
			Console.ReadKey();
			Console.Clear();
			for (int i = 0; i < height / 2; i++)
				Console.WriteLine();
			Console.WriteLine("\t\t\t\t====> GAME OVER <====");
		}

		private void GenerateEnemies()
		{
			Random ran = new Random();
			int number = ran.Next(1, 4);

			for (int i = 0; i < number; i++)
			{
				Enemies.Add(new Troll(ref boardState, height, width));
			}
		}

		private bool CheckGameState(GameState whosTurn)
		{
			if (whosTurn == GameState.PlayerTurn)
			{
				if (gameState == GameState.Running)
				{
					gameState = GameState.EnemyTurn;
				}
				else if (gameState == GameState.Win)
				{
					PrintCongratulations();
					return true;
				}
				else
				{
					PrintGameOver();
					return true;
				}
				return false;
			}
			else if (whosTurn == GameState.EnemyTurn)
			{
				if (gameState == GameState.Running)
				{
					gameState = GameState.EnemyTurn;
				}
				else if (gameState == GameState.Win)
				{
					PrintCongratulations();
					return true;
				}
				else if (gameState == GameState.GameOver)
				{
					PrintGameOver();
					return true;
				}
				return false;
			}
			return false;
		}
	}
}
