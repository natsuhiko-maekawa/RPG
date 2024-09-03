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
    }
}