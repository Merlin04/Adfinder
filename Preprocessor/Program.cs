using System;
using System.Collections.Specialized;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Preprocessor
{
    class Program
    {
        private static readonly HttpClient Client = new HttpClient();
        private static readonly FileManagement Fm = new FileManagement(new[] {"Promotional", "Good"});

        static async Task Main(string[] args)
        {
            Console.WriteLine("Adfinder Preprocessor v0.1");
            Console.WriteLine("Getting all articles with a promotional tone..."); 
            await GetArticlesInCategory("Category:All articles with a promotional tone", "Promotional");
            //await GetArticlesInCategory("Category:Aalborg University alumni‏‎", "Promotional");
            Console.WriteLine("Getting all good articles...");
            await GetArticlesInCategory("Category:Good articles", "Good");
            //await GetArticlesInCategory("Category:Aalenian taxonomic families", "Good");
            Console.WriteLine("Done - Statistics:");
            Fm.PrintStatistics();
        }

        /// <summary>
        /// Save all of the articles in a category to a folder.
        /// </summary>
        /// <param name="categoryName">The category of the articles to download.</param>
        /// <param name="shortCategoryName">The name of the folder to save them to.</param>
        private static async Task GetArticlesInCategory(string categoryName, string shortCategoryName)
        {
            bool noMoreContinue = false;
            string prevGcmc = null;
            while (!noMoreContinue)
            {
                TextExtractsApiResponse r = await GetOneArticle(categoryName, prevGcmc);
                Fm.SaveArticle(r.Title, r.Extract, shortCategoryName);
                noMoreContinue = r.GcmContinue is null;
                prevGcmc = r.GcmContinue;
            }
        }

        /// <summary>
        /// Get the contents of one article in a category.
        /// </summary>
        /// <param name="category">The category to get the article from.</param>
        /// <param name="gcmc">The GcmContinue value of the previous article; if null, this will get the first article.</param>
        /// <returns>A TextExtractsApiResponse object with the article title, extracts, and GcmContinue.</returns>
        private static async Task<TextExtractsApiResponse> GetOneArticle(string category, string gcmc=null)
        {
            /*
             * {
             * "action": "query",
             * "format": "json",
             * "prop": "extracts",
             * "generator": "categorymembers",
             * "formatversion": "2",
             * "exlimit": "1",
             * "explaintext": 1,
             * "exsectionformat": "plain",
             * "gcmtitle": category,
             * "gcmlimit": "1"
             * }
             */
            string response = await Client.GetStringAsync("https://en.wikipedia.org/w/api.php?action=query&format=json&prop=extracts&generator=categorymembers&formatversion=2&exlimit=1&explaintext=1&exsectionformat=plain&gcmtitle="
                                                          + category.Replace(" ", "%20") + "&gcmlimit=1"
                                                          + (gcmc is null ? null : "&gcmcontinue=" + gcmc));
            JObject o = JObject.Parse(response);

            return new TextExtractsApiResponse
            {
                Title = o["query"]?["pages"]?[0]?["title"]?.ToString(),
                Extract = o["query"]?["pages"]?[0]?["extract"]?.ToString(),
                GcmContinue = o["continue"]?["gcmcontinue"]?.ToString()
            };
        }
    }
}
