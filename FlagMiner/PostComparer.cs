using System.Collections.Generic;


namespace FlagMiner
{
    public class PostComparer : IComparer<Post>
	{

		public int Compare(Post x, Post y)
		{
			if (x == null) {
				if (y == null) {
					return 0;
				} else {
					return -1;
				}
			} else {
				if (y == null) {
					return 1;
				} else {
					int retval = x.no.CompareTo(y.no);
					return retval;
				}
			}
		}
	}

}
