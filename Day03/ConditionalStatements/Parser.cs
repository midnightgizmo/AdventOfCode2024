using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Day03.ConditionalStatements
{
	public class Parser
	{
		public static readonly FunctionInfo[] _statmentTypes;
		
		
		private string _UnparsedStatements = string.Empty;

		static Parser()
		{
			_statmentTypes = new FunctionInfo[] 
			{
				new FunctionInfo(){StatmentName = "do", FunctionType = FunctionName.Do},
				new FunctionInfo(){StatmentName = "don't", FunctionType = FunctionName.DontDo }
			};
		}

		public int ProcessData(string InputData)
		{
			List<FoundStatmentInfo> ListOfFoundStatments;
			List<string> ListOfProcessableCommands;

			ListOfFoundStatments = this.FindDosAndDontsCommands(InputData);
			ListOfProcessableCommands = this.FindProcessableStatments(InputData, ListOfFoundStatments);

			return this.ProcessCommands(ListOfProcessableCommands);
		}

		private List<FoundStatmentInfo> FindDosAndDontsCommands(string InputData)
		{
			List<FunctionName> functions = new List<FunctionName>();
			functions.Add(FunctionName.Do);
			functions.Add(FunctionName.DontDo);

			return this.SearchForStatments(InputData, functions);
		}

		private List<FoundStatmentInfo> SearchForStatments(string InputData, List<FunctionName> StatmentsToLookFor)
		{
			List<FoundStatmentInfo> ListOfFoundStatments = new List<FoundStatmentInfo>();

			List<FunctionInfo> ListOfFunctionsToLookFor;
			this._UnparsedStatements = InputData;

			ListOfFunctionsToLookFor = FindFunctionStatments(StatmentsToLookFor);

			for (int index = 0; index < this._UnparsedStatements.Length; index++)
			{
				FunctionName FuncName;
				FuncName = StartsWith(this._UnparsedStatements, index, ListOfFunctionsToLookFor);
				if (FuncName == FunctionName.NULL)
					continue;

				// make a note of the function we have found and the position we found it at.
				// add it to an array.
				FoundStatmentInfo foundStatmentInfo = new FoundStatmentInfo() 
				{
					PositionInTextFound = index,
					TypeFound = FuncName
				};

				ListOfFoundStatments.Add(foundStatmentInfo);
			}

			return ListOfFoundStatments;
		}

		/// <summary>
		/// Looks for which bits of text can be processed (strips out the don't parts)
		/// </summary>
		/// <param name="InputData"></param>
		/// <param name="ListOfFoundStatments">list of do's and don't commands</param>
		public List<string> FindProcessableStatments(string InputData, List<FoundStatmentInfo> ListOfFoundStatments)
		{
			List<string> Results = new List<string>();

			if (ListOfFoundStatments.Count == 0)
				return new List<string> { InputData } ;

			// check to see if there is any processable statments at the begining of the string
			if (ListOfFoundStatments[0].PositionInTextFound > 0)
			{// there is some text before the first command that can be used
				Results.Add(InputData.Substring(0, ListOfFoundStatments[0].PositionInTextFound));
			}

			// this should not be hard coded, need to make it dynamic
			int DoLength = "Do()".Length;
			int DontLength = "Don't()".Length;

			// used to hold the length of data that needs extracting
			int DataLength = 0;
			// temp holder for extracted data
			string data = string.Empty;

			for (int index = 0; index < ListOfFoundStatments.Count; index++)
			{
				FoundStatmentInfo statmentInfo = ListOfFoundStatments[index];

				if(statmentInfo.TypeFound == FunctionName.Do)
				{
					// grab all the text up to the next statment
					if(index < ListOfFoundStatments.Count - 1)
					{// if this isn't the last statment we will be looking at

						// calculate the length of the data we need to extract
						DataLength = (ListOfFoundStatments[index + 1].PositionInTextFound - (statmentInfo.PositionInTextFound + DoLength));
						data = InputData.Substring(statmentInfo.PositionInTextFound + DoLength, DataLength);
						Results.Add (data);
					}
					// this is the last statment in the list we are looking at
					else
					{
						// grab all the data all the way to the end of the string
						DataLength = InputData.Length - (statmentInfo.PositionInTextFound + DoLength);
						data = InputData.Substring(statmentInfo.PositionInTextFound + DoLength, DataLength);
						Results.Add(data);
					}
				}
				else if(statmentInfo.TypeFound == FunctionName.DontDo)
				{
					continue;
				}
			}

			return Results;
		}

		private List<FunctionInfo> FindFunctionStatments(List<FunctionName> statmentsToLookFor)
		{
			// return only the statments from _statmentTypes that match those in statmentsToLookFor
			return _statmentTypes.Where(a => statmentsToLookFor.Any(b => b == a.FunctionType)).ToList();
		}

		private FunctionName StartsWith(string DataToLookAt, int IndexInStringToStartFrom, List<FunctionInfo> WhatToLookFor)
		{
						
			string data = DataToLookAt.Substring(IndexInStringToStartFrom);
			//int LengthOfDataLeft = data.Length;
			List<FunctionInfo> PossibleMatches = new List<FunctionInfo>();

			foreach (FunctionInfo function in WhatToLookFor)
			{
				// if the function takes in parameters, we only want to search up to the "("
				// else if it does not have parameters, we need to look for "()"
				string TextToLookFor = function.HasParameters ? function.StatmentName + "(" : function.StatmentName + "()";

				if (data.StartsWith(TextToLookFor) == true)
					PossibleMatches.Add(function);

			}

			if (PossibleMatches.Count > 0)
			{
				// need to check if its sorting from low to hight or high to low
				// I want to return the one that has the longest StatmentName
				return PossibleMatches.OrderBy(o => o.StatmentName.Length).First().FunctionType;
			}
			else
				return FunctionName.NULL;

		}


		private int ProcessCommands(List<string> DataToProcess)
		{
			int Result = 0;
			string RegexPattern = @"mul\(\d+,\d+\)";
			MatchCollection RegExMatchCollection;
			string PuzzleData = string.Empty;

			foreach (string data in DataToProcess)
				PuzzleData += data;


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
			string SecondNumber = match.Value.Substring(IndexOfComma + 1, (match.Value.Length - 2 - IndexOfComma));

			int.TryParse(FirstNumber, out MultiplierInstruction.FirstInstruction);
			int.TryParse(SecondNumber, out MultiplierInstruction.SecondInstruction);

			return MultiplierInstruction;

		}

		private struct Instruction
		{
			public int FirstInstruction;
			public int SecondInstruction;
		}


	}
}
