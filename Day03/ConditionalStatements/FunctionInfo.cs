using System;
using System.Collections.Generic;
using System.Text;

namespace Day03.ConditionalStatements
{
	public class FunctionInfo
	{

		public string StatmentName { get; set; } = string.Empty;
		public bool HasParameters { get; set; } = false;
		public FunctionName FunctionType { get; set; }


	}

	public enum FunctionName {NULL, Do, DontDo, Mul}
}
