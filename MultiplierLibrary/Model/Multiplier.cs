using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiplierLibrary.Model
{
	public class Multiplier
	{
		private static readonly Dictionary<int, int[]> Factors = InitFactors();

		//public delegate void ProblemAddedCallback(Multiplier multiplier, Problem problem);
		//public delegate void GameStartedCallback(Multiplier multiplier);
		//public delegate void GameEndedCallback(Multiplier multiplier);


		public Multiplier()
		{
		}

		private static Dictionary<int, int[]> InitFactors()
		{
			var temp = new Dictionary<int, int[]>();
			#region Initializer
			temp[4] = new int[] { 1, 2, 4 };
			temp[6] = new int[] { 1, 2, 3, 6 };
			temp[8] = new int[] { 1, 2, 4, 8 };
			temp[9] = new int[] { 1, 3, 9 };
			temp[10] = new int[] { 1, 2, 5, 10 };
			temp[12] = new int[] { 1, 2, 3, 4, 6, 12 };
			temp[14] = new int[] { 1, 2, 7, 14 };
			temp[15] = new int[] { 1, 3, 5, 15 };
			temp[16] = new int[] { 1, 2, 4, 8, 16 };
			temp[18] = new int[] { 1, 2, 3, 6, 9, 18 };
			temp[20] = new int[] { 1, 2, 4, 5, 10, 20 };
			temp[21] = new int[] { 1, 3, 7, 21 };
			temp[22] = new int[] { 1, 2, 11, 22 };
			temp[24] = new int[] { 1, 2, 3, 4, 6, 8, 12, 24 };
			temp[25] = new int[] { 1, 5, 25 };
			temp[26] = new int[] { 1, 2, 13, 26 };
			temp[27] = new int[] { 1, 3, 9, 27 };
			temp[28] = new int[] { 1, 2, 4, 7, 14, 28 };
			temp[30] = new int[] { 1, 2, 3, 5, 6, 10, 15, 30 };
			temp[32] = new int[] { 1, 2, 4, 8, 16, 32 };
			temp[33] = new int[] { 1, 3, 11, 33 };
			temp[34] = new int[] { 1, 2, 17, 34 };
			temp[35] = new int[] { 1, 5, 7, 35 };
			temp[36] = new int[] { 1, 2, 3, 4, 6, 9, 12, 18, 36 };
			temp[38] = new int[] { 1, 2, 19, 38 };
			temp[39] = new int[] { 1, 3, 13, 39 };
			temp[40] = new int[] { 1, 2, 4, 5, 8, 10, 20, 40 };
			temp[42] = new int[] { 1, 2, 3, 6, 7, 14, 21, 42 };
			temp[44] = new int[] { 1, 2, 4, 11, 22, 44 };
			temp[45] = new int[] { 1, 3, 5, 9, 15, 45 };
			temp[46] = new int[] { 1, 2, 23, 46 };
			temp[48] = new int[] { 1, 2, 3, 4, 6, 8, 12, 16, 24, 48 };
			temp[49] = new int[] { 1, 7, 49 };
			temp[50] = new int[] { 1, 2, 5, 10, 25, 50 };
			temp[51] = new int[] { 1, 3, 17, 51 };
			temp[52] = new int[] { 1, 2, 4, 13, 26, 52 };
			temp[54] = new int[] { 1, 2, 3, 6, 9, 18, 27, 54 };
			temp[55] = new int[] { 1, 5, 11, 55 };
			temp[56] = new int[] { 1, 2, 4, 7, 8, 14, 28, 56 };
			temp[57] = new int[] { 1, 3, 19, 57 };
			temp[58] = new int[] { 1, 2, 29, 58 };
			temp[60] = new int[] { 1, 2, 3, 4, 5, 6, 10, 12, 15, 20, 30, 60 };
			temp[62] = new int[] { 1, 2, 31, 62 };
			temp[63] = new int[] { 1, 3, 7, 9, 21, 63 };
			temp[64] = new int[] { 1, 2, 4, 8, 16, 32, 64 };
			temp[65] = new int[] { 1, 5, 13, 65 };
			temp[66] = new int[] { 1, 2, 3, 6, 11, 22, 33, 66 };
			temp[68] = new int[] { 1, 2, 4, 17, 34, 68 };
			temp[69] = new int[] { 1, 3, 23, 69 };
			temp[70] = new int[] { 1, 2, 5, 7, 10, 14, 35, 70 };
			temp[72] = new int[] { 1, 2, 3, 4, 6, 8, 9, 12, 18, 24, 36, 72 };
			temp[74] = new int[] { 1, 2, 37, 74 };
			temp[75] = new int[] { 1, 3, 5, 15, 25, 75 };
			temp[76] = new int[] { 1, 2, 4, 19, 38, 76 };
			temp[77] = new int[] { 1, 7, 11, 77 };
			temp[78] = new int[] { 1, 2, 3, 6, 13, 26, 39, 78 };
			temp[80] = new int[] { 1, 2, 4, 5, 8, 10, 16, 20, 40, 80 };
			temp[81] = new int[] { 1, 3, 9, 27, 81 };
			temp[82] = new int[] { 1, 2, 41, 82 };
			temp[84] = new int[] { 1, 2, 3, 4, 6, 7, 12, 14, 21, 28, 42, 84 };
			temp[85] = new int[] { 1, 5, 17, 85 };
			temp[86] = new int[] { 1, 2, 43, 86 };
			temp[87] = new int[] { 1, 3, 29, 87 };
			temp[88] = new int[] { 1, 2, 4, 8, 11, 22, 44, 88 };
			temp[90] = new int[] { 1, 2, 3, 5, 6, 9, 10, 15, 18, 30, 45, 90 };
			temp[91] = new int[] { 1, 7, 13, 91 };
			temp[92] = new int[] { 1, 2, 4, 23, 46, 92 };
			temp[93] = new int[] { 1, 3, 31, 93 };
			temp[94] = new int[] { 1, 2, 47, 94 };
			temp[95] = new int[] { 1, 5, 19, 95 };
			temp[96] = new int[] { 1, 2, 3, 4, 6, 8, 12, 16, 24, 32, 48, 96 };
			temp[98] = new int[] { 1, 2, 7, 14, 49, 98 };
			temp[99] = new int[] { 1, 3, 9, 11, 33, 99 };
			temp[100] = new int[] { 1, 2, 4, 5, 10, 20, 25, 50, 100 };
			#endregion
			return temp;
		}

		public Problem GetRandomFactor()
		{
			Problem problem = new Problem();

			Random random = new Random();
			int key = Factors.Keys.ElementAt(random.Next(0, Factors.Count));

			var array = Factors[key];
			// Get a random number from the lower half
			int index = random.Next(0, array.Length/2);
			problem.LeftHand = array[index];
			problem.RightHand = array[array.Length - index];
			problem.Type = Types.Factored;

			return problem;
		}
		//EightyTo100
		public Problem DoEightyTo100()
		{
			Random random = new Random();

			Problem problem = new Problem
			{
				LeftHand = random.Next(80, 100),
				RightHand = random.Next(10, 100),
				Type = Types.EightyTo100Squared

			};

			return problem;
		}

		//FourtyToSixty
		public Problem DoFourtyToSixty()
		{
			Random random = new Random();

			Problem problem = new Problem
			{
				LeftHand = random.Next(40, 60),
				RightHand = random.Next(10, 100),
				Type = Types.FourtiesToSixties
			};

			return problem;
		}
		//teens
		public Problem DoTeens()
		{
			Random random = new Random();

			Problem problem = new Problem
			{
				LeftHand = random.Next(10, 20),
				RightHand = random.Next(10, 20),
				Type = Types.Teens
			};

			return problem;
		}
		// 2 digits
		public Problem Do3By1()
		{
			Random random = new Random();

			Problem problem = new Problem
			{
				LeftHand = random.Next(100, 999),
				RightHand = random.Next(1, 10),
				Type = Types.ThreeByOne
			};

			return problem;
		}

		public Problem Do2By1()
		{
			Random random = new Random();

			Problem problem = new Problem
			{
				LeftHand = random.Next(10, 10),
				RightHand = random.Next(1, 10),
				Type = Types.TwoByOne
			};

			return problem;
		}

		public Problem Do1By1()
		{
			Random random = new Random();

			Problem problem = new Problem
			{
				LeftHand = random.Next(1, 10),
				RightHand = random.Next(1, 10),
				Type = Types.OneByOne
			};

			return problem;
		}

		public Problem DoWarmup()
		{
			Random random = new Random();
			switch(random.Next(1, 8))
			{
				case 1: return Do1By1();
				case 2: return Do2By1();
				case 3: return Do3By1();
				case 5: return DoTeens();
				case 6: return DoFourtyToSixty();
				case 7: return DoEightyTo100();
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
