﻿using System;
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
		class FirstName
		{
			public string First { get; private set; }
			public Gender Gender { get; private set; }
			public FirstName(string name, Gender gender)
			{
				this.First = name;
				this.Gender = gender;
			}
		}
		public enum Gender
		{
			Male,
			Female,
			Either
		};

		#region Fields

		static List<FirstName> _firstNames = null;
		static List<string> _lastNames;

		#endregion Fields
	
		#region Properties

		static Random Random
		{
			get { return _random; }
		} static Random _random = new Random(Environment.TickCount);

		static List<FirstName> FemaleNames
		{
			get
			{
				return _firstNames.Where(x => x.Gender == Gender.Female).ToList();
			}
		}

		static List<FirstName> MaleNames
		{
			get
			{
				return _firstNames.Where(x => x.Gender == Gender.Male).ToList();
			}
		}

		static List<FirstName> FirstNames
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
			_firstNames = (
				from DataRow dr in seq
				select new FirstName(dr["F"] as string, Gender.Female)).ToList<FirstName>();

			dt = ds.Tables["MaleNames"];
			seq = dt.AsEnumerable();
			var maleNames = (
				from DataRow dr in seq
				select new FirstName(dr["F"] as string, Gender.Male)).ToList<FirstName>();
			_firstNames.AddRange(maleNames);
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

		static public string NextFirst(Gender gender = Gender.Either)
		{
			List<FirstName> firstNames = null;

			if (gender == Gender.Either)
				firstNames = FirstNames;
			else if (gender == Gender.Female)
				firstNames = FemaleNames;
			else if (gender == Gender.Male)
				firstNames = MaleNames;

			int i = RandomValue.Next<Int32>(0, firstNames.Count);
			return firstNames[i].First;
		}

		static public string NextLast()
		{
			int i = RandomValue.Next<Int32>(0, LastNames.Count);
			return LastNames[i];
		}

		static public string NextFirstAndLast(Gender gender = Gender.Either)
		{
			return string.Format("{0} {1}", NextFirst(gender), NextLast());
		}

	}//public class RandomName
}//namespace Eric.Morrison
