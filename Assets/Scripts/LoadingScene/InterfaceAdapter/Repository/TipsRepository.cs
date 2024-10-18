using System.Collections.Generic;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using LoadingScene.InterfaceAdapter.Repository.IScriptableObject;
using LoadingScene.UseCase.IRepository;

namespace LoadingScene.InterfaceAdapter.Repository
{
    public class TipsRepository : ITipsRepository
    {
        private readonly ILoadingSceneScriptableObject _loadingSceneScriptableObject;

        public TipsRepository(
            ILoadingSceneScriptableObject loadingSceneScriptableObject)
        {
            _loadingSceneScriptableObject = loadingSceneScriptableObject;
        }

        public IReadOnlyList<string> GetTips()
        {
            return _loadingSceneScriptableObject.GetTipsScriptableObject().Select(x => x.tips).ToList();
        }
    }
}