using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MultiplierLibrary.Model
{
	class User
	{
		public int ID { get; set; }
		public string UserName { get; set; }
		public int Score { get; set; }
		// Currently only one user can have a high score
		public int HighScore { get; set; }
	}
}
