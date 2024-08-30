using System;
using BattleScene.Domain.Code;
using BattleScene.Domain.Interface;

namespace BattleScene.Domain.DomainService
{
    [Obsolete]
    public class ResultDomainService
    {
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