using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using SQLite;

namespace MultiplierLibrary.Model
{
	public class User
	{
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		[Unique]
		public string UserName { get; set; }
	}
}
