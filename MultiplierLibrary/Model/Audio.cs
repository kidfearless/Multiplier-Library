using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace MultiplierLibrary.Model
{
	class Audio
	{
		public static Stream GetStreamFromFile(string filename)
		{
			var assembly = typeof(App).GetTypeInfo().Assembly;
			var stream = assembly.GetManifestResourceStream("YourApp." + filename);
			return stream;
		}
	}
}
