using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
namespace SOM_DEVELOP_TOOL
{
    public class XmlTool
    {
        public static void AddFile(string KeilFileName,string[] AddFileName)
        {
            //string OutFileName = Path.GetDirectoryName(KeilFileName) + "\\result.uvprojx";
            XmlDocument xmlDoc = GetXml(KeilFileName);
            var root = xmlDoc.DocumentElement;//取到根结点
            for (int i = 0; i < AddFileName.Length; i++)
            {
                string InFile = "." + AddFileName[i].Replace(Path.GetDirectoryName(KeilFileName),"");
                AddFileElement(xmlDoc, "user", InFile);
            }
            using (XmlTextWriter xtw = new XmlTextWriter(KeilFileName, null))
            {
                //None表示不应用特殊格式，另一个相反枚举值Indented表示缩进 
                xtw.Formatting = Formatting.None;

                xmlDoc.Save(xtw);
            }
        }

        public static void EditUserCmd(string KeilFileName, int CMDID,string InCmd)
        {
            //string OutFileName = Path.GetDirectoryName(KeilFileName) + "\\result.uvprojx";
            XmlDocument xmlDoc = GetXml(KeilFileName);
            var root = xmlDoc.DocumentElement;//取到根结点
            string[] NodeName = new string[] { "Targets", "Target", "TargetOption", "TargetCommonOption", "AfterMake" };
            //获取根节点的所有子节点   
            XmlNodeList nodeList = xmlDoc.SelectSingleNode("Project").ChildNodes;
            XmlElement childElement = null;
            CMDID = CMDID+1;
            for (int i = 0; i < NodeName.Length; i++)
            {
                foreach (XmlNode childNode in nodeList)
                {
                    childElement = (XmlElement)childNode;
                    if (childElement.Name == NodeName[i])
                        break;
                }
                nodeList = childElement.ChildNodes;
            }

            foreach (XmlNode childNode in nodeList)
            {
                if(childNode.Name == "RunUserProg" + CMDID.ToString())
                {
                    childNode.InnerText = "1";
                }
                if (childNode.Name == "UserProg" + CMDID.ToString() + "Name")
                {
                    childNode.InnerText = InCmd;
                }
                //Debug.AppendMsg(childNode.Name + Environment.NewLine);
            }

            using (XmlTextWriter xtw = new XmlTextWriter(KeilFileName, null))
            {
                //None表示不应用特殊格式，另一个相反枚举值Indented表示缩进 
                xtw.Formatting = Formatting.None;

                xmlDoc.Save(xtw);
            }
        }

        public static void AddFileElement(XmlDocument xmlDoc, string name,string add_element_name )
        {
            string[] NodeName = new string[] { "Targets", "Target", "Groups", "Group"};
            //获取根节点的所有子节点   
            XmlNodeList nodeList = xmlDoc.SelectSingleNode("Project").ChildNodes;
            XmlElement childElement=null;

            XmlElement personElement = xmlDoc.CreateElement("File");
            XmlElement personElement1 = xmlDoc.CreateElement("FileName");
            XmlElement personElement2 = xmlDoc.CreateElement("FileType");
            XmlElement personElement3 = xmlDoc.CreateElement("FilePath");
            personElement1.InnerText = Path.GetFileName(add_element_name);
            personElement2.InnerText = "1";
            personElement3.InnerText = add_element_name;
            personElement.AppendChild(personElement1);
            personElement.AppendChild(personElement2);
            personElement.AppendChild(personElement3);

            for (int i = 0; i < NodeName.Length; i++)
            {
                foreach (XmlNode childNode in nodeList)
                {
                     childElement = (XmlElement)childNode;
                    if (childElement.Name == NodeName[i]) 
                    break;
                }
                nodeList = childElement.ChildNodes;
            }

            bool Flag = false;
            foreach (XmlNode childNode in nodeList)
            {
                if (childNode.InnerText == name)
                {
                  
                    Flag = true;
                }
                if (Flag == true && childNode.Name == "Files")
                {
                    bool FileExist = false;
                    XmlNode xmldocSelect = (XmlNode)childNode;
                    nodeList = ((XmlElement)childNode).ChildNodes;
                    foreach (XmlNode curnode in nodeList)
                    {
                        if(curnode.InnerText.Contains(add_element_name))
                            FileExist = true;
                    }
                    if (FileExist == false)
                    {
                        xmldocSelect.AppendChild(personElement);
                        Debug.AppendMsg("File add" + Environment.NewLine);
                    }
                    else
                        Debug.AppendMsg("File repeat" + Environment.NewLine);
                    //childNode.ChildNodes;
                }
            }
              
        }
        public static XmlDocument GetXml(string FileName)
        {
            XmlDocument xmlDoc = new XmlDocument();
            if (!File.Exists(FileName))
            {
                return null;
            }
            xmlDoc.Load(FileName);
            return xmlDoc;
        }
    }
}
