using System;
using BattleScene.Domain.Code;

namespace BattleScene.Infrastructure.Factory.Dto
{
    [Serializable]
    public class AilmentDto
    {
        public AilmentCode ailmentCode;
        public Priority priority;
        public int? effectiveTurn;
    }
}