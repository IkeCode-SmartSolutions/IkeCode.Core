using IkeCode.Core.Log;
using System.IO;

namespace IkeCode.Core.Common
{
    public class FileWatcher
    {
        #region Attributes

        public string Path { get; set; }
        public string FileName { get; set; }
        public string CacheKey { get; set; }

        #endregion

        #region Public Methods

        public FileWatcher() { }

        public FileWatcher(string path, string fileName, string cacheKey)
        {
            Path = path;
            FileName = fileName;
            CacheKey = cacheKey;
        }
        private FileSystemWatcher _watcher;
        public void Start()
        {
            _watcher = new FileSystemWatcher();
            _watcher.Path = Path;
            _watcher.IncludeSubdirectories = true;
            _watcher.Filter = "*.xml";
            _watcher.EnableRaisingEvents = true;
            _watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.Size | NotifyFilters.FileName | NotifyFilters.CreationTime;
            _watcher.Renamed += new RenamedEventHandler(watcher_Renamed);
            _watcher.Changed += new FileSystemEventHandler(watcher_Changed);
        }

        public void Stop()
        {
            _watcher.EnableRaisingEvents = false;
            _watcher.Dispose();
        }

        #endregion

        #region Events

        void watcher_Changed(object sender, FileSystemEventArgs e)
        {
            IkeCodeLog.Default.Verbose(string.Format("File modified: [{0}/{1}]", e.FullPath, e.Name));
        }

        void watcher_Renamed(object sender, RenamedEventArgs e)
        {
            IkeCodeLog.Default.Warning(string.Format("File renamed: [{0}/{1}]", e.FullPath, e.Name));
        }

        #endregion
    }
}
