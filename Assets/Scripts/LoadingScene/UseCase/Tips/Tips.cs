using System.Collections.Generic;
using LoadingScene.UseCase.IRepository;
using Utility;
using Utility.Interface;

namespace LoadingScene.UseCase.Tips
{
    public class Tips : ITips
    {
        private readonly IRandomEx _randomEx;
        private readonly IList<string> _tipsList;

        public Tips(
            IRandomEx randomEx,
            ITipsRepository tipsRepository)
        {
            _randomEx = randomEx;
            _tipsList = tipsRepository.GetTips();
        }

        public string RandomGet()
        {
            return _randomEx.Choice(_tipsList);
        }
    }
}