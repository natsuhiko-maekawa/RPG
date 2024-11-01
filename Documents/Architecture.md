# アーキテクチャ

## MV(R)P

　MVPに加え、R3のイベントを使用

### Model

　DDDとクリーンアーキテクチャを参考に設計を行った。  
　Modelは、
- Entityクラス、ValueObjectクラスからなるDomain層
- ServiceクラスとそのFacadeであるUseCaseクラスがあるUseCases層
- Repositoryクラス、FactoryクラスからなるDataAccess層

　の3層で構成される。  
　Domain層とUseCases層はUnityEngineに依存していない。  
　DataAccess層にはScriptableObjectを利用したクラスがある。

### View

　ViewクラスとViewModelクラスからなる。  

### Presenter

　StateパターンとMementoパターンで構成されたStateMachineとState、
ModelとViewの橋渡しを行うPresenterおよびPresenterのFacadeクラスからなる。
