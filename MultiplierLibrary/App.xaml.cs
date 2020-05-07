using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
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
		static RecordsDatabase _database;
		static RecordsDatabase Database
		{
			set => _database = value;
			get
			{
				if (_database == null)
				{
					_database = new RecordsDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Replays.db3"));
				}
				return _database;
			}
		}

		public new static App Current;

		public Multiplier Multiplier;
		public Results Results;
		public GameController Game;

		public App()
		{
			Debug.WriteLine("OnConstructed");
			App.Current = this;
			
			InitializeComponent();
			this.Results = new Results();
			this.Multiplier = new Multiplier();
			NavPage page = new NavPage();
			MainPage = page;
			this.Game = new GameController(page.ProblemPage);
		}

		protected override void OnStart()
		{
			Results.Answers = App.Database.GetProblemsAsync().Result;
			Debug.WriteLine("Got problems");
		}
		protected override void OnSleep()
		{
			var result = from problem in Results.Answers
						 where problem.ID == 0
						 select problem;

			App.Database.SaveAllProblemsAsync(result);


			Debug.WriteLine("Saved problems");
		}
		protected override void OnResume()
		{
		}
	}
}
