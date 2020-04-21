using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using SQLite;

namespace MultiplierLibrary.Model
{
	class User
	{
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		public string UserName { get; set; }
		public int Score { get; set; }
		// Currently only one user can have a high score
		public int HighScore { get; set; }
	}
}
