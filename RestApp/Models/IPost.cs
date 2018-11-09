using System.Collections.Generic;

namespace RestApp.Models
{
    public interface IPost
    {
        string By { get; set; }
        int Comments { get; }
        List<int> Kids { get; set; }
        int Rank { get; set; }
        int Score { get; set; }
        string Title { get; set; }
        string Url { get; set; }
    }
}