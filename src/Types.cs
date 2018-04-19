using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escape_the_trolls
{
	enum Direction
	{
		Up,
		Down,
		Left,
		Right,
		Center
	};

	enum MoveState
	{
		Done,
		Error,
		Dead,
		Win,
		Lose
	}
	enum BoardState
	{
		Empty,
		Wall,
		Troll,
		Exit,
		PlayerU,
		PlayerD,
		PlayerL,
		PlayerR,
		DeadBody
	};

	enum GameState
	{
		Start,
		Running,
		GameOver,
		PlayerTurn,
		EnemyTurn,
		Win
	};
}
