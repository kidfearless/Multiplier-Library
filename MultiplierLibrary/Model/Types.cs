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
		Friendly,
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
		DifferenceOfSquares,
		Even,
		Odd,
		OddAndEven,
		// for example, 75 x 28
		FiveByEven,
		// 32 x 38
		SinglesSumToTen,
		// Circles
		Circles,
		Primes,
		// Extreme Settings
		TeensEx,
		// size of the enum
		Size
	}

	public static class TypeConverter
	{
		public static string ToString(Types type)
		{
			switch ((int)type)
			{
				case 0: return "One By One";
				case 1: return "Two By One";
				case 2: return "Three By One";
				case 3: return "Singles Squared";
				case 4: return "Teens Squared";
				case 5: return "Fourty To Sixty Squared";
				case 6: return "Eighty To 100 Squared";
				case 7: return "All The Rest Squared";
				case 8: return "Factored";
				case 9: return "Friendly";
				case 10: return "Teens";
				case 11: return "Fourties To Sixties";
				case 12: return "Eighties";
				case 13: return "Nineties";
				case 14: return "Difference Of Squares";
				case 15: return "Five By Even";
				case 16: return "Singles Sum To Ten";
				case 17: return "Circles";
				case 18: return "Primes";
				case 19: return "Size";
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