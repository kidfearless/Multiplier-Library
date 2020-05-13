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
		AnySquare,
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
}
