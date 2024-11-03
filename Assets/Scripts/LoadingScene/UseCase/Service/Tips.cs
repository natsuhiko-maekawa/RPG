using System;
using System.Collections.Generic;
using LoadingScene.UseCase.IService;
using Utility;

namespace LoadingScene.UseCase.Service
{
    public class Tips : ITips
    {
        private readonly IReadOnlyList<string> _tipsList = Array.Empty<string>();

        public string RandomGet()
        {
            return MyRandom.Choice(_tipsList);
        }
    }
}