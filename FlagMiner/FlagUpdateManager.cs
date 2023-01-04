using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BrightIdeasSoftware;

namespace FlagMiner
{
	public class FlagUpdateManager
	{
		private BlockingCollection<object> stack;

		private Task consumer;

		private TreeListView treeView;
		public FlagUpdateManager(ConcurrentQueue<object> source, TreeListView myTreeView)
		{
			stack = new BlockingCollection<object>(source);
			treeView = myTreeView;
			FlagMiner frm = (FlagMiner)treeView.Parent.Parent.Parent;
			consumer = Task.Run(() =>
			{
				List<object> accumulator = new List<object>();
				foreach (object myObj in stack.GetConsumingEnumerable())
				{
					accumulator.Add(myObj);
					if (stack.Count == 0)
					{
						treeView.Invalidate();
						accumulator.Clear();
						Thread.Sleep(200);
						// fist run ok, but now wait a little to build the queue for the next run
					}
				}
			});
		}


		public void AddToStack(object obj)
		{
			stack.Add(obj);
		}

	}
}
