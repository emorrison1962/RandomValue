using Eric.Morrison;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
namespace RandomValueTest
{
    
    
    /// <summary>
    ///This is a test class for RandomNameTest and is intended
    ///to contain all RandomNameTest Unit Tests
    ///</summary>
	[TestClass()]
	public class RandomNameTest
	{


		private TestContext testContextInstance;

		/// <summary>
		///Gets or sets the test context which provides
		///information about and functionality for the current test run.
		///</summary>
		public TestContext TestContext
		{
			get
			{
				return testContextInstance;
			}
			set
			{
				testContextInstance = value;
			}
		}

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



		[TestMethod()]
		public void Test()
		{
			const int MAX = 10000;
			try
			{
				for (int i = 0; i < MAX; ++i)
				{
					RandomName.NextFirst();
					RandomName.NextLast();
					RandomName.NextFirstAndLast();

					//Console.Write(".");
					//if ((i % 100) == 99)
					//    Console.WriteLine(string.Empty);
				}
				//Console.WriteLine(string.Empty);
				//Console.WriteLine("Success. Press any key to exit.");
				//Console.ReadKey();
			}
			catch (Exception ex)
			{
				Debug.Assert(false, ex.Message);
			}
		}

		/// <summary>
		///A test for NextFirstAndLast
		///</summary>
		[TestMethod()]
		[Ignore]
		public void NextFirstAndLastTest()
		{
			string s = RandomName.NextFirstAndLast();
			Assert.IsFalse(string.IsNullOrEmpty(s));
		}

		/// <summary>
		///A test for NextLast
		///</summary>
		[TestMethod()]
		[Ignore]
		public void NextLastTest()
		{
			string s = RandomName.NextLast();
			Assert.IsFalse(string.IsNullOrEmpty(s));
		}

		/// <summary>
		///A test for NextFirst
		///</summary>
		[TestMethod()]
		[Ignore]
		public void NextFirstTest()
		{
			string s = RandomName.NextFirst();
			Assert.IsFalse(string.IsNullOrEmpty(s));
		}
	}
}
