using System.Collections.Generic;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;

namespace BattleScene.UseCases.IService
{
    // このインタフェースを実装しているすべてのクラスの修正が終わった後、以下のTODOの修正を行うこと
    // TODO: 名前をIPrimeSkillServiceに修正する
    public interface IPrimeSkillGeneratorService<in TPrimeSkillParameter>
    {
        public IReadOnlyList<PrimeSkillValueObject> Generate(
            SkillCommonValueObject skillCommon,
            IReadOnlyList<TPrimeSkillParameter> primeSkillParameterList,
            IReadOnlyList<CharacterId> targetIdList);

        public void Register(IReadOnlyList<PrimeSkillValueObject> primeSkillList);
    }
}