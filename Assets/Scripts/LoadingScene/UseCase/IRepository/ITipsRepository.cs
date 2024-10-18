using System.Collections.Generic;

namespace LoadingScene.UseCase.IRepository
{
    public interface ITipsRepository
    {
        public IReadOnlyList<string> GetTips();
    }
}