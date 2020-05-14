using System;
using System.Collections.Generic;
using System.Text;

namespace MultiplierLibrary.Model
{
	public enum Types : int
	{
		// Warmup
		OneByOne,
		TwoByOne,
		ThreeByOne,

		// 2x2
		EightyTo100,
		// squares
		SinglesSquared,
		TeensSquared,
		FourtyToSixtySquared,
		EightyTo100Squared,
		AllTheRestSquared,
		TwentySquared,
		ThirtySquared,
		FortySquared,
		FiftySquared,
		SixtySquared,
		SeventySquared,
		EightySquared,
		NinetySquared,
		// factors
		Factored,
		// friendlies
		//Friendly,
		// 2x2
		Teens,
		FourtiesToSixties,
		Eighties,
		Nineties,
		Twenties,
		Thirties,
		Forties,
		Fifties,
		Sixties,
		Seventies,
		// either both numbers are even, or both number are odd
		//DifferenceOfSquares,
		Even,
		Odd,
		OddAndEven,
		// for example, 75 x 28
		FiveByEven,
		// 32 x 38
		SinglesSumToTen,
		// Circles
		//Circles,
		//Primes,
		// Extreme Settings
		TeensEx,
		// size of the enum
		Size
	}


	// Update this if you change the Types enum
	public static class TypeConverter
	{
		public static string ToString(Types type)
		{
			switch (type)
			{
				case Types.OneByOne: return "One By One";
				case Types.TwoByOne: return "Two By One";
				case Types.ThreeByOne: return "Three By One";
				case Types.EightyTo100: return "Eighty To One-Hundred";
				case Types.SinglesSquared: return "Singles Squared";
				case Types.TeensSquared:  return "Teens Squared";
				case Types.FourtyToSixtySquared: return "Fourty To Sixty Squared";
				case Types.EightyTo100Squared: return "80 To 100 Squared";
				case Types.AllTheRestSquared: return "All The Rest Squared";
				case Types.TwentySquared: return "Twenty Squared";
				case Types.ThirtySquared: return "Thirty Squared";
				case Types.FortySquared: return "Fourty Squared";
				case Types.FiftySquared: return "Fifty Squared";
				case Types.SixtySquared: return "Sixty Squared";
				case Types.SeventySquared: return "Seventy Squared";
				case Types.EightySquared: return "Eighty Squared";
				case Types.NinetySquared: return "Ninety Squared";
				case Types.Factored: return "Factored";
				case Types.Teens: return "Teens";
				case Types.FourtiesToSixties: return "Fourties To Sixties";
				case Types.Eighties: return "Eighties";
				case Types.Nineties: return "Nineties";
				case Types.Twenties: return "Twenties";
				case Types.Thirties: return "Thirties";
				case Types.Forties: return "Forties";
				case Types.Fifties: return "Fifties";
				case Types.Sixties: return "Sixties";
				case Types.Seventies: return "Seventies";
				case Types.Even: return "Evens";
				case Types.Odd: return "Odds";
				case Types.OddAndEven: return "Odd By Even";
				case Types.FiveByEven: return "Number Ending in 5 By Even";
				case Types.SinglesSumToTen: return "Singles Summed To Ten";
				case Types.TeensEx: return "Teens Extreme";
				default: return "One By One";
			}
		}

		public static Types FromString(string value)
		{
			try
			{
				return (Types)Enum.Parse(typeof(Types), value);

			}
			catch
			{
				return Types.Size;
			}
		}
	}
}