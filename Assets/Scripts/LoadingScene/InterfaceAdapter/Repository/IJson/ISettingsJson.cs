using LoadingScene.InterfaceAdapter.Repository.Dto;

namespace LoadingScene.InterfaceAdapter.Repository.IJson
{
    public interface ISettingsJson
    {
        public SettingsDto Get();
        public void Set(SettingsDto settingsDto);
    }
}