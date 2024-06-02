using System.Collections.Generic;
using System.Collections.Immutable;

namespace Utility.Interface
{
    public interface IJsonUtilityEx
    {
        public void Save(object save, string path);
        public void Save(IList<(object save, string path)> tupleList);
        public T Load<T>(string path);
        public ImmutableList<T> Load<T>(IList<string> pathList);
    }
}