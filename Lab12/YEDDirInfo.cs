using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab12 {
    public class YEDDirInfoException : Exception {
        public YEDDirInfoException(string message) : base(message) { }
    }

    public record YEDDirInfoRecord(
        string Name,
        string Path,
        string ParentDirectory,
        string[] Files,
        string[] Directories,
        DateTime CreationTime
    );

    public class YEDDirInfo {
        private readonly string _directoryPath;
        private YEDLog? _logger;

        public YEDLog? Logger {
            set => _logger = value;
        }

        public string DirectoryPath {
            get => _directoryPath;
        }

        public YEDDirInfo(string directoryPath) {
            if (!Directory.Exists(directoryPath)) {
                throw new YEDDirInfoException($"Directory '{directoryPath}' does not exist");
            }
            _directoryPath = directoryPath;
        }

        public YEDDirInfoRecord GetInfo() {
            _logger?.Info($"Getting directory info for directory '{_directoryPath}'");
            DirectoryInfo directoryInfo = new DirectoryInfo(_directoryPath);
            return new YEDDirInfoRecord(
                directoryInfo.Name,
                directoryInfo.FullName,
                directoryInfo.Parent?.FullName ?? "",
                directoryInfo.GetFiles().Select(fileInfo => fileInfo.Name).ToArray(),
                directoryInfo.GetDirectories().Select(directoryInfo => directoryInfo.Name).ToArray(),
                directoryInfo.CreationTime
            );
        }

        public string GetName() {
            _logger?.Info($"Getting name for directory '{_directoryPath}'");
            return GetInfo().Name;
        }

        public string GetPath() {
            _logger?.Info($"Getting path for directory '{_directoryPath}'");
            return GetInfo().Path;
        }

        public string GetParrentDirectory() {
            _logger?.Info($"Getting parrent directory for directory '{_directoryPath}'");
            return GetInfo().ParentDirectory;
        }

        public string[] GetFiles() {
            _logger?.Info($"Getting files for directory '{_directoryPath}'");
            return GetInfo().Files;
        }

        public string[] GetDirectories() {
            _logger?.Info($"Getting directories for directory '{_directoryPath}'");
            return GetInfo().Directories;
        }

        public DateTime GetCreationTime() {
            _logger?.Info($"Getting creation time for directory '{_directoryPath}'");
            return GetInfo().CreationTime;
        }

        public string GetFormattedInfo() {
            _logger?.Info($"Getting formatted directory info for directory '{_directoryPath}'");
            return FormatDirInfoRecord(GetInfo());
        }

        public string GetFormattedFiles() {
            _logger?.Info($"Getting formatted files info for directory '{_directoryPath}'");
            var dirInfo = GetInfo();
            return $"Files in '{dirInfo.Name}': {string.Join(", ", dirInfo.Files)}";
        }

        public string GetFormattedDirectories() {
            _logger?.Info($"Getting formatted directories info for directory '{_directoryPath}'");
            var dirInfo = GetInfo();
            return $"Directories in '{dirInfo.Name}': {string.Join(", ", dirInfo.Directories)}";
        }

        private static string FormatDirInfoRecord(YEDDirInfoRecord dirInfo) {
            return $"Directory '{dirInfo.Name}' ({dirInfo.Path}) " +
                $"with {dirInfo.Files.Length} file and {dirInfo.Directories.Length} subdirectories. " +
                $"Creation Time: {dirInfo.CreationTime}";
        }
    }
}
