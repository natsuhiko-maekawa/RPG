// using BattleScene.Domain.DomainService;
// using BattleScene.Domain.IRepository;
// using BattleScene.Domain.OldId;
// using BattleScene.Domain.ValueObject;
// using Utility.Interface;
// using static BattleScene.Domain.Code.BuffCode;
// using static BattleScene.Domain.Code.BodyPartCode;
//
// namespace BattleScene.Domain.Expression
// {
//     public class ConstantDamageExpression
//     {
//         private readonly BodyPartDomainService _bodyPartDomainService;
//         private readonly BuffDomainService _buffDomainService;
//         private readonly ICharacterRepository _characterRepository;
//         private readonly IRandomEx _randomEx;
//
//         public ConstantDamageExpression(
//             ICharacterRepository characterRepository,
//             IRandomEx randomEx,
//             BodyPartDomainService bodyPartDomainService,
//             BuffDomainService buffDomainService)
//         {
//             _characterRepository = characterRepository;
//             _randomEx = randomEx;
//             _bodyPartDomainService = bodyPartDomainService;
//             _buffDomainService = buffDomainService;
//         }
//
//         public int Evaluate(CharacterId actorId, AbstractDamage damage)
//         {
//             var actorStrength = _characterRepository.Select(actorId).Property.Strength;
//             var targetVitality = 1;
//             var actorBuffRate = _buffDomainService.GetRate(actorId, Attack);
//             var targetBuffRate = 1;
//             var destroyedRate = 1.0f - _bodyPartDomainService.Count(actorId, Arm) * 0.5f;
//             var rate = damage.GetDamageRate();
//             return (int)(actorStrength * actorStrength / (float)targetVitality * actorBuffRate / targetBuffRate
//                          * destroyedRate * rate * 1.5f) + _randomEx.Range(1, 3);
//         }
//     }
// }