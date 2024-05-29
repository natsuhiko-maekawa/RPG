using System;

namespace LoadingScene.InterfaceAdapter.Repository.Dto
{
    [Serializable]
    public class SettingsDto
    {
        public int addressableCount;
        
        public SettingsDto(int addressableCount)
        {
            this.addressableCount = addressableCount;
        }
    };
}