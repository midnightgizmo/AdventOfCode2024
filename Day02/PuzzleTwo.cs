using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Day02
{
	internal class PuzzleTwo : Shared.PuzzleBase
	{
		
		
		public int SolvePuzzle()
		{
			int NumberOfSafeReports = 0;
			// load the puzzle data into memory and convert it to an array for each line
			string[] PuzzleData = this.SplitIntoLines(this.LoadPuzzleDataIntoMemory());
			foreach (string Puzzle in PuzzleData)
			{
				bool IsReportSafe = false;
				// convert the line from a string to an array of int's
				int[] Levels = ConvertToNumbers(Puzzle.Split(' ', StringSplitOptions.RemoveEmptyEntries));
				// check to see if this line (levels) is safe to use
				IsReportSafe = CheckIfReportIsSafe(Levels);
				
				// if the report is safe to use
				if (IsReportSafe)
					NumberOfSafeReports++;
				// report is not safe. Lets loop through each level in the line. On each loop
				// remove a single level at that inde and then check the report to see if it safe.
				// if we reach the end of the loop (no more levels on that line) and the report is
				// still coming back unafe, we can't use the report
				else
				{
					// loop through each level in the line
					for (int eachLevelIndex = 0; eachLevelIndex < Levels.Length; eachLevelIndex++)
					{
						// remove a single level from the report at the specified index.
						int[] modifiedLevels = RemoveItemFromArray(Levels, eachLevelIndex);
						// run the report with the one level removed from the report
						IsReportSafe = CheckIfReportIsSafe(modifiedLevels);
						// if the report is safe
						if(IsReportSafe == true)
						{
							// increase safe report counter
							NumberOfSafeReports++;
							// break out of the for loop, no need to carry on because
							// we now have a safe report
							break;
						}
					}
					
				}

			
			}

			return NumberOfSafeReports;
		}


		/// <summary>
		/// Converts each element in the array from a char to an int (no checking is done on input array)
		/// </summary>
		/// <returns>input value converted to int array</returns>
		public int[] ConvertToNumbers(string[] NumbersAsString)
		{
			int[] Numbers = new int[NumbersAsString.Length];
			for (int i = 0; i < NumbersAsString.Length; i++)
				Numbers[i] = int.Parse(NumbersAsString[i].ToString());

			return Numbers;
		}

		public bool CheckIfReportIsSafe(int[] Levels)
		{
			

			bool AreLevelsIncreasing = false;
			// check if the direction of the levels for this report are going up, down or staying the same

			// Levels are increasing
			if (Levels[0] < Levels[1])
				AreLevelsIncreasing = true;
			// Levles are decreasing
			else if (Levels[0] > Levels[1])
				AreLevelsIncreasing = false;
			// Levels are the same, not increasing or decreasting. This is not allowed, so is a bad report
			else
				return false;
			

			// start at the second index and finish at the last index so we can compare
			// current index one left of it
			for (int LevelIndex = 1; LevelIndex < Levels.Length; LevelIndex++)
			{
				// from the current index level, check the index to the left of us.
				// Make sure we are heading in the current direction, increasting or decreating
				// Make sure the gap between current index and the one left of us differ by at least one and at most three
				int Level = Levels[LevelIndex];
				int PreviousLevel = Levels[LevelIndex - 1];
				LevelDirection Direction = GetLevelDirection(PreviousLevel, Level);

				// check direction we are heading and see if it is the correct direction
				switch (Direction)
				{
					case LevelDirection.Increasing:
						if (AreLevelsIncreasing == false)
						// we must have a bad report because the levels should be decreasing
							return false;
						
						break;

					case LevelDirection.Decreasing:
						if (AreLevelsIncreasing == true)
						// we must have a bad report because the levels should be increasing
							return false;
						
						break;

					// not allowed a report where 2 levels eaither side of each other are the same
					// so we must have a bad report
					case LevelDirection.Same:
						return false;

				}
				// if we make it this far we are heading in the correct direction

				// check how far apart the 2 levels are.
				int DiffBetweenLevels = Math.Abs(Level - PreviousLevel);
				if (DiffBetweenLevels > 3)
					return false;
				

			}

			// if we make it this far the report is good.
			return true;
		}

		



		/// <summary>
		/// Compare FirstLevel To second level to see if SecondLevel is bigger, smaller or the same
		/// </summary>
		/// <param name="FirstLevel"></param>
		/// <param name="SecondLevel"></param>
		/// <returns></returns>
		private LevelDirection GetLevelDirection(int FirstLevel, int SecondLevel)
		{
			LevelDirection Direction;
			if (FirstLevel < SecondLevel)
				Direction = LevelDirection.Increasing;
			else if (FirstLevel > SecondLevel)
				Direction = LevelDirection.Decreasing;
			else
				Direction = LevelDirection.Same;

			return Direction;
		}

		private int[] RemoveItemFromArray(int[] ArrayToRemoveFrom, int Index)
		{
			int [] Items = new int[ArrayToRemoveFrom.Length - 1];

			for (int i = 0; i < Items.Length; i++)
			{
				if (i >= Index)
					Items[i] = ArrayToRemoveFrom[i+ 1];
				else
					Items[i] = ArrayToRemoveFrom[i];
			}

			return Items;
		}



	}

	

}
