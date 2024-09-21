using BattleScene.Domain.Code;
using BattleScene.Domain.Id;
using UnityEngine;

namespace BattleScene.Domain.Entity
{
    public partial class CharacterEntity : BaseEntity<CharacterId>
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
        
        private int _currentHitPoint;
        public int CurrentHitPoint
        {
            get => _currentHitPoint;
            set { _currentHitPoint = Mathf.Max(value, 0); CurrentHitPointOnChange(value);}
        }

        partial void CurrentHitPointOnChange(int value);

        private int _currentTechnicalPoint;
        public int CurrentTechnicalPoint
        {
            get => _currentTechnicalPoint;
            set { _currentTechnicalPoint = Mathf.Max(value, 0); CurrentTechnicalPointOnChange(value);}
        }

        partial void CurrentTechnicalPointOnChange(int value);
        
        public int ActionTime { get; set; }
        public int Position { get; }
        public bool IsSurvive => 0 < CurrentHitPoint;
        public bool IsPlayer => CharacterTypeCode == CharacterTypeCode.Player;
    }
}