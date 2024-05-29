namespace LoadingScene.UseCase.IRepository
{
    public interface ISettingsRepository
    {
        public void Set(int addressableCount);
        public int Get();
    }
}