using System.Collections.Generic;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IDomainService;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.Service;
using NSubstitute;
using NUnit.Framework;

namespace Tests.BattleScene.UseCases.Service
{
    [TestFixture]
    public class OrderServiceTest
    {
        public void スリップダメージが5の倍数順に挿入される()
        {
            var battlePropertyFactoryStub = Substitute.For<IFactory<BattlePropertyValueObject>>();
            battlePropertyFactoryStub.Create().SlipDefaultTurn.Returns(5);

            var characterPropertyFactoryStub = Substitute.For<IFactory<PropertyValueObject, CharacterTypeCode>>();
            characterPropertyFactoryStub.Create(CharacterTypeCode.Bee).Agility.Returns(7);
            characterPropertyFactoryStub.Create(CharacterTypeCode.Player).Agility.Returns(10);

            var ailmentRepositoryStub = Substitute.For<IRepository<AilmentEntity, (CharacterId, AilmentCode)>>();
            ailmentRepositoryStub.Select().Returns(new List<AilmentEntity>());

            var buffStub = Substitute.For<IBuffDomainService>();
            buffStub.GetRate(Arg.Any<CharacterId>(), Arg.Any<BuffCode>()).Returns(1.0f);
            
            // var orderService = new OrderService(
            //     battlePropertyFactory: battlePropertyFactoryStub,
            //     characterPropertyFactory: characterPropertyFactoryStub,
            //     ailmentRepository: ailmentRepositoryStub,
            //     buff: buffStub,
            //     );
        }
    }
}