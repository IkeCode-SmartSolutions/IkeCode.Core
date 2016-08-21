using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace IkeCode.Core.Log
{
    public class IkeCodeLog
    {
        #region Attributes

        public string FileNamePrefix { get; private set; }
        public static IkeCodeLog Default { get { return new IkeCodeLog(); } }
        private XDocument xml { get; set; }

        #endregion

        public IkeCodeLog()
        {
            FileNamePrefix = "IkeCode_";
            var directoryName = AppContext.BaseDirectory;
            
            //For migrations
            if (string.IsNullOrEmpty(directoryName))
            {
                var absolutePath = new Uri(Assembly.GetEntryAssembly().CodeBase).AbsolutePath;
                directoryName = Path.GetDirectoryName(absolutePath);                
            }

            var path = directoryName + @"\Config\IkeCodeLog.config";

            if (!File.Exists(path))
            {
                throw new FileNotFoundException(string.Format("The file IkeCodeLog.config was not found on path {0}", path));
            }

            xml = XDocument.Load(path);
        }

        public IkeCodeLog(string fileNamePrefix)
            : this()
        {
            FileNamePrefix = fileNamePrefix;
        }

        #region Private Methods

        private string GetConfig(string key)
        {
            XElement element = null;

            if (xml != null)
            {
                element = (from item in xml.Root.Elements("ikecode").Elements("default")
                           select item.Element(key)).FirstOrDefault();

            }
            return element != null ? element.Value : "[default" + "/" + key + "]";
        }

        private void Write(string message, string fileName)
        {
            Task.Factory.StartNew(() =>
                {
                    var name = string.Format("{0}{1}_{2}.txt", FileNamePrefix, DateTime.Today.ToString("yyyyMMdd"), fileName);

                    var path = GetConfig("logPath");
                    if (string.IsNullOrWhiteSpace(path))
                        path = Path.Combine(AppContext.BaseDirectory, "logs");
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);

                    path = Path.Combine(path, name);
                    var logMessage = string.Format("{0}: {1}", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"), message);

                    using (var fileStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
                    {
                        using (var writer = new StreamWriter(fileStream))
                        {
                            writer.BaseStream.Seek(0, SeekOrigin.End);
                            writer.WriteLine(logMessage);
                        }
                    }
                });
        }

        private bool CheckIfEnabled(string type)
        {
            var isEnabled = false;

            if (xml != null)
            {
                var element = (from item in xml.Root.Elements("ikecode").Elements(type)
                               select item.Element("enabled")).FirstOrDefault();

                if (element != null)
                    isEnabled = element.Value.ToBoolean();
            }

            return isEnabled;
        }

        #endregion

        #region Public Methods

        public void Metric(string methodName, Stopwatch stopWatch, params object[] parameters)
        {
            if (CheckIfEnabled("metric"))
            {
                var elapsed = stopWatch.Elapsed;
                Write(string.Format("{0} [parameters: {1}]: Time Elapsed -> {2:00}:{3:00}:{4:00}.{5:00}",
                    methodName,
                    string.Join(", ", parameters),
                    elapsed.Hours,
                    elapsed.Minutes,
                    elapsed.Seconds,
                    elapsed.Milliseconds / 10), "Metric");
            }
        }

        public void Verbose(string message)
        {
            if (CheckIfEnabled("verbose"))
            {
                Write(message, "Verbose");
            }
        }

        public void Verbose(string methodName, string message, params object[] parameters)
        {
            Verbose(string.Format("{0} [parameters: {1}]: {2}", methodName, string.Join(", ", parameters), message));
        }

        public void Exception(Exception exception)
        {
            if (CheckIfEnabled("exception"))
            {
                var message = string.Format("[{0}] -> StackTrace [{1}]", exception.Message, exception.StackTrace);
                Write(message, "Exception");
            }
        }

        public void Exception(string methodName, Exception exception, params object[] parameters)
        {
            var message = string.Format("{0} [parameters: {1}]: [{2}] -> StackTrace [{3}]", methodName, string.Join(", ", parameters), exception.Message, exception.StackTrace);
            Exception(exception);
        }

        public void Warning(string message)
        {
            if (CheckIfEnabled("warning"))
            {
                Write(message, "Warning");
            }
        }

        public void Warning(string methodName, string message, params object[] parameters)
        {
            Warning(string.Format("{0} [parameters: {1}]: {2}", methodName, string.Join(", ", parameters), message));
        }

        #endregion
    }
}
