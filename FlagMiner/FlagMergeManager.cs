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
				foreach (SerializableDictionary<string, RegionalFleg> myObj in stack.GetConsumingEnumerable()) {
					foreach (RegionalFleg Fleg in myObj.Values) {
						SerializableDictionary<string, RegionalFleg> curDict = dict;
						RegionalFleg curFleg = Fleg;
						RegionalFleg prevFleg = null;

						if (!curDict.ContainsKey(curFleg.title)) {
							curDict.Add(curFleg.title, curFleg);
							// does it copy it all?  TODO CREATE DEEP COPY   48861
						} else {
							RegionalFleg presentFleg = curDict[curFleg.title];
							if (presentFleg.time < curFleg.time) {
								presentFleg.time = curFleg.time;
								presentFleg.pNo = curFleg.pNo;
								presentFleg.thread = curFleg.thread;
								presentFleg.board = curFleg.board;
							}
							if (curFleg.children.Count > 0) {
								SerializableDictionary<string, RegionalFleg> curSrcDict = curFleg.children;
								SerializableDictionary<string, RegionalFleg> curDestDict = curDict[curFleg.title].children;
								FlagMiner.Merger(ref curSrcDict, ref curDestDict);
							}
						}
					}

					Thread.Sleep(200);
					if (stack.Count == 0) {
						frm.UpdateRootsInvoker();
						//frm.updateManager.AddToStack(Tuple.Create(Of String, Object)("ut", myTreeView))
					}

				}
			});
		}


		public void AddToStack(SerializableDictionary<string, RegionalFleg> obj)
		{
			stack.Add(obj);
		}

	}

}
