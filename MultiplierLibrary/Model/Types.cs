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
		// squares
		SinglesSquared,
		TeensSquared,
		FourtyToSixtySquared,
		EightyTo100Squared,
		AllTheRestSquared,
		// factors
		Factored,
		// friendlies
		Friendly,
		// 2x2
		Teens,
		FourtiesToSixties,
		Eighties,
		Nineties,
		// either both numbers are even, or both number are odd
		DifferenceOfSquares,
		// for example, 75 x 28
		FiveByEven,
		// 32 x 38
		SinglesSumToTen,
		// Circles
		Circles,
		Primes,
		// size of the enum
		Size
	}
}
