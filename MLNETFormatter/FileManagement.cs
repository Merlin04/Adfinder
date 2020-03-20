using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace MLNETFormatter
{
    public class FileManagement
    {
        private readonly string _datasetDir;

        public FileManagement(string datasetDir)
        {
            _datasetDir = datasetDir;
        }
        
        /// <summary>
        /// Generate a path for the output file.
        /// </summary>
        /// <returns>A path for an output file.</returns>
        public string GetOutputFilePath()
        {
            return Path.Combine(Directory.GetCurrentDirectory(), "dataset-" + DateTime.Now.ToString("s") + ".tsv");
        }

        /// <summary>
        /// Gets a dictionary of files and their category.
        /// </summary>
        /// <returns>A shuffled dictionary of files, with the key being the path and the value being the category.</returns>
        public Dictionary<string, string> GetFilesList()
        {
            Dictionary<string, string> filesList = new Dictionary<string, string>();
            foreach (string categoryDirectoryPath in Directory.GetDirectories(_datasetDir))
            {
                foreach (string filePath in Directory.GetFiles(categoryDirectoryPath))
                {
                    filesList.Add(filePath, categoryDirectoryPath.Split(Path.DirectorySeparatorChar).Last());
                }
            }
            
            Random rand = new Random();
            // https://jigneshon.blogspot.com/2013/08/c-snippet-shuffling-dictionary-beginner.html
            return filesList.OrderBy(x => rand.Next()).ToDictionary(item => item.Key, item => item.Value);
        }

        /// <summary>
        /// Get the extracts of an article from a JSON file.
        /// </summary>
        /// <param name="articlePath">The path to the JSON file.</param>
        /// <returns>The extracts of an article.</returns>
        public string GetArticleExtracts(string articlePath)
        {
            return JsonConvert.DeserializeObject<Article>(File.ReadAllText(articlePath)).Extracts;
        }
    }
}