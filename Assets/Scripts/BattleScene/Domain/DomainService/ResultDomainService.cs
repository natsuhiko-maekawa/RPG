using System;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.Interface;
using BattleScene.Domain.IRepository;

namespace BattleScene.Domain.DomainService
{
    [Obsolete]
    public class ResultDomainService
    {
        private readonly IResultRepository _resultRepository;
        private readonly ITurnRepository _turnRepository;

        public IDamageResult LastDamage()
        {
            throw new NotImplementedException();
        }

        public SkillCode LastSkillCode()
        {
            throw new NotImplementedException();
        }

        public T Last<T>() where T : IResult
        {
            throw new NotImplementedException();
        }

        public bool TryGetLast<T>(out T result) where T : IResult
        {
            throw new NotImplementedException();
        }
    }
}