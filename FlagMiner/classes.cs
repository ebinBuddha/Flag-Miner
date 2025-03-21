using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Runtime.CompilerServices;


namespace FlagMiner
{

    public class ChanThread
	{
		public List<Post> posts;
	}


	public class Post
	{
		//' deserializer looks for public field names to deserialize json fields
		private Int64 _no;
        public Int64 no  // all this mess to handle OPs which have resto = 0
        {
            get { return _no; }
            set
            {
                if (_resto == 0) { _resto = value; }
                _no = value;
            }
        }
		private Int64 _resto;
        public Int64 resto
        {
            get { return _resto; }
            set { if (value == 0) { _resto = _no; }
                else { _resto = value; }
            }
        }
		public Int64 sticky;
		public Int64 closed;
		public Int64 archived;
		public Int64 archived_on;
		public string now;
		public Int64 time;
		public string name;
		public string trip;
		public string id;
		public string capcode;
		public string country;
		public string country_name;
		public string sub;

		public string com;
		public Int64 tim;
		public string filename;
		public string ext;
		public Int64 fsize;
		public string md5;
		public Int64 w;
		public Int64 h;
		public Int64 tn_w;
		public Int64 tn_h;
		public Int64 filedeleted;
		public Int64 spoiler;
		public Int64 custom_spoiler;
		public Int64 omitted_posts;
		public Int64 omitted_images;
		public Int64 replies;
		public Int64 images;
		public Int64 bumplimit;
		public Int64 imagelimit;
		//Public capcode_replies As Array   '' fcking deserializer
		public Int64 last_modified;
		public string tag;
		public string semantic_url;
		public Int64 since4pass;

		public string troll_country;
	}


	public class Fleg
	{
		public Int64 post_nr;
		public string region;
	}


	[Serializable()]
	public class RegionalFleg
	{
		public bool isTrollFlag;
		public string imgurl;
		public string title;
		public string thread;
		public string board;
		public string pNo;
		public long time;

		public SerializableDictionary<string, RegionalFleg> children = new SerializableDictionary<string, RegionalFleg>();
		[XmlIgnore()]
		public bool fetching = false;
		[XmlIgnore()]
		public bool markedfordeletion = false;
		[XmlIgnore()]
		public bool exists = false;
		[XmlIgnore()]
			// existing, invalid etc...
		public int status = 0;

        public void copySerializableItems(RegionalFleg src)
        {
            this.isTrollFlag = src.isTrollFlag;
            this.imgurl = src.imgurl;
            this.title = src.title;
            this.thread = src.thread;
            this.board = src.board;
            this.pNo = src.pNo;
            this.time = src.time;
        }
    }


	public enum PurgeEnum : int
	{
		genericError = -2,
		notFound = -1,
		undefined = 0,
		ok = 1
	}


	[Serializable()]
	public struct Options
	{
        [XmlElement]
        public List<string> backendServers;

		public bool intCheck;
		public bool polCheck;
		public bool spCheck;

		public bool exclusionByList;
		public bool exclusionByDate;
		public DateTime exclusionDate;

		public string localSaveFolder;  // where saved flags are stored, to be used for existance check
		public string localRepoFolder;  // the local folder with the full repository, used for image scraping instead of the web

		public bool enableCheck;
		public bool enablePurge;
		public bool useLocal;
		public bool markTroll;
		public bool deleteChildFree;

		public string userAgent;

		public string saveAndLoadFolder; // dafault folder for loading and saving the work tree

		public string repoUrl;
	}

    public enum WorkerStatus
    {
        unk = 0,
        starting,
        initializing,
        running,
        completed,
        cancelling,
        cancelled,
        error,
		curruptJson,
    }

    public class WorkerUserState
    {
        public WorkerStatus status;
        public String board;
        public long current;
        public String additionalString;
        public int progress;
        public int total;
    }

}
