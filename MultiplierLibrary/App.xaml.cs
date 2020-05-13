using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MultiplierLibrary.Model;
using MultiplierLibrary.View;
using MultiplierLibrary.Data;
using System.Diagnostics;
using System.Linq;
using System.IO;
using MultiplierLibrary.Controller;
using Plugin.SimpleAudioPlayer;

namespace MultiplierLibrary
{
	public partial class App : Application
	{

		public new static App Current;

		
		public GameController Game;
		public App()
		{
			Debug.WriteLine("OnConstructed");
			App.Current = this;
			Settings.OldProblemsPercentage = 0.25;
			InitializeComponent();
			this.Game = new GameController();
			var page = new HomePage();
			MainPage = page;
			Multiplier.GetRandomProblem();
		}

		protected override void OnStart()
		{
			//Results.Answers = App.Database.GetProblemsAsync().Result;

			Debug.WriteLine("Got problems");
		}
		protected override void OnSleep()
		{
			//var result = from problem in Results.Answers
			//			 where problem.ID == 0
			//			 select problem;

			//App.Database.SaveAllProblemsAsync(result);


			Debug.WriteLine("Saved problems");
		}
		protected override void OnResume()
		{
		}
	}
}
