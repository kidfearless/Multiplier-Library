

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using MultiplierLibrary.Model;
using System.Data;
using MultiplierLibrary.Controller;
using System.Diagnostics;
using SQLite;
using System.Linq;

#pragma warning disable CA2100 // Review SQL queries for security vulnerabilities

namespace MultiplierLibrary.Data
{
	[Preserve]
	class RecordsDatabase
	{
		static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
		{
			return new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
		});

		static SQLiteAsyncConnection Database => lazyInitializer.Value;

		public string Connection { get; internal set; }

		static bool initialized = false;

		public RecordsDatabase()
		{
			InitializeAsync().SafeFireAndForget(false);
		}

		async Task InitializeAsync()
		{
			if (!initialized)
			{
				//await Database.CreateTablesAsync(CreateFlags.None, typeof(Problem)).ConfigureAwait(false);
				await Database.ExecuteAsync(@"CREATE TABLE IF NOT EXISTS `UserTable` ( 
					`ID` INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
					`UserName` VARCHAR(32) NOT NULL UNIQUE)");

				await Database.ExecuteAsync(@"CREATE TABLE IF NOT EXISTS `records` (
					`ID`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
					`LeftHand`	INTEGER NOT NULL,
					`RightHand`	INTEGER NOT NULL,
					`Correct`	INTEGER NOT NULL,
					`Type`	INTEGER NOT NULL,
					`UserID`	INTEGER NOT NULL,
					FOREIGN KEY(`UserID`) REFERENCES `UserTable`(`ID`));");
				initialized = true;
			}
		}

		public Task<int> GetUserID(string userName)
		{
			return Database.ExecuteScalarAsync<int>("SELECT ID FROM UserTable WHERE UserName = '?'", userName);
		}

		internal async Task<Dictionary<Types, List<Problem>>> GetProblemHistory(int userID)
		{
			Dictionary<Types, List<Problem>> history = new Dictionary<Types, List<Problem>>();
			var list = await Database.QueryAsync<Problem>("SELECT * FROM records WHERE UserID = ?", userID);
			foreach (Problem problem in list)
			{
				if(!history.ContainsKey(problem.Type))
				{
					history[problem.Type] = new List<Problem>();
				}

				history[problem.Type].Add(problem);
			}

			return history;
		}

		internal void SaveRound(List<Problem> session)
		{
			//session.ForEach((problem) => problem.ID = 0);
			Database.InsertAllAsync(session);
		}

		public async Task<List<UserStats>> GetWorstProblems(int userID)
		{
			Debug.WriteLine($"GetWorstProblems:");

			return await Database.QueryAsync<UserStats>("SELECT AVG(Correct) AS 'Average', `LeftHand`, `RightHand`, `Type`, `UserID` " +
					"FROM records " +
					"WHERE UserID = ? " +
					"GROUP BY LeftHand, RightHand, Type " +
					"HAVING AVG(Correct) < ? " +
					"AND COUNT(Correct) > ? " +
					"ORDER BY AVG(Correct)",
					userID, Settings.RepeatProblemDropOff, Settings.RepeatProblemMinimum);
		}
	}
}
