﻿using System;
using BattleScene.Domain.Code;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;

namespace BattleScene.Infrastructure.Factory
{
    public class BodyPartFactory
    {
        public BodyPartEntity Create(CharacterId characterId, BodyPartCode bodyPartCode)
        {
            return new BodyPartEntity(
                characterId: characterId,
                bodyPartCode: bodyPartCode,
                bodyPartNumber: GetBodyPartNumber(bodyPartCode));
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