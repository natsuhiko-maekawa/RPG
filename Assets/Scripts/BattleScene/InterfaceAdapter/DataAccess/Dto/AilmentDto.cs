using System;
using BattleScene.Domain.Code;

namespace BattleScene.InterfaceAdapter.DataAccess.Dto
{
    [Serializable]
    public class AilmentDto
    {
        public AilmentCode ailmentCode;
        public Priority priority;
        public int? effectiveTurn;
    }
}