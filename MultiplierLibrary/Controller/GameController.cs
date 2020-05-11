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


// TODO: Pulling problems that were answered poorly from the database to give to the player when they answer a question

// game starts when page switches to problemspage
// query all the problems in the table where the userid==username.id
// sets the GameController.History for each type in the return results

// # some record of the previous results on this problem
// #(probably a number of times it was worked together with a percentage of times it was correct)
// select Count(*), Average(Correct) from results where Left = ? and Right = ? And ID = ?

// #When a user starts a new round, the app can go through the dictionary pretty quickly
// #and extract all of the previous problems that fit into the current options.
// That mixed with 
// #The rest of the time (80%) pick a random problem if it hasn't been solved before.
// Tells me that I should query their results at the start and give them a random(for now) problem from their history
// and then a generated problem the rest of the time.

namespace MultiplierLibrary.Controller
{
	public class GameController
	{
		public Problem CurrentProblem;
		
		public ProblemsPage Page;
		List<Problem> RoundProblems;
		Dictionary<Types, List<Problem>> History;
		List<Problem> Session;
		Multiplier Multiplier;
		RecordsDatabase Database;
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

			Multiplier = new Multiplier();
			RoundProblems = new List<Problem>();
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

			this.CurrentProblem = Multiplier.DoWarmup();
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
			Page.OnWrong();
		}

		public void OnAnsweredCorrectly(Problem problem)
		{
			Debug.WriteLine($"OnAnsweredCorrectly:");
			this.Correct++;
			this.CurrentProblem.Correct = 1;
			Page.OnCorrect();
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
