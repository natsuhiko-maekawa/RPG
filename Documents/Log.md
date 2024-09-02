# 進捗

## 9/1 (日)

StateMachineをInterfaceAdapter層に移動。これによりUseCase層のVContainerへの依存を解消。  
StateMachineをVContainerの機能を利用してエントリポイントに設定。  
Framework層のInject属性を除去。これによりFramework層のVContainerへの依存を解消。  
以上の修正で依存関係が以下のように単純化。

- MonoBehaviourを継承しているのはFramework層のクラスとInterfaceAdapter層のScriptableObjectクラスのみ
- VContainerに依存しているのはInterface層のみ
- Domain層とUseCases層は何にも依存していない

ScriptableObjectクラスはFramework層に移動予定。  
GridViewを追加。  
Controllerを削除し、入力の受け渡しを整理。  