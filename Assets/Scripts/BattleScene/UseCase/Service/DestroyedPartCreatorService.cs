using System.Collections.Generic;
using System.Linq;
using BattleScene.Domain.Entity;
using BattleScene.Domain.IFactory;
using BattleScene.Domain.ValueObject;

namespace BattleScene.UseCase.Service
{
    public class DestroyedPartCreatorService
    {
        private readonly IBodyPartFactory _bodyPartFactory;

        public DestroyedPartCreatorService(IBodyPartFactory bodyPartFactory)
        {
            _bodyPartFactory = bodyPartFactory;
        }

        public BodyPartEntity Create(
            IList<BodyPartEntity> bodyPartList,
            DestroyedPartSkillResultValueObject destroyedPartSkillResult)
        {
            var characterId = destroyedPartSkillResult.CharacterId;
            var bodyPartCode = destroyedPartSkillResult.BodyPartCode;
            return bodyPartList
                       .FirstOrDefault(x => Equals(x.CharacterId, characterId) && x.BodyPartCode == bodyPartCode)
                   ?? _bodyPartFactory.Create(characterId, bodyPartCode);
        }
    }
}