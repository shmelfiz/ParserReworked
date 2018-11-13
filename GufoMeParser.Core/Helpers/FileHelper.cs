using System;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using GufoMeParser.Core.Enum;

namespace GufoMeParser.Core.Helpers
{
    public static class FileHelper
    {
        private static string _parsedTxtPath = Directory.GetCurrentDirectory() + "\\SavedFiles";
        private static string _parsedHtmlPath = Directory.GetCurrentDirectory() + "\\SavedFilesHtml";
        private static string _parsedLinkPath = Directory.GetCurrentDirectory() + "\\LinksFile";

        public static Task Save(string text, string name, ParsedDataType fileType)
        {
            var pathForSave = GetCreatedPath(name, fileType);

            return WriteTextAsync(pathForSave, text);
        }

        private async static Task WriteTextAsync(string filePath, string text)
        {
            byte[] encodedText = Encoding.Unicode.GetBytes(text);

            using (FileStream sourceStream = new FileStream(filePath,
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
                        path.Append(name);
                        path.Append(".txt");

                        return path.ToString();
                    }
                case ParsedDataType.ParsedHtml:
                    {
                        var savingDirectory = Directory.CreateDirectory(_parsedHtmlPath);
                        var path = new StringBuilder();
                        path.Append(savingDirectory);
                        path.Append("\\");
                        path.Append(name);
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
