using System.Collections.Generic;


namespace FlagMiner
{
    public class FlegComparer : IComparer<Fleg>
	{

		public int Compare(Fleg x, Fleg y)
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
					int retval = x.post_nr.CompareTo(y.post_nr);
					return retval;
				}
			}
		}
	}

}
