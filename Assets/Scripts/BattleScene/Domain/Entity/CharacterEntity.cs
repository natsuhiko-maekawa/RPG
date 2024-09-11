using BattleScene.Domain.Code;
using BattleScene.Domain.Id;

namespace BattleScene.Domain.Entity
{
    public class CharacterEntity : BaseEntity<CharacterEntity, CharacterId>
    {
        public CharacterEntity(
            CharacterId id,
            CharacterTypeCode characterTypeCode,
            int currentHitPoint,
            int currentTechnicalPoint = 0,
            int actionTime = 0,
            int position = 0)
        {
            Id = id;
            CharacterTypeCode = characterTypeCode;
            CurrentHitPoint = currentHitPoint;
            CurrentTechnicalPoint = currentTechnicalPoint;
            ActionTime = actionTime;
            Position = position;
        }
        
        public override CharacterId Id { get; }
        public CharacterTypeCode CharacterTypeCode { get; }
        public int CurrentHitPoint { get; }
        public int CurrentTechnicalPoint { get; }
        public int ActionTime { get; set; }
        public int Position { get; }
        public bool IsSurvive => 0 < CurrentHitPoint;
        public bool IsPlayer => CharacterTypeCode == CharacterTypeCode.Player;
    }
}