using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlagMiner
{
    public static class FlegOperations
    {
        public static void CheckExistent(SerializableDictionary<string, RegionalFleg> flegs, int level)
        {
            foreach (KeyValuePair<string, RegionalFleg> ke in flegs)
            {
                RegionalFleg fleg = ke.Value;
                string initString = "";
                if (string.IsNullOrEmpty(flegsBaseUrl))
                    throw new Exception("Repository url is not set. Make sure to set a valid one in the options.");
                if (fleg.imgurl.Contains(flegsBaseUrl))
                    initString = options.localSaveFolder + "\\" + fleg.imgurl.Replace(flegsBaseUrl, "");
                // for regionals
                if (fleg.imgurl.Contains(imageBaseUrl))
                    initString = options.localSaveFolder + "\\" + fleg.imgurl.Replace(imageBaseUrl, "");
                // for nationals
                fleg.exists = File.Exists(initString);
                CheckExistent(fleg.children, level + 1);
            }
        }
    }
}
