namespace Shared
{
	public class PuzzleBase
	{
		public string FileName = "PuzzleData.txt";
		/// <summary>
		/// Loads the content of PuzzleData.txt into memory
		/// </summary>
		/// <returns>Contents of PuzzleData.txt as a string</returns>
		protected string LoadPuzzleDataIntoMemory()
		{
			// will hold the data loaded from PuzzleData.txt
			string fileData = string.Empty;
			// PuzzleData.txt has been set to be copied to output directory (meaning it will be in the same folder
			// as the executable file) so we need to find the location of the where the exe is being executed from
			string currentWorkingDirectory = System.IO.Directory.GetCurrentDirectory();
			// create the location of where the file exists on disk
			currentWorkingDirectory += $"\\{this.FileName}";

			// try and load the file from disk
			try
			{
				fileData = System.IO.File.ReadAllText(currentWorkingDirectory);
			}
			catch (Exception)
			{

			}
			// return the data loaded from PuzzleData.txt
			return fileData;
		}


		protected string[] SplitIntoLines(string PuzzleData)
		{
			return PuzzleData.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
		}
	}
}
