using Core.Helpers;
using GH_Adapter.Models;
using GitHub_Search.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GitHub_Search.Controllers
{
    public class GitHubController : Controller
    {
        // GET: GitHub
        private readonly GitHubRepositoryManager manager = new GitHubRepositoryManager(new SessionService());

        public async Task<ActionResult> GetDataByRequest(string queryText)
        {
            GitHubResponse result = null;
            if (!string.IsNullOrEmpty(queryText))
            {
                result = await manager.GetDataResponse(queryText);
            }
            return View("Result", result);
        }
    }
}