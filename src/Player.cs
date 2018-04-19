using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Escape_the_trolls
{
    class Player : Character
	{
        public Player(ref BoardState[][] board, int widthMax, int heightMax)
        {
            int xPos;
			int yPos;
			do
			{
				xPos = RandomPossition(widthMax);
				yPos = RandomPossition(heightMax);
			} while (board[xPos][yPos] != BoardState.Empty);

			board[xPos][yPos] = BoardState.PlayerD;

			myPossition.X = xPos;
			myPossition.Y = yPos;
		}

		public Player(ref BoardState[][] board, int widthMax, int heightMax, int test)
		{
			myPossition.X = 19;
			myPossition.Y = 1;
			board[myPossition.X][myPossition.Y] = BoardState.PlayerD;
		}


		public override void Move(ref BoardState[][] board, ref GameState gameState)
        {
			MoveState moveState = MoveState.Error;
            if (gameState == GameState.PlayerTurn)
			{
				bool moved = false;
				Point oldPossition = myPossition;
				do
				{
					switch (Console.ReadKey().Key)
					{
						case ConsoleKey.UpArrow:
							moveState = MoveUp(ref board);
							break;
						case ConsoleKey.DownArrow:
							moveState = MoveDown(ref board);
							break;
						case ConsoleKey.LeftArrow:
							moveState = MoveLeft(ref board);
							break;
						case ConsoleKey.RightArrow:
							moveState = MoveRight(ref board);
							break;
					}
					if (moveState == MoveState.Done)
					{
						moved = true;
						ChangePossitionOnBoard(oldPossition, ref board, BoardState.PlayerD);
						gameState = GameState.Running;
					}
					else if (moveState == MoveState.Win)
					{
						moved = true;
						gameState = GameState.Win;
						ChangePossitionOnBoard(oldPossition, ref board, BoardState.PlayerD);
					}
					else if (moveState == MoveState.Error)
					{
						moved = true;
						gameState = GameState.Running;
					}
					else if (moveState == MoveState.Dead)
					{
						moved = true;
						gameState = GameState.GameOver;
					}
				} while (!moved);
            }
            else
            {
                return;
            }
        }

		new private MoveState MoveUp(ref BoardState[][] board)
		{
			if (myPossition.X > 1)
			{
				if ((board[myPossition.X - 1][myPossition.Y] == BoardState.Empty) || (board[myPossition.X - 1][myPossition.Y] == BoardState.DeadBody))
				{
					myPossition.X--;
					return MoveState.Done;
				}
				else if (board[myPossition.X - 1][myPossition.Y] == BoardState.Exit)
				{
					return MoveState.Win;
				}
				else if (board[myPossition.X - 1][myPossition.Y] == BoardState.Wall)
				{
					if (PushWall(ref board, Direction.Up))
					{
						MoveUp(ref board);
						return MoveState.Done;
					}
					else
					{
						return MoveState.Error;
					}
				}
				else if (board[myPossition.X - 1][myPossition.Y] == BoardState.Troll)
				{
					myPossition.X--;
					return MoveState.Dead;
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
			if (myPossition.X < (board.Length-2))
			{
				if ((board[myPossition.X + 1][myPossition.Y] == BoardState.Empty) || (board[myPossition.X + 1][myPossition.Y] == BoardState.DeadBody))
				{
					myPossition.X++;
					return MoveState.Done;
				}
				else if (board[myPossition.X + 1][myPossition.Y] == BoardState.Exit)
				{
					myPossition.X++;
					return MoveState.Win;
				}
				else if (board[myPossition.X + 1][myPossition.Y] == BoardState.Wall)
				{
					if (PushWall(ref board, Direction.Down))
					{
						MoveDown(ref board);
						return MoveState.Done;
					}
					else
					{
						return MoveState.Error;
					}
				}
				else if (board[myPossition.X + 1][myPossition.Y] == BoardState.Troll)
				{
					myPossition.X++;
					return MoveState.Dead;
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
				if ((board[myPossition.X][myPossition.Y - 1] == BoardState.Empty) || (board[myPossition.X][myPossition.Y - 1] == BoardState.DeadBody))
				{
					myPossition.Y--;
					return MoveState.Done;
				}
				else if (board[myPossition.X][myPossition.Y - 1] == BoardState.Exit)
				{
					return MoveState.Win;
				}
				else if (board[myPossition.X][myPossition.Y - 1] == BoardState.Wall)
				{
					if (PushWall(ref board, Direction.Left))
					{
						MoveLeft(ref board);
						return MoveState.Done;
					}
					else
					{
						return MoveState.Error;
					}
				}
				else if (board[myPossition.X][myPossition.Y - 1] == BoardState.Troll)
				{
					myPossition.Y--;
					return MoveState.Dead;
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
			if (myPossition.Y < (board[0].Length-2))
			{
				if ((board[myPossition.X][myPossition.Y + 1] == BoardState.Empty) || (board[myPossition.X][myPossition.Y + 1] == BoardState.DeadBody))
				{
					myPossition.Y++;
					return MoveState.Done;
				}
				else if (board[myPossition.X][myPossition.Y + 1] == BoardState.Exit)
				{
					return MoveState.Win;
				}
				else if (board[myPossition.X][myPossition.Y + 1] == BoardState.Wall)
				{
					if (PushWall(ref board, Direction.Right))
					{
						MoveRight(ref board);
						return MoveState.Done;
					}
					else
					{
						return MoveState.Error;
					}
				}
				else if (board[myPossition.X][myPossition.Y + 1] == BoardState.Troll)
				{
					myPossition.Y++;
					return MoveState.Dead;
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

		private bool PushWall(ref BoardState[][] board, Direction direction)
		{
			switch (direction)
			{
				case Direction.Up:
					if (board[myPossition.X - 2][myPossition.Y] == BoardState.Empty)
					{
						board[myPossition.X - 2][myPossition.Y] = BoardState.Wall;
						board[myPossition.X - 1][myPossition.Y] = BoardState.Empty;
						return true;
					}
					else if (board[myPossition.X - 2][myPossition.Y] == BoardState.Troll)
					{
						board[myPossition.X - 2][myPossition.Y] = BoardState.DeadBody;
						board[myPossition.X - 1][myPossition.Y] = BoardState.Empty;
						return true;
					}
					else
					{
						return false;
					}
					break;
				case Direction.Down:
					if (board[myPossition.X + 2][myPossition.Y] == BoardState.Empty)
					{
						board[myPossition.X + 2][myPossition.Y] = BoardState.Wall;
						board[myPossition.X + 1][myPossition.Y] = BoardState.Empty;
						return true;
					}
					else if (board[myPossition.X + 2][myPossition.Y] == BoardState.Troll)
					{
						board[myPossition.X + 2][myPossition.Y] = BoardState.DeadBody;
						board[myPossition.X + 1][myPossition.Y] = BoardState.Empty;
						return true;
					}
					else
					{
						return false;
					}
					break;
				case Direction.Left:
					if (board[myPossition.X][myPossition.Y - 2] == BoardState.Empty)
					{
						board[myPossition.X][myPossition.Y - 2] = BoardState.Wall;
						board[myPossition.X][myPossition.Y - 1] = BoardState.Empty;
						return true;
					}
					else if (board[myPossition.X][myPossition.Y - 2] == BoardState.Troll)
					{
						board[myPossition.X][myPossition.Y - 2] = BoardState.DeadBody;
						board[myPossition.X][myPossition.Y - 1] = BoardState.Empty;
						return true;
					}
					else
					{
						return false;
					}
					break;
				case Direction.Right:
					if (board[myPossition.X][myPossition.Y + 2] == BoardState.Empty)
					{
						board[myPossition.X][myPossition.Y + 2] = BoardState.Wall;
						board[myPossition.X][myPossition.Y + 1] = BoardState.Empty;
						return true;
					}
					else if (board[myPossition.X][myPossition.Y + 2] == BoardState.Troll)
					{
						board[myPossition.X][myPossition.Y + 2] = BoardState.DeadBody;
						board[myPossition.X][myPossition.Y + 1] = BoardState.Empty;
						return true;
					}
					else
					{
						return false;
					}
					break;
			}
			return false;
		}
    }
}