using System;
using BattleScene.Domain.Code;
using BattleScene.Domain.ValueObject;

namespace BattleScene.Domain.DataAccess.ObsoleteIFactory
{
    [Obsolete]
    public interface IAilmentViewInfoFactory
    {
        public AilmentViewInfoValueObject Create(AilmentCode ailmentCode);
    }
}