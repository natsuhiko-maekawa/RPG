using System;
using BattleScene.Domain.Code;

namespace BattleScene.InterfaceAdapter.DataAccess.Factory.Dto
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