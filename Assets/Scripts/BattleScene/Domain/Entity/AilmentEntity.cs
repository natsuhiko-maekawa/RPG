﻿using BattleScene.Domain.Code;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;

namespace BattleScene.Domain.Entity
{
    public class AilmentEntity : BaseEntity<AilmentEntity, (CharacterId, AilmentCode)>
    {
        private TurnValueObject _turn;
        
        public AilmentEntity(
            CharacterId characterId,
            AilmentCode ailmentCode,
            int turn,
            bool isSelfRecovery)
        {
            CharacterId = characterId;
            AilmentCode = ailmentCode;
            Turn = turn;
            IsSelfRecovery = isSelfRecovery;
        }

        public override (CharacterId, AilmentCode) Id => (CharacterId, AilmentCode);
        public CharacterId CharacterId { get; }
        public AilmentCode AilmentCode { get; }
        public int Turn { get; private set; }
        public bool IsSelfRecovery { get; }

        public void AdvanceTurn()
        {
            if (!IsSelfRecovery) return;
            if (Turn <= 0) return;
            Turn--;
        }

        public bool TurnIsEnd
        {
            get
            {
                if (!IsSelfRecovery) return false;
                return Turn < 0;
            }
        }
    }
}