using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;
using System.Collections;
namespace SOM_DEVELOP_TOOL
{
    public class Server
    {
        public static UInt64 Version = 0xA5A50001;
        public static string HostUserName = "";
        public static bool IsOnline = false;
        public static string IpV4 = "";
        public static UInt64 LogOperationTime = 0;
        public static UInt64 UserTime = 0;
        public static UInt64 MuxTime = 0;
        public static UInt64 OpenDate = 0;
        public static UInt64 CloseDate = 0;
        public static UInt64 LogSize = 0;
        public static UInt64 CmdOperationTime = 0;
        public static UInt64 DownLoadOperationTime = 0;
        public static UInt64 OnlineTime = 0;
        public static DateTime TimeStart;
        public static DateTime TimeEnd;
        public static bool LoadOk = false;

        public static void Save(string FilePath,bool Online,bool Init=false)
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                Server.IpV4 = Server.GetLocalIPv4();
                Server.HostUserName = easy_log.HostUserName;
                if (Init == false)
                {
                    Server.GetWorkTime();
                }
                if (LoadOk == true || Init == true)
                {
                    IsOnline = Online;
                    sb.Append("Version" + "=" + Version + Environment.NewLine);
                    sb.Append("HostUserName" + "=" + HostUserName + Environment.NewLine);
                    sb.Append("IsOnline" + "=" + IsOnline + Environment.NewLine);
                    sb.Append("IpV4" + "=" + IpV4 + Environment.NewLine);
                    sb.Append("LogOperationTime" + "=" + LogOperationTime + Environment.NewLine);
                    sb.Append("UserTime" + "=" + UserTime + Environment.NewLine);
                    sb.Append("MuxTime" + "=" + MuxTime + Environment.NewLine);
                    sb.Append("OpenDate" + "=" + OpenDate + Environment.NewLine);
                    sb.Append("CloseDate" + "=" + CloseDate + Environment.NewLine);
                    sb.Append("LogSize" + "=" + LogSize + Environment.NewLine);
                    sb.Append("CmdOperationTime" + "=" + CmdOperationTime + Environment.NewLine);
                    sb.Append("DownLoadOperationTime" + "=" + DownLoadOperationTime + Environment.NewLine);
                    sb.Append("OnlineTime" + "=" + OnlineTime + Environment.NewLine);
                    string Msg = AES.Encrypt(sb.ToString());
                    File.WriteAllText(FilePath, Msg);
                }
            }
            catch 
            {

            }
        }

        public  static void Load(string FilePath)
        {
            string[] AllLine;

            try
            {
                Server.IpV4 = Server.GetLocalIPv4();
                HostUserName = easy_log.HostUserName;
                Server.StartTime(true);
                Server.IsOnline = true;
                string Content = File.ReadAllText(FilePath);
                Content = AES.Decrypt(Content);
                Content = Content.Trim(new char[] { '\r', '\n' });
                AllLine = Content.Split(new string[] {"\r\n" }, StringSplitOptions.None);
                Hashtable ht = new Hashtable();
                for (int i = 0; i < AllLine.Length; i++)
                {
                    string[] str = AllLine[i].Split('=');
                    ht.Add(str[0], str[1]);
                }

                if (ht.ContainsKey("Version"))
                    Version = Convert.ToUInt64(ht["Version"].ToString());
                if (ht.ContainsKey("HostUserName"))
                    HostUserName = ht["HostUserName"].ToString();
                if (ht.ContainsKey("IsOnline"))
                    IsOnline = Convert.ToBoolean(ht["IsOnline"].ToString());
                if (ht.ContainsKey("IpV4"))
                    IpV4 = ht["IpV4"].ToString();
                if (ht.ContainsKey("LogOperationTime"))
                    LogOperationTime = Convert.ToUInt64(ht["LogOperationTime"].ToString());
                if (ht.ContainsKey("UserTime"))
                    UserTime = Convert.ToUInt64(ht["UserTime"].ToString());
                if (ht.ContainsKey("MuxTime"))
                    MuxTime = Convert.ToUInt64(ht["MuxTime"].ToString());

                //if (ht.ContainsKey("OpenDate"))
                //    OpenDate = Convert.ToUInt64(ht["OpenDate"].ToString());

                //if (ht.ContainsKey("CloseDate"))
                //    CloseDate = Convert.ToUInt64(ht["CloseDate"].ToString());

                if (ht.ContainsKey("LogSize"))
                    LogSize = Convert.ToUInt64(ht["LogSize"].ToString());

                if (ht.ContainsKey("CmdOperationTime"))
                    CmdOperationTime = Convert.ToUInt64(ht["CmdOperationTime"].ToString());

                if (ht.ContainsKey("DownLoadOperationTime"))
                    DownLoadOperationTime = Convert.ToUInt64(ht["DownLoadOperationTime"].ToString());

                if (ht.ContainsKey("OnlineTime"))
                    OnlineTime = Convert.ToUInt64(ht["OnlineTime"].ToString());

                LoadOk = true;
            }
            catch 
            {
                LoadOk = false;
            }
        }

        public static string GetLocalIPv4()
        {
            string hostname = Dns.GetHostName();
            IPHostEntry localhost = Dns.GetHostEntry(hostname);
            if (localhost != null)
            {
                foreach (IPAddress item in localhost.AddressList)
                {
                    //判断是否是内网IPv4地址
                    if (item.AddressFamily == AddressFamily.InterNetwork)
                    {
                        return item.MapToIPv4().ToString();
                    }
                }
            }
            return "127.0.0.1";
        }

        public static void StartTime(bool flag)
        {
            if (flag)
            {
                TimeStart = DateTime.Now;;
                OpenDate = Convert.ToUInt64(DateTime.Now.ToString("yyyyMMddHHmmss"));
            }
            else
            {
                TimeEnd = DateTime.Now;
                CloseDate = Convert.ToUInt64(DateTime.Now.ToString("yyyyMMddHHmmss"));
            }
        }
        public static void GetWorkTime()
        {
            TimeSpan ts;
            StartTime(false);
            if (TimeEnd != null && TimeStart != null && TimeEnd > TimeStart)
            {
                ts = TimeEnd - TimeStart;
                OnlineTime += (ulong)ts.TotalSeconds;
            }
        }
    }
}
