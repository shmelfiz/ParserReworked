using System;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using GufoMeParser.Core.Enum;
using System.Collections.Generic;
using System.Linq;

namespace GufoMeParser.Core.Helpers
{
    public static class FileHelper
    {
        private static string _parsedTxtPath = Directory.GetCurrentDirectory() + Defaults.SavedFilesFolderName;
        private static string _parsedHtmlPath = Directory.GetCurrentDirectory() + Defaults.SavedHtmlFilesFolderName;
        private static string _parsedLinkPath = Directory.GetCurrentDirectory() + Defaults.SavedLinksFilesFolderName;

        public static List<string> GetAllTextLinesFromFile(string filePath)
        {
            if(string.IsNullOrEmpty(filePath))
            {
                return new List<string>();
            }

            try
            {
                var fileTextLines = File.ReadAllLines(filePath).ToList();

                return fileTextLines ?? new List<string>();
            }
            catch
            {
                return new List<string>();
            }
        }

        public static async void SaveAsync(string text, string name, ParsedDataType fileType)
        {
            var pathForSave = GetCreatedPath(name, fileType);

            await WriteTextAsync(pathForSave, text);
        }

        private async static Task WriteTextAsync(string filePath, string text)
        {
            byte[] encodedText = Encoding.Unicode.GetBytes(text);

            using (var sourceStream = new FileStream(filePath,
                FileMode.Append, FileAccess.Write, FileShare.None,
                bufferSize: 4096, useAsync: true))
            {
                await sourceStream.WriteAsync(encodedText, 0, encodedText.Length);
            };
        }

        private static string GetCreatedPath(string name, ParsedDataType fileType)
        {
            switch (fileType)
            {
                case ParsedDataType.ParsedTxt:
                    {
                        var savingDirectory = Directory.CreateDirectory(_parsedTxtPath);
                        var path = new StringBuilder();
                        path.Append(savingDirectory);
                        path.Append("\\");
                        path.Append(name[0].ToString() == name[0].ToString().ToUpper() ?
                            name + Defaults.DeWiktionaryBigLetterFileName : name + Defaults.DeWiktionarySmallLetterFileName);
                        path.Append(".txt");

                        return path.ToString();
                    }
                case ParsedDataType.ParsedHtml:
                    {
                        var savingDirectory = Directory.CreateDirectory(_parsedHtmlPath);
                        var path = new StringBuilder();
                        path.Append(savingDirectory);
                        path.Append("\\");
                        path.Append(name[0].ToString() == name[0].ToString().ToUpper() ?
                            name + Defaults.DeWiktionaryBigLetterFileName : name + Defaults.DeWiktionarySmallLetterFileName);
                        path.Append(".txt");

                        return path.ToString();
                    }
                case ParsedDataType.ParsedLink:
                    {
                        var savingDirectory = Directory.CreateDirectory(_parsedLinkPath);
                        var path = new StringBuilder();
                        path.Append(savingDirectory);
                        path.Append("\\");
                        path.Append(name);
                        path.Append(".txt");

                        return path.ToString();
                    }
            }
            throw new Exception("U r wrote wrong type of file!");
        }
    }
}
