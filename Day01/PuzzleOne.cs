using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day01
{
	internal class PuzzleOne : Shared.PuzzleBase
	{
		public int SolvePuzzle()
		{
			List<int> puzzleColumnOne = new List<int>();
			List<int> puzzleColumnTwo = new List<int>();
			List<int> puzzleDifferenceBetweenColumnOneAndColumnTwo  = new List<int>();


			// load the puzzle data into memory and convert it to an array for each line
			string[] PuzzleData = this.SplitIntoLines(this.LoadPuzzleDataIntoMemory());

			// go through each line in PuzzleData and split the data by spaces and extract data in array postions 0 and 1
			// and put them in a List
			foreach (string Puzzle in PuzzleData)
			{
				string[] puzzleSplit = Puzzle.Split(new char[] { ' ' },StringSplitOptions.RemoveEmptyEntries);
				puzzleColumnOne.Add(int.Parse(puzzleSplit[0]));
				puzzleColumnTwo.Add(int.Parse(puzzleSplit[1]));
			}

			// sort each list from smallest to biggest.
			puzzleColumnOne.Sort();
			puzzleColumnTwo.Sort();

			// for each row work out how far apart the numbers are
			for(int i = 0; i < puzzleColumnOne.Count; i++)
			{
				int leftColumn = puzzleColumnOne[i];
				int rightColumn = puzzleColumnTwo[i];
				int distanceApart = 0;

				// if both numbers are the same
				if (leftColumn == rightColumn)
					distanceApart = 0;
				// if left column is bigger than right column
				else if (leftColumn > rightColumn)
					distanceApart = leftColumn - rightColumn;
				// if right column is bigger than left column
				else
					distanceApart =rightColumn - leftColumn;

				// add the distance apart to the list
				puzzleDifferenceBetweenColumnOneAndColumnTwo.Add(distanceApart);
			}

			// add up allthe distances apart and thats our answer.
			return puzzleDifferenceBetweenColumnOneAndColumnTwo.Sum();


			
		}
	}
}
