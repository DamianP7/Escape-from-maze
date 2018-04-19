using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Escape_the_trolls
{
	class Troll : Character
	{
        // TODO: Troll isn't destroyed when wall is pushed

		Direction directionToPlayer = Direction.Center;
		int turnsSinceChangeDirection = 0;

		public Troll(ref BoardState[][] board, int widthMax, int heightMax)
		{
			int xPos;
			int yPos;
			do
			{
				xPos = RandomPossition(widthMax-1);
				yPos = RandomPossition(heightMax-1);

			} while (board[xPos][yPos] != BoardState.Empty);
//!CheckSurroundings(board, xPos, yPos)
			board[xPos][yPos] = BoardState.Troll;

			myPossition.X = xPos;
			myPossition.Y = yPos;
		}

		public override void Move(ref BoardState[][] board, ref GameState gameState)
		{
			if (board[myPossition.X][myPossition.Y] == BoardState.DeadBody)
			{
				return;
			}
			if (gameState == GameState.EnemyTurn)
			{
				MoveState moveState = MoveState.Error;
				bool moved = false;
				Point oldPossition = myPossition;
				int antyloopCheck = 0;

				directionToPlayer = SearchPlayer(board);

				if (turnsSinceChangeDirection > 4)
					directionToPlayer = RandomDirection();

				do
				{
					antyloopCheck++;
					switch (directionToPlayer)
					{
						case Direction.Up:
							moveState = MoveUp(ref board);
							break;
						case Direction.Down:
							moveState = MoveDown(ref board);
							break;
						case Direction.Left:
							moveState = MoveLeft(ref board);
							break;
						case Direction.Right:
							moveState = MoveRight(ref board);
							break;
					}
					if (moveState == MoveState.Done)
					{
						moved = true;
						ChangePossitionOnBoard(oldPossition, ref board, BoardState.Troll);
						gameState = GameState.Running;
					}
					else if (moveState == MoveState.Error)
					{
						directionToPlayer = RandomDirection();
						turnsSinceChangeDirection = 0;
					}
					else if (moveState == MoveState.Lose)
					{
						moved = true;
						gameState = GameState.GameOver;
					}
					if (antyloopCheck > 10)
						break;
				} while (!moved);
				
			}
			else
			{
				return;
			}
		}

		private Direction RandomDirection()
		{
			int counter = 0;
			turnsSinceChangeDirection = 0;
			for(; ;)
			{
				switch (RandomPossition(4))
				{
					case 0:
						if ((directionToPlayer != Direction.Up) || (counter > 8))
							return Direction.Down;
						else
							counter++;
						break;
					case 1:
						if ((directionToPlayer != Direction.Down) || (counter > 8))
							return Direction.Up;
						else
							counter++;
						break;
					case 2:
						if ((directionToPlayer != Direction.Right) || (counter > 8))
							return Direction.Left;
						else
							counter++;
						break;
					case 3:
						if ((directionToPlayer != Direction.Left) || (counter > 8))
							return Direction.Right;
						else
							counter++;
						break;
				}
			}
		}

		new private MoveState MoveUp(ref BoardState[][] board)
		{
			if (myPossition.X > 1)
			{
				if (board[myPossition.X - 1][myPossition.Y] == BoardState.Empty)
				{
					myPossition.X--;
					return MoveState.Done;
				}
				if (CheckIfPlayer(board[myPossition.X - 1][myPossition.Y]))
				{
					myPossition.X--;
					return MoveState.Lose;
				}
				else
				{
					return MoveState.Error;
				}
			}
			else
			{
				return MoveState.Error;
			}
		}

		new private MoveState MoveDown(ref BoardState[][] board)
		{
			if (myPossition.X < (board.Length - 2))
			{
				if (board[myPossition.X + 1][myPossition.Y] == BoardState.Empty)
				{
					myPossition.X++;
					return MoveState.Done;
				}
				if (CheckIfPlayer(board[myPossition.X + 1][myPossition.Y]))
				{
					myPossition.X++;
					return MoveState.Lose;
				}
				else
				{
					return MoveState.Error;
				}
			}
			else
			{
				return MoveState.Error;
			}
		}

		new private MoveState MoveLeft(ref BoardState[][] board)
		{
			if (myPossition.Y > 1)
			{
				if (board[myPossition.X][myPossition.Y - 1] == BoardState.Empty)
				{
					myPossition.Y--;
					return MoveState.Done;
				}
				if (CheckIfPlayer(board[myPossition.X][myPossition.Y - 1]))
				{
					myPossition.Y--;
					return MoveState.Lose;
				}
				else
				{
					return MoveState.Error;
				}
			}
			else
			{
				return MoveState.Error;
			}
		}

		new private MoveState MoveRight(ref BoardState[][] board)
		{
			if (myPossition.Y < (board[0].Length - 2))
			{
				if (board[myPossition.X][myPossition.Y + 1] == BoardState.Empty)
				{
					myPossition.Y++;
					return MoveState.Done;
				}
				if (CheckIfPlayer(board[myPossition.X][myPossition.Y + 1]))
				{
					myPossition.Y++;
					return MoveState.Lose;
				}
				else
				{
					return MoveState.Error;
				}
			}
			else
			{
				return MoveState.Error;
			}
		}

		private bool CheckSurroundings(BoardState[][] board, int X, int Y)
		{
			if (board[X][Y] == BoardState.Empty)
			{
				if ((CheckIfPlayer(board[X + 1][Y])) || (CheckIfPlayer(board[X + 1][Y])))
				{
					if ((CheckIfPlayer(board[X - 1][Y])) || (CheckIfPlayer(board[X - 1][Y])))
					{
						if ((CheckIfPlayer(board[X][Y + 1])) || (CheckIfPlayer(board[X][Y + 1])))
						{
							if ((CheckIfPlayer(board[X][Y - 1])) || (CheckIfPlayer(board[X][Y - 1])))
							{
								return true;
							}
							else
								return false;
						}
						else
							return false;
					}
					else
						return false;
				}
				else
					return false;
			}
			else
				return false;
		}

		private Direction SearchPlayer(BoardState[][] board)
		{
			int pos = myPossition.X;

			for (int i = myPossition.X - 1; i > 1; i--)
			{
				if (CheckIfPlayer(board[i][myPossition.Y]))
				{
					turnsSinceChangeDirection = 0;
					return Direction.Up;
				}
				if (board[i][myPossition.Y] != BoardState.Empty)
				{
					turnsSinceChangeDirection++;
					break;
				}
			}
			for (int i = myPossition.X + 1; i < board.Length - 2; i++)
			{
				if (CheckIfPlayer(board[i][myPossition.Y]))
				{
					turnsSinceChangeDirection = 0;
					return Direction.Down;
				}
				if (board[i][myPossition.Y] != BoardState.Empty)
				{
					turnsSinceChangeDirection++;
					break;
				}
			}
			for (int i = myPossition.Y - 1; i > 1; i--)
			{
				if (CheckIfPlayer(board[myPossition.X][i]))
				{
					turnsSinceChangeDirection = 0;
					return Direction.Left;
				}
				if (board[myPossition.X][i] != BoardState.Empty)
				{
					turnsSinceChangeDirection++;
					break;
				}
			}
			for (int i = myPossition.Y + 1; i < (board[0].Length - 2); i++)
			{
				if (CheckIfPlayer(board[myPossition.X][i]))
				{
					turnsSinceChangeDirection = 0;
					return Direction.Right;
				}
				if (board[myPossition.X][i] != BoardState.Empty)
				{
					turnsSinceChangeDirection++;
					break;
				}
			}
			return directionToPlayer;
		}

		private bool CheckIfPlayer(BoardState possition)
		{
			if ((possition == BoardState.PlayerD) || (possition == BoardState.PlayerL)
				|| (possition == BoardState.PlayerR) || (possition == BoardState.PlayerU))
				return true;
			else
				return false;

		}
	}
}
