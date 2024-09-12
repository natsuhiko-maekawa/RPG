using System;
using BattleScene.Domain.Code;
using BattleScene.Domain.ValueObject;

namespace BattleScene.Domain.Entity
{
    public class SlipDamageEntity : BaseEntity<SlipDamageEntity, SlipDamageCode>
    {
        private readonly int _defaultTurn;
        private TurnValueObject _turn;

        public SlipDamageEntity(
            SlipDamageCode id,
            SlipDamageEntityDto dto,
            TurnValueObject turn)
        {
            Id = id;
            DamageRate = dto.DamageRate;
            EnemyIntelligence = dto.EnemyIntelligence;
            PlayerIntelligence = dto.PlayerIntelligence;
            _turn = turn;
            if (_turn.Get() == null) throw new NullReferenceException();
            _defaultTurn = _turn.Get().GetValueOrDefault();
        }
        
        public override SlipDamageCode Id { get; }
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