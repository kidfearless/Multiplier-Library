using MultiplierLibrary.Data;
using MultiplierLibrary.Model;
using MultiplierLibrary.View;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using System.Reflection;
using System.Threading.Tasks;


namespace MultiplierLibrary.Controller
{
	public class GameController
	{
		public Problem CurrentProblem;

		#region Classes and Pointers
		AudioPlayer player;
		public ProblemsPage Page;
		List<Problem> RoundProblems;
		Dictionary<Types, List<Problem>> History;
		List<Problem> Session;
		RecordsDatabase Database;
		#endregion


		#region Primitives
		int _correct;
		int Correct
		{
			get => _correct;
			set
			{
				_correct = value;
				Page.LabelCorrect.Text = _correct.ToString();
			}
		}
		int _wrong;
		int Wrong
		{
			get => _wrong;
			set
			{
				_wrong = value;
				Page.LabelWrong.Text = _wrong.ToString();
			}
		}
		int _totalProblems;
		int TotalProblems
		{
			get => _totalProblems;
			set
			{
				_totalProblems = value;
				Page.LabelTotal.Text = _totalProblems.ToString();
			}
		}
		int userID;
		Types LastType;
		int maxProblems = 10;
		private string username;
		public string UserName
		{
			get => username;
			set
			{
				username = StringSanitizer.Sanitize(value);
			}
		}
		#endregion

		public GameController()
		{
			Database = new RecordsDatabase();

			RoundProblems = new List<Problem>();
			player = new AudioPlayer();
		}

		public async void StartNewGame()
		{
			this.Correct = 0;
			this.Wrong = 0;
			this.TotalProblems = 0;

			CreateNewProblem();

			// wait
			this.CurrentProblem.UserID =  this.userID = await Database.GetUserID(this.UserName);
			Debug.WriteLine($"[DEBUG] got userid {this.userID}");

			Session = new List<Problem>();
			History = await Database.GetProblemHistory(this.userID);
			OnRoundStart();
		}

		public void StartNewGame(string username)
		{
			this.UserName = username;
			StartNewGame();
		}

		void CheckAnswer(int answer)
		{
			Debug.WriteLine($"CheckAnswer:");
			if(this.CurrentProblem.GetAnswer() == answer)
			{
				OnAnsweredCorrectly(CurrentProblem);
			}
			else
			{
				OnAnsweredWrong(CurrentProblem);
			}

			OnAnsweredPost(CurrentProblem);
		}

		private void CreateNewProblem()
		{
			Debug.WriteLine($"CreateNewProblem:");

			this.CurrentProblem = Multiplier.GetRandomProblem();
			this.CurrentProblem.UserID = this.userID;
			if(new Random().NextDouble() < 0.5)
			{
				Page.LabelLeft.Text = CurrentProblem.LeftHand.ToString();
				Page.LabelRight.Text = CurrentProblem.RightHand.ToString();
			}
			else
			{
				Page.LabelLeft.Text = CurrentProblem.RightHand.ToString();
				Page.LabelRight.Text = CurrentProblem.LeftHand.ToString();
			}
		}

		public void OnRoundStart()
		{
			Debug.WriteLine($"OnRoundStart:");
			Page.OnRoundStart();
		}
		
		public void OnRoundEnd()
		{
			Debug.WriteLine($"OnRoundEnd:");
			
			Database.SaveRound(this.Session);

			if(Settings.ShouldPlaySound)
			{
				player.PlayCongrats();
			}

			Page.OnRoundEnd();
		}

		public void OnTextBoxEnter(Entry textbox)
		{
			Debug.WriteLine($"OnTextBoxEnter:");
			string answer = textbox.Text;


			int.TryParse(answer, out int intAnswer);
			CheckAnswer(intAnswer);

			// Clear answer
			textbox.Text = string.Empty;
		}

		public void OnAnsweredWrong(Problem problem)
		{
			Debug.WriteLine($"OnAnsweredWrong:");
			this.Wrong++;
			if (Settings.ShouldPlaySound)
			{
				player.PlayWrong();
			}
			Page.OnWrong();
		}

		public void OnAnsweredCorrectly(Problem problem)
		{
			Debug.WriteLine($"OnAnsweredCorrectly:");
			this.Correct++;
			this.CurrentProblem.Correct = 1;
			if (Settings.ShouldPlaySound)
			{
				player.PlayCorrect();
			}
			Page.OnCorrect();
		}

		public void OnResultsPage()
		{
			Dictionary<Types, Stats> stats = new Dictionary<Types, Stats>();

			int i = 0;
			foreach (var problem in Session)
			{
				i++;
				if (!stats.ContainsKey(problem.Type))
				{
					stats[problem.Type] = new Stats();
				}

				stats[problem.Type].Wins += problem.Correct;
				stats[problem.Type].Total++;
				var label = new Label
				{
					Text = $"Problem {i}: {problem.LeftHand} X {problem.LeftHand}",
					TextColor = problem.Correct == 1 ? Color.Green : Color.Red,
					HorizontalTextAlignment = TextAlignment.Center
				};
				var stackLayout = Navigator.RoundResultsPage.ProblemStack;
				stackLayout.Children.Add(label);
			}
			var results = Navigator.RoundResultsPage;
			results.LabelCorrect.Text = Correct.ToString();
			results.LabelWrong.Text = Wrong.ToString();
			results.LabelTotal.Text = TotalProblems.ToString();
	
			
			foreach (var stat in stats)
			{
				var type = Model.TypeConverter.ToString(stat.Key);
				type = type.PadRight(30 - type.Length);

				Button button = new Button
				{
					Text =  $"Type: {type}\t\t\tWins: {stat.Value.Wins} " +
							$"Loss: {stat.Value.Losses} AVG: {stat.Value.AVG}",
				};
				button.Clicked += delegate (object sender, EventArgs e)
				{
					TypeButton_Clicked(sender, e, stat.Key);
				};

				Navigator.RoundResultsPage.ProblemStack.Children.Insert(0, button);
			}


		}

		private void TypeButton_Clicked(object sender, EventArgs e, Types type)
		{
			
		}

		public async void OnAnsweredPost(Problem oldProblem)
		{
			Debug.WriteLine($"OnAnsweredPost:");

			this.TotalProblems++;
			Session.Add(oldProblem);

			if(this.TotalProblems >= this.maxProblems)
			{
				OnRoundEnd();
				return;
			}

			if(new Random().NextDouble() <= Settings.OldProblemsPercentage)
			{
				var list = await Database.GetWorstProblems(this.userID);

				if(list?.Count > 0)
				{
					var item = list[0];
					CurrentProblem = new Problem
					{
						ID = 0,
						LeftHand = item.LeftHand,
						RightHand = item.RightHand,
						UserID  = item.UserID,
						Type = item.Type
					};
					return;
				}
			}

			CreateNewProblem();
		}

		public void OnProblemSkipped()
		{
			Debug.WriteLine($"OnProblemSkipped: {this.CurrentProblem.LeftHand} X {this.CurrentProblem.RightHand}");
			CreateNewProblem();
		}
	}
}
