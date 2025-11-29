using Day03.ConditionalStatements;
using System;
using System.Collections.Generic;
using System.Text;

namespace Day03
{
	internal class PuzzleTwo : Shared.PuzzleBase
	{
		public int SolvePuzzle()
		{
			string PuzzleData = this.LoadPuzzleDataIntoMemory();

			Parser parser = new Parser();

			return parser.ProcessData(PuzzleData);
		}
	}
}
