using MultiplierLibrary.View;
using System;
using System.Collections.Generic;
using System.Text;

namespace MultiplierLibrary.Controller
{
	static class Navigator
	{
		public static void StartNewGame()
		{
			ProblemsPage page = new ProblemsPage();
			App.Current.MainPage = page;
			App.Current.Game.page = page;
			App.Current.Game.StartNewGame();
		}

		public static void GoToProblemsPage()
		{
			App.Current.MainPage = App.Current.Game.page;
		}

		public static void GoHome()
		{
			
		}
	}
}
