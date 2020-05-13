using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

// This contains all the classes needed in order to query the database in an orm model library
namespace MultiplierLibrary.Model
{
	//SELECT AVG(Correct) AS 'AVG_CORRECT', `Left`, `Right`
	public class UserStats
	{
		public double Average { get; set; }
		public int LeftHand { get; set; }
		public int RightHand { get; set; }
		public Types Type { get; set; }
		public int UserID { get; set; }
		
	}

	public class Problem
	{
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		public int LeftHand { get; set; }
		public int RightHand { get; set; }
		public int Correct { get; set; }
		public Types Type { get; set; }
		public int UserID { get; set; }
		public int GetAnswer()
		{
			return this.LeftHand * this.RightHand;
		}

		public string ToQueryString()
		{
			return $"({LeftHand}, {RightHand}, {Correct}, {(int)Type}, {UserID})";
		}
	}
	public class User
	{
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		[Unique]
		public string UserName { get; set; }
	}
}
