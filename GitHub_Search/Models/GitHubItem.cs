using GitHub_Search.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GitHub_Search.Models
{
    public class GitHubItem
    {
        public int id { get; set; }
        public string name { get; set; }
        public string avatar_url { get; set; }
    }

    public class GitHubItemWrapper
    {
        public GhEnums.FavoriteAction FavAction { get; set; }
        public GitHubItem GitHubItem { get; set; }
    }
}