using IkeCode.Core.Common;
using IkeCode.Core.Log;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace IkeCode.Core.Xml
{
    public class IkeCodeConfig
    {
        #region Attributes

        private static FileWatcher Watcher = new FileWatcher();
        private string File { get; set; }
        private string Section { get; set; }
        private bool IsDictionary { get; set; }

        public IkeCodeConfig Current { get; set; }

        #endregion

        #region Public Methods

        private IkeCodeConfig()
        {
            Current = this;
        }

        public IkeCodeConfig(string file, bool isDictionary = false)
            : this()
        {
            File = file;
            IsDictionary = isDictionary;
        }

        public IkeCodeConfig(string file, string section)
            : this(file)
        {
            Section = section;
        }

        public IkeCodeConfig(string file, string section, bool isDictionary = false)
            : this(file, section)
        {
            IsDictionary = isDictionary;
        }

        public static XDocument Load(string fileName, bool isDictionary, string cacheKey = "")
        {
            try
            {
                var path = GetPath(fileName, isDictionary, false);

                Watcher.Path = path;
                Watcher.FileName = fileName;
                Watcher.CacheKey = cacheKey;
                Watcher.Start();

                path += NormalizeFileName(fileName);

                IkeCodeLog.Default.Verbose("Loading configuration file: [" + path + "]");
                return XDocument.Load(path);
            }
            catch (FileNotFoundException ex)
            {
                IkeCodeLog.Default.Warning(string.Format("Configuration file not found on the configuration folders: [{0}]", ex.FileName));
                throw;
            }
            catch (Exception ex)
            {
                IkeCodeLog.Default.Exception(ex);
                throw;
            }
        }

        public static string GetConfigFolderPath()
        {
            return AppContext.BaseDirectory + @"Config\";
        }

        public string GetText(string section, string key)
        {
            return GetValue(section, key);
        }

        public Dictionary<string, Dictionary<string, string>> GetDictionary()
        {
            var result = new Dictionary<string, Dictionary<string, string>>();

            var xml = Load(File, IsDictionary);
            var doc = XDocument.Parse(xml.ToString());

            foreach (XElement element in doc.Elements().Descendants())
            {
                int keyInt = 0;
                var parentKeyName = element.Name.LocalName;
                var keyName = "";
                var values = new Dictionary<string, string>();

                foreach (var child in element.Descendants())
                {
                    keyName = child.Name.LocalName;
                    while (result.ContainsKey(child.Name.LocalName))
                        keyName = keyName + "_" + keyInt++;
                    values.Add(keyName, child.Value);
                }

                result.Add(parentKeyName, values);
            }

            return result;
        }

        //public string GetJson()
        //{
        //    var path = GetPath(File, IsDictionary, true);
        //    var doc = new XmlDocument();
        //    doc.LoadXml(path);
        //
        //    return doc.ToJsonString();
        //}

        public object GetAttribute(string name)
        {
            XDocument xml = Load(File, IsDictionary);
            XAttribute attributeValue = null;

            if (xml != null)
            {
                attributeValue = (from item in xml.Root.Descendants(Section)
                                  select item.Attribute(name)).FirstOrDefault();

                if (attributeValue == null)
                {
                    attributeValue = (from item in xml.Root.Descendants("default")
                                      select item.Attribute(name)).FirstOrDefault();
                }
            }

            return attributeValue != null ? attributeValue.Value : "[" + Section + "|default" + "/" + name + "]";
        }

        public string GetValue(string section, string key)
        {
            XDocument xml = Load(File, IsDictionary);

            var xmlElements = (from item in xml.Root.Descendants(section)
                               select item.Element(key)).FirstOrDefault();

            return xmlElements != null ? xmlElements.Value : "[" + section + "/" + key + "]";
        }

        public string GetString(string key)
        {
            return (string)GetValue(key);
        }

        public int GetInt(string key)
        {
            int result = 0;
            int.TryParse(GetString(key), out result);
            return result;
        }

        public decimal GetDecimal(string key)
        {
            decimal result = 0;
            decimal.TryParse(GetString(key), out result);
            return result;
        }

        public double GetDouble(string key)
        {
            double result = 0;
            double.TryParse(GetString(key), out result);
            return result;
        }

        public float GetFloat(string key)
        {
            float result = 0;
            float.TryParse(GetString(key), out result);
            return result;
        }

        public bool GetBoolean(string key)
        {
            bool result;
            bool.TryParse(GetString(key), out result);
            return result;
        }

        public DateTime GetDateTime(string key)
        {
            DateTime result = new DateTime();
            DateTime.TryParse(GetString(key), out result);
            return result;
        }

        public object GetObject(string key)
        {
            return GetValue(key);
        }

        #endregion

        #region Private Methods

        private static string GetPath(string fileName, bool isDictionary, bool includeFileName)
        {
            var pathSeparator = Path.DirectorySeparatorChar;
            var dicPath = isDictionary ? "Dictionary" : "";

            var path = GetConfigFolderPath();

            path += dicPath;
            path += !string.IsNullOrWhiteSpace(dicPath) ? pathSeparator.ToString() : "";
            path += includeFileName ? NormalizeFileName(fileName) : "";
            return path;
        }

        private static string NormalizeFileName(string fileName)
        {
            return fileName.EndsWith(".xml") || fileName.EndsWith(".config") ? fileName : fileName + ".xml";
        }

        private object GetValue(string key)
        {
            XDocument xml = Load(File, IsDictionary);
            XElement xmlElements = null;

            if (xml != null)
            {
                xmlElements = (from item in xml.Root.Descendants(Section)
                               select item.Element(key)).FirstOrDefault();

                if (xmlElements == null)
                {
                    xmlElements = (from item in xml.Root.Descendants("default")
                                   select item.Element(key)).FirstOrDefault();

                }
            }
            return xmlElements != null ? xmlElements.Value : "[" + Section + "|default" + "/" + key + "]";
        }

        #endregion
    }
}
