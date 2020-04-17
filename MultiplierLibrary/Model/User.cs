using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MultiplierLibrary.Model
{
	class User
	{
		public string UserName;
		public int Score;
		// Currently only one user can have a high score
		public int HighScore;
	}
}
