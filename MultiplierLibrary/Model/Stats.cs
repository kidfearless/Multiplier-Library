using System;
using System.Collections.Generic;
using System.Text;

namespace MultiplierLibrary.Model
{
	class Stats
	{
		public double Wins = 0.0;
		public double Total = 0.0;
		public double Losses { get => Total - Wins; }
		public double AVG { get => Wins / Total * 100.0; }
	}
}
