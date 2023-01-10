using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace FlagMiner
{
    class OptionsManager
    {
        private static OptionsManager _instance = null;
        private Options options;

        public static DateTime MinDate = new DateTime(1970, 1, 1);
        public static DateTime MaxDate = new DateTime(9998, 12, 31);

        public static OptionsManager GetInstance
        {
            get
            {
                if (_instance is null)
                {
                    _instance = new OptionsManager();
                }
                return _instance;
            }
        }

        public static Options OptionsInstance { get => GetInstance.options; set { GetInstance.options = value; } }

        private readonly static string optionsFile = Properties.Resources.optionsFile;

        public static void SaveOptions()
        {
            FileStream fs = new FileStream(optionsFile, FileMode.Create);

            XmlSerializer optionsSerializer = new XmlSerializer(typeof(Options));
            optionsSerializer.Serialize(fs, GetInstance.options);
            fs.Close();
        }

        public static void LoadOptions()
        {
            FileStream fs = null;
            Options loadedOptions = new Options();
            try
            {
                fs = new FileStream(optionsFile, FileMode.OpenOrCreate);

                XmlSerializer optionsSerializer = new XmlSerializer(typeof(Options));
                loadedOptions = (Options)optionsSerializer.Deserialize(fs);
            }
            catch (Exception)
            {
                // ignore it for now.
            }
            finally
            {
                if (fs != null)
                    fs.Close();
            }
            if (loadedOptions.backendServers == null)
            {
                loadedOptions.backendServers = new List<string>();
            }
            if (loadedOptions.exclusionDate > MaxDate || loadedOptions.exclusionDate < MinDate)
            {
                loadedOptions.exclusionDate = MinDate;
            }
            GetInstance.options = loadedOptions;
        }

        // board checks
        public static bool IntCheck { set { GetInstance.options.intCheck = value; } }
        public static bool PolCheck { set { GetInstance.options.polCheck = value; } }
        public static bool SpCheck { set { GetInstance.options.spCheck = value; } }

        // exclusions
        public static bool ExclusionByDate { set { GetInstance.options.exclusionByDate = value; } }
        public static bool ExclusionByList { set { GetInstance.options.exclusionByList = value; } }
        public static DateTime ExclusionDate { set { GetInstance.options.exclusionDate = value; } }

        //// strategies
        //public bool IsPurgeEnabled { get => enablePurge; }
        //public bool IsCheckEnabled { get => enableCheck; }
        //public bool IsMarkTrollEnabled { get => markTroll; }
        //public bool IsUseLocalEnabled { get => useLocal; }
        //public bool IsDeleteChildFreeEnabled { get => deleteChildFree; }

        //// servers
        //public string UserAgent { get => userAgent; }
        //public string RepoUrl { get => repoUrl; }

        //// local folders
        //public string LocalFlagSaveFolder { get => localSaveFolder; }  // where saved flags are stored, to be used for existance check
        //public string LocalRepoFolder { get => localRepoFolder; }  // the local folder with the full repository, used for image scraping instead of the web
        //public string LocalTreeSaveFolder { get => saveAndLoadFolder; }  // load and save the work tree
    }
}
