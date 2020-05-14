using MultiplierLibrary.Model;
using MultiplierLibrary.View;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MultiplierLibrary.Controller
{
	// Used to navigate between pages, They are instantiated on the first reference to the pages and kept in memory
	static class Navigator
	{
		public static ProblemsPage ProblemsPage = new ProblemsPage();
		public static HomePage HomePage = new HomePage();
		public static RoundResults RoundResultsPage;
		public static SettingsPage SettingsPage = new SettingsPage();
		public static void StartGame(string userName)
		{
			if (Multiplier.ProblemCount == 0)
			{
				return;
			}
			App.Current.MainPage = ProblemsPage;
			App.Current.Game.Page = ProblemsPage;
			App.Current.Game.StartNewGame(userName);
		}

		public static void StartGame()
		{
			if(Multiplier.ProblemCount == 0)
			{
				return;
			}
			App.Current.MainPage = ProblemsPage;
			App.Current.Game.Page = ProblemsPage;
			App.Current.Game.StartNewGame();
		}

		public static void GoToProblemsPage()
		{
			App.Current.MainPage = App.Current.Game.Page;
		}

		public static void GoHome()
		{
			App.Current.MainPage = HomePage;
		}

		public static void CheckResults()
		{
			RoundResultsPage = new RoundResults();
			App.Current.MainPage = RoundResultsPage;
			App.Current.Game.OnResultsPage(RoundResultsPage);
		}

		public static void GoToSettings()
		{
			App.Current.MainPage = SettingsPage;
		}
	}
}
