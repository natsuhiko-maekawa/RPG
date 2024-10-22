using System.Collections.Generic;
using BattleScene.Domain.Id;
using BattleScene.Domain.ValueObject;

namespace BattleScene.UseCases.IService
{
    // このインタフェースを実装しているすべてのクラスの修正が終わった後、以下のTODOの修正を行うこと
    // TODO: 名前をIPrimeSkillServiceに修正する
    // TODO: 型パラメータ<TPrimeSkill>を削除する
    public interface IPrimeSkillGeneratorService<TPrimeSkillParameter, TPrimeSkill>
    {
        public IReadOnlyList<TPrimeSkill> Generate(
            SkillCommonValueObject skillCommon,
            IReadOnlyList<TPrimeSkillParameter> primeSkillParameterList,
            IReadOnlyList<CharacterId> targetIdList);
        // TODO: デフォルト実装を削除する
        public void Register(IReadOnlyList<PrimeSkillValueObject> primeSkillList){ }
    }
}