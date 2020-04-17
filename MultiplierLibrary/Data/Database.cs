using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using MultiplierLibrary.Model;

namespace MultiplierLibrary.Data
{
	class RecordsDatabase
	{
		readonly SQLiteAsyncConnection _database;

		public RecordsDatabase(string dbPath)
		{
			_database = new SQLiteAsyncConnection(dbPath);
			_database.CreateTableAsync<Problem>().Wait();
		}
		
		public RecordsDatabase()
		{
			string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Records.db3");
			_database = new SQLiteAsyncConnection(path);
			_database.CreateTableAsync<Problem>().Wait();
		}

		public Task<List<Problem>> GetProblemsAsync()
		{
			return _database.Table<Problem>().ToListAsync();
		}

		public Task<Problem> GetProblemByIDAsync(int id)
		{
			return _database.Table<Problem>()
							.Where(i => i.ID == id)
							.FirstOrDefaultAsync();
		}

		public Task<int> SaveProblemAsync(Problem problem)
		{
			if (problem.ID != 0)
			{
				return _database.UpdateAsync(problem);
			}
			else
			{
				return _database.InsertAsync(problem);
			}
		}
		
		public Task<int> SaveAllProblemsAsync(IEnumerable<Problem> problems)
		{
			return _database.InsertAllAsync(problems);
		}

		public Task<int> DeleteProblemAsync(Problem problem)
		{
			return _database.DeleteAsync(problem);
		}
	}
}
