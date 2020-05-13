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
		//Factored,
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
		DifferenceOfSquares,
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

	public static class TypeConverter
	{
		public static string ToString(Types type)
		{
			switch ((int)type)
			{
				case 0: return "One By One";
				case 1: return "Two By One";
				case 2: return "Three By One";
				case 3: return "Teens";
				case 4: return "Fourty To Sixty";
				case 5: return "Eighty To One-Hundred";
				case 6: return "Eighties";
				case 7: return "Nineties";
				case 8: return "Singles Squared";
				case 9: return "Teens Squared";
				case 10: return "Fourties To Sixties Squared";
				case 11: return "Eighty To One-Hundred Squared";
				case 12: return "Squares One-Hundred And Beyond";
				case 13: return "Odd Numbers";
				case 14: return "Even Numbers";
				case 15: return "One Odd and Even Number";
				case 16: return "Number Ending in 5 and Number ending in Even";
				//case 8: return "Factored";
				//case 9: return "Friendly";
				//case 17: return "Circles";
				//case 18: return "Primes";
				case 17: return "Forties";
				case 18: return "Thirties";
				case 19: return "Twenties";
				case 20: return "Fifties";
				case 21: return "Sixties";
				case 22: return "Seventies";
				case 23: return "Twenties Squared";
				case 24: return "Thirties Squared";
				case 25: return "Forties Squared";
				case 26: return "Fifties Sqaured";
				case 27: return "Sixties Squared";
				case 28: return "Seventies Squared";
				case 29: return "Size";
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