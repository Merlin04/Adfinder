using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Preprocessor
{
    public class FileManagement
    {
        private string _datasetDir;
        private Dictionary<string, int> _categories = new Dictionary<string, int>();

        public FileManagement(string[] categories)
        {
            _datasetDir = CreateDatasetDir();
            foreach (var category in categories)
            {
                CreateCategoryDir(category);
                _categories.Add(category, 0);
            }
        }
        
        /// <summary>
        /// Create a directory to hold the dataset in.
        /// </summary>
        /// <returns>The path to the directory.</returns>
        private string CreateDatasetDir()
        {
            // Create a directory to store the dataset in, but make sure to not overwrite any other one
            string newDir = Path.Combine(Directory.GetCurrentDirectory(), "dataset-" + DateTime.Now.ToString("s"));
            Directory.CreateDirectory(newDir);
            return newDir;
        }

        /// <summary>
        /// Create a directory for a category.
        /// </summary>
        /// <param name="categoryName">The name of the category.</param>
        private void CreateCategoryDir(string categoryName)
        {
            string newDir = Path.Combine(_datasetDir, categoryName);
            Directory.CreateDirectory(newDir);
        }

        /// <summary>
        /// Save an article as a JSON file.
        /// </summary>
        /// <param name="pTitle">The title of the article.</param>
        /// <param name="pExtracts">The extracts (content) of the article.</param>
        /// <param name="pCategory">The subdirectory to store the JSON file in.</param>
        public void SaveArticle(string pTitle, string pExtracts, string pCategory)
        {
            // Empty means that the link was a redirect
            if (pExtracts == string.Empty) return;
            string fileContents = JsonConvert.SerializeObject(new
            {
                title = pTitle,
                extracts = pExtracts
            });
            File.WriteAllText(Path.Join(_datasetDir, pCategory, _categories[pCategory] + ".json"), fileContents);
            _categories[pCategory]++;
        }

        /// <summary>
        /// Print statistics about the files downloaded to the console.
        /// </summary>
        public void PrintStatistics()
        {
            Console.WriteLine("Statistics:");
            foreach ((string key, int value) in _categories)
            {
                Console.WriteLine(" - " + key + ": " + (value + 1) + " files");
            }
        }
    }
}