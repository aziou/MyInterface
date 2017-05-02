using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
namespace OperateData
{
   public  class FunctionXml
    {
        #region 变量
        private static object m_lock = new object();
        #endregion
        #region -----XML文件函数

        #region 读取
        /// <summary>
        /// 读取节点值，属性定位
        /// </summary>
        /// <param name="NodePath">节点路径，例:"Config/Version/Ver"</param>
        /// <param name="Tag">用于定位的属性名,为空表示无属性名，直接取第一个</param>
        /// <param name="TagValue">属性值</param>
        /// <param name="GetTag">要获取的属性值，若为空，则获取innerText</param>
        /// <param name="DefaultValue">默认值</param>
        /// <param name="XmlPath">xml文件的路径</param>
        /// <returns></returns>
        public static string ReadElement(string NodePath, string Tag, string TagValue, string GetTag, string DefaultValue, string XmlPath)
        {
            string value = "";

            if (NodePath.Substring(NodePath.Length - 1, 1) == "/")
                NodePath = NodePath.Substring(0, NodePath.Length - 1);

            if (NodePath.IndexOf("\\") > 0)
                NodePath = NodePath.Replace("\\", "/");

            XmlDocument xmlDoc = LoadXmlFile(NodePath, XmlPath);

            XmlElement xe = GetElement(NodePath, Tag, TagValue, xmlDoc);//获取节点
            if (xe != null)
            {
                if (GetTag.Length == 0)//判断取值位置
                {
                    value = xe.InnerText;
                }
                else
                {
                    value = xe.GetAttribute(GetTag);
                    if (value.Length == 0) UpdateElement(NodePath, Tag, TagValue, GetTag, DefaultValue, XmlPath);
                }
            }
            else
            {
                UpdateElement(NodePath, Tag, TagValue, GetTag, DefaultValue, XmlPath);
            }
            if (value.Length == 0)
            {
                value = DefaultValue;
            }
            return value;
        }
        /// <summary>
        /// 读取节点值，无需属性定位
        /// </summary>
        /// <param name="NodePath"></param>
        /// <param name="GetTag"></param>
        /// <param name="DefaultValue"></param>
        /// <param name="XmlPath"></param>
        /// <returns></returns>
        public static string ReadElement(string NodePath, string GetTag, string DefaultValue, string XmlPath)
        {
            return ReadElement(NodePath, "", "", GetTag, DefaultValue, XmlPath);
        }
        /// <summary>
        /// 读取指定路径下的一批节点的一批值,相对ReadElement调用复杂一点
        /// </summary>
        /// <param name="NodePath">节点所在路径</param>
        /// <param name="Tag">要读取的属性值，为空则读取innertext</param>
        /// <param name="XmlPath">XML文件路径</param>
        /// <returns></returns>
        public static List<string>[] ReadNodes(string NodePath, string[] Tag, string XmlPath)
        {
            int TagNum = Tag.Length;

            List<string>[] value = new List<string>[TagNum];
            for (int i = 0; i < TagNum; i++)
            {
                value[i] = new List<string>();
            }
            XmlDocument xmlDoc = LoadXmlFile(NodePath, XmlPath);
            XmlNodeList nodelist = xmlDoc.SelectNodes(NodePath);
            foreach (XmlNode node in nodelist)
            {
                for (int i = 0; i < TagNum; i++)
                {
                    if (Tag[i].Length == 0)
                    {
                        value[i].Add(node.InnerText);
                    }
                    else
                    {
                        XmlElement xe = (XmlElement)node;
                        value[i].Add(xe.GetAttribute(Tag[i]));
                    }
                }
            }

            return value;
        }
        private static XmlDocument LoadXmlFile(string NodePath, string XmlPath)
        {
            XmlDocument xmlDoc = new XmlDocument();
            if (!File.Exists(XmlPath))
            {
                string[] temp = NodePath.Split('/');
                string root = "";
                for (int i = 0; i < temp.Length; i++)
                {
                    if (temp[i].Length > 0)
                    {
                        root = temp[i];
                        break;
                    }
                }
                CreateXml(XmlPath, root);
            }
            xmlDoc.Load(XmlPath);
            return xmlDoc;
        }
        #endregion

        #region 更新
        /// <summary>
        /// 更新节点属性,若没有找到会自动增加该节点
        /// </summary>
        /// <param name="NodePath">节点路径</param>
        /// <param name="Tag">识别标识符</param>
        /// <param name="TagValue">标识符的值</param>
        /// <param name="UpdateTag">要更新的属性，为0更新innerText</param>
        /// <param name="UpdateValue">更新的值</param>
        /// <param name="XmlPath">XML文件路径</param>
        /// <returns></returns>
        public static bool UpdateElement(string NodePath, string Tag, string TagValue, string UpdateTag, string UpdateValue, string XmlPath)
        {
            lock (m_lock)
            {
                bool bReturn = false;
                XmlDocument xmlDoc = LoadXmlFile(NodePath, XmlPath);

                XmlElement xe = GetElement(NodePath, Tag, TagValue, xmlDoc);
                if (xe != null)
                {
                    if (UpdateTag.Length == 0)
                    {
                        xe.InnerText = UpdateValue;
                    }
                    else
                    {
                        xe.SetAttribute(UpdateTag, UpdateValue);
                    }
                    xmlDoc.Save(XmlPath);
                    bReturn = true;
                }
                else
                {
                    //增加节点
                    string[,] addAttrib = { { Tag, TagValue } };
                    string addNodePath = NodePath;
                    String addElementName = "";
                    while (true)
                    {
                        if (addNodePath.Substring(addNodePath.Length - 1).Equals("/"))
                        {
                            addNodePath = addNodePath.Substring(0, addNodePath.Length - 1);
                        }
                        else
                        {
                            int index = addNodePath.LastIndexOf('/');
                            addElementName = addNodePath.Substring(index + 1);
                            addNodePath = addNodePath.Substring(0, index);
                            break;
                        }
                    }
                    if (addNodePath.Length > 0 && addElementName.Length > 0 && AddElement(addNodePath, addElementName, addAttrib, XmlPath))
                    {
                        UpdateElement(NodePath, Tag, TagValue, UpdateTag, UpdateValue, XmlPath);
                    }
                }
                return bReturn;
            }
        }
        /// <summary>
        /// 更新节点值，无需属性定位
        /// </summary>
        /// <param name="NodePath"></param>
        /// <param name="UpdateTag"></param>
        /// <param name="UpdateValue"></param>
        /// <param name="XmlPath"></param>
        /// <returns></returns>
        public static bool UpdateElement(string NodePath, string UpdateTag, string UpdateValue, string XmlPath)
        {
            return UpdateElement(NodePath, "", "", UpdateTag, UpdateValue, XmlPath);
        }
        #endregion

        #region lee 
        public static List<string> GetAllNodeData(string NodePath, string Tag, string BackKey, string xmlPath)
        {
            XmlDocument xmlDoc = new XmlDocument();
            List<string> Value_Col = new List<string>();
            xmlDoc.Load(xmlPath); //加载xml文件
            XmlNode xn = xmlDoc.SelectSingleNode(NodePath);
            XmlElement xe = null;
            try
            {
                foreach (XmlNode temp in xn.ChildNodes)
                {
                    if (temp.Name == Tag)
                    {
                        xe = (XmlElement)temp;
                        Value_Col.Add(xe.GetAttribute(BackKey));
                    }
                }
            }
            catch (Exception e)
            { 
            
            }
         

            return Value_Col;
        }
        #endregion

        #region 增加
        /// <summary>
        /// 增加节点
        /// </summary>
        /// <param name="NodePath">增加节点的上级节点</param>
        /// <param name="ElementName">节点名称</param>
        /// <param name="attrib">[属性内容,属性值]</param>
        /// <param name="XmlPath">文件名</param>
        /// <returns></returns>
        public static bool AddElement(string NodePath, String ElementName, string[,] attrib, string XmlPath)
        {
            lock (m_lock)
            {
                bool bReturn = false;
                XmlDocument xmlDoc = LoadXmlFile(NodePath, XmlPath);
                XmlNode root = xmlDoc.SelectSingleNode(NodePath);
                if (root != null && ElementName.Trim() != "")
                {
                    XmlElement xe = xmlDoc.CreateElement(ElementName);
                    for (int i = 0; i < attrib.GetLength(0); i++)
                    {
                        if (attrib[i, 0] == null || attrib[i, 0].Length == 0)
                        {
                            if (!(attrib[i, 1] == null || attrib[i, 1].Length == 0))
                            {
                                xe.InnerText = attrib[i, 1];
                            }
                        }
                        else
                        {
                            xe.SetAttribute(attrib[i, 0], attrib[i, 1] == null ? "" : attrib[i, 1]);
                        }
                    }
                    root.AppendChild(xe);
                    xmlDoc.Save(XmlPath);
                    bReturn = true;
                }
                return bReturn;
            }
        }
        /// <summary>
        /// 生成XML文件
        /// </summary>
        /// <param name="XmlPath">文件路径，如"C:\\text.xml"</param>
        /// <param name="RootName">根元素的名称</param>
        /// <returns></returns>
        public static bool CreateXml(string XmlPath, string RootName)
        {
            bool bReturn = false;
            XmlTextWriter writer = new XmlTextWriter(XmlPath, Encoding.Default);
            writer.Formatting = Formatting.Indented;
            writer.WriteStartDocument(true);
            writer.WriteStartElement(RootName);
            writer.WriteFullEndElement();
            writer.WriteEndDocument();
            writer.Close();
            return bReturn;
        }
        #endregion

        #region 删除
        /// <summary>
        /// 彻底删除节点和子节点
        /// </summary>
        /// <param name="NodePath">节点名称</param>
        /// <param name="Tag">属性名称</param>
        /// <param name="TagValue">属性值</param>
        /// <param name="XmlPath"></param>
        /// <returns></returns>
        public static bool RemoveNode(string NodePath, string Tag, string TagValue, string XmlPath)
        {
            lock (m_lock)
            {
                bool bReturn = false;
                if (File.Exists(XmlPath))
                {
                    XmlDocument xmlDoc = LoadXmlFile(NodePath, XmlPath);
                    XmlElement xe = GetElement(NodePath, Tag, TagValue, xmlDoc);//获取节点
                    if (xe != null)
                    {
                        xe.ParentNode.RemoveChild(xe);
                        xmlDoc.Save(XmlPath);
                        bReturn = true;
                    }
                }
                return bReturn;
            }
        }
        /// <summary>
        /// 删除节点和子节点，无定位
        /// </summary>
        /// <param name="NodePath"></param>
        /// <param name="XmlPath"></param>
        /// <returns></returns>
        public static bool RemoveNode(string NodePath, string XmlPath)
        {
            return RemoveNode(NodePath, "", "", XmlPath);
        }

        /// <summary>
        /// 删除该节点的子节点
        /// </summary>
        /// <param name="NodePath">节点名称</param>
        /// <param name="Tag">属性名称</param>
        /// <param name="TagValue">属性值</param>
        /// <param name="XmlPath"></param>
        /// <returns></returns>
        public static bool RemoveChilds(string NodePath, string Tag, string TagValue, string XmlPath)
        {
            lock (m_lock)
            {
                bool bReturn = false;
                if (File.Exists(XmlPath))
                {
                    XmlDocument xmlDoc = LoadXmlFile(NodePath, XmlPath);
                    XmlElement xe = GetElement(NodePath, Tag, TagValue, xmlDoc);//获取节点
                    if (xe != null)
                    {
                        xe.RemoveAll();
                        if (Tag.Trim() != "")
                        {
                            xe.SetAttribute(Tag, TagValue);
                        }
                        xmlDoc.Save(XmlPath);
                        bReturn = true;
                    }
                }
                return bReturn;
            }
        }
        /// <summary>
        /// 删除节点的子节点，无定位
        /// </summary>
        /// <param name="NodePath"></param>
        /// <param name="XmlPath"></param>
        /// <returns></returns>
        public static bool RemoveChilds(string NodePath, string XmlPath)
        {
            return RemoveChilds(NodePath, "", "", XmlPath);
        }
        #endregion

        #region 私有
        /// <summary>
        /// 查找节点
        /// </summary>
        /// <param name="NodePath"></param>
        /// <param name="Tag"></param>
        /// <param name="TagValue"></param>
        /// <param name="xmlDoc"></param>
        /// <returns></returns>
        private static XmlElement GetElement(string NodePath, string Tag, string TagValue, XmlDocument xmlDoc)
        {
            XmlNodeList nodelist = xmlDoc.SelectNodes(NodePath);
            XmlElement xeReturn = null;
            XmlElement xe = null;
            foreach (XmlNode node in nodelist)
            {
                if (Tag.Length == 0)
                {
                    xeReturn = (XmlElement)node;
                    break;
                }
                else
                {
                    xe = (XmlElement)node;
                    if (xe.GetAttribute(Tag).Equals(TagValue))
                    {
                        xeReturn = xe;
                        break;
                    }
                }
            }
            return xeReturn;
        }
        #endregion

        #endregion
    }
}
