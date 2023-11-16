namespace lab12 {
    internal class Program {
        static void Main(string[] args) {
            string labDirPath = @"D:\Education\2 курс\Семестр 3\Объектно-ориентированное программирование\Lab12\";
            string logFilePath = Path.Combine(labDirPath, "lab12.log");
            string managedDirPath = Path.Combine(labDirPath, "Managed");

            YEDLog logger = new YEDLog(logFilePath);
            YEDDiskInfo diskInfo = new YEDDiskInfo();
            YEDFileInfo fileInfo = new YEDFileInfo(logFilePath);
            YEDDirInfo dirInfo = new YEDDirInfo(labDirPath);
            YEDFileManager fileManager = new YEDFileManager(managedDirPath);

            diskInfo.Logger = logger;
            fileInfo.Logger = logger;
            dirInfo.Logger = logger;
            fileManager.Logger = logger;


            Console.WriteLine("\n\t\tDiskInfo");
            Console.WriteLine(diskInfo.GetFormattedDisksInfo());
            Console.WriteLine(diskInfo.GetFormattedDiskInfo(@"D:\"));
            Console.WriteLine(diskInfo.GetFormattedDiskFreeSpace(@"C:\"));
            Console.WriteLine(diskInfo.GetFormattedDiskFileSystem(@"C:\"));


            Console.WriteLine("\n\t\tFileInfo");
            Console.WriteLine(fileInfo.GetFormattedInfo());
            Console.WriteLine($"{fileInfo.GetName()}, {fileInfo.GetExtension()}, {fileInfo.GetFormattedSize()}");
            

            Console.WriteLine("\n\t\tDirInfo");
            Console.WriteLine(dirInfo.GetFormattedInfo());
            Console.WriteLine(dirInfo.GetFormattedFiles());
            Console.WriteLine(dirInfo.GetFormattedDirectories());


            Console.WriteLine("\n\t\tFileManager");

            try { fileManager.DeleteFile(Path.Combine("YEDInspect", "yeddirinfo.txt")); } catch { }
            try { fileManager.DeleteFile(Path.Combine("YEDInspect", "yeddirinfo_copy.txt")); } catch { }
            try { fileManager.DeleteDirectory("YEDInspect"); } catch { }

            try { fileManager.DeleteFile(Path.Combine("YEDFiles", "test.txt")); } catch { }
            try { fileManager.DeleteFile(Path.Combine("YEDFiles", "test2.txt")); } catch { }
            try { fileManager.DeleteDirectory("YEDFiles"); } catch { }

            try { fileManager.DeleteFile("YEDFilesArchive.zip"); } catch { }

            try { fileManager.DeleteFile(Path.Combine("YEDFilesExtracted", "test.txt")); } catch { }
            try { fileManager.DeleteFile(Path.Combine("YEDFilesExtracted", "test2.txt")); } catch { }
            try { fileManager.DeleteDirectory("YEDFilesExtracted"); } catch { }

            Console.WriteLine($"Directory: {fileManager.DirectoryPath}");

            fileManager.ChangeDirectory("Prepared");
            string[] preparedFiles = fileManager.Files;
            Console.WriteLine($"Directory: {fileManager.DirectoryPath}");
            Console.WriteLine($"Files: {string.Join(", ", fileManager.Files)}");
            Console.WriteLine($"Directories: {string.Join(", ", fileManager.Directories)}");

            fileManager.ChangeDirectory("..");
            Console.WriteLine($"Directory: {fileManager.DirectoryPath}");
            fileManager.CreateDirectory("YEDInspect");
            Console.WriteLine($"Objects: {string.Join(", ", fileManager.Objects)}");

            fileManager.ChangeDirectory("YEDInspect");
            Console.WriteLine($"Directory: {fileManager.DirectoryPath}");
            fileManager.CreateFile("yeddirinfo.txt");
            fileManager.WriteToFile("yeddirinfo.txt", "Hello, world!");
            fileManager.CopyFile(
                Path.Combine(fileManager.DirectoryPath, "yeddirinfo.txt"), 
                Path.Combine(fileManager.DirectoryPath, "yeddirinfo_copy.txt")
            );
            Console.WriteLine($"Objects: {string.Join(", ", fileManager.Objects)}");
            fileManager.DeleteFile("yeddirinfo.txt");

            fileManager.ChangeDirectory("..");
            Console.WriteLine($"Directory: {fileManager.DirectoryPath}");
            fileManager.CreateDirectory("YEDFiles");
            Console.WriteLine($"Objects: {string.Join(", ", fileManager.Objects)}");

            foreach (string fileName in preparedFiles) {
                if (fileName.EndsWith(".txt")) {
                    fileManager.CopyFile(
                        Path.Combine(fileManager.DirectoryPath, "Prepared", fileName),
                        Path.Combine(fileManager.DirectoryPath, "YEDFiles", fileName)
                    );
                }
            }

            fileManager.ChangeDirectory("YEDFiles");
            Console.WriteLine($"Directory: {fileManager.DirectoryPath}");
            Console.WriteLine($"Objects: {string.Join(", ", fileManager.Objects)}");

            fileManager.ChangeDirectory("..");
            fileManager.CreateArchive(
                "YEDFiles",
                Path.Combine(fileManager.DirectoryPath, "YEDFilesArchive.zip")
            );
            fileManager.CreateDirectory("YEDFilesExtracted");
            fileManager.ExtractArchive(
                "YEDFilesArchive.zip",
                "YEDFilesExtracted"
            );
            Console.WriteLine($"Directory: {fileManager.DirectoryPath}");
            Console.WriteLine($"Objects: {string.Join(", ", fileManager.Objects)}");

            fileManager.ChangeDirectory("YEDFilesExtracted");
            Console.WriteLine($"Directory: {fileManager.DirectoryPath}");
            Console.WriteLine($"Objects: {string.Join(", ", fileManager.Objects)}");

            fileManager.ChangeDirectory("..");


            Console.WriteLine("\n\t\tLogger");
            Console.WriteLine(logger.FindLogs($"{DateTime.Now.Day.ToString("D2")}." +
                                              $"{DateTime.Now.Month.ToString("D2")}." +
                                              $"{DateTime.Now.Year.ToString("D4")} " +
                                              $"{DateTime.Now.Hour.ToString("D2")}"));

            if (logger.GetLogs().Split(Environment.NewLine).Length > 2500) {
                logger.ClearLogs();
            }
        }
    }
}