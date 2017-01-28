# RandomValue

## Synopsis

RandomValue is a utility library that contains classes for generating random data. I've used it countless times over the last 10 years, for unit testing and data generation.

## Code Example

		class Address
		{// demonstration class
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
			var randomStreets = new RandomList<string>(streets);  //RandomList usage
			var cities = new List<string>() { "Dallas", "Austin", "Corpus Cristi", "El Paso", "Abeleine" };
			var randomCities = new RandomList<string>(cities);    //RandomList usage
			var states = new List<string>() { "TX", "OK" };
			var randomStates = new RandomList<string>(states);    //RandomList usage
			const int MAX = 100;
			for (int i = 0; i < MAX; ++i)
			{
				var address = new Address
				{
					//RandomName usage....
					Name = RandomName.NextFirstAndLast(RandomName.Gender.Either), 
					//RandomValue usage....
					AddressLineOne = string.Format("{0} {1}", RandomValue.Next(100, 20000), randomStreets.Next()),
					//RandomList usage....
					City = randomCities.Next(),
					//RandomList usage....
					State = randomStates.Next(),
					//RandomValue usage....
					ZipCode = RandomValue.Next(10000, 99999).ToString()
				};

				Debug.WriteLine(address);
				Debug.WriteLine(String.Empty);
			}
		}

OUTPUT: 
Corey Owens
12827 Alma Rd
Dallas, OK 67959

Jace Moon
3048 Main St
El Paso, OK 36975

Isaiah Jennings
11081 Elm St
Corpus Cristi, OK 13567

Miles Thompson
2504 Main St
Corpus Cristi, TX 31632

Johnny Solis
9441 Park Blvd
Abeleine, OK 10723

Julian Rhodes
18947 Elm St
Austin, TX 32453

Davion Peters
16346 Elm St
El Paso, TX 19619

Emiliano Vaughn
8839 Alma Rd
Austin, OK 15838
...


## Motivation

I've used this for so long, and finally decided to share it with the development community. 
I hope you find it useful.

## Installation

Package is available on nuget.

## API Reference

Syntax
C#
public static class RandomValue

The RandomValue type exposes the following members.
Methods
	Name	Description
  	GetRandomEnumValue(T)
Returns a random value of the specified enumeration type.
  	Next()
Returns a random System.Int32.
  	Next(Int32)
Returns a random System.Int32 that is less than or equal to the specified maximum.
  	Next(Int32, Int32)
Returns a random System.Int32 that is within a specified range.
  	Next(T)()
Returns a random value greater than or equal to the mimnimum supported value and less than the maximum supported value of T.
  	Next(T)(T)
Returns a random value that is less than the specified maximum.
  	Next(T)(T, T)
Returns a random value that is within a specified range.
  	NextBoolean()
Returns a random Boolean value.
  	NextBoolean(Boolean)
Returns a random Boolean value that is less than or equal to the specified maximum.
  	NextBoolean(Boolean, Boolean)
Returns a random Boolean value that is within a specified range.
  	NextByte()
Returns a random System.Byte.
  	NextByte(Byte)
Returns a random System.Byte that is less than or equal to the specified maximum.
  	NextByte(Byte, Byte)
Returns a random System.Byte that is within a specified range.
  	NextChar()
Returns a random System.Char.
  	NextChar(Char)
Returns a random System.Char that is less than or equal to the specified maximum.
  	NextChar(Char, Char)
Returns a random System.Char that is within a specified range.
  	NextDateTime()
Returns a random System.DateTime.
  	NextDateTime(DateTime)
Returns a random System.DateTime that is less than or equal to the specified maximum.
  	NextDateTime(DateTime, DateTime)
Returns a random System.DateTime that is within a specified range.
  	NextDecimal()
Returns a random System.Decimal.
  	NextDecimal(Decimal)
Returns a random System.Decimal that is less than or equal to the specified maximum.
  	NextDecimal(Decimal, Decimal)
Returns a random System.Decimal that is within a specified range.
  	NextDouble()
Returns a random System.Double.
  	NextDouble(Double)
Returns a random System.Double that is less than or equal to the specified maximum.
  	NextDouble(Double, Double)
Returns a random System.Double that is within a specified range.
  	NextInt16()
Returns a random System.Int16.
  	NextInt16(Int16)
Returns a random System.Int16 that is less than or equal to the specified maximum.
  	NextInt16(Int16, Int16)
Returns a random System.Int16 that is within a specified range.
  	NextInt32()
Returns a random System.Int32.
  	NextInt32(Int32)
Returns a random System.Int32 that is less than or equal to the specified maximum.
  	NextInt32(Int32, Int32)
Returns a random System.Int32 that is within a specified range.
  	NextInt64()
Returns a random System.Int64.
  	NextInt64(Int64)
Returns a random System.Int64 that is less than or equal to the specified maximum.
  	NextInt64(Int64, Int64)
Returns a random System.Int64 that is within a specified range.
  	NextNullable(T)(Boolean, Byte)
Returns a random System.Nullable<T>.
  	NextNullable(T)(T, Boolean, Byte)
Returns a random System.Nullable<T>.
  	NextNullable(T)(T, T, Boolean, Byte)
Returns a random System.Nullable<T>.
  	NextSByte()
Returns a random System.SByte.
  	NextSByte(SByte)
Returns a random System.SByte that is less than or equal to the specified maximum.
  	NextSByte(SByte, SByte)
Returns a random System.SByte that is within a specified range.
  	NextSingle()
Returns a random System.Single.
  	NextSingle(Single)
Returns a random System.Single that is less than or equal to the specified maximum.
  	NextSingle(Single, Single)
Returns a random System.Single that is within a specified range.
  	NextUInt16()
Returns a random System.UInt16.
  	NextUInt16(UInt16)
Returns a random System.UInt16 that is less than or equal to the specified maximum.
  	NextUInt16(UInt16, UInt16)
Returns a random System.UInt16 that is within a specified range.
  	NextUInt32()
Returns a random System.UInt32.
  	NextUInt32(UInt32)
Returns a random System.UInt32 that is less than or equal to the specified maximum.
  	NextUInt32(UInt32, UInt32)
Returns a random System.UInt32 that is within a specified range.
  	NextUInt64()
Returns a random System.UInt64.
  	NextUInt64(UInt64)
Returns a random System.UInt64 that is less than or equal to the specified maximum.
  	NextUInt64(UInt64, UInt64)
Returns a random System.UInt64 that is within a specified range.

See Also
Eric.Morrison Namespace
?
RandomValue.RandomValue Methods
The RandomValue type exposes the following members.
Methods
	Name	Description
  	GetRandomEnumValue(T)
Returns a random value of the specified enumeration type.
  	Next()
Returns a random System.Int32.
  	Next(Int32)
Returns a random System.Int32 that is less than or equal to the specified maximum.
  	Next(Int32, Int32)
Returns a random System.Int32 that is within a specified range.
  	Next(T)()
Returns a random value greater than or equal to the mimnimum supported value and less than the maximum supported value of T.
  	Next(T)(T)
Returns a random value that is less than the specified maximum.
  	Next(T)(T, T)
Returns a random value that is within a specified range.
  	NextBoolean()
Returns a random Boolean value.
  	NextBoolean(Boolean)
Returns a random Boolean value that is less than or equal to the specified maximum.
  	NextBoolean(Boolean, Boolean)
Returns a random Boolean value that is within a specified range.
  	NextByte()
Returns a random System.Byte.
  	NextByte(Byte)
Returns a random System.Byte that is less than or equal to the specified maximum.
  	NextByte(Byte, Byte)
Returns a random System.Byte that is within a specified range.
  	NextChar()
Returns a random System.Char.
  	NextChar(Char)
Returns a random System.Char that is less than or equal to the specified maximum.
  	NextChar(Char, Char)
Returns a random System.Char that is within a specified range.
  	NextDateTime()
Returns a random System.DateTime.
  	NextDateTime(DateTime)
Returns a random System.DateTime that is less than or equal to the specified maximum.
  	NextDateTime(DateTime, DateTime)
Returns a random System.DateTime that is within a specified range.
  	NextDecimal()
Returns a random System.Decimal.
  	NextDecimal(Decimal)
Returns a random System.Decimal that is less than or equal to the specified maximum.
  	NextDecimal(Decimal, Decimal)
Returns a random System.Decimal that is within a specified range.
  	NextDouble()
Returns a random System.Double.
  	NextDouble(Double)
Returns a random System.Double that is less than or equal to the specified maximum.
  	NextDouble(Double, Double)
Returns a random System.Double that is within a specified range.
  	NextInt16()
Returns a random System.Int16.
  	NextInt16(Int16)
Returns a random System.Int16 that is less than or equal to the specified maximum.
  	NextInt16(Int16, Int16)
Returns a random System.Int16 that is within a specified range.
  	NextInt32()
Returns a random System.Int32.
  	NextInt32(Int32)
Returns a random System.Int32 that is less than or equal to the specified maximum.
  	NextInt32(Int32, Int32)
Returns a random System.Int32 that is within a specified range.
  	NextInt64()
Returns a random System.Int64.
  	NextInt64(Int64)
Returns a random System.Int64 that is less than or equal to the specified maximum.
  	NextInt64(Int64, Int64)
Returns a random System.Int64 that is within a specified range.
  	NextNullable(T)(Boolean, Byte)
Returns a random System.Nullable<T>.
  	NextNullable(T)(T, Boolean, Byte)
Returns a random System.Nullable<T>.
  	NextNullable(T)(T, T, Boolean, Byte)
Returns a random System.Nullable<T>.
  	NextSByte()
Returns a random System.SByte.
  	NextSByte(SByte)
Returns a random System.SByte that is less than or equal to the specified maximum.
  	NextSByte(SByte, SByte)
Returns a random System.SByte that is within a specified range.
  	NextSingle()
Returns a random System.Single.
  	NextSingle(Single)
Returns a random System.Single that is less than or equal to the specified maximum.
  	NextSingle(Single, Single)
Returns a random System.Single that is within a specified range.
  	NextUInt16()
Returns a random System.UInt16.
  	NextUInt16(UInt16)
Returns a random System.UInt16 that is less than or equal to the specified maximum.
  	NextUInt16(UInt16, UInt16)
Returns a random System.UInt16 that is within a specified range.
  	NextUInt32()
Returns a random System.UInt32.
  	NextUInt32(UInt32)
Returns a random System.UInt32 that is less than or equal to the specified maximum.
  	NextUInt32(UInt32, UInt32)
Returns a random System.UInt32 that is within a specified range.
  	NextUInt64()
Returns a random System.UInt64.
  	NextUInt64(UInt64)
Returns a random System.UInt64 that is less than or equal to the specified maximum.
  	NextUInt64(UInt64, UInt64)
Returns a random System.UInt64 that is within a specified range.


## Tests

Unit tests are in the RandomValueTest project.


## License

A short snippet describing the license (MIT, Apache, etc.)