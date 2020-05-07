

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using MultiplierLibrary.Model;
using System.Data.SQLite;
using System.Data;
using MultiplierLibrary.Controller;
using System.Diagnostics;

#pragma warning disable CA2100 // Review SQL queries for security vulnerabilities

namespace MultiplierLibrary.Data
{
	class RecordsDatabase
	{
		const bool debug = true;
		const bool alertErrors = true;
		public SQLiteConnection Connection;

		public RecordsDatabase(string path)
		{
			string datasource = $"Data Source='{path}';";
			Connection = new SQLiteConnection(datasource);
			Connection.Open();

			// CREATE USERS
			using (SQLiteCommand command = new SQLiteCommand(Connection))
			{
				command.CommandText =
					@"CREATE TABLE IF NOT EXISTS `users` ( 
						`ID` INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
						`UserName` VARCHAR ( 32 ) NOT NULL UNIQUE
					)";

				command.ExecuteNonQuery();
			}
			// CREATE RECORDS
			using (SQLiteCommand command = new SQLiteCommand(Connection))
			{
				command.CommandText =
					@"CREATE TABLE IF NOT EXISTS `records` (
						`ID`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
						`Left`	INTEGER NOT NULL,
						`Right`	INTEGER NOT NULL,
						`Correct`	INTEGER NOT NULL,
						`Type`	INTEGER NOT NULL,
						`User`	INTEGER NOT NULL,
						FOREIGN KEY(`User`) REFERENCES `user`(`ID`)
					);";

				command.ExecuteNonQuery();
			}
		}

		public Dictionary<Types, List<Problem>> GetProblemHistory(int id)
		{
			SQLiteDataReader reader;
			// GET USER
			using (SQLiteCommand command = new SQLiteCommand(Connection))
			{
				command.CommandText =
					"SELECT ID, Left, Right, Correct, Type FROM records " +
					$"WHERE User = '{id}';";

				// Get a reader
				reader = command.ExecuteReader();
				Dictionary<Types, List<Problem>> results = new Dictionary<Types, List<Problem>>();
				if(reader != null && reader.HasRows)
				{
					while (reader.Read())
					{
						Problem problem = new Problem
						{
							ID = reader.GetInt32(0),
							Left = reader.GetInt32(1),
							Right = reader.GetInt32(2),
							Correct = reader.GetInt32(3) != 0,
							Type = (Types)reader.GetInt32(4)
						};

						if(!results.ContainsKey(problem.Type))
						{
							results[problem.Type] = new List<Problem>();
						}

						results[problem.Type].Add(problem);
					}
				}
				return results;
			}
		}

		public int GetUserID(string name)
		{
			name = StringSanitizer.Sanitize(name);
			SQLiteDataReader reader;
			// GET USER
			using (SQLiteCommand command = new SQLiteCommand(Connection))
			{
				command.CommandText =
					"SELECT ID FROM users " +
					$"WHERE UserName = '{name}';";

				// Get a reader
				reader = command.ExecuteReader();
			}
			// IF WE FAILED TO GET A USER
			if (reader == null || !reader.HasRows)
			{
				return InsertNewUserID(name);
			}
				
			// RETURN THE USER IF WE CAN
			if(reader.Read())
			{
				return reader.GetInt32(0);
			}
			else
			{
				Debug.Fail("[ERROR] Could not read from results set");
				if (alertErrors)
				{
					App.Current.Navigation.DisplayAlert("Database Error", $"Could not add user '{name}'", "continue");
				}
				return -1;
			}
		}

		private int InsertNewUserID(string name)
		{
			// CREATE A USER
			using (SQLiteCommand command = new SQLiteCommand(Connection))
			{
				command.CommandText =
					"INSERT INTO users " +
					"(UserName) " +
					"VALUES " +
					$"('{name}');";
				var results = command.ExecuteNonQuery();
			}
			SQLiteDataReader reader;
			// GET USER
			using (SQLiteCommand command = new SQLiteCommand(Connection))
			{
				command.CommandText =
				"SELECT ID FROM users " +
				$"WHERE UserName = '{name}';";

				reader = command.ExecuteReader();
			}
			// WE'VE FAILED THEM
			if (reader == null || !reader.HasRows)
			{
				Debug.Fail($"[ERROR] Could not add user '{name}'");
				if (alertErrors)
				{
					App.Current.Navigation.DisplayAlert("Database Error", $"Could not add user '{name}'", "continue");
				}
				return -1;
			}
			else if (reader.Read())
			{
				return reader.GetInt32(0);
			}
			else
			{
				Debug.Fail("[ERROR] Could not read from second results set");
				if (alertErrors)
				{
					App.Current.Navigation.DisplayAlert("Database Error", $"Could not retreive second result set for username '{name}'", "continue");
				}
				return -1;
			}
		}

		public void SaveRound(int userID, List<Problem> session)
		{
			using (SQLiteCommand command = new SQLiteCommand(Connection))
			{
				var text = @"INSERT INTO `records`
					(Left, Right, Correct, Type, User)
					VALUES ";

				for (int i = 0; i < session.Count-1; i++)
				{
					text += session[i].ToQueryString(userID) + ",\n";
				}

				text += session[session.Count - 1].ToQueryString(userID) + ";";

				command.CommandText = text;

				command.ExecuteNonQuery();
			}
		}
	}

}
