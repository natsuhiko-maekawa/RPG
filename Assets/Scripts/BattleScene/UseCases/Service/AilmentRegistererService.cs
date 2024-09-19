using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using BattleScene.Domain.ValueObject;

namespace BattleScene.UseCases.Service
{
    public class AilmentRegistererService
    {
        private readonly IFactory<AilmentPropertyValueObject, AilmentCode> _ailmentPropertyFactory;
        private readonly IRepository<AilmentEntity, (CharacterId, AilmentCode)> _ailmentRepository;

        public AilmentRegistererService(
            IFactory<AilmentPropertyValueObject, AilmentCode> ailmentPropertyFactory,
            IRepository<AilmentEntity, (CharacterId, AilmentCode)> ailmentRepository)
        {
            _ailmentPropertyFactory = ailmentPropertyFactory;
            _ailmentRepository = ailmentRepository;
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
            _ailmentRepository.Update(ailmentEntityList);
        }

        public void Register(IReadOnlyList<AilmentValueObject> ailmentList)
        {
            foreach (var ailment in ailmentList) Register(ailment);
        }
    }
}