using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.IService;

namespace BattleScene.UseCases.Service
{
    public class AilmentRegistererService : IPrimeSkillRegistererService<AilmentValueObject>
    {
        private readonly IFactory<AilmentPropertyValueObject, AilmentCode> _ailmentPropertyFactory;
        private readonly ICollection<AilmentEntity, (CharacterId, AilmentCode)> _ailmentCollection;

        public AilmentRegistererService(
            IFactory<AilmentPropertyValueObject, AilmentCode> ailmentPropertyFactory,
            ICollection<AilmentEntity, (CharacterId, AilmentCode)> ailmentCollection)
        {
            _ailmentPropertyFactory = ailmentPropertyFactory;
            _ailmentCollection = ailmentCollection;
        }

        public void Register(AilmentValueObject ailment)
        {
            var ailmentProperty = _ailmentPropertyFactory.Create(ailment.AilmentCode);
            var ailmentEntityList = ailment.ActualTargetIdList
                .Select(x => new AilmentEntity(
                    ailmentCode: ailment.AilmentCode,
                    characterId: x,
                    effects: true,
                    turn: ailmentProperty.Turn,
                    isSelfRecovery: ailmentProperty.IsSelfRecovery))
                .ToImmutableList();
            _ailmentCollection.Add(ailmentEntityList);
        }

        public void Register(IReadOnlyList<AilmentValueObject> ailmentList)
        {
            foreach (var ailment in ailmentList) Register(ailment);
        }
    }
}