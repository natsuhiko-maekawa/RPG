using LoadingScene.InterfaceAdapter.Repository.Dto;
using LoadingScene.InterfaceAdapter.Repository.IAddressable;
using LoadingScene.InterfaceAdapter.Repository.IJson;
using LoadingScene.UseCase.IRepository;

namespace LoadingScene.InterfaceAdapter.Repository
{
    public class SettingsRepository : ISettingsRepository
    {
        private readonly IAddressableCount _addressableCount;
        private readonly ISettingsJson _settingsJson;

        public SettingsRepository(
            ISettingsJson settingsJson,
            IAddressableCount addressableCount)
        {
            _settingsJson = settingsJson;
            _addressableCount = addressableCount;
        }

        public void Set(int addressableCount)
        {
            _settingsJson.Set(new SettingsDto(addressableCount));
        }

        public int Get()
        {
#if UNITY_EDITOR
            return _addressableCount.Get();
#else
            return _settingsJson.Get().addressableCount;
#endif
        }
    }
}