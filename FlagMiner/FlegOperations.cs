using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace FlagMiner
{
    public static class FlegOperations
    {
        private readonly static string imageBaseUrl = Properties.Resources.imageBaseUrl;

        public static void CheckExistent(SerializableDictionary<string, RegionalFleg> flegs, int level)
        {
            string localSaveFolder = OptionsManager.OptionsInstance.localSaveFolder;
            string flegsBaseUrl = OptionsManager.OptionsInstance.repoUrl;
            foreach (KeyValuePair<string, RegionalFleg> ke in flegs)
            {
                RegionalFleg fleg = ke.Value;
                string initString = "";
                if (string.IsNullOrEmpty(flegsBaseUrl))
                    throw new Exception("Repository url is not set. Make sure to set a valid one in the options.");
                if (fleg.imgurl.Contains(flegsBaseUrl))
                { initString = localSaveFolder + "\\" + fleg.imgurl.Replace(flegsBaseUrl, ""); }
                // for regionals
                if (fleg.imgurl.Contains(imageBaseUrl))
                { initString = localSaveFolder + "\\" + fleg.imgurl.Replace(imageBaseUrl, ""); }
                // for nationals
                fleg.exists = File.Exists(initString);
                CheckExistent(fleg.children, level + 1);
            }
        }

        public static PurgeEnum QueryFlag(string imgurl)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(imgurl);
                request.UserAgent = OptionsManager.OptionsInstance.userAgent;
                request.Method = "HEAD";
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    HttpStatusCode status = response.StatusCode;
                    response.Dispose();

                    return status == HttpStatusCode.NotFound ? PurgeEnum.notFound : PurgeEnum.ok;
                }
            }
            catch (WebException ex)
            {
                var resp = (HttpWebResponse)ex.Response;
                return resp != null && resp.StatusCode == HttpStatusCode.NotFound ? PurgeEnum.notFound : PurgeEnum.genericError;
            }
            catch (Exception)
            {
                return PurgeEnum.genericError;
                // network error
            }
        }

        public static PurgeEnum PurgeInvalid(SerializableDictionary<string, RegionalFleg> flegs, string path, int level)
        {
            string flegsBaseUrl = OptionsManager.OptionsInstance.repoUrl;

            foreach (RegionalFleg fleg in flegs.Values)
            {
                var basestr = path + "\\" + fleg.title;

                PurgeEnum checkedFlag = default(PurgeEnum);
                if (OptionsManager.OptionsInstance.useLocal && (level > 0))
                {
                    string initString = "";
                    if (fleg.imgurl.Contains(flegsBaseUrl))
                    { initString = OptionsManager.OptionsInstance.localRepoFolder + "\\" + fleg.imgurl.Replace(flegsBaseUrl, ""); }
                    // for regionals
                    if (fleg.imgurl.Contains(imageBaseUrl))
                    { initString = OptionsManager.OptionsInstance.localRepoFolder + "\\" + fleg.imgurl.Replace(imageBaseUrl, ""); }
                    // for nationals
                    checkedFlag = File.Exists(initString) ? PurgeEnum.ok : PurgeEnum.notFound;
                }
                else
                { checkedFlag = QueryFlag(fleg.imgurl); }

                if (checkedFlag == PurgeEnum.genericError)
                { return PurgeEnum.genericError; }

                fleg.markedfordeletion = fleg.isTrollFlag && OptionsManager.OptionsInstance.markTroll;

                if (checkedFlag == PurgeEnum.notFound)
                { fleg.markedfordeletion = true; }
                else
                {
                    if (PurgeInvalid(fleg.children, basestr, level + 1) == PurgeEnum.genericError)
                    { return PurgeEnum.genericError; }
                    SerializableDictionary<string, RegionalFleg> mirror = fleg.children;
                    fleg.children.Where((KeyValuePair<string, RegionalFleg> pair) => pair.Value.markedfordeletion).ToArray().Apply((KeyValuePair<string, RegionalFleg> pair) => mirror.Remove(pair.Key)).Apply();
                }
            }
            return PurgeEnum.ok;
        }

        public static void MergeFlegs(IEnumerable<RegionalFleg> collectedFlegs, ref SerializableDictionary<string, RegionalFleg> flegTree)
        {
            foreach (RegionalFleg fleg in collectedFlegs)
            {
                SerializableDictionary<string, RegionalFleg> curDict = flegTree;
                RegionalFleg curFleg = fleg;

                if (!curDict.ContainsKey(curFleg.title))
                { curDict.Add(curFleg.title, curFleg); }
                else
                {
                    RegionalFleg presentFleg = curDict[curFleg.title];
                    if (presentFleg.time < curFleg.time)
                    { presentFleg.copySerializableItems(curFleg); }
                    if (curFleg.children.Count > 0)
                    {
                        SerializableDictionary<string, RegionalFleg> curSrcDict = curFleg.children;
                        SerializableDictionary<string, RegionalFleg> curDestDict = curDict[curFleg.title].children;
                        MergeFlegs(curSrcDict.Values, ref curDestDict);
                    }
                }
            }
        }

        /// <summary>
        /// subtracts src from dest: dest = dest-src
        /// </summary>
        /// <param name="src">subtrahend</param>
        /// <param name="dest">minuend</param>
        public static void SubtractFlegs(SerializableDictionary<String, RegionalFleg> src, ref SerializableDictionary<String, RegionalFleg> dest)
        {
            SerializableDictionary<String, RegionalFleg> curDestDict = dest;
            SerializableDictionary<String, RegionalFleg> curSrcDict = src;

            foreach (KeyValuePair<String, RegionalFleg> ke in curDestDict)
            {
                RegionalFleg Fleg = ke.Value;
                if (curSrcDict.ContainsKey(ke.Key))
                {
                    SubtractFlegs(curSrcDict[ke.Key].children, ref curDestDict[ke.Key].children);
                    if (Fleg.children.Count == 0)
                    { curDestDict[ke.Key].markedfordeletion = true; }
                }
            }
            curDestDict.Where(pair => pair.Value.markedfordeletion).ToList().
                Apply(pair => curDestDict.Remove(pair.Key)).Apply();  // ToList isn't useless. It allows to avoid an InvalidOperationException by editing an object that is being looped. duplicate first maybe?
        }

        /// <summary>
        /// removes flags marked for deletion on dest
        /// </summary>
        public static void DeleteCheckedFlegs(ref SerializableDictionary<String, RegionalFleg> dest)
        {
            SerializableDictionary<String, RegionalFleg> curDestDict = dest;

            foreach (KeyValuePair<String, RegionalFleg> ke in curDestDict)
            {
                RegionalFleg Fleg = ke.Value;
                DeleteCheckedFlegs(ref curDestDict[ke.Key].children);
                if (Fleg.children.Count == 0)
                { curDestDict[ke.Key].markedfordeletion = Fleg.exists; }
            }
            curDestDict.Where(pair => pair.Value.markedfordeletion).ToList().
                Apply(pair => curDestDict.Remove(pair.Key)).Apply();  // ToList isn't useless. It allows to avoid an InvalidOperationException by editing an object that is being looped. duplicate first maybe?
        }

        /// <summary>
        /// Produces the flag dump text of the given tree
        /// </summary>
        public static void AppendPasta(SerializableDictionary<string, RegionalFleg> dict, string str, ref StringBuilder pasta)
        {
            foreach (KeyValuePair<string, RegionalFleg> ch in dict)
            {
                RegionalFleg curFleg = ch.Value;
                SerializableDictionary<string, RegionalFleg> curDict = curFleg.children;
                string curString = String.IsNullOrEmpty(str) ? curFleg.title : (curFleg.title + ", " + str);
                if (curDict.Count == 0)
                { pasta.AppendLine(">>>/" + curFleg.board + "/" + curFleg.pNo + " " + curString); }
                else
                { AppendPasta(curDict, curString, ref pasta); }
            }
        }

    }
}
