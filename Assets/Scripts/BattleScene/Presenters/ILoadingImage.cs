using System.Collections.Generic;

namespace BattleScene.Presenters
{
    public interface ILoadingImage
    {
        public IReadOnlyList<string> GetImageNameList();
    }
}