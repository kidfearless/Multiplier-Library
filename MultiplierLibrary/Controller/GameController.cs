using MultiplierLibrary.Data;
using MultiplierLibrary.Model;
using MultiplierLibrary.View;
using System.Data.SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using System.Reflection;


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
		
		public ProblemsPage page;
		List<Problem> RoundProblems;
		Dictionary<Types, List<Problem>> History;
		List<Problem> Session;
		Multiplier Multiplier;
		Results Results;
		RecordsDatabase Database;
		int _correct;
		int correct
		{
			get => _correct;
			set
			{
				_correct = value;
				page.LabelCorrect.Text = _correct.ToString();
			}
		}
		int _wrong;
		int Wrong
		{
			get => _wrong;
			set
			{
				_wrong = value;
				page.LabelWrong.Text = _wrong.ToString();
			}
		}
		int _totalProblems;
		int TotalProblems
		{
			get => _totalProblems;
			set
			{
				_totalProblems = value;
				page.LabelTotal.Text = _totalProblems.ToString();
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

		public GameController()
		{
			var localData = Environment.SpecialFolder.LocalApplicationData;
			string path = Path.Combine(Environment.GetFolderPath(localData), "Multiplier.db3");
			Database = new RecordsDatabase(path);

			Multiplier = new Multiplier();
			RoundProblems = new List<Problem>();
		}

		public void StartNewGame()
		{
			this.correct = 0;
			this.Wrong = 0;
			this.TotalProblems = 0;

			CreateNewProblem();

			// wait
			this.userID = Database.GetUserID(this.UserName);
			Debug.WriteLine($"[DEBUG] got userid {this.userID}");

			Session = new List<Problem>();
			History = Database.GetProblemHistory(this.userID);
			OnRoundStart();
		}

		public void StartNewGame(string username)
		{
			this.correct = 0;
			this.Wrong = 0;
			this.TotalProblems = 0;


			CreateNewProblem();

			this.UserName = username;

			// wait
			this.userID = Database.GetUserID(this.UserName);
			Debug.WriteLine($"[DEBUG] got userid {this.userID}");

			Session = new List<Problem>();
			History = Database.GetProblemHistory(this.userID);

			OnRoundStart();
		}
		Stream GetStreamFromFile(string filename)
		{
			var assembly = typeof(App).GetTypeInfo().Assembly;
			var stream = assembly.GetManifestResourceStream("YourApp." + filename);
			return stream;
		}


		void CheckAnswer(int answer)
		{
			Debug.WriteLine($"CheckAnswer:");
			if(this.CurrentProblem.GetAnswer() == answer)
			{
				this.correct++;
				this.CurrentProblem.Correct = true;
				OnAnsweredCorrectly(CurrentProblem);
			}
			else
			{
				this.Wrong++;
				OnAnsweredWrong(CurrentProblem);
			}

			this.TotalProblems++;

			OnAnsweredPost(CurrentProblem);
		}

		private void CreateNewProblem()
		{
			Debug.WriteLine($"CreateNewProblem:");

			this.CurrentProblem = Multiplier.DoWarmup();
			if(new Random().NextDouble() < 0.5)
			{
				page.LabelLeft.Text = CurrentProblem.Left.ToString();
				page.LabelRight.Text = CurrentProblem.Right.ToString();
			}
			else
			{
				page.LabelLeft.Text = CurrentProblem.Right.ToString();
				page.LabelRight.Text = CurrentProblem.Left.ToString();
			}
		}


		/** Query average
		 * SELECT AVG(Correct), TYPE
		 * FROM records
		 * WHERE USER = 9
		 * GROUP BY Type			
		 */
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Security", "CA2100:Review SQL queries for security vulnerabilities", Justification = "<Pending>")]
		private void PickOldProblem(Types type)
		{
			Debug.WriteLine($"PickOldProblem:");

			using (SQLiteCommand command = new SQLiteCommand(Database.Connection))
			{
				command.CommandText =
					"SELECT AVG(Correct) AS 'AVG_CORRECT', `Left`, `Right` " +
					"FROM records " +
					$"WHERE USER = {this.userID} AND `Type` = {(int)type} " +
					"GROUP BY `Left`, `Right` " +
					$"HAVING AVG(Correct) < {Settings.RepeatProblemDropOff} " +
					$"AND COUNT(Correct) > {Settings.RepeatProblemMinimum} " +
					"ORDER BY AVG(Correct)";
				Problem problem;
				SQLiteDataReader reader =  command.ExecuteReader();
				if (reader != null && reader.HasRows)
				{
					if(reader.Read())
					{
						problem = new Problem()
						{
							Left = reader.GetInt32(1),
							Right = reader.GetInt32(2),
						};
						Debug.WriteLine($"[DEBUG]: Giving old problem {problem.Left} X {problem.Right}");
					}
					else
					{
						problem = Multiplier.DoWarmup();
					}
				}
				else
				{
					problem = Multiplier.DoWarmup();
				}

				Debug.WriteLine($"[DEBUG]: Giving new problem {problem.Left} X {problem.Right}");

				if (new Random().NextDouble() < 0.5)
				{
					page.LabelLeft.Text = CurrentProblem.Left.ToString();
					page.LabelRight.Text = CurrentProblem.Right.ToString();
				}
				else
				{
					page.LabelLeft.Text = CurrentProblem.Right.ToString();
					page.LabelRight.Text = CurrentProblem.Left.ToString();
				}
			}
		}

		public void OnRoundStart()
		{
			Debug.WriteLine($"OnRoundStart:");
			page.OnRoundStart();
		}
		
		public void OnRoundEnd()
		{
			Debug.WriteLine($"OnRoundEnd:");

			Database.SaveRound(this.userID, this.Session);

			page.OnRoundEnd();
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
		}

		public void OnAnsweredCorrectly(Problem problem)
		{
			Debug.WriteLine($"OnAnsweredCorrectly:");
		}

		public void OnAnsweredPost(Problem oldProblem)
		{
			Debug.WriteLine($"OnAnsweredPost:");
			Session.Add(oldProblem);
			if(this.TotalProblems >= this.maxProblems)
			{
				OnRoundEnd();
				return;
			}

			if(new Random().NextDouble() <= Settings.OldProblemsPercentage)
			{
				double lowestValue = 1.0;
				Types lowestType = Types.Size;

				for (Types type = 0; type < Types.Size; type++)
				{
					if (!History.ContainsKey(type))
					{
						continue;
					}

					var list = History[type];

					var avg = list.Average(problem => problem.Correct? 1 : 0);
					if(avg < lowestValue && type != LastType)
					{
						lowestType = type;
						lowestValue = avg;
					}
				}

				if (lowestType == Types.Size)
				{
					CreateNewProblem();
				}
				else
				{
					PickOldProblem(lowestType);
				}
			}
			else
			{
				CreateNewProblem();
			}
		}

		public void OnProblemSkipped()
		{
			Debug.WriteLine($"OnProblemSkipped: {this.CurrentProblem.Left} X {this.CurrentProblem.Right}");
			CreateNewProblem();
		}
	}
}
