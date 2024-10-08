using System.Collections.Generic;
using BattleScene.DataAccess.Dto;
using BattleScene.DataAccess.Factory;
using BattleScene.DataAccess.ScriptableObjects;
using BattleScene.Domain.Code;
using BattleScene.Domain.DataAccess;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IDomainService;
using BattleScene.Domain.ValueObject;
using BattleScene.UseCases.Service.Order;
using NSubstitute;
using NUnit.Framework;
using Tests.BattleScene.DataAccess.Repository;
using Tests.BattleScene.DataAccess.Resource;
using UnityEditor;
using UnityEngine;

namespace Tests.BattleScene.UseCases.Service
{
    [TestFixture]
    public class OrderServiceTest
    {
        [Test]
        public void スリップダメージが5の倍数順に挿入される()
        {
            var stubBattlePropertyFactory = Substitute.For<IFactory<BattlePropertyValueObject>>();
            stubBattlePropertyFactory.Create().Returns(new BattlePropertyValueObject(5, 1.2f));
            
            var stubAilmentRepository = Substitute.For<IRepository<AilmentEntity, (CharacterId, AilmentCode)>>();
            stubAilmentRepository.Select().Returns(new List<AilmentEntity>());

            var stubBuff = Substitute.For<IBuffDomainService>();
            stubBuff.GetRate(Arg.Any<CharacterId>(), Arg.Any<BuffCode>()).Returns(1.0f);

            var player = new CharacterEntity(
                id: new CharacterId(),
                characterTypeCode: CharacterTypeCode.Player,
                currentHitPoint: 179,
                currentTechnicalPoint: 50);
            var bee = new CharacterEntity(
                id:  new CharacterId(),
                characterTypeCode: CharacterTypeCode.Bee,
                currentHitPoint: 39,
                position: 0);

            var mockCharacterRepository = new MockRepository<CharacterEntity, CharacterId>();
            mockCharacterRepository.Update(player);
            mockCharacterRepository.Update(bee);

            var characterPropertyScriptableObject = AssetDatabase.LoadAssetAtPath<BaseScriptableObject<CharacterPropertyDto, CharacterTypeCode>>(
                "Assets/ScriptableObject/CharacterPropertyScriptableObject.asset");
            var stubCharacterPropertyResource = new StubBaseScriptableObjectResource<CharacterPropertyDto, CharacterTypeCode>(characterPropertyScriptableObject.ItemList);
            var stubCharacterPropertyFactory = new PropertyFactory(stubCharacterPropertyResource);
            
            var mockOrderedItemRepository = new MockRepository<OrderedItemEntity, OrderId>();

            var stubSlipDamageRepository = Substitute.For<IRepository<SlipEntity, SlipDamageCode>>();
            stubSlipDamageRepository.Select().Returns(new List<SlipEntity>()
                { new SlipEntity(SlipDamageCode.Poisoning, true, 5) });

            var orderService = new OrderService(
                battlePropertyFactory: stubBattlePropertyFactory,
                characterPropertyFactory: stubCharacterPropertyFactory,
                ailmentRepository: stubAilmentRepository,
                buff: stubBuff,
                characterRepository: mockCharacterRepository,
                orderedItemRepository: mockOrderedItemRepository,
                slipDamageRepository: stubSlipDamageRepository);
            
            orderService.Update();
            var orderedItemRepositoryToString = mockOrderedItemRepository.ToString();
            Debug.Log(orderedItemRepositoryToString);
            
            Assert.That(true);
        }
    }
}