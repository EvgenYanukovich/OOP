using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab12 {
    public class YEDDiskInfoException : Exception {
        public YEDDiskInfoException(string message) : base(message) { }
    }

    public record YEDDiskInfoRecord(
        string Name,
        string Label,
        string Format,
        long FreeSpace,
        long TotalSpace
    );

    public class YEDDiskInfo {
        private YEDLog? _logger;

        public YEDLog? Logger {
            set => _logger = value;
        }

        public YEDDiskInfoRecord[] GetDisksInfo() {
            _logger?.Info("Getting disks info");
            return (
                DriveInfo.GetDrives()
                    .Select(driveInfo => new YEDDiskInfoRecord(driveInfo.Name, driveInfo.VolumeLabel, driveInfo.DriveFormat, driveInfo.TotalFreeSpace, driveInfo.TotalSize))
                    .ToArray()
            );
        }

        public YEDDiskInfoRecord GetDiskInfo(string diskName) {
            _logger?.Info($"Getting disk info for disk '{diskName}'");
            try {
                return GetDisksInfo().Where(diskInfo => diskInfo.Name == diskName).First();
            }
            catch (Exception ex) {
                string message = $"Can't find disk '{diskName}': {ex.Message}";
                _logger?.Error(message);
                throw new YEDDiskInfoException(message);
            }
        }

        public long GetDiskFreeSpace(string diskName) {
            _logger?.Info($"Getting free space for disk '{diskName}'");
            return GetDiskInfo(diskName).FreeSpace;
        }

        public string GetDiskFileSystem(string diskName) {
            _logger?.Info($"Getting file system for disk '{diskName}'");
            return GetDiskInfo(diskName).Format;
        }

        public string GetFormattedDisksInfo() {
            _logger?.Info("Getting formatted disks info");
            return string.Join(
                Environment.NewLine, 
                GetDisksInfo().Select(diskInfo => FormatDiskInfoRecord(diskInfo))
            );
        }

        public string GetFormattedDiskInfo(string diskName) {
            _logger?.Info($"Getting formatted disk info for disk '{diskName}'");
            return FormatDiskInfoRecord(GetDiskInfo(diskName));
        }

        public string GetFormattedDiskFreeSpace(string diskName) {
            _logger?.Info($"Getting formatted free space for disk '{diskName}'");
            return $"Free space of disk '{diskName}' is {FormatBytes(GetDiskFreeSpace(diskName))}";
        }

        public string GetFormattedDiskFileSystem(string diskName) {
            _logger?.Info($"Getting formatted file system for disk '{diskName}'");
            return $"File system of disk '{diskName}' is {GetDiskFileSystem(diskName)}";
        }

        public static string FormatBytes(long bytes) {
            string[] Suffix = { "B", "KB", "MB", "GB", "TB" };
            int i;
            double dblSByte = bytes;

            for (i = 0; i < Suffix.Length && bytes >= 1024; i++, bytes /= 1024) {
                dblSByte = bytes / 1024.0;
            }

            return string.Format("{0:0.##} {1}", dblSByte, Suffix[i]);
        }

        public static string FormatDiskInfoRecord(YEDDiskInfoRecord diskInfo) {
            string label = string.IsNullOrWhiteSpace(diskInfo.Label) ? "" : $" ({diskInfo.Label})";
            return $"Disk '{diskInfo.Name}'{label} with {diskInfo.Format} storage: {FormatBytes(diskInfo.FreeSpace)} / {FormatBytes(diskInfo.TotalSpace)}";
        }
    }
}
