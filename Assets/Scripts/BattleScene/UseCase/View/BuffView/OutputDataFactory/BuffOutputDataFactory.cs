using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using BattleScene.Domain.Id;
using BattleScene.UseCase.Service;
using BattleScene.UseCase.View.BuffView.OutputData;

namespace BattleScene.UseCase.View.BuffView.OutputDataFactory
{
    public class BuffOutputDataFactory
    {
        private readonly ToBuffNumberService _toBuffNumberService;

        public ImmutableList<BuffOutputData> Create()
        {
            throw new NotImplementedException();
        }

        public BuffOutputData Create(IList<CharacterId> characterIdList)
        {
            throw new NotImplementedException();
        }
    }
}