using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;

namespace RestApp
{
    public  class Program
    {
        private  static  readonly  HttpClient client = new HttpClient();

        //RestApp 5
        //hackernews --posts n
        static void Main(string[] args)
        {
            var postCount = 10;
            if(args.Length > 1)
            {                
                if (args[0].ToLower() == "--posts" && Int32.TryParse(args[1], out int count))
                    postCount = count;
            }
            var posts = ProcessRepositories(postCount).Result;

            if(posts == null)
            {
                // Do Something
                // Exit?
            }

            foreach (var post in posts)
            {
                Console.WriteLine("====================");
                Console.WriteLine("Post Title: {0}", post.Title);
                Console.WriteLine("Author: {0}", post.By);
                Console.WriteLine("Url: {0}", post.Url);
                Console.WriteLine("====================");

                Console.ReadLine();
            }               
        }

        private static async Task<List<Models.Post>> ProcessRepositories(int resultCount)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".ner fundation repo reporter");

            // var getTheString = client.GetStreamAsync("https://api.github.com/orgs/dotnet/repos");
            var response = await client.GetAsync("https://hacker-news.firebaseio.com/v0/topstories.json?print=pretty").ConfigureAwait(false);
            if (response == null || !response.IsSuccessStatusCode)
            {
                // error Handling
                return null;
            }

            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var postIds = JsonConvert.DeserializeObject<List<int>>(content);

            var posts = new List<Models.Post>();
            for (int i = 0; i < resultCount; i++)
            {
                // Get Details
                var url = string.Format("https://hacker-news.firebaseio.com/v0/item/{0}.json", postIds[i]);
                response = await client.GetAsync(url).ConfigureAwait(false);
                if (response == null || !response.IsSuccessStatusCode)
                {
                    // error Handling
                    return null;
                }

                content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var post = JsonConvert.DeserializeObject<Models.Post>(content);
                post.Rank = i + 1;
                posts.Add(post);
            }

            return posts;
        }
    }
}
//[
 //   {
    //    "title": "Web Scraping in 2016",
    //    "uri": "https://franciskim.co/2016/08/24/dont-need-no-stinking-api-web-scraping-2016-beyond/",
    //    "author": "franciskim",
    //    "points": 133,
    //    "comments": 80,
    //    "rank": 1
    //},
    //{
    //    "title": "Instapaper is joining Pinterest",
    //    "uri": "http://blog.instapaper.com/post/149374303661",
    //    "author": "ropiku",
    //    "points": 182,
    //    "comments": 99,
//        "rank": 2
//    }
//]