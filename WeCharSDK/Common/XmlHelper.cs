using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using System.Xml.Serialization;
using weixinweb.MessageType;

namespace weixinweb.Common
{
    public static class XmlHelper
    {
        public static readonly IDictionary<Type, List<string>> TypeOrdersMapping = new Dictionary<Type, List<string>>
        {
            {
                typeof (TextResponse),
                new List<string> {"ToUserName", "FromUserName", "CreateTime", "MsgType", "Content"}
            },
            //{
            //    typeof (NewsResponseMsg),
            //    new List<string> {"ToUserName", "FromUserName", "CreateTime", "MsgType", "ArticleCount", "Articles"}
            //},
        };

        public static XDocument ToXml<T>(this T obj)
            where T : class
        {
            try
            {
                var doc = new XDocument();
                doc.Add(new XElement("xml"));
                XElement root = doc.Root;

                List<PropertyInfo> props = obj.GetType().GetProperties().ToList();

                if (TypeOrdersMapping.ContainsKey(typeof(T)))
                {
                    props =
                        obj.GetType()
                            .GetProperties()
                            .OrderBy(p => TypeOrdersMapping[typeof(T)].IndexOf(p.Name))
                            .ToList();
                }

                foreach (PropertyInfo prop in props)
                {
                    string propName = prop.Name;

                    if (prop.IsDefined(typeof(XmlCDataAttribute), true))
                    {
                        root.Add(prop.PropertyType == typeof(Enum)
                            ? new XElement(propName, new XCData(prop.GetValue(obj, null).ToString().ToLower()))
                            : new XElement(propName, new XCData(prop.GetValue(obj, null).ToString())));
                    }
                    else if (prop.IsDefined(typeof(XmlArrayItemAttribute), true))
                    {
                        var pElement = new XElement(propName);
                        var itemValues = prop.GetValue(obj, null) as IEnumerable;
                        foreach (object itemValue in itemValues)
                        {
                            IEnumerable<XElement> content = itemValue.ToXml().Root.Elements();
                            var sElement = new XElement("item", content);
                            pElement.Add(sElement);
                        }
                        root.Add(pElement);
                    }
                    else if (prop.IsDefined(typeof(XmlArrayAttribute), true))
                    {
                    }
                    else
                    {
                        root.Add(prop.PropertyType == typeof(DateTime)
                            ? new XElement(propName, ((DateTime)prop.GetValue(obj, null)).GetWeixinDateTime())
                            : new XElement(propName, prop.GetValue(obj, null)));
                    }
                }

                return doc;
            }
            catch (Exception ex)
            {
                throw new Exception("调用XmlHelper ToXml方法出错！", ex);
            }
        }

        public static long GetWeixinDateTime(this DateTime dateTime)
        {
             DateTime baseTime = new DateTime(1970, 1, 1); //Unix起始时间
            return (dateTime.Ticks - baseTime.Ticks) / 10000000 - 8 * 60 * 60;
        }
    }
}