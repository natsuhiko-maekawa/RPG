using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.IService;

namespace BattleScene.UseCases.Service
{
    // TODO: 削除すること
    public class AilmentRegistererService : IPrimeSkillRegistererService<PrimeSkillValueObject>
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

        public void Register(PrimeSkillValueObject ailment)
        {
            var ailmentProperty = _ailmentPropertyFactory.Create(ailment.AilmentCode);
            var ailmentEntityList = ailment.ActualTargetIdList
                .Select(x => new AilmentEntity(
                    ailmentCode: ailment.AilmentCode,
                    characterId: x,
                    effects: true,
                    turn: ailmentProperty.Turn,
                    isSelfRecovery: ailmentProperty.IsSelfRecovery))
                .ToList();
            _ailmentCollection.Add(ailmentEntityList);
        }

        public void Register(IReadOnlyList<PrimeSkillValueObject> ailmentList)
        {
            foreach (var ailment in ailmentList) Register(ailment);
        }
    }
}