using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GufoMeParser.Core.Enum;

namespace GufoMeParser.FileAndDirectoryDetecting
{
    public static class DirectoryHelper
    {
        public static void CheckCurrentDirectory(string parsedStockUrl, List<string> urls)
        {
            if (IsExists((int)ParsedDataType.ParsedTxt))
            {
                urls.Add(parsedStockUrl + GetLastFileName(GetPath(ParsedDataType.ParsedTxt)));
            }
        }

        public static string GetPath(ParsedDataType fileType)
        {
            switch (fileType)
            {
                case ParsedDataType.ParsedTxt:
                    {
                        var savingDirectory = Directory.GetCurrentDirectory() + "\\SavedFiles";

                        return savingDirectory;
                    }
                case ParsedDataType.ParsedHtml:
                    {
                        var savingDirectory = Directory.GetCurrentDirectory() + "\\SavedFilesHtml";

                        return savingDirectory;
                    }
            }
            throw new Exception("U r wrote wrong type of file!");
        }

        public static string GetLastFileName(string directoryPath)
        {
            var filesPaths = Directory.GetFiles(directoryPath);

            if (filesPaths.Count() == 0)
            {
                return string.Empty;
            }

            Dictionary<string, DateTime> files = new Dictionary<string, DateTime>();

            foreach (string path in filesPaths)
            {
                files.Add(Path.GetFileName(path), File.GetCreationTime(path));
            }

            files = files.OrderBy(x => x.Value.Millisecond).ToDictionary(x => x.Key, x => x.Value);

            return files.Last().Key.Replace(".txt", "");
        }

        private static bool IsExists(int fileType)
        {
            switch (fileType)
            {
                case (int)ParsedDataType.ParsedTxt:
                    {
                        var savingDirectory = Directory.GetCurrentDirectory() + "\\SavedFiles";
                        var isExists = Directory.Exists(savingDirectory);

                        return isExists;
                    }
                case (int)ParsedDataType.ParsedHtml:
                    {
                        var savingDirectory = Directory.GetCurrentDirectory() + "\\SavedFilesHtml";
                        var isExists = Directory.Exists(savingDirectory);

                        return isExists;
                    }
            }

            throw new Exception("U r wrote wrong type of file!");
        }
    }
}

