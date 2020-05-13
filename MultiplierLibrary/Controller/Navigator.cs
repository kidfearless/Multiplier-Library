using MultiplierLibrary.View;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MultiplierLibrary.Controller
{
	static class Navigator
	{
		public static ProblemsPage ProblemsPage = new ProblemsPage();
		public static HomePage HomePage = new HomePage();
		public static RoundResults RoundResultsPage = new RoundResults();
		public static SettingsPage SettingsPage = new SettingsPage();
		public static void StartGame(string userName)
		{
			App.Current.MainPage = ProblemsPage;
			App.Current.Game.Page = ProblemsPage;
			App.Current.Game.StartNewGame(userName);
		}

		public static void StartGame()
		{
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
			App.Current.Game.OnResultsPage();
			App.Current.MainPage = RoundResultsPage;
		}

		public static void GoToSettings()
		{
			App.Current.MainPage = SettingsPage;
		}
	}
}
