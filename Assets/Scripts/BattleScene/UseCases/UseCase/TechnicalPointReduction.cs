using BattleScene.Domain.Aggregate;
using BattleScene.Domain.DomainService;
using BattleScene.Domain.Entity;
using BattleScene.Domain.Id;
using BattleScene.Domain.IRepository;
using BattleScene.UseCases.UseCase.Interface;

namespace BattleScene.UseCases.UseCase
{
    public class TechnicalPointReduction : IUseCase
    {
        private readonly ICharacterRepository _characterRepository;
        private readonly OrderedItemsDomainService _orderedItems;
        private readonly ISkillRepository _skillRepository;
        private readonly IRepository<TechnicalPointEntity, CharacterId> _technicalPointRepository;
        
        // TODO: コンストラクタを記述すること
        
        public void Execute()
        {
            var characterId = _orderedItems.FirstCharacterId();
            var skill = _skillRepository.Select(characterId);
            
            var technicalPoint = _technicalPointRepository.Select(characterId);
            technicalPoint.Reduce(skill.Skill.TechnicalPoint);
            _technicalPointRepository.Update(technicalPoint);
        }
    }
}