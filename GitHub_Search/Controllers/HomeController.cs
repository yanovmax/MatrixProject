using Core.Helpers;
using GH_Adapter.Models;
using GitHub_Search.Code;
using GitHub_Search.Models;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GitHub_Search.Controllers
{
    public class HomeController : Controller
    {
        private readonly GitHubRepositoryManager manager;
        public HomeController()
        {
            manager = new GitHubRepositoryManager(new SessionService());
        }
        public ActionResult Index()
        {
            return View();
        }
 
        public async Task<JsonResult> GetGitHubData(string queryTxt)
        {
            GitHubResponse result = null;
            if (!string.IsNullOrEmpty(queryTxt))
            {
                result = await manager.GetDataResponse(queryTxt);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SetFavoriteSession(GitHubItemWrapper GhItemWrapper)
        {
            bool result = false;
            var FavRes = manager.SetFavoritesToSession(GhItemWrapper);

            if (FavRes != null)
                result = true;

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Favorites()
        {
            var favorites = manager.GetFavoritesFromSession();

            return View("ResultFavorites", favorites);
        }

    }
}