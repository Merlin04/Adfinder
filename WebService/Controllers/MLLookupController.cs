using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ModelAccess;
using Newtonsoft.Json.Linq;

namespace WebService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    // ReSharper disable once InconsistentNaming
    public class MLLookupController : ControllerBase
    {
        private readonly ILogger<MLLookupController> _logger;
        private readonly Access _access;
        private static readonly HttpClient Client = new HttpClient();

        public MLLookupController(ILogger<MLLookupController> logger)
        {
            _logger = logger;
            _access = new Access();
        }

        [HttpPost]
        public async Task<float> Post(string articleTitle)
        {
            return _access.Predict(await GetOneArticle(articleTitle));
        }
        
        /// <summary>
        /// Get the extracts of one page.
        /// </summary>
        /// <param name="pageName">The name of the page to get.</param>
        /// <returns>The article extracts.</returns>
        private static async Task<string> GetOneArticle(string pageName)
        {
            string response = await Client.GetStringAsync("https://en.wikipedia.org/w/api.php?action=query&format=json&prop=extracts&generator=allpages&exlimit=1&explaintext=1&gapfrom="
                                                          + pageName.Replace(" ", "%20") + "&gaplimit=1");
            JObject o = JObject.Parse(response);

            return o["query"]["pages"].First()["extract"].ToString()
                .Replace("\n", " ")
                .Replace("\t", "");
        }
    }
}