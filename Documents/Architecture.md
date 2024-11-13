# アーキテクチャ

## MV(R)P

　MVPに加え、R3のイベントを使用

### Model

　DDDを参考に設計を行った。  
　Modelは、  
1. Entityクラス、ValueObjectクラスからなるDomain層
2. ServiceクラスとそのFacadeであるUseCaseクラスがあるUseCases層
3. Repositoryクラス、FactoryクラスからなるDataAccess層

　の3層で構成される。  
　Domain層とUseCases層はUnityEngineに依存していない。  
　DataAccess層にはScriptableObjectを利用したクラスがある。  

### View

　Viewは、
- コントローラの入力を受け付けるInputActionクラス
- 画面の出力を担うViewクラスとViewModelクラス

　からなる。  
　コントローラの入力をPresenterを介さずに即時画面に出力するために、
InputActionクラスとViewクラスの両方の性質を持つクラスも存在する。  

### Presenter

　PresenterはViewからの入力をModelに反映し、Modelの変更をViewに出力する役割を持つ。  
　まず、ViewのInputActionクラスがインタフェースを介してStateMachineクラスを操作する。
なお、StateMachineクラスはStateパターンとMementoパターンで構成される。  
　次に、StateMachineクラスがContextクラスを通してStateクラスを操作する。  
　StateクラスはModelのUseCaseクラスを介してRepositoryを操作し、
Repositoryの変更に応じてPresenterクラスのFacadeであるOutputクラスを操作する。  
　最後に、Outputクラスを介して操作されたPresenterクラスがViewにViewModelを返す。  

## 有限オートマトン

　StateMachineは自身に対する巡回をのぞき、非巡回となっている。  
