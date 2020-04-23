using SQLite;
using System;
using System.Collections.Generic;
using System.Text;


namespace MultiplierLibrary.Model
{
	public class Problem
	{
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		public int Left { get; set; }
		public int Right { get; set; }
		public bool Correct { get; set; }
		public Types Type { get; set; }
		public int GetAnswer()
		{
			return this.Left * this.Right;
		}

	}
}
