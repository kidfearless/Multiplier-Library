using MultiplierLibrary.View;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MultiplierLibrary.Controller
{
	static class Navigator
	{

		public static void StartGame(string userName)
		{
			ProblemsPage page = new ProblemsPage();
			App.Current.MainPage = page;
			App.Current.Game.Page = page;
			App.Current.Game.StartNewGame(userName);
		}

		public static void StartGame()
		{
			ProblemsPage page = new ProblemsPage();
			App.Current.MainPage = page;
			App.Current.Game.Page = page;
			App.Current.Game.StartNewGame();
		}

		public static void GoToProblemsPage()
		{
			App.Current.MainPage = App.Current.Game.Page;
		}

		public static void GoHome()
		{
			HomePage page = new HomePage();
			App.Current.MainPage = page;
			App.Current.Game.Page = null;
		}

		public static void CheckResults()
		{
			RoundResults page = new RoundResults();
			App.Current.MainPage = page;
		}

		public static void GoToSettings()
		{
			SettingsPage page = new SettingsPage();
			App.Current.MainPage = page;
		}
	}
}
