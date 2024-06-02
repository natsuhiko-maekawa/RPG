using System;
using BattleScene.Domain.Code;

namespace BattleScene.Infrastructure.Factory.Dto
{
    [Serializable]
    public class MessageDto
    {
        public MessageCode key;
        public string message;
    }
}