using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GufoMeParser.Core;
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
                        var savingDirectory = Directory.GetCurrentDirectory() + Defaults.SavedFilesFolderName;

                        return savingDirectory;
                    }
                case ParsedDataType.ParsedHtml:
                    {
                        var savingDirectory = Directory.GetCurrentDirectory() + Defaults.SavedHtmlFilesFolderName;

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

            var files = new Dictionary<string, DateTime>();

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
                        var savingDirectory = Directory.GetCurrentDirectory() + Defaults.SavedFilesFolderName;
                        var isExists = Directory.Exists(savingDirectory);

                        return isExists;
                    }
                case (int)ParsedDataType.ParsedHtml:
                    {
                        var savingDirectory = Directory.GetCurrentDirectory() + Defaults.SavedHtmlFilesFolderName;
                        var isExists = Directory.Exists(savingDirectory);

                        return isExists;
                    }
            }

            throw new Exception("U r wrote wrong type of file!");
        }
    }
}

