using System;
using System.Diagnostics;
using Eric.Morrison;
using System.Text;
using System.Collections.Generic;

#if !NUNIT
using Microsoft.VisualStudio.TestTools.UnitTesting;
#else
using NUnit.Framework;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestMethod = NUnit.Framework.TestAttribute;
using TestInitialize = NUnit.Framework.SetUpAttribute;
using TestCleanup = NUnit.Framework.TearDownAttribute;
#endif



namespace TestProject_01
{

	/// <summary>
	///This is a test class for RandomValueTest and is intended
	///to contain all RandomValueTest Unit Tests
	///</summary>
	[TestClass()]
	public class RandomValueTest
	{
		//private TestContext testContextInstance;

		///// <summary>
		/////Gets or sets the test context which provides
		/////information about and functionality for the current test run.
		/////</summary>
		//public TestContext TestContext
		//{
		//    get
		//    {
		//        return testContextInstance;
		//    }
		//    set
		//    {
		//        testContextInstance = value;
		//    }
		//}

		#region Additional test attributes
		// 
		//You can use the following additional attributes as you write your tests:
		//
		//Use ClassInitialize to run code before running the first test in the class
		//[ClassInitialize()]
		//public static void MyClassInitialize(TestContext testContext)
		//{
		//}
		//
		//Use ClassCleanup to run code after all tests in a class have run
		//[ClassCleanup()]
		//public static void MyClassCleanup()
		//{
		//}
		//
		//Use TestInitialize to run code before running each test
		//[TestInitialize()]
		//public void MyTestInitialize()
		//{
		//}
		//
		//Use TestCleanup to run code after each test has run
		//[TestCleanup()]
		//public void MyTestCleanup()
		//{
		//}
		//
		#endregion

		/// <summary>
		///A test for Next
		///</summary>
		[TestMethod()]
		public void NextTest1()
		{
			{
				sbyte max = RandomValue.Next<sbyte>();
				sbyte result = RandomValue.Next<sbyte>(max);
				Assert.IsTrue(result <= max);
			}
			{
				bool max = RandomValue.Next<bool>();
				bool result = RandomValue.Next<bool>(true);
				Assert.IsTrue((result == true) || (result == false));
			}
			{
				byte max = RandomValue.Next<byte>();
				byte result = RandomValue.Next<byte>(max);
				Assert.IsTrue(result <= max);
			}
			{
				Char max = RandomValue.Next<Char>();
				Char result = RandomValue.Next<Char>(max);
				Assert.IsTrue(result <= max);
			}
			{
				Int16 max = RandomValue.Next<Int16>();
				Int16 result = RandomValue.Next<Int16>(max);
				Assert.IsTrue(result <= max);
			}
			{
				UInt16 max = RandomValue.Next<UInt16>();
				UInt16 result = RandomValue.Next<UInt16>(max);
				Assert.IsTrue(result <= max);
			}
			{
				Int32 max = RandomValue.Next<Int32>();
				Int32 result = RandomValue.Next<Int32>(max);
				Assert.IsTrue(result <= max);
			}
			{
				UInt32 max = RandomValue.Next<UInt32>();
				UInt32 result = RandomValue.Next<UInt32>(max);
				Assert.IsTrue(result <= max);
			}
			{
				Single max = RandomValue.Next<Single>();
				Single result = RandomValue.Next<Single>(max);
				Assert.IsTrue(result <= max);
			}
			{
				Int64 max = RandomValue.Next<Int64>();
				Int64 result = RandomValue.Next<Int64>(max);
				Assert.IsTrue(result <= max);
			}
			{
				UInt64 max = RandomValue.Next<UInt64>();
				UInt64 result = RandomValue.Next<UInt64>(max);
				Assert.IsTrue(result <= max);
			}
			{
				Double max = RandomValue.Next<Double>();
				Double result = RandomValue.Next<Double>(max);
				Assert.IsTrue(result <= max);
			}
			{
				Decimal max = RandomValue.Next<Decimal>();
				Decimal result = RandomValue.Next<Decimal>(max);
				Assert.IsTrue(result <= max);
			}
			{
				DateTime max = RandomValue.Next<DateTime>();
				DateTime result = RandomValue.Next<DateTime>(max);
				Assert.IsTrue(result <= max);
			}
		}

		[TestMethod()]
		public void NullableTest01()
		{
			const int MAX = 1000;
			int hasValue = 0, isNull = 0;
			for (int i = 0; i < MAX; ++i)
			{
				var n = RandomValue.NextNullable<decimal>(true);
				if (n.HasValue)
					++hasValue;
				else
					++isNull;
			}
			Debug.WriteLine("hasValue={0}, isNull={1}", hasValue, isNull);
		}

		class Address
		{
			public string Name { get; set; }
			public string AddressLineOne { get; set; }
			public string AddressLineTwo { get; set; }
			public string City { get; set; }
			public string State { get; set; }
			public string ZipCode { get; set; }

			public override string ToString()
			{
				var sb = new StringBuilder();
				sb.AppendLine(this.Name);
				sb.AppendLine(this.AddressLineOne);
				if (!string.IsNullOrEmpty(this.AddressLineTwo))
					sb.AppendLine(this.AddressLineTwo);
				sb.AppendFormat("{0}, {1} {2}", this.City, this.State, this.ZipCode);
				return sb.ToString();
			}
		}

		[TestMethod()]
		public void AddressTest()
		{
			var streets = new List<string>() { "Alma Rd", "Park Blvd", "Parker Rd", "Elm St", "Main St" };
			var cities = new List<string>() { "Dallas", "Austin", "Corpus Cristi", "El Paso", "Abeleine" };
			var states = new List<string>() { "TX", "OK" };
			var randomStreets = new RandomList<string>(streets);
			var randomCities = new RandomList<string>(cities);
			var randomStates = new RandomList<string>(states);
			const int MAX = 100;
			for (int i = 0; i < MAX; ++i)
			{
				var address = new Address
				{
					Name = RandomName.NextFirstAndLast(RandomName.Gender.Male),
					AddressLineOne = string.Format("{0} {1}", RandomValue.Next(100, 20000), randomStreets.Next()),
					City = randomCities.Next(),
					State = randomStates.Next(),
					ZipCode = RandomValue.Next(10000, 99999).ToString()
				};

				Debug.WriteLine(address);
				Debug.WriteLine(String.Empty);
			}
		}


		[TestMethod()]
		public void BoolTest()
		{
			const int MAX = 1000;
			int isTrue = 0, isFalse = 0;
			for (int i = 0; i < MAX; ++i)
			{
				var b = RandomValue.Next<bool>(false, true);
				if (b)
					++isTrue;
				else
					++isFalse;
			}
			Debug.WriteLine("isTrue={0}, isFalse={1}", isTrue, isFalse);
		}

		T NextTest<T>() where T : struct, IConvertible
		{
			var result = RandomValue.Next<T>();
			return result;
		}

		[TestMethod()]
		public void NextTest()
		{
			{
				sbyte result = RandomValue.Next<sbyte>();
				Assert.IsTrue(result >= sbyte.MinValue && result <= sbyte.MaxValue);
			}
			{
				bool result = RandomValue.Next<bool>();
				Assert.IsTrue((result == true) || (result == false));
			}
			{
				byte result = RandomValue.Next<byte>();
				Assert.IsTrue(result >= byte.MinValue && result <= byte.MaxValue);
			}
			{
				Char result = RandomValue.Next<Char>();
				Assert.IsTrue(result >= Char.MinValue && result <= Char.MaxValue);
			}
			{
				Int16 result = RandomValue.Next<Int16>();
				Assert.IsTrue(result >= Int16.MinValue && result <= Int16.MaxValue);
			}
			{
				UInt16 result = RandomValue.Next<UInt16>();
				Assert.IsTrue(result >= UInt16.MinValue && result <= UInt16.MaxValue);
			}
			{
				Int32 result = RandomValue.Next<Int32>();
				Assert.IsTrue(result >= Int32.MinValue && result <= Int32.MaxValue);
			}
			{
				UInt32 result = RandomValue.Next<UInt32>();
				Assert.IsTrue(result >= UInt32.MinValue && result <= UInt32.MaxValue);
			}
			{
				Single result = RandomValue.Next<Single>();
				Assert.IsTrue(result >= Single.MinValue && result <= Single.MaxValue);
			}
			{
				Int64 result = RandomValue.Next<Int64>();
				Assert.IsTrue(result >= Int64.MinValue && result <= Int64.MaxValue);
			}
			{
				UInt64 result = RandomValue.Next<UInt64>();
				Assert.IsTrue(result >= UInt64.MinValue && result <= UInt64.MaxValue);
			}
			{
				Double result = RandomValue.Next<Double>();
				Assert.IsTrue(result >= Double.MinValue && result <= Double.MaxValue);
			}
			{
				Decimal result = RandomValue.Next<Decimal>();
				Assert.IsTrue(result >= Decimal.MinValue && result <= Decimal.MaxValue);
			}
			{
				DateTime result = RandomValue.Next<DateTime>();
				Assert.IsTrue(result >= DateTime.MinValue && result <= DateTime.MaxValue);
			}
		}

		/// <summary>
		///A test for NextUInt64
		///</summary>
		[TestMethod()]
		public void NextUInt64Test2()
		{
			UInt64 actual = RandomValue.NextUInt64();
			Assert.IsTrue(actual >= UInt64.MinValue
				&& actual <= UInt64.MaxValue);
		}

		/// <summary>
		///A test for NextUInt64
		///</summary>
		[TestMethod()]
		public void NextUInt64Test1()
		{
			UInt64 minValue = RandomValue.NextUInt64();
			UInt64 maxValue = RandomValue.NextUInt64();
			UInt64 actual = RandomValue.NextUInt64(minValue, maxValue);

			Assert.IsTrue(actual >= Math.Min(minValue, maxValue) && actual <= Math.Max(minValue, maxValue));
		}

		/// <summary>
		///A test for NextUInt64
		///</summary>
		[TestMethod()]
		public void NextUInt64Test()
		{
			UInt64 maxValue = RandomValue.NextUInt64();
			UInt64 actual = RandomValue.NextUInt64(maxValue);
			Assert.IsTrue(actual <= maxValue);
		}

		/// <summary>
		///A test for NextUInt32
		///</summary>
		[TestMethod()]
		public void NextUInt32Test2()
		{
			UInt32 actual = RandomValue.NextUInt32();
			Assert.IsTrue(actual >= UInt32.MinValue
				&& actual <= UInt32.MaxValue);
		}

		/// <summary>
		///A test for NextUInt32
		///</summary>
		[TestMethod()]
		public void NextUInt32Test1()
		{
			UInt32 maxValue = RandomValue.NextUInt32();
			UInt32 actual = RandomValue.NextUInt32(maxValue);
			Assert.IsTrue(actual <= maxValue);
		}

		/// <summary>
		///A test for NextUInt32
		///</summary>
		[TestMethod()]
		public void NextUInt32Test()
		{
			UInt32 minValue = RandomValue.NextUInt32();
			UInt32 maxValue = RandomValue.NextUInt32();
			UInt32 actual = RandomValue.NextUInt32(minValue, maxValue);

			Assert.IsTrue(actual >= Math.Min(minValue, maxValue) && actual <= Math.Max(minValue, maxValue));
		}

		/// <summary>
		///A test for NextUInt16
		///</summary>
		[TestMethod()]
		public void NextUInt16Test2()
		{
			UInt16 minValue = RandomValue.NextUInt16();
			UInt16 maxValue = RandomValue.NextUInt16();
			UInt16 actual = RandomValue.NextUInt16(minValue, maxValue);

			Assert.IsTrue(actual >= Math.Min(minValue, maxValue) && actual <= Math.Max(minValue, maxValue));
		}

		/// <summary>
		///A test for NextUInt16
		///</summary>
		[TestMethod()]
		public void NextUInt16Test1()
		{
			UInt16 actual = RandomValue.NextUInt16();
			Assert.IsTrue(actual >= UInt16.MinValue
				&& actual <= UInt16.MaxValue);
		}

		/// <summary>
		///A test for NextUInt16
		///</summary>
		[TestMethod()]
		public void NextUInt16Test()
		{
			UInt16 maxValue = RandomValue.NextUInt16();
			UInt16 actual = RandomValue.NextUInt16(maxValue);
			Assert.IsTrue(actual <= maxValue);
		}

		/// <summary>
		///A test for NextSingle
		///</summary>
		[TestMethod()]
		public void NextSingleTest2()
		{
			Single actual = RandomValue.NextSingle();
			Assert.IsTrue(actual >= Single.MinValue
				&& actual <= Single.MaxValue);
		}

		/// <summary>
		///A test for NextSingle
		///</summary>
		[TestMethod()]
		public void NextSingleTest1()
		{
			Single minValue = RandomValue.NextSingle();
			Single maxValue = RandomValue.NextSingle();
			Single actual = RandomValue.NextSingle(minValue, maxValue);

			Single min = Math.Min(minValue, maxValue);
			Single max = Math.Max(minValue, maxValue);
			Assert.IsTrue(actual >= min && actual <= max, string.Format("min={0}, max={1}", min, max));
		}

		/// <summary>
		///A test for NextSingle
		///</summary>
		[TestMethod()]
		public void NextSingleTest()
		{
			float maxValue = RandomValue.NextSingle();
			float actual = RandomValue.NextSingle(maxValue);
			Assert.IsTrue(actual <= maxValue);
		}

		/// <summary>
		///A test for NextSByte
		///</summary>
		[TestMethod()]
		public void NextSByteTest2()
		{
			SByte maxValue = RandomValue.NextSByte();
			SByte actual = RandomValue.NextSByte(maxValue);
			Assert.IsTrue(actual <= maxValue);

			maxValue = SByte.MinValue;
			actual = RandomValue.NextSByte(maxValue);
			Assert.IsTrue(actual <= maxValue);

		}

		/// <summary>
		///A test for NextSByte
		///</summary>
		[TestMethod()]
		public void NextSByteTest1()
		{
			SByte minValue = RandomValue.NextSByte();
			SByte maxValue = RandomValue.NextSByte();
			SByte actual = RandomValue.NextSByte(minValue, maxValue);

			Assert.IsTrue(actual >= Math.Min(minValue, maxValue) && actual <= Math.Max(minValue, maxValue));
		}

		/// <summary>
		///A test for NextSByte
		///</summary>
		[TestMethod()]
		public void NextSByteTest()
		{
			SByte actual = RandomValue.NextSByte();
			Assert.IsTrue(actual >= SByte.MinValue
				&& actual <= SByte.MaxValue);
		}

		/// <summary>
		///A test for NextInt64
		///</summary>
		[TestMethod()]
		public void NextInt64Test2()
		{
			Int64 actual = RandomValue.NextInt64();
			Assert.IsTrue(actual >= Int64.MinValue
				&& actual <= Int64.MaxValue);
		}

		/// <summary>
		///A test for NextInt64
		///</summary>
		[TestMethod()]
		public void NextInt64Test1()
		{
			Int64 minValue = RandomValue.NextInt64();
			Int64 maxValue = RandomValue.NextInt64();
			Int64 actual = RandomValue.NextInt64(minValue, maxValue);

			Assert.IsTrue(actual >= Math.Min(minValue, maxValue) && actual <= Math.Max(minValue, maxValue));
		}

		/// <summary>
		///A test for NextInt64
		///</summary>
		[TestMethod()]
		public void NextInt64Test()
		{
			Int64 maxValue = RandomValue.NextInt64();
			Int64 actual = RandomValue.NextInt64(maxValue);
			Assert.IsTrue(actual <= maxValue);
		}

		/// <summary>
		///A test for NextInt32
		///</summary>
		[TestMethod()]
		public void NextInt32Test2()
		{
			Int32 maxValue = RandomValue.NextInt32();
			Int32 actual = RandomValue.NextInt32(maxValue);
			Assert.IsTrue(actual <= maxValue);
		}

		/// <summary>
		///A test for NextInt32
		///</summary>
		[TestMethod()]
		public void NextInt32Test1()
		{
			Int32 minValue = RandomValue.NextInt32();
			Int32 maxValue = RandomValue.NextInt32();
			Int32 actual = RandomValue.NextInt32(minValue, maxValue);

			Assert.IsTrue(actual >= Math.Min(minValue, maxValue) && actual <= Math.Max(minValue, maxValue));
		}

		/// <summary>
		///A test for NextInt32
		///</summary>
		[TestMethod()]
		public void NextInt32Test()
		{
			Int32 actual = RandomValue.NextInt32();
			Assert.IsTrue(actual >= Int32.MinValue
				&& actual <= Int32.MaxValue);
		}

		/// <summary>
		///A test for NextInt16
		///</summary>
		[TestMethod()]
		public void NextInt16Test2()
		{
			Int16 actual = RandomValue.NextInt16();
			Assert.IsTrue(actual >= Int16.MinValue
				&& actual <= Int16.MaxValue);
		}

		/// <summary>
		///A test for NextInt16
		///</summary>
		[TestMethod()]
		public void NextInt16Test1()
		{
			Int16 maxValue = RandomValue.NextInt16();
			Int16 actual = RandomValue.NextInt16(maxValue);
			Assert.IsTrue(actual <= maxValue);
		}

		/// <summary>
		///A test for NextInt16
		///</summary>
		[TestMethod()]
		public void NextInt16Test()
		{
			Int16 minValue = RandomValue.NextInt16();
			Int16 maxValue = RandomValue.NextInt16();
			Int16 actual = RandomValue.NextInt16(minValue, maxValue);

			Assert.IsTrue(actual >= Math.Min(minValue, maxValue) && actual <= Math.Max(minValue, maxValue));
		}

		/// <summary>
		///A test for NextDouble
		///</summary>
		[TestMethod()]
		public void NextDoubleTest2()
		{
			Double maxValue = RandomValue.NextDouble();
			Double actual = RandomValue.NextDouble(maxValue);
			Assert.IsTrue(actual <= maxValue);
		}

		/// <summary>
		///A test for NextDouble
		///</summary>
		[TestMethod()]
		public void NextDoubleTest1()
		{
			Double minValue = RandomValue.NextDouble();
			Double maxValue = RandomValue.NextDouble();
			Double actual = RandomValue.NextDouble(minValue, maxValue);

			Assert.IsTrue(actual >= Math.Min(minValue, maxValue) && actual <= Math.Max(minValue, maxValue));
		}

		/// <summary>
		///A test for NextDouble
		///</summary>
		[TestMethod()]
		public void NextDoubleTest()
		{
			Double actual = RandomValue.NextDouble();
			Assert.IsTrue(actual >= Double.MinValue
				&& actual <= Double.MaxValue);
		}

		/// <summary>
		///A test for NextDecimal
		///</summary>
		[TestMethod()]
		public void NextDecimalTest1()
		{
			Decimal actual = RandomValue.NextDecimal();
			Assert.IsTrue(actual >= Decimal.MinValue
				&& actual <= Decimal.MaxValue);
		}

		/// <summary>
		///A test for NextDecimal
		///</summary>
		[TestMethod()]
		public void NextDecimalTest2()
		{
			Decimal minValue = RandomValue.NextDecimal();
			Decimal maxValue = RandomValue.NextDecimal();
			Decimal actual = RandomValue.NextDecimal(minValue, maxValue);

			Assert.IsTrue(actual <= Math.Max(minValue, maxValue));
		}

		/// <summary>
		///A test for NextDecimal
		///</summary>
		[TestMethod()]
		public void NextDecimalTest()
		{
			Decimal minValue = RandomValue.NextDecimal();
			Decimal maxValue = RandomValue.NextDecimal();
			Decimal actual = RandomValue.NextDecimal(minValue, maxValue);

			Assert.IsTrue(actual >= Math.Min(minValue, maxValue) && actual <= Math.Max(minValue, maxValue));
		}

		/// <summary>
		///A test for NextDateTime
		///</summary>
		[TestMethod()]
		public void NextDateTimeTest()
		{
			DateTime actual = RandomValue.NextDateTime();
			Assert.IsTrue(actual >= DateTime.MinValue
				&& actual <= DateTime.MaxValue);
		}

		/// <summary>
		///A test for NextChar
		///</summary>
		[TestMethod()]
		public void NextCharTest2()
		{
			Char actual = RandomValue.NextChar();
			Assert.IsTrue(actual >= Char.MinValue
				&& actual <= Char.MaxValue);
		}

		/// <summary>
		///A test for NextChar
		///</summary>
		[TestMethod()]
		public void NextCharTest1()
		{
			Char maxValue = RandomValue.NextChar();
			Char actual = RandomValue.NextChar(maxValue);
			Assert.IsTrue(actual <= maxValue);
		}

		/// <summary>
		///A test for NextChar
		///</summary>
		[TestMethod()]
		public void NextCharTest()
		{
			char actual = RandomValue.NextChar();
			Assert.IsTrue(actual >= char.MinValue
				&& actual <= char.MaxValue);
		}

		/// <summary>
		///A test for NextByte
		///</summary>
		[TestMethod()]
		public void NextByteTest2()
		{
			Byte minValue = RandomValue.NextByte();
			Byte maxValue = RandomValue.NextByte();
			Byte actual = RandomValue.NextByte(minValue, maxValue);

			Assert.IsTrue(actual >= Math.Min(minValue, maxValue) && actual <= Math.Max(minValue, maxValue));
		}

		/// <summary>
		///A test for NextByte
		///</summary>
		[TestMethod()]
		public void NextByteTest1()
		{
			byte maxValue = RandomValue.NextByte(); ;
			byte actual = RandomValue.NextByte(maxValue);
			Assert.IsTrue(actual <= maxValue);
		}

		/// <summary>
		///A test for NextByte
		///</summary>
		[TestMethod()]
		public void NextByteTest()
		{
			byte actual = RandomValue.NextByte();
			Assert.IsTrue(actual >= byte.MinValue
				&& actual <= byte.MaxValue);
		}

		/// <summary>
		///A test for NextBoolean
		///</summary>
		[TestMethod()]
		public void NextBooleanTest2()
		{
			bool minValue = false;
			bool maxValue = false;
			bool expected = false;

			bool actual = RandomValue.NextBoolean(minValue, maxValue);
			Assert.AreEqual(expected, actual);


			minValue = true;
			maxValue = true;
			expected = true;

			actual = RandomValue.NextBoolean(minValue, maxValue);
			Assert.AreEqual(expected, actual);

			minValue = false;
			maxValue = true;

			actual = RandomValue.NextBoolean(minValue, maxValue);
			Assert.IsTrue((actual == true) || (actual == false));

		}

		/// <summary>
		///A test for NextBoolean
		///</summary>
		[TestMethod()]
		public void NextBooleanTest1()
		{
			bool maxValue = false;
			bool actual = RandomValue.NextBoolean(maxValue);
			Assert.AreEqual(false, actual);

			maxValue = true;
			actual = RandomValue.NextBoolean(maxValue);
			Assert.IsTrue((actual == true) || (actual == false));
		}

		/// <summary>
		///A test for NextBoolean
		///</summary>
		[TestMethod()]
		public void NextBooleanTest()
		{
			bool actual = RandomValue.NextBoolean();
			Assert.IsTrue((actual == true) || (actual == false));
		}

		/// <summary>
		///A test for Next
		///</summary>
		public void NextTest7Helper<T>()
			where T : struct, IConvertible, IComparable
		{
			T min = RandomValue.Next<T>();
			T max = RandomValue.Next<T>();
			T tmp;
			if (min.CompareTo(max) > 0)
			{
				tmp = max;
				max = min;
				min = tmp;
			}

			T result = RandomValue.Next<T>(min, max);

			Assert.IsTrue(result.CompareTo(min) >= 0, string.Format("result = {0}, min = {1}", result, min));
			Assert.IsTrue(result.CompareTo(max) <= 0, string.Format("result = {0}, max = {1}", result, max));
		}

		[TestMethod()]
		public void NextTest7()
		{
			NextTest7Helper<Boolean>();
			NextTest7Helper<SByte>();
			NextTest7Helper<Byte>();
			NextTest7Helper<Char>();
			NextTest7Helper<Int16>();
			NextTest7Helper<UInt16>();
			NextTest7Helper<Int32>();
			NextTest7Helper<UInt32>();
			NextTest7Helper<Int64>();
			NextTest7Helper<UInt64>();
			NextTest7Helper<Decimal>();
			NextTest7Helper<Single>();
			NextTest7Helper<Double>();
			NextTest7Helper<DateTime>();
		}

		/// <summary>
		///A test for Next
		///</summary>
		public void NextTest6Helper<T>()
			where T : struct, IConvertible, IComparable
		{
			//Results of this test are not predictable.
			//Simply test that the correct type is returned and that we don't crash.

			string actual = RandomValue.Next<T>().GetType().ToString();
			string expected = typeof(T).ToString();
			Assert.AreEqual(expected, actual);
		}

		[TestMethod()]
		public void NextTest6()
		{
			NextTest6Helper<Boolean>();
			NextTest6Helper<SByte>();
			NextTest6Helper<Byte>();
			NextTest6Helper<Char>();
			NextTest6Helper<Int16>();
			NextTest6Helper<UInt16>();
			NextTest6Helper<Int32>();
			NextTest6Helper<UInt32>();
			NextTest6Helper<Int64>();
			NextTest6Helper<UInt64>();
			NextTest6Helper<Decimal>();
			NextTest6Helper<Single>();
			NextTest6Helper<Double>();
		}

		/// <summary>
		///A test for Next
		///</summary>
		public void NextTest5Helper<T>()
			where T : struct, IConvertible, IComparable
		{
			T max = RandomValue.Next<T>();
			T result = RandomValue.Next<T>(max);
			Assert.IsTrue(result.CompareTo(max) <= 0);
		}

		[TestMethod()]
		public void NextTest5()
		{
			NextTest5Helper<Boolean>();
			NextTest5Helper<SByte>();
			NextTest5Helper<Byte>();
			NextTest5Helper<Char>();
			NextTest5Helper<Int16>();
			NextTest5Helper<UInt16>();
			NextTest5Helper<Int32>();
			NextTest5Helper<UInt32>();
			NextTest5Helper<Int64>();
			NextTest5Helper<UInt64>();
			NextTest5Helper<Decimal>();
			NextTest5Helper<Single>();
			NextTest5Helper<Double>();
		}

		/// <summary>
		///A test for Next
		///</summary>
		[TestMethod()]
		public void NextTest4()
		{
			int minValue = RandomValue.Next();
			int maxValue = RandomValue.Next();
			int actual = RandomValue.Next(minValue, maxValue);

			Assert.IsTrue(actual >= Math.Min(minValue, maxValue) && actual <= Math.Max(minValue, maxValue));
		}

		/// <summary>
		///A test for Next
		///</summary>
		[TestMethod()]
		public void NextTest3()
		{
			Int32 maxValue = RandomValue.Next();
			Int32 actual = RandomValue.Next(maxValue);
			Assert.IsTrue(actual <= maxValue);
		}

		/// <summary>
		///A test for Next
		///</summary>
		[TestMethod()]
		public void NextTest2()
		{
			Int32 actual = RandomValue.Next();
			Assert.IsTrue(actual <= Int32.MaxValue
				&& actual >= Int32.MinValue);
		}
	}
}
