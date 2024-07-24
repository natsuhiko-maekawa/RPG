using System;
using BattleScene.Domain.Code;
using BattleScene.Domain.Entity;
using BattleScene.Domain.IFactory;
using BattleScene.Domain.OldId;

namespace BattleScene.InterfaceAdapter.DataAccess.Factory
{
    public class BodyPartFactory : IBodyPartFactory
    {
        public BodyPartEntity Create(CharacterId characterId, BodyPartCode bodyPartCode)
        {
            return new BodyPartEntity(
                characterId,
                bodyPartCode,
                GetBodyPartNumber(bodyPartCode));
        }

        private int GetBodyPartNumber(BodyPartCode bodyPartCode)
        {
            return bodyPartCode switch
            {
                BodyPartCode.Arm => Constant.ArmNumber,
                BodyPartCode.Leg => Constant.LegNumber,
                BodyPartCode.Stomach => Constant.StomachNumber,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}