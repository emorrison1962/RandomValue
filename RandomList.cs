using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eric.Morrison
{
	/// <summary>
	/// RandomList creates a wrapper around System.Collections.Generic.List&lt;T&gt; that allows random access to list elements.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class RandomList<T>
	{
		List<T> _list;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="list">The list to which you want random access. The list is stored as a reference.</param>
		public RandomList(List<T> list)
		{
			if (null == list)
				throw new ArgumentNullException("list");
			_list = list;
		}

		/// <summary>
		/// Returns a random element in the list.
		/// </summary>
		/// <returns>Returns a random element in the list.</returns>
		public T Next()
		{
			T result = default(T);
			int count = _list.Count;
			if (count > 0)
			{
				int index = RandomValue.Next(0, count);
				result = _list[index];
			}
			return result;
		}
	}
}
