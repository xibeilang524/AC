﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AC.Base.ClassifySearchs
{
    /// <summary>
    /// 按照分类编号搜索分类。
    /// </summary>
    public class IdFilter : IClassifyFilter
    {
        /// <summary>
        /// 按照分类编号搜索分类。
        /// </summary>
        public IdFilter()
        {
        }

        /// <summary>
        /// 按照分类编号搜索分类。
        /// </summary>
        /// <param name="id"></param>
        public IdFilter(int id)
        {
            this.Ids = new int[] { id };
        }

        /// <summary>
        /// 按照分类编号搜索分类。
        /// </summary>
        /// <param name="ids"></param>
        public IdFilter(int[] ids)
        {
            this.Ids = ids;
        }

        /// <summary>
        /// 按照分类编号搜索分类。
        /// </summary>
        /// <param name="ids"></param>
        public IdFilter(ICollection<int> ids)
        {
            this.Ids = new int[ids.Count];
            ids.CopyTo(this.Ids, 0);
        }

        /// <summary>
        /// 按照分类编号搜索分类。
        /// </summary>
        /// <param name="ids">一个或多个分类 ID 编号组成的字符串，如果是多个 ID 则使用“,”分隔。</param>
        public IdFilter(string ids)
        {
            string[] strIds = ids.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            this.Ids = new int[strIds.Length];
            for (int intIndex = 0; intIndex < strIds.Length; intIndex++)
            {
                this.Ids[intIndex] = Function.ToInt(strIds[intIndex]);
            }
        }

        /// <summary>
        /// 该筛选器所筛选的分类编号。
        /// </summary>
        public int[] Ids { get; set; }

        #region IClassifyFilter 成员

        /// <summary>
        /// 检查传入的分类是否符合该筛选器筛选要求。
        /// </summary>
        /// <param name="classify">被检查的分类。</param>
        /// <returns></returns>
        public bool ClassifyFilterCheck(Classify classify)
        {
            if (classify.ClassifyId > 0 && this.Ids != null && this.Ids.Length > 0)
            {
                foreach (int intId in this.Ids)
                {
                    if (classify.ClassifyId == intId)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        #endregion

        #region IFilter 成员

        /// <summary>
        /// 筛选器应用场合选项。如果该筛选器不可以应用在任何场合则返回“0”
        /// </summary>
        public Searchs.FilterAppliesOptions FilterAppliesAttribute
        {
            get { return Searchs.FilterAppliesOptions.All; }
        }

        /// <summary>
        /// 有关该筛选器的功能说明、使用说明。
        /// </summary>
        public string FilterRemarkAttribute
        {
            get { return "搜索指定 ID 编号的分类"; }
        }

        /// <summary>
        /// 设置应用程序框架。
        /// </summary>
        /// <param name="application"></param>
        public void SetApplication(ApplicationClass application)
        {
        }

        /// <summary>
        /// 筛选器名称。一个筛选器可以有一个或多个名称，通常应至少设置一个名称；如果不希望当前筛选器被使用，则 null。
        /// </summary>
        public string[] FilterNamesAttribute
        {
            get { return new string[] { "分类ID" }; }
        }

        /// <summary>
        /// 设置当前使用的筛选器名称。例如用户自定义分类，筛选器只能有一个，而通过 FilterNamesAttribute 返回诸如地区、行业、类型等参数后，可以将此筛选器变为地区筛选器、行业筛选器、类型筛选器，filterName 参数则是指定当前是地区筛选器还是行业筛选器。
        /// </summary>
        /// <param name="filterName">当前使用的筛选器名称。</param>
        public void SetFilterName(string filterName)
        {
        }

        /// <summary>
        /// 此筛选器执行的 SQL 语句。
        /// </summary>
        /// <param name="enableTableName">返回的条件语句是否需要附加表名。例如 table.column = 12 或者 column = 12</param>
        /// <returns></returns>
        public string GetFilterSql(bool enableTableName)
        {
            if (this.Ids != null && this.Ids.Length > 0)
            {
                if (this.Ids.Length == 1)
                {
                    return (enableTableName ? Tables.Classify.TableName + "." : "") + Tables.Classify.ClassifyId + "=" + this.Ids[0];
                }
                else
                {
                    List<int> lstId = new List<int>();
                    string strSql = (enableTableName ? Tables.Classify.TableName + "." : "") + Tables.Classify.ClassifyId + " IN (";
                    foreach (int intId in this.Ids)
                    {
                        if (lstId.Contains(intId) == false)
                        {
                            strSql += intId + ",";
                            lstId.Add(intId);
                        }
                    }
                    strSql = strSql.Substring(0, strSql.Length - 1) + ")";
                    return strSql;
                }
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 获取该筛选器所执行的筛选逻辑的文字描述。
        /// </summary>
        /// <returns></returns>
        public string GetFilterDescription()
        {
            if (this.Ids != null && this.Ids.Length > 0)
            {
                if (this.Ids.Length == 1)
                {
                    return "分类编号为" + this.Ids[0];
                }
                else
                {
                    List<int> lstId = new List<int>();
                    string strSql = "";
                    foreach (int intId in this.Ids)
                    {
                        if (lstId.Contains(intId) == false)
                        {
                            strSql += "、" + intId;
                            lstId.Add(intId);
                        }
                    }
                    strSql = "分类编号是" + strSql.Substring(1) + "的分类";
                    return strSql;
                }
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 从保存此筛选器数据的 XML 文档节点集合初始化当前筛选器。
        /// </summary>
        /// <param name="xmlNode">该对象节点的数据集合</param>
        public void SetFilterConfig(System.Xml.XmlNode xmlNode)
        {
            this.Ids = new int[xmlNode.ChildNodes.Count];
            for (int intIndex = 0; intIndex < xmlNode.ChildNodes.Count; intIndex++)
            {
                this.Ids[intIndex] = Function.ToInt(xmlNode.ChildNodes[intIndex].InnerText);
            }
        }

        /// <summary>
        /// 获取当前筛选器的状态，将序列化的内容填充至 XmlNode 的 InnerText 属性或者 ChildNodes 子节点中。
        /// </summary>
        /// <param name="xmlDoc">创建 XmlNode 时所需使用到的 System.Xml.XmlDocument。</param>
        public System.Xml.XmlNode GetFilterConfig(System.Xml.XmlDocument xmlDoc)
        {
            if (this.Ids != null && this.Ids.Length > 0)
            {
                System.Xml.XmlNode xnConfig = xmlDoc.CreateElement(this.GetType().Name);

                foreach (int intId in this.Ids)
                {
                    System.Xml.XmlNode xnId = xmlDoc.CreateElement("Id");
                    xnId.InnerText = intId.ToString();
                    xnConfig.AppendChild(xnId);
                }
                return xnConfig;
            }
            else
            {
                return null;
            }
        }

        #endregion
    }
}
