using System;
using System.Collections.Generic;
using System.Text;

namespace MultiplierLibrary.Controller
{
	// Gotta love how I have to put this in a class in order to make a simple function
	// The orm database library parameterizes the strings so it's not nessecary to use this.
	// But there's no harm in keeping it
	public static class StringSanitizer
	{
		public static string Sanitize(string value)
		{
			StringBuilder str = new StringBuilder(value, 32);
			for (int i = str.Length-1; i >= 0; i--)
			{
				if (str[i] == '\'' || str[i] == '"' || str[i] == ';')
				{
					str.Remove(i, 1);
				}
			}

			return str.ToString();
		}
	}
}
