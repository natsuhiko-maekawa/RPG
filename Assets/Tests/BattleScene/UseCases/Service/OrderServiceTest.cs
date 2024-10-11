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
        private CharacterPropertyFactory _stubCharacterPropertyFactory;
        private readonly MockRepository<CharacterEntity, CharacterId> _mockCharacterRepository = new();
        
        [SetUp]
        public void SetUp()
        {
            var characterPropertyScriptableObject = AssetDatabase.LoadAssetAtPath<BaseScriptableObject<CharacterPropertyDto, CharacterTypeCode>>(
                "Assets/ScriptableObject/CharacterPropertyScriptableObject.asset");
            var stubCharacterPropertyResource = new StubBaseScriptableObjectResource<CharacterPropertyDto, CharacterTypeCode>(characterPropertyScriptableObject.ItemList);
            _stubCharacterPropertyFactory = new CharacterPropertyFactory(stubCharacterPropertyResource);
        }
        
        [Test]
        public void スリップダメージが5の倍数順に挿入される()
        {
            var stubBattlePropertyFactory = Substitute.For<IFactory<BattlePropertyValueObject>>();
            stubBattlePropertyFactory.Create().Returns(new BattlePropertyValueObject(
                SlipDefaultTurn: 5, 
                SlipDefaultDamageRate:1.2f,
                IsHitThreshold: 20.0f));
            
            var stubAilmentRepository = Substitute.For<IRepository<AilmentEntity, (CharacterId, AilmentCode)>>();
            stubAilmentRepository.Select().Returns(new List<AilmentEntity>());

            var stubBuff = Substitute.For<IBuffDomainService>();
            stubBuff.GetRate(Arg.Any<CharacterId>(), Arg.Any<BuffCode>()).Returns(1.0f);

            var playerId = new CharacterId();
            var player = new CharacterEntity(
                id: playerId,
                characterTypeCode: CharacterTypeCode.Player,
                currentHitPoint: 179,
                currentTechnicalPoint: 50);
            var beeId = new CharacterId();
            var bee = new CharacterEntity(
                id: beeId,
                characterTypeCode: CharacterTypeCode.Bee,
                currentHitPoint: 39,
                position: 0);

            _mockCharacterRepository.Update(player);
            _mockCharacterRepository.Update(bee);
            
            var mockOrderedItemRepository = new MockRepository<OrderedItemEntity, OrderId>();

            var stubSlipDamageRepository = Substitute.For<IRepository<SlipEntity, SlipDamageCode>>();
            stubSlipDamageRepository.Select().Returns(new List<SlipEntity>()
                { new SlipEntity(SlipDamageCode.Poisoning, true, 5) });

            var orderService = new OrderService(
                battlePropertyFactory: stubBattlePropertyFactory,
                characterPropertyFactory: _stubCharacterPropertyFactory,
                ailmentRepository: stubAilmentRepository,
                buff: stubBuff,
                characterRepository: _mockCharacterRepository,
                orderedItemRepository: mockOrderedItemRepository,
                slipDamageRepository: stubSlipDamageRepository);
            
            orderService.Update();
            var orderedItemRepositoryToString = mockOrderedItemRepository.ToString();
            Debug.Log(orderedItemRepositoryToString);

            var orderedItemList = mockOrderedItemRepository.Select();

            var orderedItem1 = new OrderedItemEntity(
                orderId: new OrderId(),
                orderNumber: 0,
                orderedItem: new OrderedItem(playerId));
            
            orderedItemList[0].TryGetCharacterId(out var characterId1);
            Assert.That(characterId1, Is.EqualTo(playerId), "順番の1番目は正しい");
            orderedItemList[1].TryGetCharacterId(out var characterId2);
            Assert.That(characterId2, Is.EqualTo(beeId), "順番の2番目は正しい");
        }
    }
}