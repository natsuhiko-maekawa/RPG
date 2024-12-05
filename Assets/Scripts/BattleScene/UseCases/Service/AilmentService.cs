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
    public class AilmentService : ISkillElementService<AilmentValueObject>
    {
        private readonly IActualTargetIdPickerService _actualTargetIdPicker;
        private readonly IRepository<AilmentEntity, (CharacterId, AilmentCode)> _ailmentRepository;

        public AilmentService(
            IActualTargetIdPickerService actualTargetIdPicker,
            IRepository<AilmentEntity, (CharacterId, AilmentCode)> ailmentRepository)
        {
            _actualTargetIdPicker = actualTargetIdPicker;
            _ailmentRepository = ailmentRepository;
        }

        public IReadOnlyList<BattleEventValueObject> GenerateBattleEvent(
            CharacterId actorId,
            SkillCommonValueObject skillCommon,
            IReadOnlyList<AilmentValueObject> primeSkillParameterList,
            IReadOnlyList<CharacterId> targetIdList)
        {
            var ailmentList = primeSkillParameterList.Select(GetAilment).ToList();
            return ailmentList;

            BattleEventValueObject GetAilment(AilmentValueObject ailmentParameter)
            {
                var actualTargetIdList = _actualTargetIdPicker.Pick(
                    actorId: actorId,
                    targetIdList: targetIdList,
                    luckRate: ailmentParameter.LuckRate);

                var ailment = BattleEventValueObject.CreateAilment(
                    actorId: actorId,
                    skillCode: skillCommon.SkillCode,
                    ailmentCode: ailmentParameter.AilmentCode,
                    targetIdList: targetIdList,
                    actualTargetIdList: actualTargetIdList);

                return ailment;
            }
        }

        public void RegisterBattleEvent(IReadOnlyList<BattleEventValueObject> ailmentList)
        {
            foreach (var ailment in ailmentList)
            {
                foreach (var characterId in ailment.ActualTargetIdList)
                {
                    var ailment1 = _ailmentRepository.Get((characterId, ailment.AilmentCode));
                    ailment1.Effects = true;
                }
            }
        }
    }
}