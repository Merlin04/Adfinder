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
                file.WriteLine((filePath.Value == "Promotional" ? "1" : "0") + "\t" +
                               ProcessExtracts(fm.GetArticleExtracts(filePath.Key))
                                   .Replace("\n", " ")
                                   .Replace("\t", ""));
            }
        }

        static string ProcessExtracts(string extracts)
        {
            try
            {
                string[] sectionSplit = extracts.Split("\n\n\n");
                return sectionSplit[0] + "   " + sectionSplit[1] + "   " + sectionSplit[2] + "   " + sectionSplit[3] +
                       "   " + sectionSplit[4];
            }
            catch (Exception ex)
            {
                if (ex is IndexOutOfRangeException)
                {
                    return extracts;
                }

                throw;
            }
        }
    }
}