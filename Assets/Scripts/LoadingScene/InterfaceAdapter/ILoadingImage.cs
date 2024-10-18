using System.Collections.Generic;

namespace LoadingScene.InterfaceAdapter
{
    public interface ILoadingImage
    {
        public IReadOnlyList<string> GetImageNameList();
    }
}