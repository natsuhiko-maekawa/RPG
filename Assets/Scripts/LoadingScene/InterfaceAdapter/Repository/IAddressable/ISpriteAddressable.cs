using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoadingScene.InterfaceAdapter.Repository.IAddressable
{
    public interface ISpriteAddressable
    {
        public float GetProgress(int addressableCount);
        public Task Load(IList<string> imageNameList);
    }
}