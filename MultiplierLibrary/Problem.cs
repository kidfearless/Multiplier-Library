using System;
using System.Collections.Generic;
using System.Text;

namespace MultiplierLibrary
{
	class Problem
	{
		public int Left;
		public int Right;
		public int Response;
		public int Answer
		{
			get
			{
				return this.Left * this.Right;
			}
		}
	}
}
