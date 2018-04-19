using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Escape_the_trolls
{
	class Character
	{
		protected Point myPossition;

		protected int RandomPossition(int max)
		{
			Random ran = new Random();
			int number = ran.Next(0, max);

			return number;
		}

		public virtual void Move(ref BoardState[][] board, ref GameState gameState)
		{
			// Move character
		}

		protected MoveState MoveUp(ref BoardState[][] board)
		{
			if (myPossition.X > 1)
			{
				if (board[myPossition.X - 1][myPossition.Y] == BoardState.Empty)
				{
					myPossition.X--;
					return MoveState.Done;
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

		protected MoveState MoveDown(ref BoardState[][] board)
		{
			if (myPossition.X < (board.Length - 2))
			{
				if (board[myPossition.X + 1][myPossition.Y] == BoardState.Empty)
				{
					myPossition.X++;
					return MoveState.Done;
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

		protected MoveState MoveLeft(ref BoardState[][] board)
		{
			if (myPossition.Y > 1)
			{
				if (board[myPossition.X][myPossition.Y - 1] == BoardState.Empty)
				{
					myPossition.Y--;
					return MoveState.Done;
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

		protected MoveState MoveRight(ref BoardState[][] board)
		{
			if (myPossition.Y < (board[0].Length - 2))
			{
				if (board[myPossition.X][myPossition.Y + 1] == BoardState.Empty)
				{
					myPossition.Y++;
					return MoveState.Done;
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

		protected void ChangePossitionOnBoard(Point possitionToChange, ref BoardState[][] board, BoardState character)
		{
			board[possitionToChange.X][possitionToChange.Y] = BoardState.Empty;
			board[myPossition.X][myPossition.Y] = character;
		}
	}
}
