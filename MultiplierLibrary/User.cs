using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MultiplierLibrary
{
	class User
	{
		public string UserName;
		public int Score;
		// Currently only one user can have a high score
		public int HighScore
		{
			get
			{
				Dictionary<string, int> scores = (Dictionary<string, int>)Application.Current.Properties["highscore"];
				return scores[this.UserName];
			}
			set
			{
				Dictionary<string, int> scores = (Dictionary<string, int>)Application.Current.Properties["highscore"];
				scores[this.UserName] = value;
			}
		}
	}
}
