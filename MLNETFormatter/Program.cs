using System;
using System.Collections.Generic;
using System.IO;

namespace MLNETFormatter
{
    class Program
    {
        static void Main(string[] args)
        {
            // This program will take the folder outputted from Preprocessor and make it one file that can be used by ML.NET.
            // The argument is the path to the folder.
            
            FileManagement fm = new FileManagement(args[0]);
            using StreamWriter file = new StreamWriter(fm.GetOutputFilePath());
            file.WriteLine("Category\tExtracts");
            
            foreach (KeyValuePair<string, string> filePath in fm.GetFilesList())
            {
                file.WriteLine(filePath.Value + "\t" + fm.GetArticleExtracts(filePath.Key));
            }
        }
    }
}