using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Diagnostics;

namespace MultiplierLibrary.Model
{
	public static class Multiplier
	{
		private static readonly Dictionary<int, int[]> Factors = InitFactors();
		private static Dictionary<Types, Func<Problem>> Problems = InitProblems();

		private static Dictionary<Types, Func<Problem>> InitProblems()
		{
			var dict = new Dictionary<Types, Func<Problem>>();

			for (Types t = 0; t < Types.Size; t++)
			{
				if(Settings.GetProperty(t.ToString(), true))
				{
					dict[t] = GetMethodByName("Get" + t.ToString());
				}
			}

			Settings.SettingChanged += Settings_SettingChanged;
			
			return dict;
		}

		private static void Settings_SettingChanged(object sender, SettingsChangedEventArgs args)
		{
			Types type = TypeConverter.FromString(args.SettingChanged);
			if(type != Types.Size)
			{
				if((bool)args.NewValue)
				{
					Problems[type] = GetMethodByName("Get" + type.ToString());
				}
				else if(Problems.ContainsKey(type))
				{
					Problems.Remove(type);
				}
			}
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

		#region Problem Getters
		public static Problem GetFactored()
		{
			Problem problem = new Problem();

			Random random = new Random();
			int key = Factors.Keys.ElementAt(random.Next(0, Factors.Count));

			var array = Factors[key];
			// Get a random number from the lower half
			int index = random.Next(0, array.Length/2);
			problem.LeftHand = array[index];
			problem.RightHand = array[array.Length - index];
			//problem.LeftHandSquare = array[index];
			problem.Type = Types.Factored;

			return problem;
		}
		
		//Eighties
		public static Problem GetEighties()
		{
			Random random = new Random();

			Problem problem = new Problem
			{
				LeftHand = random.Next(80, 90),
				RightHand = random.Next(10, 90),
				Type = Types.Eighties

			};

			return problem;
		}

		//Twenties
		public static Problem GetTwenties()
		{
			Random random = new Random();

			Problem problem = new Problem
			{
				LeftHand = random.Next(20, 30),
				RightHand = random.Next(20, 30),
				Type = Types.Twenties

			};

			return problem;
		}

		//Thirties
		public static Problem GetThirties()
		{
			Random random = new Random();

			Problem problem = new Problem
			{
				LeftHand = random.Next(30, 40),
				RightHand = random.Next(30, 40),
				Type = Types.Thirties

			};

			return problem;
		}

		//Forties
		public static Problem GetForties()
		{
			Random random = new Random();

			Problem problem = new Problem
			{
				LeftHand = random.Next(40, 50),
				RightHand = random.Next(40, 50),
				Type = Types.Forties

			};

			return problem;
		}

		//Fifties
		public static Problem GetFifties()
		{
			Random random = new Random();

			Problem problem = new Problem
			{
				LeftHand = random.Next(50, 60),
				RightHand = random.Next(50, 60),
				Type = Types.Fifties

			};

			return problem;
		}

		//Sixties
		public static Problem GetSixties()
		{
			Random random = new Random();

			Problem problem = new Problem
			{
				LeftHand = random.Next(60, 70),
				RightHand = random.Next(60, 70),
				Type = Types.Sixties

			};

			return problem;
		}

		//Seventies
		public static Problem GetSeventies()
		{
			Random random = new Random();

			Problem problem = new Problem
			{
				LeftHand = random.Next(70, 80),
				RightHand = random.Next(70, 80),
				Type = Types.Seventies

			};

			return problem;
		}


		//EightyTo100
		public static Problem GetEightyTo100()
		{
			Random random = new Random();

			Problem problem = new Problem
			{
				LeftHand = random.Next(80, 100),
				RightHand = random.Next(1, 100),
				Type = Types.EightyTo100

			};

			return problem;
		}

		//Nineties 
		public static Problem GetNineties()
		{
			Random random = new Random();

			Problem problem = new Problem
			{
				LeftHand = random.Next(90, 100),
				RightHand = random.Next(90, 100),
				Type = Types.Nineties

			};

			return problem;
		}

		//FourtyToSixty
		public static Problem GetFourtiesToSixties()
		{
			Random random = new Random();

			Problem problem = new Problem
			{
				LeftHand = random.Next(40, 60),
				RightHand = random.Next(40, 60),
				Type = Types.FourtiesToSixties
			};

			return problem;
		}
		//teens
		public static Problem GetTeens()
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
		//teens
		public static Problem GetTeensEx()
		{
			Random random = new Random();

			Problem problem = new Problem
			{
				LeftHand = random.Next(10, 20),
				RightHand = random.Next(1, 1000),
				Type = Types.TeensEx
			};

			return problem;
		}

		// Warm Up // basic problems
		public static Problem GetThreeByOne()
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

		public static Problem GetTwoByOne()
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

		public static Problem GetOneByOne()
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

		public static Problem GetWarmUp()
		{
			Random random = new Random();
			switch (random.Next(1, 4))
			{
				case 1: return GetOneByOne();
				case 2: return GetTwoByOne();
				case 3: return GetThreeByOne();				
				default: return GetOneByOne();
			}
		}

		// Square Problems
		public static Problem GetSinglesSquared()
		{
			Random random = new Random();
			
			Problem problem = new Problem
			{
				LeftHand = random.Next(1, 10),
				Type = Types.SinglesSquared
			};
			problem.RightHand = problem.LeftHand;

			return problem;
		}

		public static Problem GetTeensSquared()
		{
			Random random = new Random();

			Problem problem = new Problem
			{
				LeftHand = random.Next(10, 20),
				Type = Types.TeensSquared
			};
			problem.RightHand = problem.LeftHand;

			return problem;
		}

		public static Problem GetFourtyToSixtySquared()
		{
			Random random = new Random();

			Problem problem = new Problem
			{
				LeftHand = random.Next(40, 60),
				Type = Types.FourtyToSixtySquared
			};
			problem.RightHand = problem.LeftHand;

			return problem;
		}

		public static Problem GetEightyTo100Squared()
		{
			Random random = new Random();

			Problem problem = new Problem
			{
				LeftHand = random.Next(80, 100),
				Type = Types.EightyTo100Squared
			};
			problem.RightHand = problem.LeftHand;

			return problem;
		}

		public static Problem GetAllTheRestSquared()
		{
			Random random = new Random();

			Problem problem = new Problem
			{
				LeftHand = random.Next(100, 1000),

				Type = Types.AllTheRestSquared
			};
			problem.RightHand = problem.LeftHand;

			return problem;
		}

		public static Problem GetTwentySquared()
		{
			Random random = new Random();

			Problem problem = new Problem
			{
				LeftHand = random.Next(20, 30),

				Type = Types.TwentySquared
			};
			problem.RightHand = problem.LeftHand;

			return problem;
		}

		public static Problem GetThirtySquared()
		{
			Random random = new Random();

			Problem problem = new Problem
			{
				LeftHand = random.Next(30, 40),

				Type = Types.ThirtySquared
			};
			problem.RightHand = problem.LeftHand;

			return problem;
		}

		public static Problem GetFortySquared()
		{
			Random random = new Random();

			Problem problem = new Problem
			{
				LeftHand = random.Next(40, 50),

				Type = Types.FortySquared
			};
			problem.RightHand = problem.LeftHand;

			return problem;
		}

		public static Problem GetFiftySquared()
		{
			Random random = new Random();

			Problem problem = new Problem
			{
				LeftHand = random.Next(50, 60),

				Type = Types.FiftySquared
			};
			problem.RightHand = problem.LeftHand;

			return problem;
		}

		public static Problem GetSixtySquared()
		{
			Random random = new Random();

			Problem problem = new Problem
			{
				LeftHand = random.Next(60, 70),

				Type = Types.SixtySquared
			};
			problem.RightHand = problem.LeftHand;

			return problem;
		}

		public static Problem GetSeventySquared()
		{
			Random random = new Random();

			Problem problem = new Problem
			{
				LeftHand = random.Next(70, 80),

				Type = Types.SeventySquared
			};
			problem.RightHand = problem.LeftHand;

			return problem;
		}

		public static Problem GetEightySquared()
		{
			Random random = new Random();

			Problem problem = new Problem
			{
				LeftHand = random.Next(80, 90),

				Type = Types.EightySquared
			};
			problem.RightHand = problem.LeftHand;

			return problem;
		}

		public static Problem GetNinetySquared()
		{
			Random random = new Random();

			Problem problem = new Problem
			{
				LeftHand = random.Next(90, 100),

				Type = Types.NinetySquared
			};
			problem.RightHand = problem.LeftHand;

			return problem;
		}
		// Even Numbers
		public static Problem GetEven()
		{
			Random random = new Random();

			Problem problem = new Problem
			{
				LeftHand = (2 * random.Next(2 / 2, 100 / 2)),
				RightHand = (2 * random.Next(2 / 2, 100 / 2)),

				Type = Types.Even
			};			

			return problem;
		}

		//Odd
		public static Problem GetOdd()
		{
			Random random = new Random();

			Problem problem = new Problem
			{
				LeftHand = (2 * random.Next(0 / 2, 100 / 2))+1,
				RightHand = (2 * random.Next(0 / 2, 98 / 2))+1,

				Type = Types.Odd
			};

			return problem;
		}

		// Odd & Even
		public static Problem GetOddAndEven()
		{
			Random random = new Random();

			Problem problem = new Problem
			{
				LeftHand = (2 * random.Next(2 / 2, 98 / 2)),
				RightHand = (2 * random.Next(0 / 2, 98 / 2)) + 1,

				Type = Types.OddAndEven
			};

			return problem;
		}

		// Number ending in Even and 5 
		public static Problem GetFiveByEven()
		{
			Random random = new Random();
			string fiveString = "5";

			Problem problem = new Problem
			{
				LeftHand = (2 * random.Next(2 / 2, 98 / 2)),
				RightHand = random.Next(1,10),

				Type = Types.FiveByEven
			};

			string numString = problem.RightHand.ToString();
			string comString = numString + fiveString;
			problem.RightHand = Convert.ToInt32(comString);

			return problem;
		}

		// SingleSumtoTen
		public static Problem GetSinglesSumToTen()
		{
			Random random = new Random();

			Problem problem = new Problem
			{
				LeftHand = random.Next(1, 100),
				RightHand = random.Next(1, 10),
				Type = Types.SinglesSumToTen
			};
			int rightNum2 = problem.LeftHand % 10;
			while (problem.LeftHand % 10 == 0)
			{
				problem.LeftHand = random.Next(1, 100);

			}
			
			int rightNum2Sol = 10 - rightNum2;
			string rightString = problem.RightHand.ToString();
			string rightString2 = rightNum2Sol.ToString();
			string RightHandString = rightString + rightString2;
			problem.RightHand = Convert.ToInt32(RightHandString);
			
			return problem;
		}

		public static Problem GetRandomProblem()
		{
			Random rand = new Random();
			var auto  = Problems.ElementAt(rand.Next(0, Problems.Count)).Value;
			return auto.Invoke();
		}

#endregion

		private static Func<Problem> GetMethodByName(string name)
		{
			try
			{
				var flags = (BindingFlags.Public | BindingFlags.Static);
				Type type = typeof(Multiplier);
				MethodInfo info = type.GetMethod(name, flags);
				Func<Problem> problemFunc = (Func<Problem>) Delegate.CreateDelegate(typeof(Func<Problem>), info);
				return problemFunc;

			}
			catch (Exception e)
			{
				Debug.Fail($"[ERROR] Could not find method '{name}'");
				return null;
			}
		}

		public static void OnProblemAdded()
		{

		}

		public static void OnGameFinished()
		{

		}

		public static void OnGameStarted()
		{
			
		}
	}
}
