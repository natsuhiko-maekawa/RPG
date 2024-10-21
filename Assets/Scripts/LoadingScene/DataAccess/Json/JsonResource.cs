using UnityEngine;
using Utility;

namespace LoadingScene.DataAccess.Json
{
    public class JsonResource<T> where T : notnull
    {
        private readonly string _path;

        public JsonResource(
            string filename)
        {
            _path = $"{Application.dataPath}/{filename}";
        }

        public T Get()
        {
            return MyJsonUtility.Load<T>(_path);
        }

        public void Set(T dto)
        {
            MyJsonUtility.Save(dto, _path);
        }
    }
}