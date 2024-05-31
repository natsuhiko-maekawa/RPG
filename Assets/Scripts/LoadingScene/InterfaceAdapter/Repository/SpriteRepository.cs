using System.Threading.Tasks;
using LoadingScene.InterfaceAdapter.Repository.IAddressable;
using LoadingScene.UseCase.IRepository;

namespace LoadingScene.InterfaceAdapter.Repository
{
    public class SpriteRepository : ISpriteRepository
    {
        private readonly ILoadingImage _loadingImage;
        private readonly ISpriteAddressable _spriteAddressable;

        public SpriteRepository(
            ISpriteAddressable spriteAddressable,
            ILoadingImage loadingImage)
        {
            _spriteAddressable = spriteAddressable;
            _loadingImage = loadingImage;
        }

        public float GetProgress(int addressableCount)
        {
            return _spriteAddressable.GetProgress(addressableCount);
        }

        public async Task LoadImage()
        {
            await _spriteAddressable.Load(_loadingImage.GetImageNameList());
        }
    }
}