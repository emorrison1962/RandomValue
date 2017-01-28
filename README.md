# RandomValue

## Synopsis

RandomValue is a utility library that contains classes for generating random data. I've used it countless times of the last 10 years, for unit testing and data generation.

## Code Example

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
					Name = RandomName.NextFirstAndLast(RandomName.Gender.Either),
					AddressLineOne = string.Format("{0} {1}", RandomValue.Next(100, 20000), randomStreets.Next()),
					City = randomCities.Next(),
					State = randomStates.Next(),
					ZipCode = RandomValue.Next(10000, 99999).ToString()
				};

				Debug.WriteLine(address);
				Debug.WriteLine(String.Empty);
			}
		}


## Motivation

I've used this for so long, and finally decided to share it with the development community. 
I hope you find it useful.

## Installation

Package is available on nuget.

## API Reference

Depending on the size of the project, if it is small and simple enough the reference docs can be added to the README. For medium size to larger projects it is important to at least provide a link to where the API reference docs live.

## Tests

Unit tests are in the RandomValueTest project.


## License

A short snippet describing the license (MIT, Apache, etc.)