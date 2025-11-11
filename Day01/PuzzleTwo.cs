using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Day01
{
	internal class PuzzleTwo : Shared.PuzzleBase
	{
		List<int> puzzleColumnOne = new List<int>();
		List<int> puzzleColumnTwo = new List<int>();

		/// <summary>
		/// array that holds the sum of how many times each number exists.
		/// e.g. the value to index 1 would how many many times the number one
		/// exists in the right hand column. index zero will always be zero
		/// </summary>
		private int[] _RightColumnNumCount = new int[10];
		public int SolvePuzzle()
		{
			// load the puzzle data into memory and convert it to an array for each line
			string[] PuzzleData = this.SplitIntoLines(this.LoadPuzzleDataIntoMemory());

			// go through each line in PuzzleData
			foreach (string Puzzle in PuzzleData)
			{
				// split the data by spaces and extract data in array postions 0 and 1
				string[] puzzleSplit = Puzzle.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
				puzzleColumnOne.Add(int.Parse(puzzleSplit[0]));
				puzzleColumnTwo.Add(int.Parse(puzzleSplit[1]));
			}

			return CreateSimilarityScore();
		}

		private int CreateSimilarityScore()
		{

			int Score = 0;
			for (int EachRowInColumnOne = 0; EachRowInColumnOne < puzzleColumnOne.Count; EachRowInColumnOne++)
			{
				int ColumnOneCurrentNumber = puzzleColumnOne[EachRowInColumnOne];

				int NoTimesRowOneNumberFoundInRowTwo = 0;
				for (int EachRowInColumnTwo = 0; EachRowInColumnTwo < puzzleColumnTwo.Count; EachRowInColumnTwo++)
				{
					if (ColumnOneCurrentNumber == puzzleColumnTwo[EachRowInColumnTwo])
						NoTimesRowOneNumberFoundInRowTwo++;
				}
				Score += (ColumnOneCurrentNumber * NoTimesRowOneNumberFoundInRowTwo);
			}

			return Score;
		}
	}
}
