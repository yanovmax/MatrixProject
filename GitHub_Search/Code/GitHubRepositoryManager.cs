using Core.Cache;
using Core.Helpers;
using Core.Logger;
using GitHub.Adapter.Api;
using GH_Adapter.Models;
using GitHub_Search.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using static GitHub_Search.Models.Enums.GhEnums;

namespace GitHub_Search.Code
{

    public class GitHubRepositoryManager
    {
        public const string keyGHRepository = "key-ghResponse";
        private readonly ISessionService sessionService;

        public GitHubRepositoryManager(ISessionService sessionService )
        {
            this.sessionService = sessionService;
        }

        public  async Task<GitHubResponse> GetDataResponse(string queryTxt)
        {
            GitClient requestManager = new GitClient();
            var res = await requestManager.GetData(queryTxt);
            return res;
        }
        public List<GitHubItem> SetFavoritesToSession(GitHubItemWrapper _ghItemWrapper)
        {
            List<GitHubItem> data = null;
            try
            {
                if (_ghItemWrapper != null && _ghItemWrapper.GitHubItem != null)
                {
                    data = sessionService.Get<List<GitHubItem>>("myFavorites");
                    if (_ghItemWrapper.FavAction == FavoriteAction.AddTofavorites)
                    {
                        if (data != null)
                        {
                            AddGHubItem(data, _ghItemWrapper);
                        }
                        else
                        {
                            data = new List<GitHubItem>();
                            AddGHubItem(data, _ghItemWrapper);
                        }
                    }
                    else if (_ghItemWrapper.FavAction == FavoriteAction.RemoveFromFavorites)
                    {
                        if (data != null)
                        {
                            var favToRemove = data.SingleOrDefault(d => d.id == _ghItemWrapper.GitHubItem.id);
                            if (favToRemove.id != 0)
                                data.Remove(favToRemove);
                        }
                    }
                }
                if (data != null)
                {
                    sessionService.Set("myFavorites", data);
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex.Message, "RequestManager.cs", "GitHubSearch", "SetFavoritesToSession");
            }

            return data;
        }
        public List<GitHubItem> GetFavoritesFromSession()
        {
            List<GitHubItem> favResult = null;
            try
            {
                favResult = sessionService.Get<List<GitHubItem>>("myFavorites");
            }
            catch (Exception ex)
            {
                Log.Write(ex.Message, "RequestManager.cs", "GitHubSearch", "GetFavoritesFromSession");
            }

            return favResult;
        }
        public void AddGHubItem(List<GitHubItem> data, GitHubItemWrapper _ghItemWrapper)
        {
            try
            {
                data.Add(new GitHubItem()
                {
                    id = _ghItemWrapper.GitHubItem.id,
                    name = _ghItemWrapper.GitHubItem.name,
                    avatar_url = _ghItemWrapper.GitHubItem.avatar_url,

                });
            }
            catch (Exception ex)
            {
                Log.Write(ex.Message, "RequestManager.cs", "GitHubSearch", "AddGHubItem");

            }
        }
    }
}