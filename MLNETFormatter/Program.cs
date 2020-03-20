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
            
            Console.WriteLine("Adfinder MLNETFormatter");
            Console.WriteLine("Using dataset at " + args[0]);
            FileManagement fm = new FileManagement(args[0]);
            string outputFilePath = fm.GetOutputFilePath();
            Console.WriteLine("Output file is " + outputFilePath);
            using StreamWriter file = new StreamWriter(outputFilePath);
            file.WriteLine("Category\tExtracts");
            
            foreach (KeyValuePair<string, string> filePath in fm.GetFilesList())
            {
                file.WriteLine((filePath.Value == "Promotional" ? "1" : "0") + "\t" + fm.GetArticleExtracts(filePath.Key)
                    .Replace("\n", " ")
                    .Replace("\t", ""));
            }
        }
    }
}