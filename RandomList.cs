using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eric.Morrison
{
	public class RandomList<T>
	{
		List<T> _list;

		public RandomList(List<T> list)
		{
			if (null == list)
				throw new ArgumentNullException("list");
			_list = list;
		}

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
