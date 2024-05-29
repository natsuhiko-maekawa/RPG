namespace LoadingScene.InterfaceAdapter.Repository.IAddressable
{
    public interface IAddressableCount
    {
#if UNITY_EDITOR
        public int Get();
#endif
    }
}