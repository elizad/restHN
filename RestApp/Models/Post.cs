using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestApp.Models
{
    public class Post : IPost
    {
        // Author
        public string By { get; set;}

        // URL
        public string Url { get; set; }

        public string Title { get; set; }

        // POints
        public int Score { get; set; }

        // Associated Comments
        public List<int> Kids { get; set; }

        public int Rank { get; set; }

        public int Comments
        {
            get
            {
                return Kids.Count;
            }
        }

        //    https://hacker-news.firebaseio.com/v0/item/18378332.json?print=pretty
        //    "title": "Instapaper is joining Pinterest",
        //    "uri": "http://blog.instapaper.com/post/149374303661",
        //    "author": "ropiku",
        //    "points": 182,
        //    "comments": 99,
        //        "rank": 2
    }
}
