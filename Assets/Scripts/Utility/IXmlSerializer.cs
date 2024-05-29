namespace Utility
{
    public interface IXmlSerializer
    {
        public void Save<T>(T obj);
        public T Load<T>();
    }
}