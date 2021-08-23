using System;
using System.Collections.Generic;
using System.Linq;

namespace FlagMiner
{
	// array mangling
	public static class EnumerableExtensions
	{
		//[Extension()]
		public static IEnumerable<T> Apply<T>(this IEnumerable<T> source, Action<T> action)
		{
			//Argument.NotNull(source, Function() Function() source)
			//Argument.NotNull(action, Function() Function() action)
			return ApplyInternal(source, action);
		}

		//[Extension()]
		public static void Apply<T>(this IEnumerable<T> source)
		{
			foreach (object e in source)
			{
				//' do nothing, just make sure the elements are enumerated.
			}
		}

		static internal IEnumerable<T> ApplyInternal<T>(IEnumerable<T> source, Action<T> action)
		{
			List<T> res = new List<T>();
			foreach (T e in source)
			{
				action(e);
				res.Add(e);
			}
			return res;
		}

	}


	public static class ListExtensions
	{
		public static List<List<T>> ChunkBy<T>(this IEnumerable<T> source, int chunkSize)
		{
			return source
				.Select((x, i) => new { Index = i, Value = x })
				.GroupBy(x => x.Index / chunkSize)
				.Select(x => x.Select(v => v.Value).ToList())
				.ToList();
		}
	}
}
