using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CefiBrowser
{
   public static class mDownloadRecode
    {
        /// <summary>
        /// 写入下载记录
        /// </summary>
        /// <param name="JdownloadItem"></param>
        public static void WritemDownloadRecode(Jdownload JdownloadItem)
        {
            string currDirectiory = Environment.CurrentDirectory.ToString();
            CheckFiles("mDownloadRecodes"); //检查
            JObject jsonObject = new JObject();
            string jsonStr="";
            try
            {
                jsonStr = File.ReadAllText(currDirectiory + @"\UserData\mDownloadRecodes");
                if (jsonStr == "")
                {
                    jsonObject.Add("DownLoadTime", JdownloadItem.DownLoadTime);
                    jsonObject.Add("FileName", JdownloadItem.FileName);
                    jsonObject.Add("FullFilePaths", JdownloadItem.FullFilePaths);
                    jsonObject.Add("DownloadUrl", JdownloadItem.DownloadUrl);
                    jsonObject.Add("ImageBase64str", JdownloadItem.ImageBase64str);
                    jsonObject.Add("FileAlreadyDele", JdownloadItem.FileAlreadyDele.ToString());
                    jsonObject.Add("IsDownloading", JdownloadItem.IsDownloading.ToString());
                    jsonObject.Add("DownloadID", JdownloadItem.DownloadID);
                    File.WriteAllText(currDirectiory + @"\UserData\mDownloadRecodes", "[" + jsonObject.ToString() + "]");
                }
                else
                {
                    jsonObject = new JObject();
                    jsonObject.Add("DownLoadTime", JdownloadItem.DownLoadTime);
                    jsonObject.Add("FileName", JdownloadItem.FileName);
                    jsonObject.Add("FullFilePaths", JdownloadItem.FullFilePaths);
                    jsonObject.Add("DownloadUrl", JdownloadItem.DownloadUrl);
                    jsonObject.Add("ImageBase64str", JdownloadItem.ImageBase64str);
                    jsonObject.Add("FileAlreadyDele", JdownloadItem.FileAlreadyDele.ToString());
                    jsonObject.Add("IsDownloading", JdownloadItem.IsDownloading.ToString());
                    jsonObject.Add("DownloadID", JdownloadItem.DownloadID);

                    //jsonObject.Add("Startime", JdownloadItem.Startime);
                    jsonStr = jsonStr.Replace("[", "");
                    jsonStr = jsonStr.Replace("]", "");
                   // jsonStr = jsonStr + ",";
                    jsonStr = "[" +  jsonObject.ToString() +"," + jsonStr +"]";
                    jsonStr = jsonStr.Replace(",,", ",");
                    File.WriteAllText(currDirectiory + @"\UserData\mDownloadRecodes", jsonStr);
                    // GC.Collect();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            CheckFiles("Bookmarks"); //备份
            
            jsonObject = null;
            jsonStr = null;
            currDirectiory = null;
            GC.Collect();
        }

        /// <summary>
        /// 删除指定的Jdownload信息
        /// </summary>
        /// <param name="JdownloadItem"></param>
        /// <returns></returns>
        public static bool DelBookmarks(Jdownload JdownloadItem)
        {
            string currDirectiory = Environment.CurrentDirectory.ToString();
            CheckFiles("mDownloadRecodes"); //检查
            JObject jsonObject = new JObject();
            string jsonStr = "";
            try
            {
                jsonStr = File.ReadAllText(currDirectiory + @"\UserData\mDownloadRecodes");
                if (jsonStr != "")
                {
                    List<Jdownload> jobInfoList = JsonConvert.DeserializeObject<List<Jdownload>>(jsonStr);
                    jsonStr = "";
                    foreach (Jdownload DownloadItem in jobInfoList)
                    {
                        if (DownloadItem.FullFilePaths == JdownloadItem.FullFilePaths)
                        {
                            //jsonObject = new JObject();
                            //jsonObject.Add("DownLoadTime", JdownloadItem.DownLoadTime);
                            //jsonObject.Add("FileName", JdownloadItem.FileName);
                            //jsonObject.Add("FullFilePaths", JdownloadItem.FullFilePaths);
                            //jsonObject.Add("DownloadUrl", JdownloadItem.DownloadUrl);
                            //jsonObject.Add("ImageBase64str", JdownloadItem.ImageBase64str);
                            //jsonStr = jsonStr.Replace("[", "");
                            //jsonStr = jsonStr.Replace("]", "");
                            //jsonStr += jsonObject.ToString() + ",";
                        }
                        else
                        {
                            jsonObject = new JObject();
                            jsonObject.Add("DownLoadTime", DownloadItem.DownLoadTime);
                            jsonObject.Add("FileName", DownloadItem.FileName);
                            jsonObject.Add("FullFilePaths", DownloadItem.FullFilePaths);
                            jsonObject.Add("DownloadUrl", DownloadItem.DownloadUrl);
                            jsonObject.Add("ImageBase64str", DownloadItem.ImageBase64str);
                            jsonObject.Add("FileAlreadyDele", JdownloadItem.FileAlreadyDele.ToString());
                            jsonObject.Add("IsDownloading", JdownloadItem.IsDownloading.ToString());
                            jsonObject.Add("DownloadID", JdownloadItem.DownloadID);

                            //jsonObject.Add("Startime", JdownloadItem.Startime);
                            jsonStr = jsonStr.Replace("[", "");
                            jsonStr = jsonStr.Replace("]", "");
                            jsonStr += jsonObject.ToString() + ",";
                        }
                    }
                }

                File.WriteAllText(currDirectiory + @"\UserData\mDownloadRecodes", "[" + jsonStr.Replace(",,", ",") + "]");
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 更新下载Item信息
        /// </summary>
        /// <param name="JdownloadItem"></param>
        /// <returns></returns>
        public static bool Update(Jdownload JdownloadItem)
        {
            string currDirectiory = Environment.CurrentDirectory.ToString();
            CheckFiles("mDownloadRecodes"); //检查
            JObject jsonObject = new JObject();
            string jsonStr="";
            try
            {
                jsonStr = File.ReadAllText(currDirectiory + @"\UserData\mDownloadRecodes");
                if (jsonStr != "")
                {
                    List<Jdownload> jobInfoList = JsonConvert.DeserializeObject<List<Jdownload>>(jsonStr);
                    jsonStr = "";
                    foreach (Jdownload DownloadItem in jobInfoList)
                    {
                        if (DownloadItem.FullFilePaths == JdownloadItem.FullFilePaths)
                        {
                            jsonObject = new JObject();
                            jsonObject.Add("DownLoadTime", DownloadItem.DownLoadTime);
                            jsonObject.Add("FileName", JdownloadItem.FileName);
                            jsonObject.Add("FullFilePaths", JdownloadItem.FullFilePaths);
                            jsonObject.Add("DownloadUrl", JdownloadItem.DownloadUrl);
                            jsonObject.Add("ImageBase64str", JdownloadItem.ImageBase64str);
                            jsonObject.Add("FileAlreadyDele", JdownloadItem.FileAlreadyDele.ToString());
                            jsonObject.Add("IsDownloading", JdownloadItem.IsDownloading.ToString());
                            jsonObject.Add("DownloadID", JdownloadItem.DownloadID);

                            //jsonObject.Add("Startime", JdownloadItem.Startime);
                            jsonStr = jsonStr.Replace("[", "");
                            jsonStr = jsonStr.Replace("]", "");
                            jsonStr += jsonObject.ToString() + ",";
                        }
                        else
                        {
                            jsonObject = new JObject();
                            jsonObject.Add("DownLoadTime", DownloadItem.DownLoadTime);
                            jsonObject.Add("FileName", DownloadItem.FileName);
                            jsonObject.Add("FullFilePaths", DownloadItem.FullFilePaths);
                            jsonObject.Add("DownloadUrl", DownloadItem.DownloadUrl);
                            jsonObject.Add("ImageBase64str", DownloadItem.ImageBase64str);
                            jsonObject.Add("FileAlreadyDele", JdownloadItem.FileAlreadyDele.ToString());
                            jsonObject.Add("IsDownloading", JdownloadItem.IsDownloading.ToString());
                            jsonObject.Add("DownloadID", JdownloadItem.DownloadID);

                            //jsonObject.Add("Startime", JdownloadItem.Startime);
                            jsonStr = jsonStr.Replace("[", "");
                            jsonStr = jsonStr.Replace("]", "");
                            jsonStr += jsonObject.ToString() + ",";
                        }
                    }
                }

                File.WriteAllText(currDirectiory + @"\UserData\mDownloadRecodes", "[" + jsonStr.Replace(",,", ",") + "]");
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
        }

        //检查文件是否存在
        private static bool CheckFiles(string fileName)
        {
            string currDirectiory = Environment.CurrentDirectory.ToString();
            if (!Directory.Exists(currDirectiory + @"\UserData\"))
                Directory.CreateDirectory(currDirectiory + @"\UserData\");


            if (!File.Exists(currDirectiory + @"\UserData\" + fileName))
            {
                File.Create(currDirectiory + @"\UserData\" + fileName).Close();
                File.Create(currDirectiory + @"\UserData\" + fileName + ".bak").Close();
            }
            else
            {
                //有就备份
                File.Copy(currDirectiory + @"\UserData\" + fileName, currDirectiory + @"\UserData\" + fileName + ".bak", true);
            }
            currDirectiory = null;
            GC.Collect();
            return true;
        }

    }
}
