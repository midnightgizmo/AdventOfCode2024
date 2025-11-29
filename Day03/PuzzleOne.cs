using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Day03
{
	internal class PuzzleOne : Shared.PuzzleBase
	{

		public int SolvePuzzle()
		{
			int Result = 0;
			// load the puzzle data into memory and convert it to an array for each line
			string PuzzleData = this.LoadPuzzleDataIntoMemory();

			string RegexPattern = @"mul\(\d+,\d+\)";
			MatchCollection RegExMatchCollection;

			RegExMatchCollection = Regex.Matches(PuzzleData, RegexPattern);
			foreach (Match match in RegExMatchCollection)
			{
				Instruction MultiplierInstruction;

				MultiplierInstruction = ExtractInstruction(match);

				Result += MultiplierInstruction.FirstInstruction * MultiplierInstruction.SecondInstruction;
			}

			return Result;
		}

		private Instruction ExtractInstruction(Match match)
		{
			Instruction MultiplierInstruction = new Instruction();
			int IndexOfFirstBracket;
			int IndexOfComma;

			IndexOfFirstBracket = match.Value.IndexOf('(');
			IndexOfComma = match.Value.IndexOf(',');

			string FirstNumber = match.Value.Substring(IndexOfFirstBracket + 1, (IndexOfComma - 1 - IndexOfFirstBracket));
			string SecondNumber = match.Value.Substring(IndexOfComma + 1, (match.Value.Length -2 - IndexOfComma));

			int.TryParse(FirstNumber, out MultiplierInstruction.FirstInstruction);
			int.TryParse(SecondNumber, out MultiplierInstruction.SecondInstruction);

			return MultiplierInstruction;

		}
	}

	struct Instruction
	{
		public int FirstInstruction;
		public int SecondInstruction;
	}
}
