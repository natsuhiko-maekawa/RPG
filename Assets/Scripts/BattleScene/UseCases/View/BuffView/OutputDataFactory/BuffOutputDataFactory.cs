using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using BattleScene.Domain.OldId;
using BattleScene.UseCases.Service;
using BattleScene.UseCases.View.BuffView.OutputData;

namespace BattleScene.UseCases.View.BuffView.OutputDataFactory
{
    public class BuffOutputDataFactory
    {
        private readonly ToBuffNumberService _toBuffNumberService;

        public BuffOutputDataFactory(ToBuffNumberService toBuffNumberService)
        {
            _toBuffNumberService = toBuffNumberService;
        }

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