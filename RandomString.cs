using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;

namespace Eric.Morrison
{
	public class RandomString
	{
		#region Constants
		const int MIN_PRINTABLE = 0x21;
		const int MAX_PRINTABLE = 0x7e;

		const int MIN_UPPERCASE = 0x41;
		const int MIN_LOWERCASE = 0x61;
		const int LETTER_COUNT = 26;

		#endregion Constants

		#region Properties

		static public Random Random
		{
			get { return _random; }
		} static Random _random = new Random(Environment.TickCount);

		#endregion Properties

		#region Implementation
		static public string Get(Int32 minLength, Int32 maxLength)
		{
			return RandomString.Get(RandomValue.Next(minLength, maxLength));
		}

		static public string Get(int length)
		{
			string result = string.Empty;
			if (length > 0)
			{
				char[] chars = new char[length];
				for (int i = 0; i < length; ++i)
					chars[i] = Convert.ToChar(Random.Next(MIN_PRINTABLE, MAX_PRINTABLE));
				result = new string(chars);
			}
			return result;
		}

		static public string GetCharsOnly(int length)
		{
			string result = string.Empty;
			if (length > 0)
			{
				char[] chars = new char[length];
				for (int i = 0; i < length; ++i)
				{
					int ascii = Random.Next(LETTER_COUNT);

					int cap = Random.Next(4);
					if (0 == cap % 4)
						ascii += MIN_UPPERCASE;
					else
						ascii += MIN_LOWERCASE;

					char c = Convert.ToChar(ascii);
					Debug.Assert(
						((c >= 'A') && (c <= 'Z'))
						|| ((c >= 'a') && (c <= 'z'))
						);

					chars[i] = c;
				}
				result = new string(chars);
			}
			return result;
		}
		#endregion Implementation
	}//public class RandomString
}


