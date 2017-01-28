using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Eric.Morrison
{
	/// <summary>
	/// RandomValue is a utility class that easily creates random values of various types.
	/// </summary>
	static public class RandomValue
	{
		#region Constants

		const int ONE = 1;
		const string MIN_VALUE = "MinValue";
		const string MAX_VALUE = "MaxValue";

		#endregion Constants

		#region Properties

		/// <summary>
		/// Note: Don't use _random directly. 
		/// Use the sychronized GetNext or GetNextBytes methods in order to ensure thread safety.
		/// </summary>
		static Random _random = new Random(Environment.TickCount);

		#endregion Properties

		/// <summary>
		/// Returns a random value greater than or equal to the mimnimum supported value and less than the maximum supported value of T.
		/// </summary>
		/// <typeparam name="T">The type of value to return.</typeparam>
		/// <returns>A value that is greater than or equal to the mimnimum supported value and less than the maximum supported value of T.</returns>
		static public T Next<T>() where T : struct, IConvertible
		{
			T result = default(T);
			try
			{
				Type type = typeof(T);
				var min = GetMinValue<T>();
				var max = GetMaxValue<T>();
				result = RandomValue.Next(min, max);
			}
			catch (Exception)
			{
				throw;
			}
			return (T)result;
		}

		/// <summary>
		/// Returns a random value that is less than the specified maximum.
		/// </summary>
		/// <typeparam name="T">The type of value to return.</typeparam>
		/// <param name="maxValue">The exclusive upper bound of the random value to be generated.</param>
		/// <returns>A value that is less than maxValue; that is, the range of return values does not include maxValue.</returns>
		static public T Next<T>(T maxValue) where T : struct, IConvertible
		{
			object result = null;
			try
			{
				Type type = typeof(T);

				if (typeof(Boolean) == type)
				{
					if (Boolean.FalseString == maxValue.ToString())
						result = false;
					else
						result = (T)(object)RandomValue.Next<bool>(false, true);
				}
				else
				{
					T minSupportedValue;
					FieldInfo fi = typeof(T).GetField(MIN_VALUE);
					Debug.Assert(null != fi);
					minSupportedValue = (T)fi.GetValue(null);

					result = RandomValue.Next(minSupportedValue, maxValue);
				}
			}
			catch (Exception)
			{
				throw;
			}
			return (T)result;
		}


		/// <summary>
		/// Returns a random value that is within a specified range.
		/// </summary>
		/// <typeparam name="T">The type of value to return.</typeparam>
		/// <param name="minValue">The inclusive lower bound of the random number returned.</param>
		/// <param name="maxValue">The exclusive upper bound of the random number returned. maxValue must be greater than or equal to minValue.</param>
		/// <returns>A value greater than or equal to minValue and less than maxValue; that is, the range of return values includes minValue but not maxValue. If minValue equals maxValue, minValue is returned.</returns>
		static public T Next<T>(T minValue, T maxValue) where T : struct, IConvertible
		{
			object result = null;
			Type type = typeof(T);
			T retval = default(T);

			try
			{
				switch (Type.GetTypeCode(type))
				{
					case TypeCode.Boolean:
						result = RandomValue.NextBoolean(Convert.ToBoolean(minValue), Convert.ToBoolean(maxValue));
						break;

					// 8 bit data types
					case TypeCode.SByte:
						result = RandomValue.NextSByte(Convert.ToSByte(minValue), Convert.ToSByte(maxValue));
						break;
					case TypeCode.Byte:
						result = RandomValue.NextByte(Convert.ToByte(minValue), Convert.ToByte(maxValue));
						break;

					// 16 bit data types
					case TypeCode.Char:
						result = RandomValue.NextChar(Convert.ToChar(minValue), Convert.ToChar(maxValue));
						break;

					case TypeCode.Int16:
						result = RandomValue.NextInt16(Convert.ToInt16(minValue), Convert.ToInt16(maxValue));
						break;

					case TypeCode.UInt16:
						result = RandomValue.NextUInt16(Convert.ToUInt16(minValue), Convert.ToUInt16(maxValue));
						break;

					// 32 bit data types
					case TypeCode.Int32:
						result = RandomValue.NextInt32(Convert.ToInt32(minValue), Convert.ToInt32(maxValue));
						break;

					case TypeCode.UInt32:
						result = RandomValue.NextUInt32(Convert.ToUInt32(minValue), Convert.ToUInt32(maxValue));
						break;

					case TypeCode.Single:
						result = RandomValue.NextSingle(Convert.ToSingle(minValue), Convert.ToSingle(maxValue));
						break;

					// 64 bit data types
					case TypeCode.Int64:
						result = RandomValue.NextInt64(Convert.ToInt64(minValue), Convert.ToInt64(maxValue));
						break;

					case TypeCode.UInt64:
						result = RandomValue.NextUInt64(Convert.ToUInt64(minValue), Convert.ToUInt64(maxValue));
						break;

					case TypeCode.Double:
						result = RandomValue.NextDouble(Convert.ToDouble(minValue), Convert.ToDouble(maxValue));
						break;

					case TypeCode.Decimal:
						result = RandomValue.NextDecimal(Convert.ToDecimal(minValue), Convert.ToDecimal(maxValue));
						break;

					case TypeCode.DateTime:
						result = RandomValue.NextDateTime(Convert.ToDateTime(minValue), Convert.ToDateTime(maxValue));
						break;

					default:
						Debug.WriteLine(minValue);
						Debug.WriteLine(maxValue);
						Debug.WriteLine(type.Name.ToString());
						Debug.WriteLine(type.GUID.ToString());
						string msg = string.Format("{0}.Next<T>() does not support the {1} data type",
							typeof(RandomValue).Name,
							type.Name);
						throw new NotSupportedException(msg);
						//break;

				}
				retval = (T)result;
			}
			catch (Exception ex)
			{
				Debug.Assert(false, ex.Message);
				throw;
			}

			return retval;
		}

		#region Boolean

		/// <summary>
		/// Returns a random Boolean value.
		/// </summary>
		/// <returns>A random Boolean value.</returns>
		static public bool NextBoolean()
		{
			return RandomValue.NextBoolean(false, true);
		}

		/// <summary>
		/// Returns a random Boolean value that is less than or equal to the specified maximum.
		/// </summary>
		/// <param name="maxValue">The inclusive upper bound of the random value to be generated.</param>
		/// <returns>A random Boolean value that is less than or equal to the specified maximum.</returns>
		static public bool NextBoolean(bool maxValue)
		{
			return RandomValue.NextBoolean(false, maxValue);
		}

		/// <summary>
		/// Returns a random Boolean value that is within a specified range.
		/// </summary>
		/// <param name="minValue">The inclusive lower bound of the random number returned.</param>
		/// <param name="maxValue">The inclusive upper bound of the random number returned.</param>
		/// <returns>A Boolean value greater than or equal to minValue and less than or equal to maxValue.</returns>
		static public bool NextBoolean(bool minValue, bool maxValue)
		{
			bool result = false;
			if (minValue && maxValue)
				result = true;
			if (!minValue && !maxValue)
				result = false;
			else
			{
				if (0 == RandomValue.Next() % 2)
					result = true;
			}
			return result;
		}

		#endregion Boolean

		#region SByte

		/// <summary>
		/// Returns a random System.SByte.
		/// </summary>
		/// <returns>A random System.SByte.</returns>
		static public SByte NextSByte()
		{
			return RandomValue.NextSByte(SByte.MinValue, SByte.MaxValue);
		}

		/// <summary>
		/// Returns a random System.SByte that is less than or equal to the specified maximum.
		/// </summary>
		/// <param name="maxValue">The inclusive upper bound of the random value to be generated.</param>
		/// <returns>A random System.SByte that is less than or equal to the specified maximum.</returns>
		static public SByte NextSByte(SByte maxValue)
		{
			return RandomValue.NextSByte(SByte.MinValue, maxValue);
		}

		/// <summary>
		/// Returns a random System.SByte that is within a specified range.
		/// </summary>
		/// <param name="minValue">The inclusive lower bound of the random number returned.</param>
		/// <param name="maxValue">The inclusive upper bound of the random number returned.</param>
		/// <returns>A System.SByte greater than or equal to minValue and less than or equal to maxValue.</returns>
		static public SByte NextSByte(SByte minValue, SByte maxValue)
		{
			NormalizeInput(ref minValue, ref maxValue);
			return (SByte)GetNext(minValue, maxValue);
		}

		#endregion SByte

		#region Byte

		/// <summary>
		/// Returns a random System.Byte.
		/// </summary>
		/// <returns>A random System.Byte.</returns>
		static public Byte NextByte()
		{
			return RandomValue.NextByte(Byte.MinValue, Byte.MaxValue);
		}

		/// <summary>
		/// Returns a random System.Byte that is less than or equal to the specified maximum.
		/// </summary>
		/// <param name="maxValue">The inclusive upper bound of the random value to be generated.</param>
		/// <returns>A random System.Byte that is less than or equal to the specified maximum.</returns>
		static public Byte NextByte(Byte maxValue)
		{
			return RandomValue.NextByte(Byte.MinValue, maxValue);
		}

		/// <summary>
		/// Returns a random System.Byte that is within a specified range.
		/// </summary>
		/// <param name="minValue">The inclusive lower bound of the random number returned.</param>
		/// <param name="maxValue">The inclusive upper bound of the random number returned.</param>
		/// <returns>A System.Byte greater than or equal to minValue and less than or equal to maxValue.</returns>
		static public Byte NextByte(Byte minValue, Byte maxValue)
		{
			NormalizeInput(ref minValue, ref maxValue);
			return (Byte)GetNext(minValue, maxValue);
		}

		#endregion Byte

		#region Char

		/// <summary>
		/// Returns a random System.Char.
		/// </summary>
		/// <returns>A random System.Char.</returns>
		static public Char NextChar()
		{
			return RandomValue.NextChar(Char.MinValue, Char.MaxValue);
		}

		/// <summary>
		/// Returns a random System.Char that is less than or equal to the specified maximum.
		/// </summary>
		/// <param name="maxValue">The inclusive upper bound of the random value to be generated.</param>
		/// <returns>A random System.Char that is less than or equal to the specified maximum.</returns>
		static public Char NextChar(Char maxValue)
		{
			return RandomValue.NextChar(Char.MinValue, maxValue);
		}

		/// <summary>
		/// Returns a random System.Char that is within a specified range.
		/// </summary>
		/// <param name="minValue">The inclusive lower bound of the random number returned.</param>
		/// <param name="maxValue">The inclusive upper bound of the random number returned.</param>
		/// <returns>A System.Char greater than or equal to minValue and less than or equal to maxValue.</returns>
		static public Char NextChar(Char minValue, Char maxValue)
		{
			NormalizeInput(ref minValue, ref maxValue);
			return (Char)GetNext(minValue, maxValue);
		}

		#endregion Char

		#region Int16

		/// <summary>
		/// Returns a random System.Int16.
		/// </summary>
		/// <returns>A random System.Int16.</returns>
		static public Int16 NextInt16()
		{
			return RandomValue.NextInt16(Int16.MinValue, Int16.MaxValue);
		}

		/// <summary>
		/// Returns a random System.Int16 that is less than or equal to the specified maximum.
		/// </summary>
		/// <param name="maxValue">The inclusive upper bound of the random value to be generated.</param>
		/// <returns>A random System.Int16 that is less than or equal to the specified maximum.</returns>
		static public Int16 NextInt16(Int16 maxValue)
		{
			return RandomValue.NextInt16(Int16.MinValue, maxValue);
		}

		/// <summary>
		/// Returns a random System.Int16 that is within a specified range.
		/// </summary>
		/// <param name="minValue">The inclusive lower bound of the random number returned.</param>
		/// <param name="maxValue">The inclusive upper bound of the random number returned.</param>
		/// <returns>A System.Int16 greater than or equal to minValue and less than or equal to maxValue.</returns>
		static public Int16 NextInt16(Int16 minValue, Int16 maxValue)
		{
			NormalizeInput(ref minValue, ref maxValue);
			return (Int16)GetNext(minValue, maxValue);
		}

		#endregion Int16

		#region UInt16

		/// <summary>
		/// Returns a random System.UInt16.
		/// </summary>
		/// <returns>A random System.UInt16.</returns>
		static public UInt16 NextUInt16()
		{
			return RandomValue.NextUInt16(UInt16.MinValue, UInt16.MaxValue);
		}

		/// <summary>
		/// Returns a random System.UInt16 that is less than or equal to the specified maximum.
		/// </summary>
		/// <param name="maxValue">The inclusive upper bound of the random value to be generated.</param>
		/// <returns>A random System.UInt16 that is less than or equal to the specified maximum.</returns>
		static public UInt16 NextUInt16(UInt16 maxValue)
		{
			return RandomValue.NextUInt16(UInt16.MinValue, maxValue);
		}

		/// <summary>
		/// Returns a random System.UInt16 that is within a specified range.
		/// </summary>
		/// <param name="minValue">The inclusive lower bound of the random number returned.</param>
		/// <param name="maxValue">The inclusive upper bound of the random number returned.</param>
		/// <returns>A System.UInt16 greater than or equal to minValue and less than or equal to maxValue.</returns>
		static public UInt16 NextUInt16(UInt16 minValue, UInt16 maxValue)
		{
			NormalizeInput(ref minValue, ref maxValue);
			return (UInt16)GetNext(minValue, maxValue);
		}

		#endregion UInt16

		#region Int32

		/// <summary>
		/// Returns a random System.Int32.
		/// </summary>
		/// <returns>A random System.Int32.</returns>
		static public Int32 Next()
		{
			return RandomValue.NextInt32();
		}

		/// <summary>
		/// Returns a random System.Int32 that is less than or equal to the specified maximum.
		/// </summary>
		/// <param name="maxValue">The inclusive upper bound of the random value to be generated.</param>
		/// <returns>A random System.Int32 that is less than or equal to the specified maximum.</returns>
		static public Int32 Next(Int32 maxValue)
		{
			return RandomValue.NextInt32(maxValue);
		}

		/// <summary>
		/// Returns a random System.Int32 that is within a specified range.
		/// </summary>
		/// <param name="minValue">The inclusive lower bound of the random number returned.</param>
		/// <param name="maxValue">The inclusive upper bound of the random number returned.</param>
		/// <returns>A System.Int32 greater than or equal to minValue and less than or equal to maxValue.</returns>
		static public Int32 Next(Int32 minValue, Int32 maxValue)
		{
			return RandomValue.NextInt32(minValue, maxValue);
		}

		/// <summary>
		/// Returns a random System.Int32.
		/// </summary>
		/// <returns>A random System.Int32.</returns>
		static public Int32 NextInt32()
		{
			return RandomValue.NextInt32(Int32.MinValue, Int32.MaxValue);
		}

		/// <summary>
		/// Returns a random System.Int32 that is less than or equal to the specified maximum.
		/// </summary>
		/// <param name="maxValue">The inclusive upper bound of the random value to be generated.</param>
		/// <returns>A random System.Int32 that is less than or equal to the specified maximum.</returns>
		static public Int32 NextInt32(Int32 maxValue)
		{
			return RandomValue.NextInt32(Int32.MinValue, maxValue);
		}

		/// <summary>
		/// Returns a random System.Int32 that is within a specified range.
		/// </summary>
		/// <param name="minValue">The inclusive lower bound of the random number returned.</param>
		/// <param name="maxValue">The inclusive upper bound of the random number returned.</param>
		/// <returns>A System.Int32 greater than or equal to minValue and less than or equal to maxValue.</returns>
		static public Int32 NextInt32(Int32 minValue, Int32 maxValue)
		{
			NormalizeInput(ref minValue, ref maxValue);
			return (Int32)GetNext(minValue, maxValue);
		}

		#endregion Int32

		#region UInt32

		/// <summary>
		/// Returns a random System.UInt32.
		/// </summary>
		/// <returns>A random System.UInt32.</returns>
		static public UInt32 NextUInt32()
		{
			return RandomValue.NextUInt32(UInt32.MinValue, UInt32.MaxValue);
		}

		/// <summary>
		/// Returns a random System.UInt32 that is less than or equal to the specified maximum.
		/// </summary>
		/// <param name="maxValue">The inclusive upper bound of the random value to be generated.</param>
		/// <returns>A random System.UInt32 that is less than or equal to the specified maximum.</returns>
		static public UInt32 NextUInt32(UInt32 maxValue)
		{
			return RandomValue.NextUInt32(UInt32.MinValue, maxValue);
		}

		/// <summary>
		/// Returns a random System.UInt32 that is within a specified range.
		/// </summary>
		/// <param name="minValue">The inclusive lower bound of the random number returned.</param>
		/// <param name="maxValue">The inclusive upper bound of the random number returned.</param>
		/// <returns>A System.UInt32 greater than or equal to minValue and less than or equal to maxValue.</returns>
		static public UInt32 NextUInt32(UInt32 minValue, UInt32 maxValue)
		{
			int cb = Marshal.SizeOf(typeof(UInt32));
			byte[] buffer = GetNextBytes(cb);

			UInt32 result = BitConverter.ToUInt32(buffer, 0);
			result = Math.Max(minValue, result);
			result = Math.Min(maxValue, result);
			return result;
		}

		#endregion UInt32

		#region Int64

		/// <summary>
		/// Returns a random System.Int64.
		/// </summary>
		/// <returns>A random System.Int64.</returns>
		static public Int64 NextInt64()
		{
			return RandomValue.NextInt64(Int64.MinValue, Int64.MaxValue);
		}

		/// <summary>
		/// Returns a random System.Int64 that is less than or equal to the specified maximum.
		/// </summary>
		/// <param name="maxValue">The inclusive upper bound of the random value to be generated.</param>
		/// <returns>A random System.Int64 that is less than or equal to the specified maximum.</returns>
		static public Int64 NextInt64(Int64 maxValue)
		{
			return RandomValue.NextInt64(Int64.MinValue, maxValue);
		}

		/// <summary>
		/// Returns a random System.Int64 that is within a specified range.
		/// </summary>
		/// <param name="minValue">The inclusive lower bound of the random number returned.</param>
		/// <param name="maxValue">The inclusive upper bound of the random number returned.</param>
		/// <returns>A System.Int64 greater than or equal to minValue and less than or equal to maxValue.</returns>
		static public Int64 NextInt64(Int64 minValue, Int64 maxValue)
		{
			int cb = Marshal.SizeOf(typeof(Int64));
			byte[] buffer = GetNextBytes(cb);

			Int64 result = BitConverter.ToInt64(buffer, 0);
			result = Math.Max(minValue, result);
			result = Math.Min(maxValue, result);
			return result;
		}

		#endregion Int64

		#region UInt64

		/// <summary>
		/// Returns a random System.UInt64.
		/// </summary>
		/// <returns>A random System.UInt64.</returns>
		static public UInt64 NextUInt64()
		{
			return RandomValue.NextUInt64(UInt64.MinValue, UInt64.MaxValue);
		}

		/// <summary>
		/// Returns a random System.UInt64 that is less than or equal to the specified maximum.
		/// </summary>
		/// <param name="maxValue">The inclusive upper bound of the random value to be generated.</param>
		/// <returns>A random System.UInt64 that is less than or equal to the specified maximum.</returns>
		static public UInt64 NextUInt64(UInt64 maxValue)
		{
			return RandomValue.NextUInt64(UInt64.MinValue, maxValue);
		}

		/// <summary>
		/// Returns a random System.UInt64 that is within a specified range.
		/// </summary>
		/// <param name="minValue">The inclusive lower bound of the random number returned.</param>
		/// <param name="maxValue">The inclusive upper bound of the random number returned.</param>
		/// <returns>A System.UInt64 greater than or equal to minValue and less than or equal to maxValue.</returns>
		static public UInt64 NextUInt64(UInt64 minValue, UInt64 maxValue)
		{
			int cb = Marshal.SizeOf(typeof(UInt64));
			byte[] buffer = GetNextBytes(cb);

			UInt64 result = BitConverter.ToUInt64(buffer, 0);
			result = Math.Max(minValue, result);
			result = Math.Min(maxValue, result);
			return result;
		}

		#endregion UInt64

		#region Decimal

		/// <summary>
		/// Returns a random System.Decimal.
		/// </summary>
		/// <returns>A random System.Decimal.</returns>
		static public Decimal NextDecimal()
		{
			return RandomValue.NextDecimal(Decimal.MinValue, Decimal.MaxValue);
		}

		/// <summary>
		/// Returns a random System.Decimal that is less than or equal to the specified maximum.
		/// </summary>
		/// <param name="maxValue">The inclusive upper bound of the random value to be generated.</param>
		/// <returns>A random System.Decimal that is less than or equal to the specified maximum.</returns>
		static public Decimal NextDecimal(Decimal maxValue)
		{
			return RandomValue.NextDecimal(Decimal.MinValue, maxValue);
		}

		/// <summary>
		/// Returns a random System.Decimal that is within a specified range.
		/// </summary>
		/// <param name="minValue">The inclusive lower bound of the random number returned.</param>
		/// <param name="maxValue">The inclusive upper bound of the random number returned.</param>
		/// <returns>A System.Decimal greater than or equal to minValue and less than or equal to maxValue.</returns>
		static public Decimal NextDecimal(Decimal minValue, Decimal maxValue)
		{
			int cb = Marshal.SizeOf(typeof(Decimal));
			byte[] buffer = GetNextBytes(cb);

			int[] ints = new int[4];

			//Bits 0 to 15, the lower word, are unused and must be zero.
			//Bits 24 to 30 are unused and must be zero.
			ints[3] = 0;

			//Bit 31 contains the sign; 0 meaning positive, and 1 meaning negative.
			ints[3] = RandomValue.Next<byte>(ONE); //Zero or one.
			ints[3] <<= 31; //Shift to highest bit position. 

			//Bits 16 to 23 must contain an exponent between 0 and 28, which 
			//indicates the power of 10 to divide the integer number.
			const int MAX_EXPONENT = 28;
			int exponent = RandomValue.Next<byte>(MAX_EXPONENT);
			exponent <<= 16;

			ints[3] |= exponent;

			ints[2] = BitConverter.ToInt32(buffer, 2);
			ints[1] = BitConverter.ToInt32(buffer, 4);
			ints[0] = BitConverter.ToInt32(buffer, 6);

			Decimal result = new Decimal(ints);
			result = Math.Max(minValue, result);
			result = Math.Min(maxValue, result);
			return result;
		}

		#endregion Decimal

		#region Single

		/// <summary>
		/// Returns a random System.Single.
		/// </summary>
		/// <returns>A random System.Single.</returns>
		static public Single NextSingle()
		{
			return RandomValue.NextSingle(Single.MinValue, Single.MaxValue);
		}

		/// <summary>
		/// Returns a random System.Single that is less than or equal to the specified maximum.
		/// </summary>
		/// <param name="maxValue">The inclusive upper bound of the random value to be generated.</param>
		/// <returns>A random System.Single that is less than or equal to the specified maximum.</returns>
		static public Single NextSingle(Single maxValue)
		{
			return RandomValue.NextSingle(Single.MinValue, maxValue);
		}

		/// <summary>
		/// Returns a random System.Single that is within a specified range.
		/// </summary>
		/// <param name="minValue">The inclusive lower bound of the random number returned.</param>
		/// <param name="maxValue">The inclusive upper bound of the random number returned.</param>
		/// <returns>A System.Single greater than or equal to minValue and less than or equal to maxValue.</returns>
		static public Single NextSingle(Single minValue, Single maxValue)
		{
			int cb = Marshal.SizeOf(typeof(Single));
			Single result = 0;

			byte[] buffer = GetNextBytes(cb);
			result = BitConverter.ToSingle(buffer, 0);

			result = Math.Max(minValue, result);
			result = Math.Min(maxValue, result);
			return result;
		}

		#endregion Single

		#region Double

		/// <summary>
		/// Returns a random System.Double.
		/// </summary>
		/// <returns>A random System.Double.</returns>
		static public Double NextDouble()
		{
			return RandomValue.NextDouble(Double.MinValue, Double.MaxValue);
		}

		/// <summary>
		/// Returns a random System.Double that is less than or equal to the specified maximum.
		/// </summary>
		/// <param name="maxValue">The inclusive upper bound of the random value to be generated.</param>
		/// <returns>A random System.Double that is less than or equal to the specified maximum.</returns>
		static public Double NextDouble(Double maxValue)
		{
			return RandomValue.NextDouble(Double.MinValue, maxValue);
		}

		/// <summary>
		/// Returns a random System.Double that is within a specified range.
		/// </summary>
		/// <param name="minValue">The inclusive lower bound of the random number returned.</param>
		/// <param name="maxValue">The inclusive upper bound of the random number returned.</param>
		/// <returns>A System.Double greater than or equal to minValue and less than or equal to maxValue.</returns>
		static public Double NextDouble(Double minValue, Double maxValue)
		{
			int cb = Marshal.SizeOf(typeof(Double));
			byte[] buffer = GetNextBytes(cb);

			Double result = BitConverter.ToDouble(buffer, 0);
			result = Math.Max(minValue, result);
			result = Math.Min(maxValue, result);
			return result;
		}

		#endregion Double

		#region DateTime

		/// <summary>
		/// Returns a random System.DateTime.
		/// </summary>
		/// <returns>A random System.DateTime.</returns>
		static public DateTime NextDateTime()
		{
			return NextDateTime(DateTime.MinValue, DateTime.MaxValue);
		}

		/// <summary>
		/// Returns a random System.DateTime that is less than or equal to the specified maximum.
		/// </summary>
		/// <param name="maxValue">The inclusive upper bound of the random value to be generated.</param>
		/// <returns>A random System.DateTime that is less than or equal to the specified maximum.</returns>
		static public DateTime NextDateTime(DateTime maxValue)
		{
			return NextDateTime(DateTime.MinValue, maxValue);
		}

		/// <summary>
		/// Returns a random System.DateTime that is within a specified range.
		/// </summary>
		/// <param name="minValue">The inclusive lower bound of the random number returned.</param>
		/// <param name="maxValue">The inclusive upper bound of the random number returned.</param>
		/// <returns>A System.DateTime greater than or equal to minValue and less than or equal to maxValue.</returns>
		static public DateTime NextDateTime(DateTime minValue, DateTime maxValue)
		{
			NormalizeInput(ref minValue, ref maxValue);

			int min = minValue.Year;
			int max = maxValue.Year;
			int year = RandomValue.Next(min, max);  //The year (1 through 9999). 

			min = minValue.Month;
			max = maxValue.Month;
			int month = RandomValue.Next(min, max); //The month (1 through 12). 

			min = minValue.Day;
			max = maxValue.Day;
			int day = RandomValue.Next(min, max);       //The day (1 through the number of days in month).
			day = Math.Min(day, DateTime.DaysInMonth(year, month));

			min = minValue.Hour;
			max = maxValue.Hour;
			int hour = RandomValue.Next(min, max);      //The hours (0 through 23). 

			min = minValue.Minute;
			max = maxValue.Minute;
			int minute = RandomValue.Next(min, max);    //The minutes (0 through 59). 

			min = minValue.Second;
			max = maxValue.Second;
			int second = RandomValue.Next(min, max);    //The seconds (0 through 59). 

			return new DateTime(year, month, day, hour, minute, second);
		}

		#endregion DateTime

		/// <summary>
		/// Returns a random System.Nullable&lt;T&gt;.
		/// </summary>
		/// <typeparam name="T">Generic type parameter.</typeparam>
		/// <param name="limitNulls">Flage to limit the amount of null values returned by the method.</param>
		/// <param name="percentageOfNulls">Used in combination with the "limitNulls" parameter. If "limitNulls" is true, percentageOfNulls specifies the probability that the return value will be null.</param>
		/// <returns>A random System.Nullable&lt;T&gt;.</returns>
		static public Nullable<T> NextNullable<T>(bool limitNulls, byte percentageOfNulls = 50) where T : struct, IConvertible
		{
			var min = GetMinValue<T>();
			var max = GetMaxValue<T>();
			return NextNullable(min, max, limitNulls, percentageOfNulls);
		}

		/// <summary>
		/// Returns a random System.Nullable&lt;T&gt;.
		/// </summary>
		/// <typeparam name="T">Generic type parameter.</typeparam>
		/// <param name="maxValue">The inclusive upper bound of the random value to be generated.</param>
		/// <param name="limitNulls">Flage to limit the amount of null values returned by the method.</param>
		/// <param name="percentageOfNulls">Used in combination with the "limitNulls" parameter. If "limitNulls" is true, percentageOfNulls specifies the probability that the return value will be null.</param>
		/// <returns>A random System.Nullable&lt;T&gt;.</returns>
		static public Nullable<T> NextNullable<T>(T maxValue, bool limitNulls, byte percentageOfNulls = 50) where T : struct, IConvertible
		{
			var minValue = GetMinValue<T>();
			return NextNullable(minValue, maxValue, limitNulls, percentageOfNulls);
		}

		/// <summary>
		/// Returns a random System.Nullable&lt;T&gt;.
		/// </summary>
		/// <typeparam name="T">Generic type parameter.</typeparam>
		/// <param name="minValue">The inclusive lower bound of the random number returned.</param>
		/// <param name="maxValue">The inclusive upper bound of the random value to be generated.</param>
		/// <param name="limitNulls">Flage to limit the amount of null values returned by the method.</param>
		/// <param name="percentageOfNulls">Used in combination with the "limitNulls" parameter. If "limitNulls" is true, percentageOfNulls specifies the probability that the return value will be null.</param>
		/// <returns>A random System.Nullable&lt;T&gt;.</returns>
		static public Nullable<T> NextNullable<T>(T minValue, T maxValue, bool limitNulls, byte percentageOfNulls = 50) where T : struct, IConvertible
		{
			if (100 < percentageOfNulls)
			{
				throw new ArgumentOutOfRangeException("RandomValue cannot give more than 100% of nothing.");
			}
			Nullable<T> result;
			if (limitNulls)
			{
				var val = Next(0, 101);
				if (val < percentageOfNulls)
				{
					result = null;
				}
				else
				{
					result = Next<T>(minValue, maxValue);
				}
			}
			else
			{
				result = Next<T>(minValue, maxValue);
			}
			return result;
		}

		/// <summary>
		/// Returns a random value of the specified enumeration type.
		/// </summary>
		/// <typeparam name="T">The type of enum value to return.</typeparam>
		/// <returns>A random value of the specified enumeration type.</returns>
		static public T GetRandomEnumValue<T>()
		{
			Array arr = Enum.GetValues(typeof(T));
			return (T)arr.GetValue(RandomValue.Next(0, arr.Length));
		}

		static void NormalizeInput<T>(ref T minValue, ref T maxValue) where T : struct, IComparable
		{
			if (minValue.CompareTo(maxValue) > 0)
			{
				T tmp = minValue;
				minValue = maxValue;
				maxValue = tmp;
			}
		}

		static byte[] GetNextBytes(int cb)
		{
			lock (_random)
			{
				byte[] buffer = new byte[cb];
				_random.NextBytes(buffer);
				return buffer;
			}
		}

		static int GetNext(int minValue, int maxValue)
		{
			lock (_random)
			{
				return _random.Next(minValue, maxValue);
			}
		}

		static T GetMinValue<T>() where T : struct
		{
			T result = default(T);
			if (typeof(T) == typeof(bool))
			{
				result = (dynamic)false;
			}
			else
			{
				FieldInfo fi = typeof(T).GetField(MIN_VALUE);
				Debug.Assert(null != fi);
				result = (T)fi.GetValue(null);
			}
			return result;
		}

		static T GetMaxValue<T>() where T : struct, IConvertible
		{
			T result = default(T);
			if (typeof(T) == typeof(bool))
			{
				result = (dynamic)true;
			}
			else
			{
				FieldInfo fi = typeof(T).GetField(MAX_VALUE);
				Debug.Assert(null != fi);
				result = (T)fi.GetValue(null);
			}
			return result;
		}
	}//class
}//ns

