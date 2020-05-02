using System;
using System.Collections.Generic;
using System.Text;

namespace MultiplierLibrary.Model
{
	public static class Settings
	{
		// ensures no null values get returned causing an exception
		private static bool GetProperty(string value, bool defaultValue = false)
		{
			if (App.Current.Properties[value] == null)
			{
				App.Current.Properties[value] = defaultValue;
			}
			return (bool)App.Current.Properties[value];
		}

		public static bool SetProperty(string setting, bool value)
		{
			App.Current.Properties[setting] = value;
			return value;
		}

		public static bool Warmup
		{
			get => GetProperty("Warmup");
			set => SetProperty("Warmup", value);
		}
		// Warmup		
		public static bool OneByOne
		{
			get => GetProperty("OneByOne");
			set => SetProperty("OneByOne", value);
		}
		public static bool TwoByOne
		{
			get => GetProperty("TwoByOne");
			set => SetProperty("TwoByOne", value);
		}
		public static bool ThreeByOne
		{
			get => GetProperty("ThreeByOne");
			set => SetProperty("ThreeByOne", value);
		}

		// 2x2	
		// squares	
		public static bool SinglesSquared
		{
			get => GetProperty("SinglesSquared");
			set => SetProperty("SinglesSquared", value);
		}
		public static bool TeensSquared
		{
			get => GetProperty("TeensSquared");
			set => SetProperty("TeensSquared", value);
		}
		public static bool FourtyToSixtySquared
		{
			get => GetProperty("FourtyToSixtySquared");
			set => SetProperty("FourtyToSixtySquared", value);
		}
		public static bool EightyTo100Squared
		{
			get => GetProperty("EightyTo100Squared");
			set => SetProperty("EightyTo100Squared", value);
		}
		public static bool AllTheRestSquared
		{
			get => GetProperty("AllTheRestSquared");
			set => SetProperty("AllTheRestSquared", value);
		}

		// factors
		public static bool Factors
		{
			get => GetProperty("Factors");
			set => SetProperty("Factors", value);
		}
		// friendlies
		public static bool Friendly
		{
			get => GetProperty("Friendly");
			set => SetProperty("Friendly", value);
		}
		// 2x2
		public static bool Teens
		{
			get => GetProperty("Teens");
			set => SetProperty("Teens", value);
		}
		public static bool FourtiesToSixties
		{
			get => GetProperty("FourtiesToSixties");
			set => SetProperty("FourtiesToSixties", value);
		}
		public static bool Eighties
		{
			get => GetProperty("Eighties");
			set => SetProperty("Eighties", value);
		}
		public static bool Nineties
		{
			get => GetProperty("Nineties");
			set => SetProperty("Nineties", value);
		}
		// either both numbers are even, or both number are odd
		public static bool DifferenceOfSquares
		{
			get => GetProperty("DifferenceOfSquares");
			set => SetProperty("DifferenceOfSquares", value);
		}
		// for example, 75 x 28
		public static bool FiveByEven
		{
			get => GetProperty("FiveByEven");
			set => SetProperty("FiveByEven", value);
		}
		// 32 x 38
		public static bool SinglesSumToTen
		{
			get => GetProperty("SinglesSumToTen");
			set => SetProperty("SinglesSumToTen", value);
		}
		// Circles
		public static bool Circles
		{
			get => GetProperty("Circles");
			set => SetProperty("Circles", value);
		}
		// Primes
		public static bool Primes
		{
			get => GetProperty("Primes");
			set => SetProperty("Primes", value);
		}
	}
}
