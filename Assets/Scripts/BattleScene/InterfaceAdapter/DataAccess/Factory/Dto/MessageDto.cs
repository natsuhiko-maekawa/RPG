using System;
using BattleScene.Domain.Code;

namespace BattleScene.InterfaceAdapter.DataAccess.Factory.Dto
{
    [Serializable]
    public class MessageDto
    {
        public MessageCode key;
        public string message;
    }
}