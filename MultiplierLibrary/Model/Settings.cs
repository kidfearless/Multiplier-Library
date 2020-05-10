﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MultiplierLibrary.Model
{
	public class SettingsChangedEventArgs : EventArgs
	{
		public string SettingChanged { get; set; }
		public object NewValue;
	}
	public static class Settings
	{
		// ensures no null values get returned causing an exception

		public static IDictionary<string, object> Properties { get => App.Current.Properties; }

		

		public static event EventHandler<SettingsChangedEventArgs> SettingChanged;

		private static bool GetProperty(string value, bool defaultValue)
		{
			if (!App.Current.Properties.ContainsKey(value))
			{
				App.Current.Properties[value] = defaultValue;
			}
			return (bool)App.Current.Properties[value];
		}
		
		private static int GetProperty(string value, int defaultValue)
		{
			if (!App.Current.Properties.ContainsKey(value))
			{
				App.Current.Properties[value] = defaultValue;
			}
			return (int)App.Current.Properties[value];
		}

		private static double GetProperty(string value, double defaultValue)
		{
			if (!App.Current.Properties.ContainsKey(value))
			{
				App.Current.Properties[value] = defaultValue;
			}
			return (double)App.Current.Properties[value];
		}

		public static bool SetProperty(string setting, bool value)
		{
			SettingChanged?.Invoke(null, new SettingsChangedEventArgs() { SettingChanged = setting, NewValue = value });
			App.Current.Properties[setting] = value;
			return value;
		}
		
		public static int SetProperty(string setting, int value)
		{
			SettingChanged?.Invoke(null, new SettingsChangedEventArgs() { SettingChanged = setting, NewValue = value });
			App.Current.Properties[setting] = value;
			return value;
		}

		public static double SetProperty(string setting, double value)
		{
			SettingChanged?.Invoke(null, new SettingsChangedEventArgs() { SettingChanged = setting, NewValue = value });
			App.Current.Properties[setting] = value;
			return value;
		}
		// Percentage of old problems to give to the player
		public static double OldProblemsPercentage
		{
			get => GetProperty("OldProblemsPercentage", 0.25);
			set => SetProperty("OldProblemsPercentage", value);
		}

		// The average score on a problem to before it's no longer shown as an old problem
		public static double RepeatProblemDropOff
		{
			get => GetProperty("OldProblemsPercentage", 0.9);
			set => SetProperty("OldProblemsPercentage", value);
		}

		// The minimum number of times the problem must be answered before we check RepeatProblemDropOff
		public static int RepeatProblemMinimum
		{
			get => GetProperty("RepeatProblemMinimum", 10);
			set => SetProperty("RepeatProblemMinimum", value);
		}
	}
}
