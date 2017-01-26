using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Diagnostics;


namespace Eric.Morrison
{
	public static class RandomName
	{
		//public enum Gender
		//{
		//    Male,
		//    Female,
		//    Either
		//};

		#region Fields

		//Gender _gender = Gender.Either;
		static List<string> _firstNames;
		static List<string> _lastNames;

		#endregion Fields
	
		#region Properties

		static Random Random
		{
			get { return _random; }
		} static Random _random = new Random(Environment.TickCount);

		static List<string> FirstNames 
		{
			get 
			{
				return _firstNames;
			}
		}

		static List<string> LastNames
		{
			get
			{
				return _lastNames;
			}
		}

		#endregion Properties


		static RandomName() 
		{
			Debug.WriteLine("static ctor called.");
			try
			{
				LoadNames();
			}
			catch (Exception ex)
			{
				Debug.Assert(false, ex.Message);
			}
		}

		//public RandomName(Gender gender) 
		//{
		//    _gender = gender;
		//}

		static void LoadNames()
		{
			DataSet ds = LoadDataSet();

			DataTable dt = ds.Tables["LastNames"];
			IEnumerable<DataRow> seq = dt.AsEnumerable();
			_lastNames = (
				from DataRow dr in seq
				select dr["L"] as string).ToList<string>();

			dt = ds.Tables["FemaleNames"];
			seq = dt.AsEnumerable();
			List<string> femaleNames = (
				from DataRow dr in seq
				select dr["F"] as string).ToList<string>();

			dt = ds.Tables["MaleNames"];
			seq = dt.AsEnumerable();
			List<string> maleNames = (
				from DataRow dr in seq
				select dr["F"] as string).ToList<string>();


			_firstNames = femaleNames.Union(maleNames).ToList();
			//if (Gender.Female == _gender) 
			//{
			//    _firstNames = femaleNames;
			//}
			//else if (Gender.Male == _gender)	 
			//{
			//    _firstNames = maleNames;
			//}
			//else if (Gender.Either == _gender)
			//{
			//    _firstNames = femaleNames.Union(maleNames).ToList();
			//}
		}

		static DataSet LoadDataSet()
		{
			System.Reflection.Assembly a = System.Reflection.Assembly.GetExecutingAssembly();
			string[] rscNames = a.GetManifestResourceNames();

			string rscName = (from s in rscNames
							  where s.EndsWith("RandomNames.xml")
							  select s).FirstOrDefault();

			Stream stream = a.GetManifestResourceStream(rscName);
			System.Data.DataSet ds = new System.Data.DataSet();
			ds.ReadXml(stream, System.Data.XmlReadMode.ReadSchema);

			ds.Tables[0].TableName = "LastNames";
			ds.Tables[1].TableName = "MaleNames";
			ds.Tables[2].TableName = "FemaleNames";

			return ds;
		}

		static public string NextFirst()
		{
			int i = RandomValue.Next<Int32>(0, FirstNames.Count);
			return FirstNames[i];
		}

		static public string NextLast()
		{
			int i = RandomValue.Next<Int32>(0, LastNames.Count);
			return LastNames[i];
		}

		static public string NextFirstAndLast()
		{
			return string.Format("{0} {1}", NextFirst(), NextLast());
		}

	}//public class RandomName
}//namespace Eric.Morrison
