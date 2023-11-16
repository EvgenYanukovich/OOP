using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab12 {
    public class YEDFileInfoException : Exception {
        public YEDFileInfoException(string message) : base(message) { }
    }

    public record YEDFileInfoRecord(
        string FullPath,
        string Directory,
        string Name,
        string Extension,
        long Size,
        DateTime CreationTime,
        DateTime ModificationTime
    );

    public class YEDFileInfo {
        private readonly string _filePath;
        private YEDLog? _logger;

        public YEDLog? Logger {
            set => _logger = value;
        }

        public string FilePath { 
            get => _filePath;
        }

        public YEDFileInfo(string filePath) {
            if (!File.Exists(filePath)) {
                throw new YEDFileInfoException($"File '{filePath}' does not exist");
            }
            _filePath = filePath;
        }

        public YEDFileInfoRecord GetInfo() {
            _logger?.Info($"Getting file info for file '{_filePath}'");
            FileInfo fileInfo = new FileInfo(_filePath);
            return new YEDFileInfoRecord(
                fileInfo.FullName,
                fileInfo.Directory?.FullName ?? "",
                fileInfo.Name,
                fileInfo.Extension,
                fileInfo.Length,
                fileInfo.CreationTime,
                fileInfo.LastWriteTime
            ); ;
        }

        public string GetFullPath() {
            _logger?.Info($"Getting full path for file '{_filePath}'");
            return GetInfo().FullPath;
        }

        public string GetDirectory() {
            _logger?.Info($"Getting directory for file '{_filePath}'");
            return GetInfo().Directory;
        }

        public string GetName() {
            _logger?.Info($"Getting file name for file '{_filePath}'");
            return GetInfo().Name;
        }

        public string GetExtension() {
            _logger?.Info($"Getting file extension for file '{_filePath}'");
            return GetInfo().Extension;
        }

        public long GetSize() {
            _logger?.Info($"Getting file size for file '{_filePath}'");
            return GetInfo().Size;
        }

        public DateTime GetCreationTime() {
            _logger?.Info($"Getting creation time for file '{_filePath}'");
            return GetInfo().CreationTime;
        }

        public DateTime GetModificationTime() {
            _logger?.Info($"Getting modification time for file '{_filePath}'");
            return GetInfo().ModificationTime;
        }

        public string GetFormattedInfo() {
            _logger?.Info($"Getting formatted file info for file '{_filePath}'");
            return FormatFileInfoRecord(GetInfo());
        }

        public string GetFormattedSize() {
            _logger?.Info($"Getting formatted file size for file '{_filePath}'");
            return FormatBytes(GetSize());
        }

        private static string FormatBytes(long bytes) {
            string[] Suffix = { "B", "KB", "MB", "GB", "TB" };
            int i;
            double dblSByte = bytes;

            for (i = 0; i < Suffix.Length && bytes >= 1024; i++, bytes /= 1024) {
                dblSByte = bytes / 1024.0;
            }

            return string.Format("{0:0.##} {1}", dblSByte, Suffix[i]);
        }

        private static string FormatFileInfoRecord(YEDFileInfoRecord fileInfo) {
            return $"File '{fileInfo.Name}' with extension '{fileInfo.Extension}' in directory '{fileInfo.Directory}'. " +
                $"Size: {FormatBytes(fileInfo.Size)} | Creation Time: {fileInfo.CreationTime} | Modification Time: {fileInfo.ModificationTime}";
        }
    }
}
