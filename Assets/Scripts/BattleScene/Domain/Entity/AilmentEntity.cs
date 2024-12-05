using System;
using BattleScene.Domain.Code;
using BattleScene.Domain.Id;

namespace BattleScene.Domain.Entity
{
    public partial class AilmentEntity : BaseEntity<(CharacterId, AilmentCode)>
    {
        public AilmentEntity(
            CharacterId characterId,
            AilmentCode ailmentCode,
            bool isSelfRecovery,
            int defaultTurn)
        {
            CharacterId = characterId;
            AilmentCode = ailmentCode;
            IsSelfRecovery = isSelfRecovery;
            _defaultTurn = defaultTurn;
        }

        public override (CharacterId, AilmentCode) Id => (CharacterId, AilmentCode);
        public CharacterId CharacterId { get; }
        public AilmentCode AilmentCode { get; }
        public bool IsSelfRecovery { get; }
        private readonly int _defaultTurn;
        private int _turn = -1;

        /// <summary>
        /// 状態異常の残りターン数を表す。-1のとき状態異常は無効。
        /// </summary>
        public int Turn
        {
            get => _turn;
            private set => _turn = Math.Max(value, -1);
        }

        private bool _effects;

        public bool Effects
        {
            get => _effects;
            set
            {
                _effects = value;
                EffectsOnChange(value);
                Turn = _defaultTurn;
            }
        }

        partial void EffectsOnChange(bool value);

        public void AdvanceTurn()
        {
            if (!IsSelfRecovery) return;
            Turn--;
            if (Turn < 0 && Effects) Effects = false;
        }
    }
}