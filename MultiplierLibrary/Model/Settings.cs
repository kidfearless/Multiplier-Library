using System;
using System.Collections.Generic;
using System.Text;

namespace MultiplierLibrary.Model
{
	public static class Settings
	{
		// ensures no null values get returned causing an exception


		private static bool GetProperty(string value, bool defaultValue)
		{
			if (App.Current.Properties[value] == null)
			{
				App.Current.Properties[value] = defaultValue;
			}
			return (bool)App.Current.Properties[value];
		}
		private static double GetProperty(string value, double defaultValue)
		{
			if (App.Current.Properties[value] == null)
			{
				App.Current.Properties[value] = defaultValue;
			}
			return (double)App.Current.Properties[value];
		}

		public static bool SetProperty(string setting, bool value)
		{
			App.Current.Properties[setting] = value;
			return value;
		}

		public static double SetProperty(string setting, double value)
		{
			App.Current.Properties[setting] = value;
			return value;
		}

		public static double OldProblemsPercentage
		{
			get => GetProperty("OldProblemsPercentage", 0.25);
			set => SetProperty("OldProblemsPercentage", value);
		}
	}
}
