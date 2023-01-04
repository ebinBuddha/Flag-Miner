using System.Threading.Tasks;
using BrightIdeasSoftware;
using System.Collections.Concurrent;
using System.Threading;


namespace FlagMiner
{
    public class FlagMergeManager
	{
		private BlockingCollection<SerializableDictionary<string, RegionalFleg>> stack;

		private Task consumer;

		private TreeListView treeView;
		public FlagMergeManager(ConcurrentQueue<SerializableDictionary<string, RegionalFleg>> source, ref SerializableDictionary<string, RegionalFleg> dest, TreeListView myTreeView)
		{
			stack = new BlockingCollection<SerializableDictionary<string, RegionalFleg>>(source);
			treeView = myTreeView;
			FlagMiner frm = (FlagMiner)treeView.Parent.Parent.Parent;
			SerializableDictionary<string, RegionalFleg> dict = dest;
            consumer = Task.Run(() =>
            {
                foreach (SerializableDictionary<string, RegionalFleg> myObj in stack.GetConsumingEnumerable())
                {
                    FlegOperations.MergeFlegs(myObj.Values, ref dict);

                    Thread.Sleep(200);
                    if (stack.Count == 0)
                    { frm.UpdateRootsInvoker(); }

                }
            });
        }


        public void AddToStack(SerializableDictionary<string, RegionalFleg> obj)
		{
			stack.Add(obj);
		}

	}

}
