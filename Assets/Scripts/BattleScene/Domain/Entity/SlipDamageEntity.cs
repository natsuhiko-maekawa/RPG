using System;
using BattleScene.Domain.Code;
using BattleScene.Domain.OldId;
using BattleScene.Domain.ValueObject;

namespace BattleScene.Domain.Entity
{
    public class SlipDamageEntity : BaseEntity<SlipDamageEntity, SlipDamageId>
    {
        private readonly int _defaultTurn;
        private TurnValueObject _turn;

        public SlipDamageEntity(
            SlipDamageCode slipDamageCode,
            SlipDamageEntityDto dto,
            TurnValueObject turn)
        {
            SlipDamageCode = slipDamageCode;
            DamageRate = dto.DamageRate;
            EnemyIntelligence = dto.EnemyIntelligence;
            PlayerIntelligence = dto.PlayerIntelligence;
            _turn = turn;
            if (_turn.Get() == null) throw new NullReferenceException();
            _defaultTurn = _turn.Get().GetValueOrDefault();
        }

        public override SlipDamageId Id => new(SlipDamageCode);
        public SlipDamageCode SlipDamageCode { get; }
        public float DamageRate { get; }
        public int EnemyIntelligence { get; }
        public int PlayerIntelligence { get; }

        public int? GetTurn()
        {
            return _turn.Get();
        }

        public void AdvanceTurn()
        {
            _turn.Advance();
            if (_turn.IsEnd()) _turn = new TurnValueObject(_defaultTurn);
        }
    }

    public record SlipDamageEntityDto(
        float DamageRate,
        int PlayerIntelligence,
        int EnemyIntelligence);
}