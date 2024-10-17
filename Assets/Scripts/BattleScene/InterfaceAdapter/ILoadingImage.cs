using System.Collections.Generic;

namespace BattleScene.InterfaceAdapter
{
    public interface ILoadingImage
    {
        public IReadOnlyList<string> GetImageNameList();
    }
}