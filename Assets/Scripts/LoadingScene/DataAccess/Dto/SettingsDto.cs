using System;

namespace LoadingScene.DataAccess.Dto
{
    [Serializable]
    public class SettingsDto
    {
        public int addressableCount;

        public SettingsDto(int addressableCount)
        {
            this.addressableCount = addressableCount;
        }
    }
}