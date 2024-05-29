using System;
using BattleScene.Domain.Code;

namespace BattleScene.Infrastructure.Factory.Dto
{
    [Serializable]
    public class AilmentViewInfoDto
    {
        public AilmentCode ailmentCode;
        public string ailmentName;
        public MessageCode messageCode;
        public PlayerImageCode playerImageCode;
    }
}