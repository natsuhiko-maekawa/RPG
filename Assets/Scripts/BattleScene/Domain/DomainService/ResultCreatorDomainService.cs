using System;
using System.Linq;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Interface;
using BattleScene.Domain.IRepository;

namespace BattleScene.Domain.DomainService
{
    public class ResultCreatorDomainService
    {
        private readonly ISequenceRepository _sequenceRepository;
        private readonly ITurnRepository _turnRepository;

        public ResultEntity Create(IResult result)
        {
            throw new NotImplementedException();
        }
    }
}