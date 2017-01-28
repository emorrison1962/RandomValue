using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;

namespace Eric.Morrison
{
	/// <summary>
	/// RandomString creates random strings. All characters in the string are limited to printable characters.
	/// </summary>
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

		static Random Random
		{
			get { return _random; }
		} static Random _random = new Random(Environment.TickCount);

		#endregion Properties

		#region Implementation
		/// <summary>
		/// Returns a random String value that is within a specified length.
		/// </summary>
		/// <param name="minLength">The inclusive lower bound of the length of the String returned.</param>
		/// <param name="maxLength">The exclsuve upper bound of the the length of the String returned.</param>
		/// <param name="alphaOnly">Optional flag to specify alpha characters only.</param>
		/// <returns>A random String value that is within a specified length.</returns>
		static public string Get(UInt32 minLength, UInt32 maxLength, bool alphaOnly = false)
		{
			return RandomString.Get(RandomValue.Next(minLength, maxLength), alphaOnly);
		}

		/// <summary>
		/// Returns a random String value of the specified length.
		/// </summary>
		/// <param name="length">Length of String to return.</param>
		/// <param name="alphaOnly">Optional flag to specify alpha characters only.</param>
		/// <returns>A random String value of the specified length.</returns>
		static public string Get(UInt32 length, bool alphaOnly = false)
		{
			string result = string.Empty;

			if (alphaOnly)
			{
				result = GetAlphaOnly(length);
			}
			else if (length > 0)
			{
				char[] chars = new char[length];
				for (int i = 0; i < length; ++i)
					chars[i] = Convert.ToChar(Random.Next(MIN_PRINTABLE, MAX_PRINTABLE));
				result = new string(chars);
			}
			return result;
		}

		/// <summary>
		/// Returns a random String value of the specified length, containing only alphabetic characters.
		/// </summary>
		/// <param name="length">Length of String to return.</param>
		/// <returns>A random String value of the specified length, containing only alphabetic characters.</returns>
		static public string GetAlphaOnly(UInt32 length)
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


