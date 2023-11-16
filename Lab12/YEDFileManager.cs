using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab12 {
    public class YEDFileManagerException : Exception {
        public YEDFileManagerException(string message) : base(message) { }
    }

    public class YEDFileManager {
        private string _directoryPath;
        private YEDLog? _logger;

        public YEDLog? Logger {
            set => _logger = value;
        }

        public string DirectoryPath {
            get => _directoryPath;
        }

        public string[] Files {
            get => new DirectoryInfo(DirectoryPath).GetFiles().Select(fileInfo => fileInfo.Name).ToArray();
        }

        public string[] Directories {
            get => new DirectoryInfo(DirectoryPath).GetDirectories().Select(dirInfo => dirInfo.Name).ToArray();
        }

        public string[] Objects {
            get => Files.Concat(Directories).ToArray();
        }

        public int FilesCount {
            get => Files.Length;
        }

        public int DirectoriesCount {
            get => Directories.Length;
        }

        public int ObjectsCount {
            get => Objects.Length;
        }

        public YEDFileManager(string directoryPath) {
            _directoryPath = directoryPath;
            ChangeDirectory(directoryPath);
        }

        public void ChangeDirectory(string directoryPath) {
            _logger?.Info($"Changing directory to '{directoryPath}'");
            if (directoryPath == "..") {
                _directoryPath = new DirectoryInfo(_directoryPath).Parent?.FullName ?? _directoryPath;
                return;
            }
            if (!Directory.Exists(directoryPath)) {
                directoryPath = Path.Combine(_directoryPath, directoryPath);
                if (!Directory.Exists(directoryPath)) {
                    ThrowException($"Directory '{directoryPath}' does not exist");
                }
            }
            _directoryPath = directoryPath;
        }

        public void CreateDirectory(string directoryName) {
            _logger?.Info($"Creating directory '{directoryName}'");
            string directoryPath = Path.Combine(_directoryPath, directoryName);
            if (Directory.Exists(directoryPath)) {
                ThrowException($"Directory '{directoryPath}' is already exist");
            }
            try {
                Directory.CreateDirectory(directoryPath);
            }
            catch (Exception ex) {
                ThrowException($"Can't create directory '{directoryPath}': {ex.Message}");
            }
        }

        public void DeleteDirectory(string directoryName) {
            _logger?.Info($"Deleting directory '{directoryName}'");
            string directoryPath = Path.Combine(DirectoryPath, directoryName);
            if (!Directory.Exists(directoryPath)) {
                ThrowException($"Directory '{directoryPath}' does not exist");
            }
            try {
                Directory.Delete(directoryPath);
            }
            catch (Exception ex) {
                ThrowException($"Can't delete directory '{directoryPath}': {ex.Message}");
            }
        }

        public void CreateFile(string fileName) {
            _logger?.Info($"Creating file '{fileName}'");
            string filePath = Path.Combine(_directoryPath, fileName);
            if (File.Exists(filePath)) {
                ThrowException($"File '{filePath}' is already exists");
            }
            try {
                File.WriteAllText(filePath, null);
            }
            catch (Exception ex) {
                ThrowException($"Can't create file '{filePath}': {ex.Message}");
            }
        }

        public void DeleteFile(string fileName) {
            _logger?.Info($"Deleting file '{fileName}'");
            string filePath = Path.Combine(_directoryPath, fileName);
            if (!File.Exists(filePath)) {
                ThrowException($"File '{filePath}' does not exist");
            }
            try {
                File.Delete(filePath);
            }
            catch (Exception ex) {
                ThrowException($"Can't delete file '{filePath}': {ex.Message}");
            }
        }

        public void WriteToFile(string fileName, string content) {
            _logger?.Info($"Writing to file '{fileName}'");
            string filePath = Path.Combine(_directoryPath, fileName);
            if (!File.Exists(filePath)) {
                ThrowException($"File '{filePath}' does not exist");
            }
            try {
                File.WriteAllText(filePath, content);
            }
            catch (Exception ex) {
                ThrowException($"Can't write to file '{fileName}': {ex.Message}");
            }
        }

        public string ReadFromFile(string fileName) {
            _logger?.Info($"Reading from file '{fileName}'");
            string filePath = Path.Combine(_directoryPath, fileName);
            if (!File.Exists(filePath)) {
                ThrowException($"File '{filePath}' does not exist");
            }
            try {
                return File.ReadAllText(filePath);
            }
            catch (Exception ex) {
                ThrowException($"Can't read from file '{fileName}': {ex.Message}");
            }
            return "";
        }

        public void CopyFile(string sourceFilePath, string destinationFilePath) {
            _logger?.Info($"Copy '{sourceFilePath}' to '{destinationFilePath}'");
            if (!File.Exists(sourceFilePath)) {
                ThrowException($"File '{sourceFilePath}' does not exist");
            }
            try {
                File.Copy(sourceFilePath, destinationFilePath);
            }
            catch (Exception ex) {
                ThrowException($"Can't copy '{sourceFilePath}' to '{destinationFilePath}': {ex.Message}");
            }
        }

        public void CreateArchive(string sourseFolder, string destinationFileName) {
            _logger?.Info($"Creating archive '{destinationFileName}' from '{sourseFolder}'");
            if (!Directory.Exists(sourseFolder)) {
                sourseFolder = Path.Join(_directoryPath, sourseFolder);
                if (!Directory.Exists(sourseFolder)) {
                    ThrowException($"Directory '{sourseFolder}' does not exists");
                }
            }
            try {
                ZipFile.CreateFromDirectory(sourseFolder, destinationFileName);
            }
            catch (Exception ex) {
                ThrowException($"Can't create archive '{destinationFileName}' from '{sourseFolder}': {ex.Message}");
            }
        }

        public void ExtractArchive(string archivePath, string destinationDirectory) {
            _logger?.Info($"Extracting archive '{archivePath}' to '{destinationDirectory}'");
            if (!File.Exists(archivePath)) {
                archivePath = Path.Join(_directoryPath, archivePath);
                if (!File.Exists(archivePath)) {
                    ThrowException($"File '{archivePath}' does not exists");
                }
            }
            if (!Directory.Exists(destinationDirectory)) {
                destinationDirectory = Path.Join(_directoryPath, destinationDirectory);
                if (!Directory.Exists(destinationDirectory)) {
                    ThrowException($"Directory '{destinationDirectory}' does not exists");
                }
            }
            try {
                ZipFile.ExtractToDirectory(archivePath, destinationDirectory);
            }
            catch (Exception ex) {
                ThrowException($"Can't extract '{archivePath}' to '{destinationDirectory}': {ex.Message}");
            }
        }

        private void ThrowException(string message) {
            _logger?.Error(message);
            throw new YEDFileManagerException(message);
        }
    }
}
