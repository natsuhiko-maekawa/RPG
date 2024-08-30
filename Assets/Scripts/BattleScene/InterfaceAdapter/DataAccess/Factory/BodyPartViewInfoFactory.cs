using BattleScene.Domain.Code;
using BattleScene.Domain.IFactory;
using BattleScene.InterfaceAdapter.DataAccess.Factory.Dto;

namespace BattleScene.InterfaceAdapter.DataAccess.Factory
{
    public class BodyPartViewInfoFactory : IFactory<BodyPartViewInfoDto, BodyPartCode>
    {
        public BodyPartViewInfoDto Create(BodyPartCode bodyPartCode)
        {
            throw new System.NotImplementedException();
        }
    }
}