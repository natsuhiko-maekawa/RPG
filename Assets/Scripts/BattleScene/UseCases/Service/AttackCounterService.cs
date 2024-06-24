using System.Linq;
using BattleScene.Domain.IFactory;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.ValueObject;
using UnityEngine;

namespace BattleScene.UseCases.Service
{
    public class AttackCounterService
    {
        private readonly ICharacterRepository _characterRepository;
        private readonly IPlayerPropertyFactory _playerPropertyFactory;
        private readonly IResultRepository _resultRepository;

        public AttackCounterService(
            ICharacterRepository characterRepository,
            IPlayerPropertyFactory playerPropertyFactory,
            IResultRepository resultRepository)
        {
            _characterRepository = characterRepository;
            _playerPropertyFactory = playerPropertyFactory;
            _resultRepository = resultRepository;
        }

        public float GetRate()
        {
            return Mathf.Min((float)Count() / Domain.Constant.AttackCountUpperLimit, 1.0f);
        }

        public bool IsOverflow()
        {
            return Domain.Constant.AttackCountUpperLimit < Count();
        }

        private int Count()
        {
            var resultList = _resultRepository.Select();
            var player = _characterRepository.Select().First(x => x.IsPlayer());
            var playerId = player.CharacterId;
            var fatalitySkills = _playerPropertyFactory.Get().FatalitySkills;
            return resultList
                .OrderByDescending(x => x)
                .Select(x => x.Result)
                .OfType<DamageSkillResultValueObject>()
                .TakeWhile(x => Equals(x.ActorId, playerId)
                                && fatalitySkills.Contains(x.SkillCode))
                .Select(x => x.HitCount())
                .Aggregate((x, y) => x + y);
        }
    }
}