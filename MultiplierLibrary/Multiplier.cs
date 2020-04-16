using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiplierLibrary
{
	class Multiplier
	{
		List<Problem> Problems = new List<Problem>();
		public int MaxGames;
		private static readonly Dictionary<int, int[]> Primes = InitPrimes();

		public delegate void ProblemAddedCallback(Multiplier multiplier, Problem problem);
		public delegate void GameStartedCallback(Multiplier multiplier);
		public delegate void GameEndedCallback(Multiplier multiplier);


		public Multiplier(int maxgames)
		{
			this.MaxGames = maxgames;
		}

		public Multiplier()
		{
			this.MaxGames = 80;
		}

		private static Dictionary<int, int[]> InitPrimes()
		{
			var temp = new Dictionary<int, int[]>();
			#region Initializer
			Primes[4] = new int[] { 1, 2, 4 };
			Primes[6] = new int[] { 1, 2, 3, 6 };
			Primes[8] = new int[] { 1, 2, 4, 8 };
			Primes[9] = new int[] { 1, 3, 9 };
			Primes[10] = new int[] { 1, 2, 5, 10 };
			Primes[12] = new int[] { 1, 2, 3, 4, 6, 12 };
			Primes[14] = new int[] { 1, 2, 7, 14 };
			Primes[15] = new int[] { 1, 3, 5, 15 };
			Primes[16] = new int[] { 1, 2, 4, 8, 16 };
			Primes[18] = new int[] { 1, 2, 3, 6, 9, 18 };
			Primes[20] = new int[] { 1, 2, 4, 5, 10, 20 };
			Primes[21] = new int[] { 1, 3, 7, 21 };
			Primes[22] = new int[] { 1, 2, 11, 22 };
			Primes[24] = new int[] { 1, 2, 3, 4, 6, 8, 12, 24 };
			Primes[25] = new int[] { 1, 5, 25 };
			Primes[26] = new int[] { 1, 2, 13, 26 };
			Primes[27] = new int[] { 1, 3, 9, 27 };
			Primes[28] = new int[] { 1, 2, 4, 7, 14, 28 };
			Primes[30] = new int[] { 1, 2, 3, 5, 6, 10, 15, 30 };
			Primes[32] = new int[] { 1, 2, 4, 8, 16, 32 };
			Primes[33] = new int[] { 1, 3, 11, 33 };
			Primes[34] = new int[] { 1, 2, 17, 34 };
			Primes[35] = new int[] { 1, 5, 7, 35 };
			Primes[36] = new int[] { 1, 2, 3, 4, 6, 9, 12, 18, 36 };
			Primes[38] = new int[] { 1, 2, 19, 38 };
			Primes[39] = new int[] { 1, 3, 13, 39 };
			Primes[40] = new int[] { 1, 2, 4, 5, 8, 10, 20, 40 };
			Primes[42] = new int[] { 1, 2, 3, 6, 7, 14, 21, 42 };
			Primes[44] = new int[] { 1, 2, 4, 11, 22, 44 };
			Primes[45] = new int[] { 1, 3, 5, 9, 15, 45 };
			Primes[46] = new int[] { 1, 2, 23, 46 };
			Primes[48] = new int[] { 1, 2, 3, 4, 6, 8, 12, 16, 24, 48 };
			Primes[49] = new int[] { 1, 7, 49 };
			Primes[50] = new int[] { 1, 2, 5, 10, 25, 50 };
			Primes[51] = new int[] { 1, 3, 17, 51 };
			Primes[52] = new int[] { 1, 2, 4, 13, 26, 52 };
			Primes[54] = new int[] { 1, 2, 3, 6, 9, 18, 27, 54 };
			Primes[55] = new int[] { 1, 5, 11, 55 };
			Primes[56] = new int[] { 1, 2, 4, 7, 8, 14, 28, 56 };
			Primes[57] = new int[] { 1, 3, 19, 57 };
			Primes[58] = new int[] { 1, 2, 29, 58 };
			Primes[60] = new int[] { 1, 2, 3, 4, 5, 6, 10, 12, 15, 20, 30, 60 };
			Primes[62] = new int[] { 1, 2, 31, 62 };
			Primes[63] = new int[] { 1, 3, 7, 9, 21, 63 };
			Primes[64] = new int[] { 1, 2, 4, 8, 16, 32, 64 };
			Primes[65] = new int[] { 1, 5, 13, 65 };
			Primes[66] = new int[] { 1, 2, 3, 6, 11, 22, 33, 66 };
			Primes[68] = new int[] { 1, 2, 4, 17, 34, 68 };
			Primes[69] = new int[] { 1, 3, 23, 69 };
			Primes[70] = new int[] { 1, 2, 5, 7, 10, 14, 35, 70 };
			Primes[72] = new int[] { 1, 2, 3, 4, 6, 8, 9, 12, 18, 24, 36, 72 };
			Primes[74] = new int[] { 1, 2, 37, 74 };
			Primes[75] = new int[] { 1, 3, 5, 15, 25, 75 };
			Primes[76] = new int[] { 1, 2, 4, 19, 38, 76 };
			Primes[77] = new int[] { 1, 7, 11, 77 };
			Primes[78] = new int[] { 1, 2, 3, 6, 13, 26, 39, 78 };
			Primes[80] = new int[] { 1, 2, 4, 5, 8, 10, 16, 20, 40, 80 };
			Primes[81] = new int[] { 1, 3, 9, 27, 81 };
			Primes[82] = new int[] { 1, 2, 41, 82 };
			Primes[84] = new int[] { 1, 2, 3, 4, 6, 7, 12, 14, 21, 28, 42, 84 };
			Primes[85] = new int[] { 1, 5, 17, 85 };
			Primes[86] = new int[] { 1, 2, 43, 86 };
			Primes[87] = new int[] { 1, 3, 29, 87 };
			Primes[88] = new int[] { 1, 2, 4, 8, 11, 22, 44, 88 };
			Primes[90] = new int[] { 1, 2, 3, 5, 6, 9, 10, 15, 18, 30, 45, 90 };
			Primes[91] = new int[] { 1, 7, 13, 91 };
			Primes[92] = new int[] { 1, 2, 4, 23, 46, 92 };
			Primes[93] = new int[] { 1, 3, 31, 93 };
			Primes[94] = new int[] { 1, 2, 47, 94 };
			Primes[95] = new int[] { 1, 5, 19, 95 };
			Primes[96] = new int[] { 1, 2, 3, 4, 6, 8, 12, 16, 24, 32, 48, 96 };
			Primes[98] = new int[] { 1, 2, 7, 14, 49, 98 };
			Primes[99] = new int[] { 1, 3, 9, 11, 33, 99 };
			Primes[100] = new int[] { 1, 2, 4, 5, 10, 20, 25, 50, 100 };
			#endregion
			return temp;
		}

		public Problem GetRandomFactor()
		{
			Problem problem = new Problem();

			Random random = new Random();
			int key = Primes.Keys.ElementAt(random.Next(0, Primes.Count));

			var array = Primes[key];
			// Get a random number from the lower half
			int index = random.Next(0, array.Length/2);
			problem.Left = array[index];
			problem.Right = array[array.Length - index];

			this.Problems.Add(problem);

			return problem;
		}

		public Problem Do3By1()
		{
			Random random = new Random();

			Problem problem = new Problem
			{
				Left = random.Next(100, 999),
				Right = random.Next(1, 9)
			};

			this.Problems.Add(problem);

			return problem;
		}

		public Problem Do2By1()
		{
			Random random = new Random();

			Problem problem = new Problem
			{
				Left = random.Next(10, 99),
				Right = random.Next(1, 9)
			};

			this.Problems.Add(problem);

			return problem;
		}

		public Problem Do1By1()
		{
			Random random = new Random();

			Problem problem = new Problem
			{
				Left = random.Next(10, 9),
				Right = random.Next(1, 9)
			};

			this.Problems.Add(problem);

			return problem;
		}

		public Problem DoWarmup()
		{
			Random random = new Random();
			switch(random.Next(1, 3))
			{
				case 1: return Do1By1();
				case 2: return Do2By1();
				case 3: return Do3By1();
				default: return Do1By1();
			}
		}

		protected void OnProblemAdded()
		{

		}

		protected void OnGameFinished()
		{

		}

		protected void OnGameStarted()
		{
			
		}
	}
}
