using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CefiBrowser
{
    public class Bookmarks
    {
        public void WriteBookmarks(FavirteButton favirteButton)
        {
           // string currDirectiory = Environment.CurrentDirectory.ToString();
            CheckFiles("Bookmarks"); //检查
            JObject jsonObject = new JObject();
            string jsonStr;
            try
            {
                jsonStr = File.ReadAllText(PublicClass.currDirectiory + @"\UserData\Bookmarks");
                if (jsonStr == "")
                {
                    jsonObject.Add("Date_Added", favirteButton.Date_Added);
                    jsonObject.Add("Last_Visited", favirteButton.Last_Visited);
                    jsonObject.Add("Title", favirteButton.Title);
                    jsonObject.Add("URL", favirteButton.URL);
                    jsonObject.Add("Layer", favirteButton.Layer);
                    jsonObject.Add("ID", favirteButton.ID);
                    jsonObject.Add("FatherID", favirteButton.FatherID);
                    jsonObject.Add("Type", favirteButton.Type);
                    jsonObject.Add("IconBase64str", favirteButton.IconBase64str);
                    File.WriteAllText(PublicClass.currDirectiory + @"\UserData\Bookmarks", "[" + jsonObject.ToString() + "]");
                    // GC.Collect();
                }
                else
                {
                    jsonObject = new JObject();
                    jsonObject.Add("Date_Added", favirteButton.Date_Added);
                    jsonObject.Add("Last_Visited", favirteButton.Last_Visited);
                    jsonObject.Add("Title", favirteButton.Title);
                    jsonObject.Add("URL", favirteButton.URL);
                    jsonObject.Add("Layer", favirteButton.Layer);
                    jsonObject.Add("ID", favirteButton.ID);
                    jsonObject.Add("FatherID", favirteButton.FatherID);
                    jsonObject.Add("Type", favirteButton.Type);
                    jsonObject.Add("IconBase64str", favirteButton.IconBase64str);
                    jsonStr = jsonStr.Replace("[", "");
                    jsonStr = jsonStr.Replace("]", "");
                    jsonStr = jsonStr + ",";
                    jsonStr = "[" + jsonStr + jsonObject.ToString() + "]";
                    jsonStr = jsonStr.Replace(",,", ",");
                    File.WriteAllText(PublicClass.currDirectiory + @"\UserData\Bookmarks", jsonStr);
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
           // currDirectiory = null;
            GC.Collect();
        }
        public bool DelBookmarks(FavirteButton favirteButton)
        {
          //  string currDirectiory = Environment.CurrentDirectory.ToString();
            CheckFiles("Bookmarks"); //检查
            JObject jsonObject = new JObject();
            string jsonStr;
            try
            {
                jsonStr = File.ReadAllText(PublicClass.currDirectiory + @"\UserData\Bookmarks");
                if (jsonStr != "")
                {
                    List<FavireBT> jobInfoList = JsonConvert.DeserializeObject<List<FavireBT>>(jsonStr);
                    jsonStr = "";
                    foreach (FavireBT favBT in jobInfoList)
                    {
                        if(favBT.Date_Added!=favirteButton.Date_Added)
                        {
                            jsonObject = new JObject();
                            jsonObject.Add("Date_Added", favBT.Date_Added);
                            jsonObject.Add("Last_Visited", favBT.Last_Visited);
                            jsonObject.Add("Title", favBT.Title);
                            jsonObject.Add("URL", favBT.URL);
                            jsonObject.Add("Layer", favBT.Layer);
                            jsonObject.Add("ID", favBT.ID);
                            jsonObject.Add("FatherID", favBT.FatherID);
                            jsonObject.Add("Type", favBT.Type);
                            jsonObject.Add("IconBase64str", favBT.IconBase64str);
                            jsonStr = jsonStr.Replace("[", "");
                            jsonStr = jsonStr.Replace("]", "");
                            jsonStr += jsonObject.ToString() + ",";

                        }
                    }
                }

                File.WriteAllText(PublicClass.currDirectiory + @"\UserData\Bookmarks", "[" + jsonStr.Replace(",,",",") + "]");
                return true;
            }
            catch
            {
                return false;
            }
        }

        //public void ClearFileText()
        //{
        //    string currDirectiory = Environment.CurrentDirectory.ToString();
        //    FileStream stream2 = File.Open(currDirectiory + @"\UserData\Bookmarks", FileMode.OpenOrCreate, FileAccess.Write);
        //    stream2.Seek(0, SeekOrigin.Begin);
        //    stream2.SetLength(0); //清空txt文件
        //    stream2.Close();
        //    File.WriteAllLines(PathBase + inforpath, content1);
        //}


        //更新
        public static void Update()
        {
            string jsonfile = PublicClass.currDirectiory + @"\UserData\Bookmarks";
            if (File.Exists(jsonfile))
            {
                string jsonString = File.ReadAllText(jsonfile);//读取文件

                List<FavirteButton> jobInfoList = JsonConvert.DeserializeObject<List<FavirteButton>>(jsonString);

                foreach (FavirteButton jobInfo in jobInfoList)
                {
                    Console.WriteLine("UserName:" + jobInfo.Title);
                }

                JObject jobject = JObject.Parse(jsonString);//解析成json
                jobject["Devices"]["name"] = "555555";//替换需要的文件
                string convertString = Convert.ToString(jobject);//将json装换为string
                File.WriteAllText(jsonfile, convertString);//将内容写进jon文件中
            }
            else
            {

            }
        }

        //检查文件是否存在
        private bool CheckFiles(string fileName)
        {
          //  string currDirectiory = Environment.CurrentDirectory.ToString();
            if (!Directory.Exists(PublicClass.currDirectiory + @"\UserData\"))
                Directory.CreateDirectory(PublicClass.currDirectiory + @"\UserData\");


            if (!File.Exists(PublicClass.currDirectiory + @"\UserData\"+fileName))
            {
                File.Create(PublicClass.currDirectiory + @"\UserData\" + fileName).Close();
                File.Create(PublicClass.currDirectiory + @"\UserData\" + fileName+".bak").Close();
            }
            else
            {
                //有就备份
                File.Copy(PublicClass.currDirectiory + @"\UserData\" + fileName, PublicClass.currDirectiory + @"\UserData\" + fileName + ".bak",true);
            }
           // currDirectiory = null;
            GC.Collect();
            return true;
        }

    }

    public class FavireBT
    {
        public string Date_Added { set; get; }
        public string Last_Visited { set; get; }
        public string Title { set; get; }
        public string URL { set; get; }
        public string Layer { set; get; }
        public string ID { set; get; }
        public string FatherID { set; get; }
        public string Type { set; get; }
        public string IconBase64str { set; get; }
    }
}
