using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Eric.Morrison
{
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

		static public T Next<T>() where T : struct, IConvertible
		{
			object result = null;
			try
			{
				Type type = typeof(T);

				if (typeof(Boolean) == type)
				{
					result = (T)(object)RandomValue.Next<bool>(false, true);
				}
				else
				{
					T minSupportedValue;
					FieldInfo fi = typeof(T).GetField(MIN_VALUE);
					Debug.Assert(null != fi);
					minSupportedValue = (T)fi.GetValue(null);

					T maxSupportedValue;
					fi = typeof(T).GetField(MAX_VALUE);
					Debug.Assert(null != fi);
					maxSupportedValue = (T)fi.GetValue(null);

					result = RandomValue.Next(minSupportedValue, maxSupportedValue);
				}
			}
			catch(Exception)
			{
				throw;
			}
			return (T)result;
		}

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

		static public T Next<T>(T minValue, T maxValue) where T : struct, IConvertible
		{
			object result = null;
			Type type = typeof(T);
			T retval = default(T);

			try
			{
				switch (type.ToString())
				{
					case "System.Boolean"://Boolean 
						result = RandomValue.NextBoolean(Convert.ToBoolean(minValue), Convert.ToBoolean(maxValue));
						break;

					// 8 bit data types
					case "System.SByte"://SByte
						result = RandomValue.NextSByte(Convert.ToSByte(minValue), Convert.ToSByte(maxValue));
						break;
					case "System.Byte"://Byte
						result = RandomValue.NextByte(Convert.ToByte(minValue), Convert.ToByte(maxValue));
						break;

					// 16 bit data types
					case "System.Char"://Char    
						result = RandomValue.NextChar(Convert.ToChar(minValue), Convert.ToChar(maxValue));
						break;

					case "System.Int16"://Int16
						result = RandomValue.NextInt16(Convert.ToInt16(minValue), Convert.ToInt16(maxValue));
						break;

					case "System.UInt16"://UInt16  
						result = RandomValue.NextUInt16(Convert.ToUInt16(minValue), Convert.ToUInt16(maxValue));
						break;

					// 32 bit data types
					case "System.Int32"://Int32   
						result = RandomValue.NextInt32(Convert.ToInt32(minValue), Convert.ToInt32(maxValue));
						break;

					case "System.UInt32"://UInt32  
						result = RandomValue.NextUInt32(Convert.ToUInt32(minValue), Convert.ToUInt32(maxValue));
						break;

					case "System.Single"://Single  
						result = RandomValue.NextSingle(Convert.ToSingle(minValue), Convert.ToSingle(maxValue));
						break;

					// 64 bit data types
					case "System.Int64"://Int64   
						result = RandomValue.NextInt64(Convert.ToInt64(minValue), Convert.ToInt64(maxValue));
						break;

					case "System.UInt64"://UInt64  
						result = RandomValue.NextUInt64(Convert.ToUInt64(minValue), Convert.ToUInt64(maxValue));
						break;

					case "System.Double"://Double  
						result = RandomValue.NextDouble(Convert.ToDouble(minValue), Convert.ToDouble(maxValue));
						break;

					case "System.Decimal"://Decimal 
						result = RandomValue.NextDecimal(Convert.ToDecimal(minValue), Convert.ToDecimal(maxValue));
						break;

					case "System.DateTime":
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

		static public bool NextBoolean()
		{
			return RandomValue.NextBoolean(false, true);
		}

		static public bool NextBoolean(bool maxValue)
		{
			return RandomValue.NextBoolean(false, maxValue);
		}

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

		static public SByte NextSByte()
		{
			return RandomValue.NextSByte(SByte.MinValue, SByte.MaxValue);
		}

		static public SByte NextSByte(SByte maxValue)
		{
			return RandomValue.NextSByte(SByte.MinValue, maxValue);
		}

		static public SByte NextSByte(SByte minValue, SByte maxValue)
		{
			NormalizeInput(ref minValue, ref maxValue);
			return (SByte)GetNext(minValue, maxValue);
		}

		#endregion SByte

		#region Byte

		static public Byte NextByte()
		{
			return RandomValue.NextByte(Byte.MinValue, Byte.MaxValue);
		}

		static public Byte NextByte(Byte maxValue)
		{
			return RandomValue.NextByte(Byte.MinValue, maxValue);
		}

		static public Byte NextByte(Byte minValue, Byte maxValue)
		{
			NormalizeInput(ref minValue, ref maxValue);
			return (Byte)GetNext(minValue, maxValue);
		}

		#endregion Byte

		#region Char

		static public Char NextChar()
		{
			return RandomValue.NextChar(Char.MinValue, Char.MaxValue);
		}

		static public Char NextChar(Char maxValue)
		{
			return RandomValue.NextChar(Char.MinValue, maxValue);
		}

		static public Char NextChar(Char minValue, Char maxValue)
		{
			NormalizeInput(ref minValue, ref maxValue);
			return (Char)GetNext(minValue, maxValue);
		}

		#endregion Char

		#region Int16

		static public Int16 NextInt16()
		{
			return RandomValue.NextInt16(Int16.MinValue, Int16.MaxValue);
		}

		static public Int16 NextInt16(Int16 maxValue)
		{
			return RandomValue.NextInt16(Int16.MinValue, maxValue);
		}

		static public Int16 NextInt16(Int16 minValue, Int16 maxValue)
		{
			NormalizeInput(ref minValue, ref maxValue);
			return (Int16)GetNext(minValue, maxValue);
		}

		#endregion Int16

		#region UInt16

		static public UInt16 NextUInt16()
		{
			return RandomValue.NextUInt16(UInt16.MinValue, UInt16.MaxValue);
		}

		static public UInt16 NextUInt16(UInt16 maxValue)
		{
			return RandomValue.NextUInt16(UInt16.MinValue, maxValue);
		}

		static public UInt16 NextUInt16(UInt16 minValue, UInt16 maxValue)
		{
			NormalizeInput(ref minValue, ref maxValue);
			return (UInt16)GetNext(minValue, maxValue);
		}

		#endregion UInt16

		#region Int32

		static public Int32 Next()
		{
			return RandomValue.NextInt32();
		}

		static public Int32 Next(Int32 maxValue)
		{
			return RandomValue.NextInt32(maxValue);
		}

		static public Int32 Next(Int32 minValue, Int32 maxValue)
		{
			return RandomValue.NextInt32(minValue, maxValue);
		}

		static public Int32 NextInt32()
		{
			return RandomValue.NextInt32(Int32.MinValue, Int32.MaxValue);
		}

		static public Int32 NextInt32(Int32 maxValue)
		{
			return RandomValue.NextInt32(Int32.MinValue, maxValue);
		}

		static public Int32 NextInt32(Int32 minValue, Int32 maxValue)
		{
			NormalizeInput(ref minValue, ref maxValue);
			return (Int32)GetNext(minValue, maxValue);
		}

		#endregion Int32

		#region UInt32

		static public UInt32 NextUInt32()
		{
			return RandomValue.NextUInt32(UInt32.MinValue, UInt32.MaxValue);
		}

		static public UInt32 NextUInt32(UInt32 maxValue)
		{
			return RandomValue.NextUInt32(UInt32.MinValue, maxValue);
		}

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

		static public Int64 NextInt64()
		{
			return RandomValue.NextInt64(Int64.MinValue, Int64.MaxValue);
		}

		static public Int64 NextInt64(Int64 maxValue)
		{
			return RandomValue.NextInt64(Int64.MinValue, maxValue);
		}

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

		static public UInt64 NextUInt64()
		{
			return RandomValue.NextUInt64(UInt64.MinValue, UInt64.MaxValue);
		}

		static public UInt64 NextUInt64(UInt64 maxValue)
		{
			return RandomValue.NextUInt64(UInt64.MinValue, maxValue);
		}

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

		static public Decimal NextDecimal()
		{
			return RandomValue.NextDecimal(Decimal.MinValue, Decimal.MaxValue);
		}

		static public Decimal NextDecimal(Decimal maxValue)
		{
			return RandomValue.NextDecimal(Decimal.MinValue, maxValue);
		}

		static public Decimal NextDecimal(Decimal minValue, Decimal maxValue)
		{
			int cb = Marshal.SizeOf(typeof(Decimal));
			byte[] buffer = GetNextBytes(cb);

			int [] ints = new int[4];

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

		static public Single NextSingle()
		{
			return RandomValue.NextSingle(Single.MinValue, Single.MaxValue);
		}

		static public Single NextSingle(Single maxValue)
		{
			return RandomValue.NextSingle(Single.MinValue, maxValue);
		}

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

		static public Double NextDouble()
		{
			return RandomValue.NextDouble(Double.MinValue, Double.MaxValue);
		}

		static public Double NextDouble(Double maxValue)
		{
			return RandomValue.NextDouble(Double.MinValue, maxValue);
		}

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

		static public DateTime NextDateTime()
		{
			return NextDateTime(DateTime.MinValue, DateTime.MaxValue);
		}

		static public DateTime NextDateTime(DateTime maxValue)
		{
			return NextDateTime(DateTime.MinValue, maxValue);
		}

		static public DateTime NextDateTime(DateTime minValue, DateTime maxValue)
		{
			NormalizeInput(ref minValue, ref maxValue);

			int min = minValue.Year;
			int max = maxValue.Year;
			int year = RandomValue.Next(min, max);	//The year (1 through 9999). 

			min = minValue.Month;
			max = maxValue.Month;
			int month = RandomValue.Next(min, max);	//The month (1 through 12). 

			min = minValue.Day;
			max = maxValue.Day;
			int day = RandomValue.Next(min, max);		//The day (1 through the number of days in month).
			day = Math.Min(day, DateTime.DaysInMonth(year, month));

			min = minValue.Hour;
			max = maxValue.Hour;
			int hour = RandomValue.Next(min, max);		//The hours (0 through 23). 

			min = minValue.Minute;
			max = maxValue.Minute;
			int minute = RandomValue.Next(min, max);	//The minutes (0 through 59). 

			min = minValue.Second;
			max = maxValue.Second;
			int second = RandomValue.Next(min, max);	//The seconds (0 through 59). 

			return new DateTime(year, month, day, hour, minute, second);
		}

		#endregion DateTime

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

		static public T GetRandomEnumValue<T>()
		{
			Array arr = Enum.GetValues(typeof(T));
			return (T)arr.GetValue(RandomValue.Next(0, arr.Length));
		}


	}
}

