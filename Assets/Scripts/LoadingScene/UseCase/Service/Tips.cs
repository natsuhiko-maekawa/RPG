using System.Collections.Generic;
using LoadingScene.UseCase.IService;
using Utility;

namespace LoadingScene.UseCase.Service
{
    public class Tips : ITips
    {
        private readonly IReadOnlyList<string> _tipsList = MyList<string>.Empty;
        
        public string RandomGet()
        {
            return MyRandom.Choice(_tipsList);
        }
    }
}